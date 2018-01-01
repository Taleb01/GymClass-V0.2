namespace GymClass_v0._2.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GymClass_v0._2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "GymClass_v0._2.Models.ApplicationDbContext";
        }

        protected override void Seed(GymClass_v0._2.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole { Name = "admin" });

            }

            if (!context.Users.Any(u => u.UserName == "admin@gymbooking.se"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser
                {
                    UserName = "admin@gymbooking.se",
                    Email = "admin@gymbooking.se",
                    TimeOfRegistration = DateTime.Now,
                    FirstName = "Admin",
                    LastName = "User"
                };
                var result = userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "admin");
            }

            if (!context.Users.Any(u => u.UserName == "member@email.se"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser
                {
                    UserName = "member@email.se",
                    Email = "member@email.se",
                    TimeOfRegistration = DateTime.Now,
                    FirstName = "Member",
                    LastName = "User"
                };
                var result = userManager.Create(user, "password");
            }

            context.GymClasses.AddOrUpdate(c => c.Name,
                new GymClass { Name = "SwimmingClass", Description = "Members", StartTime = DateTime.Now, Duration = TimeSpan.FromMinutes(60) },
                new GymClass { Name = "RunningClass", Description = "OpenClass", StartTime = DateTime.Now.AddHours(-10), Duration = TimeSpan.FromMinutes(45) },
                new GymClass { Name = "KickboxingClass", Description = "Members+15", StartTime = DateTime.Now.AddDays(-12), Duration = TimeSpan.FromMinutes(60) },
                new GymClass { Name = "YogaClass", Description = "OpenClass", StartTime = DateTime.Now.AddHours(-8), Duration = TimeSpan.FromMinutes(90) },
                new GymClass { Name = "AiropicClass", Description = "OpenClass", StartTime = DateTime.Now.AddHours(-11), Duration = TimeSpan.FromMinutes(50) },
                new GymClass { Name = "KungFu", Description = "Members+10", StartTime = DateTime.Now.AddDays(8), Duration = TimeSpan.FromMinutes(90) }

                );
        }
    }
}