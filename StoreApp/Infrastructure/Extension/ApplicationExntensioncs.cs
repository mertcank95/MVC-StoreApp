using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructe.Extension
{
    public static class ApplicationExntensioncs
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {
            //uygulama üzerinden ihtiyaç duyulan servisi alıyor
            RepositoryContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RepositoryContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }


        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("tr-TR")
                    .AddSupportedCultures("tr-TR")
                    .SetDefaultCulture("tr-TR");
            });
        }

        public static async void ConfigureAdminUser(this IApplicationBuilder app)
        {
            const string adminUser = "Admin";
            const string adminPassword = "Admin+123456";
            //User manager
            UserManager<IdentityUser> userManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();//ilgili servisi IoC üzerinden almış olacağız
            //Role manager --admine tüm rolleri verme
            RoleManager<IdentityRole> roleManager = app
                .ApplicationServices
                .CreateAsyncScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if(user is null)
            {
                user = new IdentityUser()
                {
                    Email="krtl@gmail.com",
                    PhoneNumber="55664488",
                    UserName=adminUser,
                    EmailConfirmed = true,

                };
                var result = await userManager.CreateAsync(user,adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception("Admin User could not created.");
                }

                var roleResult = await userManager.AddToRolesAsync(user,
                   roleManager
                    .Roles
                    .Select(r => r.Name)
                    .ToList()
                );

                if(!roleResult.Succeeded) {
                    throw new Exception("System have problems with role defination");
                
                }

            }


        }


    }



}
