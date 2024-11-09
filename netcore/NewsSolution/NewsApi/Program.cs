using NewsApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(conf =>
{
    conf.AddPolicy("dev", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// document api
builder.Services.AddSwagger();

var app = builder.Build();

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

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
