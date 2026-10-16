using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POE_Part2.Models
{
    public class Result
    {
        [Key]
        public int ResultID { get; set; }

        [Required]
        public int EnrolmentID { get; set; }

        [ForeignKey("EnrolmentID")]
        public EventEnrolment EventEnrolment { get; set; }

        public TimeSpan FinishTime { get; set; }

        public int Position { get; set; }
    }
}