using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchooProject.Data.Entities
{
    public class StudentSubject
    {
        [Key]
        public int StudID { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey("StudID")]
        [InverseProperty("StudentSubjects")]
        public virtual Student Student { get; set; }

        [ForeignKey("SubID")]
        [InverseProperty("StudentsSubjects")]
        public virtual Subject Subject { get; set; }

    }
}
