using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolProject.Infrastructure.InfrastructureBases
{
    public interface IGenericRepository<T> where T : class
    {
        public Task DeleteRangeAsync(ICollection<T> entities);
        public Task<T> GetByIdAsync(int id);
        Task SaveChangeAsync();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        Task AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);



    }
}
