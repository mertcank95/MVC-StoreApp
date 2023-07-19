using StoreApp.Infrastructe.Extension;
//midlewarelarda sýralam önemli

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);//Api desteði için
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();//razor pages için



builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureSession();


builder.Services.ConfigureRepositoryRegistration();

builder.Services.ConfigureServiceRegistration();

builder.Services.ConfigureApplicationCookie();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureRouting();

var app = builder.Build();

app.UseStaticFiles(); // statik dosyalar için - wwwroot
app.UseSession();// uygulamada sessionlarý kullanacaðýz

app.UseHttpsRedirection();//Yönlendirmeler
//app.MapControllerRoute("default", "{controller=Home}/{action=index}/{id?}");
app.UseRouting();
//oturum midlewarelarý
//routinge endpoint arasýnda olmalý
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
       name: "Admin",
       areaName: "Admin",
       pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=index}/{id?}");

    endpoints.MapRazorPages(); //razer page için

    endpoints.MapControllers();//Api end pointleri için
});



app.ConfigureAndCheckMigration();
app.ConfigureLocalization();
app.ConfigureAdminUser();
app.Run();
