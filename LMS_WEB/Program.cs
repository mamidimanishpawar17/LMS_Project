using LMS_WEB.Services.IServices;
using LMS_WEB.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using LMS_WEB;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));



builder.Services.AddHttpClient<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddHttpClient<IBookService, BookService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddHttpClient<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddHttpClient<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

builder.Services.AddHttpClient<IIssueOverDueService, IssueOverDueService>();
builder.Services.AddScoped<IIssueOverDueService, IssueOverDueService>();

builder.Services.AddHttpClient<IIssueService, IssueService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication
    (CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Cookie.HttpOnly = true;
                  options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                  options.LoginPath = "/Auth/Login";
                  options.AccessDeniedPath = "/Auth/AccessDenied";
                  options.SlidingExpiration = true;
              });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


