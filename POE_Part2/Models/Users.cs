using System.ComponentModel.DataAnnotations;

namespace POE_Part2.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } // 'Organiser', 'Participant', 'Admin'

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}