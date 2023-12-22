var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HttpClient to be able to call apis
builder.Services.AddHttpClient();

// Sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

/* ---------------------------- 
|  
| ROUTES
|
*/


app.MapControllerRoute(name: "register-view", pattern: "/register-view", defaults: new { controller = "Front", action = "Register" });
app.MapControllerRoute(name: "login-view", pattern: "/login-view", defaults: new { controller = "Front", action = "Login" });
app.MapControllerRoute(name: "books-view", pattern: "/books-view", defaults: new { controller = "Front", action = "Books" });

app.MapControllerRoute(name: "register", pattern: "/register", defaults: new { controller = "Auth", action = "Register" });
app.MapControllerRoute(name: "login", pattern: "/login", defaults: new { controller = "Auth", action = "Login" });

app.Run();
