using ContactTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactTrackingSystem.Context
{
    public class DbInitializer
    {
        //private readonly ModelBuilder modelBuilder;
        private readonly ApplicationDbContext _context;

        public DbInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public static void Initialize(ApplicationDbContext _context)
        {
            _context.Database.EnsureCreated();

            if (_context.Contacts.Any())
                return;

            var contacts = new List<Contact> {
              new Contact{ FirstName = "DJ", LastName = "Tanner", Email = "DJTanner@gmail.com", PhoneNumber = "2131234569", Zip = "77002" },
              new Contact{ FirstName = "Stephanie", LastName = "Tanner", Email = "sTanner@aol.com", PhoneNumber = "8189876540", Zip = "77002" },
              new Contact{ FirstName = "Kimmy", LastName = "Gibbler", Email = "kgibbler@yahoo.com", PhoneNumber = "8328529630", Zip = "77007" },
              new Contact{ FirstName = "Joey", LastName = "Gladstone", Email = "JGladstone@hotmail.com", PhoneNumber = "7135306022", Zip = "77002" },
              new Contact{ FirstName = "Danny", LastName = "Tanner", Email = "DTanner@outlook.com", PhoneNumber = "123456790", Zip = "77002" },
              new Contact{ FirstName = "Jesse", LastName = "Katsopolous", Email = "jKats@aol.com", PhoneNumber = "9876543210", Zip = "77072" },
              };

         _context.Contacts.AddRange(contacts);
            _context.SaveChanges();
        }
    }
}
