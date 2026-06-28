using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchooProject.Data.Entities;

namespace SchoolProject.Data.Entities
{
    public class Ins_Subject
    {
        [Key]
        public int InsId { get; set; }
        [Key]
        public int SubId { get; set; }
        [ForeignKey(nameof(InsId))]
        [InverseProperty("Ins_Subjects")]
        public Instructor instructor { get; set; }
        [ForeignKey(nameof(SubId))]
        public Subject subject { get; set; }
    }
}
