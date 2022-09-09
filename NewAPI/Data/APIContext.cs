using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewAPI.Model;

namespace NewAPI.Data
{
    public class APIContext : IdentityDbContext<User>
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }
        //public DbSet <User> Users { get; set; }
        public DbSet <Task> Tasks { get; set; }
        public DbSet <UserTask> UserTasks { get; set; }
    }
}
