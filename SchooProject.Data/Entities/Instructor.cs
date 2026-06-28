using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchooProject.Data.Entities;

namespace SchoolProject.Data.Entities
{
    public class Instructor
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }
        [Key]
        public int insId { get; set; }
        public string IName { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public int SupervisorId { get; set; }
        public decimal Salary { get; set; }
        public int DID { get; set; }
        [ForeignKey(nameof(DID))]
        [InverseProperty("Instructors")]
        public Department department { get; set; }

        [InverseProperty("Instructor")]
        public Department departmentManager { get; set; }
        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty("Instructors")]
        public Instructor Supervisor { get; set; }

        [InverseProperty("Supervisor")]
        public virtual ICollection<Instructor> Instructors { get; set; }


        [InverseProperty("instructor")]
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
    }
}
