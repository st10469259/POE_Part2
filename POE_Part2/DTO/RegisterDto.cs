namespace POE_Part2.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // 'Organiser' or 'Participant'
        public string OrganizationName { get; set; }
        public string EmergencyContact { get; set; }
    }
}