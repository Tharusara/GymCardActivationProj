using Microsoft.EntityFrameworkCore;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.Data
{
    public class AppDbContext: DbContext
    {
        // Database Tables/Entities
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLines> InvoiceLines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {            
        }
    }
}
