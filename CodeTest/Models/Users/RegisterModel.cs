using System.ComponentModel.DataAnnotations;

namespace CodeTest.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}