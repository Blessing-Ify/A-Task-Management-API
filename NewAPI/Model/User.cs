using System;

namespace NewAPI.Model
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
