using System.ComponentModel.DataAnnotations;

namespace CoreWebAPIPract.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password not matched with the Password.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Range(18, 62)]
        public int Age {  get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Please enter 8 character Username")]
        public string Username { get; set; }

    }
}
