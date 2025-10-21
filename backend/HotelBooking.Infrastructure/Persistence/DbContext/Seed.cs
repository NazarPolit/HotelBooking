using HotelBooking.Domain.Entities;
using HotelBooking.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Persistence.DbContext
{
    public class Seed
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seed(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataContextAsync()
        {
            // === 1. MIGRATIONS (Recommended to add) ===
            // Гарантує, що база даних створена
            await _context.Database.MigrateAsync();

            // === 2. SEED ROLES ===
            // Перевіряємо, чи існують ролі
            if (await _roleManager.Roles.AnyAsync())
            {
                // Якщо ролі вже є, ми припускаємо, що сідинг відбувся
                // Це відповідає логіці твого прикладу
                return;
            }

            // Створюємо ролі Administrator та Client
            var roles = new List<IdentityRole>
            {
                new IdentityRole {Name = "Administrator"},
                new IdentityRole {Name = "Client"}
            };

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            // === 3. SEED HOTELS AND ROOMS ===
            // Перевіряємо, чи є вже готелі
            if (await _context.Hotels.AnyAsync())
            {
                return; // Дані вже існують
            }

            // --- Create Hotels ---
            var h1 = new Hotel
            {
                Name = "The Plaza",
                Address = "768 5th Ave, New York, NY 10019, USA",
                Description = "A timeless luxury hotel in the heart of Manhattan."
            };

            var h2 = new Hotel
            {
                Name = "The Savoy",
                Address = "Strand, London WC2R 0EZ, UK",
                Description = "Iconic hotel on the River Thames, near Covent Garden."
            };

            var h3 = new Hotel
            {
                Name = "Ritz Paris",
                Address = "15 Place Vendôme, 75001 Paris, France",
                Description = "Synonymous with elegance and the French art de vivre."
            };

            var h4 = new Hotel
            {
                Name = "Park Hyatt Tokyo",
                Address = "3-7-1-2 Nishi-Shinjuku, Shinjuku-Ku, Tokyo, 163-1055, Japan",
                Description = "Stunning views and sophisticated luxury in Shinjuku."
            };

            var h5 = new Hotel
            {
                Name = "Grand Hotel Lviv",
                Address = "13 Svobody Ave, Lviv, 79000, Ukraine",
                Description = "Historic and luxurious hotel in the city center."
            };

            // --- Create Rooms (linking them to hotels) ---
            var rooms = new List<Room>
            {
                // Rooms for The Plaza (h1)
                new Room { Hotel = h1, Capacity = 2, PricePerNight = 650.00m },
                new Room { Hotel = h1, Capacity = 4, PricePerNight = 1200.00m },
                new Room { Hotel = h1, Capacity = 1, PricePerNight = 480.00m },

                // Rooms for The Savoy (h2)
                new Room { Hotel = h2, Capacity = 2, PricePerNight = 580.00m },
                new Room { Hotel = h2, Capacity = 3, PricePerNight = 850.00m },

                // Rooms for Ritz Paris (h3)
                new Room { Hotel = h3, Capacity = 2, PricePerNight = 950.00m },
                new Room { Hotel = h3, Capacity = 2, PricePerNight = 1100.00m },

                // Rooms for Park Hyatt Tokyo (h4)
                new Room { Hotel = h4, Capacity = 2, PricePerNight = 700.00m },
                new Room { Hotel = h4, Capacity = 3, PricePerNight = 920.00m },

                // Rooms for Grand Hotel Lviv (h5)
                new Room { Hotel = h5, Capacity = 1, PricePerNight = 120.00m },
                new Room { Hotel = h5, Capacity = 2, PricePerNight = 180.00m },
                new Room { Hotel = h5, Capacity = 4, PricePerNight = 250.00m }
            };

            // Додаємо готелі та кімнати в DbContext
            _context.Hotels.AddRange(h1, h2, h3, h4, h5);
            _context.Rooms.AddRange(rooms);

            // === 4. SEED USERS ===

            // --- Administrator User ---
            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@hotel.com",
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(adminUser, "Admin123!");
            await _userManager.AddToRoleAsync(adminUser, "Administrator");

            // --- Client User 1 ---
            var clientUser1 = new User
            {
                UserName = "alice",
                Email = "alice@example.com",
                FirstName = "Alice",
                LastName = "Smith",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(clientUser1, "Client123!");
            await _userManager.AddToRoleAsync(clientUser1, "Client");

            // --- Client User 2 ---
            var clientUser2 = new User
            {
                UserName = "bob",
                Email = "bob@example.com",
                FirstName = "Bob",
                LastName = "Johnson",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(clientUser2, "Client123!");
            await _userManager.AddToRoleAsync(clientUser2, "Client");

            // === 5. SAVE CHANGES ===
            // Зберігаємо всі зміни (готелі, кімнати, користувачі) однією транзакцією
            await _context.SaveChangesAsync();
        }
    }
}