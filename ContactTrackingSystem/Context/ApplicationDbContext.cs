using ContactTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ContactTrackingSystem.Context
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //new DbInitializer();
            //using (var context = new ApplicationDbContext())
            //{
            //    context.Database.EnsureCreated();
            //}


            //modelBuilder.Entity<Contact>()
            //    .HasData(
            //      new Contact { Id = 1, FirstName = "Danny", LastName = "Tanner", Email = "DTanner@outlook.com", PhoneNumber = "123456790", Zip = "77002" },
            //      new Contact { Id = 2, FirstName = "Jesse", LastName = "Katsopolous", Email = "jKats@aol.com", PhoneNumber = "9876543210", Zip = "77072" },
            //      new Contact { Id = 3, FirstName = "DJ", LastName = "Tanner", Email = "DJTanner@gmail.com", PhoneNumber = "2131234569", Zip = "77002" },
            //      new Contact { Id = 4, FirstName = "Stephanie", LastName = "Tanner", Email = "sTanner@aol.com", PhoneNumber = "8189876540", Zip = "77002" },
            //      new Contact { Id = 5, FirstName = "Kimmy", LastName = "Gibbler", Email = "kgibbler@yahoo.com", PhoneNumber = "8328529630", Zip = "77007" },
            //      new Contact { Id = 6, FirstName = "Joey", LastName = "Gladstone", Email = "JGladstone@hotmail.com", PhoneNumber = "7135306022", Zip = "77002" }
            //    );
        }
    }
}
