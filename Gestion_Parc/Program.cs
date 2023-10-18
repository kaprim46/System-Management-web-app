using Gestion_Parc.DataDbContext;
using Gestion_Parc.IRepository;
using Gestion_Parc.IRepository.ServiceRepository;
using Gestion_Parc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;
using Gestion_Parc.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContxt>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("sqlCon")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContxt>().AddDefaultTokenProviders();

builder.Services.AddSession();




builder.Services.ConfigureApplicationCookie(option =>  //to return login page
{
    //option.LoginPath = "/Account";
    option.AccessDeniedPath = "/Home/Denied";
});




builder.Services.AddScoped<SignInManager<ApplicationUser>>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
   options.ValidationInterval = TimeSpan.Zero;
});


builder.Services.AddScoped<IServiceRepositoryDepartment<Department>, ServiceDepartment>();
builder.Services.AddScoped<IServiceRepositoryBrand<Brand>, ServiceBrand>();
builder.Services.AddScoped<IServiceRepositoryLogBrand<LogBrand>, ServiceLogBrand>();
builder.Services.AddScoped<IServiceRepositoryComputer<Computer>, ServiceComputer>();
builder.Services.AddScoped<IServiceRepositoryLogComputer<LogComputer>, ServiceLogComputer>();
builder.Services.AddScoped<IServiceRepositoryPrinter<Printer>, ServicePrinter>();
builder.Services.AddScoped<IServiceRepositoryLogPrinter<LogPrinter>, ServiceLogPrinter>();
builder.Services.AddScoped<IServiceRepositoryServer<Server>, ServiceServer>();
builder.Services.AddScoped<IServiceRepositoryLogServer<LogServer>, ServiceLogServer>();
builder.Services.AddScoped<IServiceRepositoryOther<Other>, ServiceOther>();
builder.Services.AddScoped<IServiceRepositoryLogOther<LogOther>, ServiceLogOther>();
builder.Services.AddScoped<IServiceRepositoryBreakDown<BreakDown>, ServiceBreakDown>();
builder.Services.AddScoped<IServiceRepositoryMaintenance<Maintenance>, ServiceMaintenance>();
builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

using var Scope = app.Services.CreateScope();
var Services = Scope.ServiceProvider;
try
{
    var userManager = Services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = Services.GetRequiredService<RoleManager<IdentityRole>>();

    await DefaultRole.CreateRoleAsync(roleManager);
    await DefaultUser.CreateAdminAsync(userManager, roleManager);
}
catch (Exception)
{
    throw;
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

