using SchoolProject.Data.Helper;
using SchooProject.Data.Entities;

namespace SchoolProject.Service.Contracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        public Task<Student> GetStudentByIdAsync(int id);

        public Task<string> AddAsync(Student student);
        public Task<bool> IsNameExist(string name);

        public Task Update(Student student);
        Task<bool> CheckIsExist(string name, int id);
        Task<bool> DeleteAsync(Student id);
        public IQueryable<Student> GetStudentsListQuerable();
        public IQueryable<Student> GetFilterStudentPaginatedQuerable(StudentOrderingEnum orderBy, string search);
    }
}
