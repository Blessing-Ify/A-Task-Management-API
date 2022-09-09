using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace NewAPI.Model
{
    public class User: IdentityUser
    {
        //public string Id { get; set; } = Guid.NewGuid().ToString();
        public string LastName { get; set; }
        public string FirstName { get; set; }
        //public string Email { get; set; }
        //public string Role { get; set; } //Identity will manage the role
        public IEnumerable<UserTask> Tasks { get; set; }

        public User()
        {
            Tasks = new List<UserTask>();    
        }
    }
}
