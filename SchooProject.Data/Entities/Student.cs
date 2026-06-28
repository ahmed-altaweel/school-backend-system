using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchooProject.Data.Entities
{
    public class Student
    {
        public Student()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }
        [Key]
        public int StudentId { get; set; }
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(500)]
        public string? Phone { get; set; }

        public int DID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty("Students")]
        public virtual Department Department { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }


    }
}
