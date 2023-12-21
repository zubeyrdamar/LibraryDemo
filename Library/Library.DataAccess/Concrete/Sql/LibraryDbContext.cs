using Library.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Concrete.Sql
{
    public class LibraryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=db_library;Trusted_Connection=True;TrustServerCertificate=True");
        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasOne(o => o.Borrowing).WithOne(o => o.Book).HasForeignKey<Borrowing>(o => o.Id);
        }
        */

        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowing { get; set; }
    }
}
