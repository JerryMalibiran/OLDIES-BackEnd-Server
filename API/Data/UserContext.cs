using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserContext: DbContext
    {

        public UserContext(DbContextOptions<UserContext> options): base(options)
        { 
        }

        public DbSet<User> Users { get; set; }

    }
}
