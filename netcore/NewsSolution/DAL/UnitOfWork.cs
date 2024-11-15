namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsDbContext _dbContext;

        public UnitOfWork(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
