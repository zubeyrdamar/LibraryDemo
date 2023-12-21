using Library.Api.Identity;
using Library.Api.Mappings;
using Library.Business.Abstract;
using Library.Business.Concrete;
using Library.DataAccess.Abstract;
using Library.DataAccess.Concrete.Sql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

/*----------------------------- 
|
| IDENTITY SETTINGS
|
*/

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Identity"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/denied";

    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
    options.Cookie = new CookieBuilder
    {
        Name = "ShopApp.Security.Cookie",
        HttpOnly = true,
    };
});

/*----------------------------- 
|
| JWT SETTINGS
|
*/

/*----------------------------- 
|
| DEPENDENCY INJECTIONS
|
*/

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddScoped<IBookRepository, SqlBookRepository>();
builder.Services.AddScoped<IBorrowingRepository, SqlBorrowingRepository>();

builder.Services.AddScoped<IBookService, BookManager>();
builder.Services.AddScoped<IBorrowingService, BorrowingManager>();

/*----------------------------- 
|
| MIGRATIONS
|
*/

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

DatabaseSeeder.Seed();
app.Run();
