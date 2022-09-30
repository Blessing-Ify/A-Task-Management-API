using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to 15 characters", MinimumLength = 4)]
        public string Password { get; set; }
        /*public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }*/
    }
}
