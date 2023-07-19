using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using Services;
using Entities.Models;
using StoreApp.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Infrastructe.Extension
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("sqlconnection")
                    , b => b.MigrationsAssembly("StoreApp"));
                options.EnableSensitiveDataLogging(true);//hassas bilgileri gösterme //geliştirme aşamasında gerekli olabilir yayında kaldırılabilir 
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options => {

                options.SignIn.RequireConfirmedEmail = false;//e-mail onaylamadığı sürece oturum açma durumu
                options.User.RequireUniqueEmail = true; //e-mail adresleri gerekli olsun mu
                //şifre Oluşturma
                options.Password.RequireUppercase = false;//büyük harf gereksin mi
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false; //Rakam gereksin mi
                options.Password.RequiredLength = 6;//şifre uzunluğu

            }).AddEntityFrameworkStores<RepositoryContext>();
        }


        public static void ConfigureSession(this IServiceCollection services)
        {
            //Sesion
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "StoreApp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//kullanıcıların sürekli bu nesneden üretmesine gerek yok
            services.AddScoped<Cart>(c => SessionCart.GetCart(c));//getcart içerisinden bize cart veriyor
        }


        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IAutService, AutService>();
        }

        public static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                //url kısmındaki isimler artık küçük
                options.LowercaseUrls= true;
                options.AppendTrailingSlash= false;//sona / eklemez - true olursa ekler
            });

        }

        public static void ConfigureApplicationCookie(this IServiceCollection services) 
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });
        }

    }
}
