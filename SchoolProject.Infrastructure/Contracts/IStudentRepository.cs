using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchooProject.Data.Entities;

namespace SchoolProject.Infrastructure.Contracts
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
        public Task<List<Student>> GetStudentsListAsync();
    }
}
