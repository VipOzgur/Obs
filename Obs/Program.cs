using ApiLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add(new AuthorizeFilter());
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagementViewer", policy =>
        policy.RequireRole("Admin", "SuperAdmin"));

    options.AddPolicy("SuperAdminOnly", policy =>
        policy.RequireRole("SuperAdmin"));

    options.AddPolicy("GradesViewer", policy =>
        policy.RequireRole("User", "Ogrenci", "Admin", "SuperAdmin"));
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.Cookie.Name = "Obs.Auth";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

string apiBaseUrl = "http://localhost:5296/";

builder.Services.AddHttpClient<DepartmanApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<DepartmanIstatistikApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<FakulteApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<OgrenciApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<OgrenciDersNotlariApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<DerslerApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<OgrDersApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<PersonelApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<ApiLayer.ObsWeb.Services.PersonelDetayApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<RolesApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<ApiLayer.ObsWeb.Services.SinifApiService>(client => {
    client.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
