using DotNetAssign2;
using DotNetAssign2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

class Program { 
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Configure email settings and add email sender service
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
        builder.Services.AddTransient<IEmailSender, EmailSender>();

        //  Configure database contexts
        builder.Services.AddDbContext<UsersContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UsersContext") ?? throw new InvalidOperationException("Connection string 'UsersContext' not found.")));

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Configure identity services
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();


        // Using MVC
        builder.Services.AddControllersWithViews();
        
        // Creates the WebApp 
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Apply any pending migrations 
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<UsersContext>();
            db.Database.Migrate();

            // Create user roles (Staff/Admin)
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            CreateRoles(roleManager).Wait();
        }

        // Configure middleware pipeline
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();
        app.Run();

        // Helper method for creating roles 
        async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Staff", "Administrator" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Create the roles and seed them to the database
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}