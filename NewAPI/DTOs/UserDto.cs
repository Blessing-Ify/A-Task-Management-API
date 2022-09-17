using System.ComponentModel.DataAnnotations;

namespace NewAPI.DTOs
{
    public class UserDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage ="3 to 15 characters allowed!")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "3 to 15 characters allowed!")]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /*[Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "3 to 15 characters allowed!")]*/
        public string Role { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password mismatched")]
        public string ConfirmPassword { get; set; }

    }
}
