using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VtM.Data;
using VtM.Enums;
using VtM.Models;

namespace VtM.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<VtMUser> _userManager;

        public DataService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<VtMUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            await _context.Database.MigrateAsync();
            await SeedRolesAsync();
            await SeedBooksAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (_context.Roles.Any()) return;
            foreach(var role in Enum.GetNames(typeof(Roles)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedBooksAsync()
        {
            if(_context.Books.Any()) return;
            try
            {
                IList<Book> books = new List<Book>() {
                     new Book(){ Title = "N/A" },
                     new Book(){ Title = "Core Rulebook" },
                     new Book(){ Title = "Camarilla" },
                     new Book(){ Title = "Anarch" },
                     new Book(){ Title = "Sabbat" },
                     new Book(){ Title = "Chicago by Night" },
                     new Book(){ Title = "Chigago Folio" },
                     new Book(){ Title = "Cult of the Blood Gods" },
                     new Book(){ Title = "The Fall of London" },
                     new Book(){ Title = "Trails of Ash and Bone" },
                     new Book(){ Title = "Companion" },
                     new Book(){ Title = "Second Inquisition" },
                     new Book(){ Title = "Book of Nod" },
                     new Book(){ Title = "Let the Streets Run Red" },
                     new Book(){ Title = "Children of the Blood" },
                     new Book(){ Title = "Auld Sanguine" }
                };
         
                await _context.AddRangeAsync(books);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Books.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        } 
        public async Task SeedClans()
        {
            if(_context.Clans.Any()) return;
            Clan clan = new Clan()
            {
                Name = "Human",
                Bane = "None",

            };
        }
    }
}
