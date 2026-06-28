using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Contracts;
using SchoolProject.Service.Contracts;
using SchooProject.Data.Entities;

namespace SchoolProject.Service.Implementaion
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository student)
        {
            _studentRepository = student;
        }


        public async Task<List<Student>> GetStudentsListAsync()
        {

            return await _studentRepository.GetStudentsListAsync();
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetTableNoTracking().
                Include(x => x.Department).Where(s => s.StudentId == id).FirstOrDefaultAsync();
        }

        public async Task<string> AddAsync(Student student)
        {

            await _studentRepository.AddAsync(student);
            return "Added Successfully";

        }

        public async Task<bool> IsNameExist(string name)
        {
            var result = await _studentRepository.GetTableNoTracking().Where(x => x.Name == name).FirstOrDefaultAsync();
            return result == null ? false : true;
        }

        public async Task Update(Student student)
        {
            await _studentRepository.UpdateAsync(student);
        }

        public async Task<bool> CheckIsExist(string name, int id)
        {
            var result = await _studentRepository.GetTableNoTracking()
                .Where(x => x.Name.ToLower() == name.ToLower() && x.StudentId != id).FirstOrDefaultAsync();
            return result == null;
        }



        public async Task<bool> DeleteAsync(Student id)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(id);
                await trans.CommitAsync();
                return true;
            }
            catch
            {
                await trans.RollbackAsync();
                return false;
            }
        }

        public IQueryable<Student> GetStudentsListQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public IQueryable<Student> GetFilterStudentPaginatedQuerable(StudentOrderingEnum orderBy, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (!string.IsNullOrEmpty(search))
                querable = querable.Where(x => x.Name.Contains(search) || x.Address.Contains(search));
            switch (orderBy)
            {
                case StudentOrderingEnum.StudentId: querable = querable.OrderBy(x => x.StudentId); break;
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.Name); break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address); break;

            }

            return querable;
        }
    }
}
