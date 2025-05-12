using ChatEmailProject.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatEmailProject.Context
{
    public class ProjectContext: IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-J55IQTS\\SQLEXPRESS;initial catalog=ChatEmailProjectdb;integrated security=true;trust server certificate=true");
        }

        public DbSet<Message> Messages { get; set; }

    }
}
