using StoreApp.Infrastructe.Extension;
//midlewarelarda s�ralam �nemli

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);//Api deste�i i�in
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();//razor pages i�in



builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureSession();


builder.Services.ConfigureRepositoryRegistration();

builder.Services.ConfigureServiceRegistration();

builder.Services.ConfigureApplicationCookie();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureRouting();

var app = builder.Build();

app.UseStaticFiles(); // statik dosyalar i�in - wwwroot
app.UseSession();// uygulamada sessionlar� kullanaca��z

app.UseHttpsRedirection();//Y�nlendirmeler
//app.MapControllerRoute("default", "{controller=Home}/{action=index}/{id?}");
app.UseRouting();
//oturum midlewarelar�
//routinge endpoint aras�nda olmal�
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

    endpoints.MapRazorPages(); //razer page i�in

    endpoints.MapControllers();//Api end pointleri i�in
});



app.ConfigureAndCheckMigration();
app.ConfigureLocalization();
app.ConfigureAdminUser();
app.Run();
