using Microsoft.AspNetCore.Identity;
using RunGroupWebApp.Data.Enum;
using RunGroupWebApp.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace RunGroupWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Racing Club 1",
                            Image = "https://i.pinimg.com/564x/f3/af/dd/f3afddae33344bb1ff41653bc86df2f8.jpg",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.City,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                         },
                        new Club()
                        {
                            Title = "Racing Club 2",
                            Image = "https://i.pinimg.com/564x/dc/58/72/dc5872404ec53b283d3d47ea09ac379d.jpg",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.Endurance,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Club()
                        {
                            Title = "Racing Club 3",
                            Image = "https://i.pinimg.com/564x/a6/07/76/a60776fb1796be0fa9bfc3002755914d.jpg",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.Trail,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Races.Any())
                {
                    context.Races.AddRange(new List<Race>()
                    {
                        new Race()
                        {
                            Title = "Sprint",
                            Image = "https://i.pinimg.com/564x/e9/4c/67/e94c679adf9f551d031ce8756cee54f0.jpg",
                            Description = "Sprint is a high-speed discipline in which drivers take turns to set a time around a lap of a race circuit or a point-to-point course, with the fastest times determining the results.",
                            RaceCategory = RaceCategory.Sprint,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Race()
                        {
                            Title = "Timelapse",
                            Image = "https://i.pinimg.com/564x/0c/2e/ce/0c2ece9b647021ebfffdb14c6b5e3e11.jpg",
                            Description = "A time-lapse race for cars is a visually captivating and condensed representation of a racing event",
                            RaceCategory = RaceCategory.Timelapse,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Race()
                        {
                            Title = "Drift",
                            Image = "https://i.pinimg.com/564x/6d/7d/e5/6d7de50b8d4553bfca0596552c109c2c.jpg",
                            Description = "Drifting is a globally recognised motorsport discipline, which involves the driver intentionally oversteering a car to break traction of the rear (or sometimes all four) tyres around a corner.",
                            RaceCategory = RaceCategory.Drift,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Race()
                        {
                            Title = "Drag",
                            Image = "https://i.pinimg.com/564x/1f/1e/51/1f1e519e3f562d7d2dfcb4c4c975be3c.jpg",
                            Description = "Drag racing is the fastest, loudest and perhaps most spectacular of motorsports, pitting drivers and their cars against one another in pairs from a standing start. The standard course is a straight quarter-mile – sometimes shorter, never longer – and the racing format is instant-knockout.",
                            RaceCategory = RaceCategory.Drag,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Race()
                        {
                            Title = "Circle",
                            Image = "https://i.pinimg.com/564x/03/7e/a9/037ea93ef950abd9a51c0e86721a1a85.jpg",
                            Description = "Racing on a circular track means running on a circular path such that the starting point and the finishing point coincide with each other.",
                            RaceCategory = RaceCategory.Circle,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "safchik9@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "safchik",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
