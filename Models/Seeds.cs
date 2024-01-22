using EmployeeMgtSystemAPI.DTO;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EmployeeMgtSystemAPI.Models
{
    public class Seeds
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceProvider = applicationBuilder.ApplicationServices.CreateScope())
            {
                //roles
                var roleManager = serviceProvider.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.ScrumMaster))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.ScrumMaster));
                if (!await roleManager.RoleExistsAsync(UserRoles.Developer))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Developer));
                if (!await roleManager.RoleExistsAsync(UserRoles.Design))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Design));
                

                //Users
                var userManager = serviceProvider.ServiceProvider.GetRequiredService<UserManager<Employee>>();
                string adminUserEmail = "ogbonnajoy@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {

                    var newAdminUser = new Employee()
                    {
                        UserName = "ogbonnajoy",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        JobRole = new JobRole
                        {
                            Name = UserRoles.Admin,
                            Description = "Admin Manager"
                        }
                        
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "developer@gmail.com";

                var devUser = await userManager.FindByEmailAsync(appUserEmail);
                if (devUser == null)
                {
                    var newAppUser = new Employee()
                    {
                        UserName = "dev-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        JobRole = new JobRole
                        {
                            Name = UserRoles.Developer,
                            Description = "Developer"
                        }

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Developer);
                }

                appUserEmail = "scrummaster@gmail.com";

                var scrumUser = await userManager.FindByEmailAsync(appUserEmail);
                if (scrumUser == null)
                {
                    var newAppUser = new Employee()
                    {
                        UserName = "Scrum-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        JobRole = new JobRole
                        {
                            Name = UserRoles.ScrumMaster,
                            Description = "Scrum Master"
                        }

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.ScrumMaster);
                }

                appUserEmail = "designuser@gmail.com";

                var designUser = await userManager.FindByEmailAsync(appUserEmail);
                if (designUser == null)
                {
                    var newAppUser = new Employee()
                    {
                        UserName = "design-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        JobRole = new JobRole
                        {
                            Name = UserRoles.Design,
                            Description = "Design Manager"
                        }

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Design);
                }

                appUserEmail = "manager@gmail.com";

                var managerUser = await userManager.FindByEmailAsync(appUserEmail);
                if (managerUser == null)
                {
                    var newAppUser = new Employee()
                    {
                        UserName = "manager-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        JobRole = new JobRole
                        {
                            Name = UserRoles.Manager,
                            Description = "Group Manager"
                        }

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Manager);
                }

            }
        }
    }
}

