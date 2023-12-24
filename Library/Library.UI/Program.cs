var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HttpClient to be able to call apis
builder.Services.AddHttpClient();

// Sessions
builder.Services.AddHttpContextAccessor();
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

// Front
app.MapControllerRoute(name: "error-view", pattern: "/error", defaults: new { controller = "Front", action = "Error" });
app.MapControllerRoute(name: "register-view", pattern: "/register-view", defaults: new { controller = "Front", action = "Register" });
app.MapControllerRoute(name: "login-view", pattern: "/login-view", defaults: new { controller = "Front", action = "Login" });
app.MapControllerRoute(name: "book-list-view", pattern: "/book-list-view", defaults: new { controller = "Front", action = "Books" });
app.MapControllerRoute(name: "book-detail-view", pattern: "/book-detail-view", defaults: new { controller = "Front", action = "Detail" });
app.MapControllerRoute(name: "book-create-view", pattern: "/book-create-view", defaults: new { controller = "Front", action = "Create" });
app.MapControllerRoute(name: "book-update-view", pattern: "/book-update-view", defaults: new { controller = "Front", action = "Update" });
app.MapControllerRoute(name: "book-delete-view", pattern: "/book-delete-view", defaults: new { controller = "Front", action = "Delete" });

// Auth
app.MapControllerRoute(name: "register", pattern: "/register", defaults: new { controller = "Auth", action = "Register" });
app.MapControllerRoute(name: "login", pattern: "/login", defaults: new { controller = "Auth", action = "Login" });
app.MapControllerRoute(name: "logout", pattern: "/logout", defaults: new { controller = "Auth", action = "Logout" });

// Books
app.MapControllerRoute(name: "book-create", pattern: "/book/create", defaults: new { controller = "Books", action = "Create" });
app.MapControllerRoute(name: "book-update", pattern: "/book/update", defaults: new { controller = "Books", action = "Update" });
app.MapControllerRoute(name: "book-delete", pattern: "/book/delete", defaults: new { controller = "Books", action = "Delete" });

app.Run();
