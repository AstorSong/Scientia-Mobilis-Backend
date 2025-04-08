using ScientiaMobilis.Repositories;
using ScientiaMobilis.Services;
using FirebaseAdmin;

var builder = WebApplication.CreateBuilder(args);

FirebaseApp.Create(new AppOptions
{
    Credential = Google.Apis.Auth.OAuth2.GoogleCredential
    .FromFile("scientiamobilis-firebase-adminsdk-fbsvc-960af04016.json")
});

// Add services to the container.
builder.Services.AddScoped<IUserRepository, FirebaseUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
