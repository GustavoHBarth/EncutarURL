using Microsoft.EntityFrameworkCore;
using EncutarURL.Models;

namespace EncutarURL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }
    }
}
