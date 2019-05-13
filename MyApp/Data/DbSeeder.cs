using Microsoft.AspNetCore.Identity;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Data
{
    public class DbSeeder
    {
        private readonly MyAppContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbSeeder(MyAppContext context,
                        UserManager<ApplicationUser> userManager,
                        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDatabase()
        {
            if (!_context.Teachers.Any())
            {
                List<Teacher> teachers = new List<Teacher>()
                {
                    new Teacher() { Name = "Teacher1", Class = "Korean" },
                    new Teacher() { Name = "Teacher2", Class = "English" },
                    new Teacher() { Name = "Teacher3", Class = "French" },
                    new Teacher() { Name = "Teacher4", Class = "Spanish" }
                };

                await _context.AddRangeAsync(teachers);
                await _context.SaveChangesAsync();
            }

            var adminAccount = await _userManager.FindByNameAsync("admin@gmail.com");
            var adminRole = new IdentityRole("Admin");
            await _roleManager.CreateAsync(adminRole);
            await _userManager.AddToRoleAsync(adminAccount, adminRole.Name);
        }
    }
}
