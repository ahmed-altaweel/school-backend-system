using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolProject.Data.Entities;

namespace SchooProject.Data.Entities
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }

        [Key]
        public int DID { get; set; }

        [StringLength(500)]
        public string DName { get; set; }

        public int ManagerId { get; set; }
        [ForeignKey(nameof(ManagerId))]
        [InverseProperty("departmentManager")]
        public Instructor Instructor { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
        [InverseProperty("department")]
        public virtual ICollection<Instructor> Instructors { get; set; }

    }
}
