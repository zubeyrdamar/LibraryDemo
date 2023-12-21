var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HttpClient to be able to call apis
builder.Services.AddHttpClient();

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

/* ---------------------------- 
|  
| ROUTES
|
*/

app.MapControllerRoute(name: "register", pattern: "/users", defaults: new { controller = "Auth", action = "Users" });

app.MapControllerRoute(name: "register", pattern: "/register", defaults: new { controller = "Auth", action = "Register" });

app.Run();
