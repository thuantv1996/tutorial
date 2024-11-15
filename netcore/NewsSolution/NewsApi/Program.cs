using BLL.Abstracts;
using BLL.Implements;
using DAL;
using DAL.Repositories.Abstracts;
using DAL.Repositories.Implements;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NewsApi.Extensions;
using System.Globalization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

// use memory cache
builder.Services.AddMemoryCache(opt =>
{
    opt.SizeLimit = 100;
});

// add DbContext
builder.Services.AddDbContext<NewsDbContext>(opt =>
{
    opt.UseInMemoryDatabase("NewsDB");
});

// add auto mapper
builder.Services.AddAutoMapper(typeof(DTO.MapperProfile).Assembly);

// add cors
builder.Services.AddCors(conf =>
{
    conf.AddPolicy("dev", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// document api
builder.Services.AddSwagger();

// output cache
builder.Services.AddOutputCache(opt =>
{
    opt.AddPolicy("BaseCache", conf =>
    {
        conf.Expire(TimeSpan.FromSeconds(5)).SetVaryByQuery("id");
    });
});

// rate limmiter
builder.Services.AddRateLimiter(opt =>
{
    // used fixed window algorithm
    opt.AddFixedWindowLimiter("fixed", conf =>
    {
        // number request
        conf.PermitLimit = 5;
        // time for 10 (PermitLimit) request
        conf.Window = TimeSpan.FromSeconds(10);

        // number request wait if over
        //conf.QueueLimit = 3; 
        // In case of new time frame start, which request is executed first if in queue?
        //conf.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });

    opt.AddSlidingWindowLimiter("sliding", conf =>
    {
        conf.PermitLimit = 5;
        conf.Window = TimeSpan.FromSeconds(10);
        conf.SegmentsPerWindow = 2;
    });

    opt.AddTokenBucketLimiter("token", conf =>
    {
        conf.TokenLimit = 5;
        conf.ReplenishmentPeriod = TimeSpan.FromSeconds(2);
        conf.TokensPerPeriod = 1;
    });

    opt.AddConcurrencyLimiter("concurrency", conf =>
    {
        conf.PermitLimit = 1;
    });

    // create a rate limmiter by ip
    opt.AddPolicy("ip", context =>
    {
        var ip = context.Connection.RemoteIpAddress;
        return RateLimitPartition.GetSlidingWindowLimiter(ip, _ =>
        new SlidingWindowRateLimiterOptions
        {
            PermitLimit = 3,
            Window = TimeSpan.FromSeconds(10),
            SegmentsPerWindow = 2
        });
    });

    opt.OnRejected = (context, cancellationToken) =>
    {
        if(context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            context.HttpContext.Response.Headers.RetryAfter = retryAfter.TotalSeconds.ToString(NumberFormatInfo.InvariantInfo);
        }
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        return new ValueTask();
    };
});

// DI
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        setup.RoutePrefix = string.Empty;
    });
    app.UseCors("dev");
}

app.Use(async (context, next) =>
{
    System.Console.WriteLine(context.Request.Headers.ETag);
    await next.Invoke();
});

app.UseOutputCache();

app.UseRateLimiter();

app.MapControllers();

app.Run();
