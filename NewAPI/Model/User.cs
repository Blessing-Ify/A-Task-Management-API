using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace NewAPI.Model
{
    public class User: IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
       /* public string? Password { get; set; }*/
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public IEnumerable<UserTask> Tasks { get; set; }
        //public string Id { get; set; } = Guid.NewGuid().ToString();
        //public string Email { get; set; }
        //public string Role { get; set; } //Identity will manage these ones

        public User()
        {
            Tasks = new List<UserTask>();    
        }
    }
}
