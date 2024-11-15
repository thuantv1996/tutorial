namespace DAL
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
