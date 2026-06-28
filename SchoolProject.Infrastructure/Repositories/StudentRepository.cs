using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Contracts;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchooProject.Data.Entities;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        #region Fields

        private readonly DbSet<Student> _student;
        #endregion

        #region Constructors
        public StudentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _student = dbContext.Set<Student>();
        }
        #endregion

        #region Handles Functions
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _student.Include(x => x.Department).ToListAsync();
        }
        #endregion


    }
}
