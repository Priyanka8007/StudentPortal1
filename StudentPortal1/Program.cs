
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using StudentPortal1.Data;
using StudentPortal1.Mappings;
using StudentPortal1.Repositories;
using StudentPortal1.Services;

//using StudentPortal1.Services;
using System.Text;

namespace StudentPortal1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddAntiforgery(options =>
            //{
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //    options.Cookie.SameSite = SameSiteMode.Strict;
            //});

            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //    options.Cookie.SameSite = SameSiteMode.Strict;
            //});


            builder.Services.AddControllersWithViews();
            //builder.Services.AddAntiforgery(options =>
            //{
            //    options.HeaderName = "RequestVerificationToken";
            //});

            var logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File("Log/NzWalks_Log.txt", rollingInterval: RollingInterval.Day)
           .MinimumLevel.Information()
           .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);


            // Add services to the container.
          
            builder.Services.AddRazorPages()
                .AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<EmployeeDbContext>(options =>
        
            
            options.UseSqlServer(builder.Configuration.GetConnectionString("employeeConnectionString1")));

            builder.Services.AddDbContext<StudentDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("StudentConnectionString")));

         

            builder.Services.AddDbContext<StudentAuthDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("StudentAuthConnectionString")));

            builder.Services.AddIdentityCore<IdentityUser>()
                 .AddRoles<IdentityRole>()
                 .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("StudentPortal1")
                 .AddDefaultTokenProviders();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<StudentAuthDbContext>();


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;



            });
            builder.Services.AddScoped<IStudentRepository, SqlStudentRepository>();
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
          builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<IStateRepository, StateRepository>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IPayrollRepository, PayrollRepository>();
            builder.Services.AddScoped<IPayrollService, PayrollService>();

            // Add HttpClient
            builder.Services.AddHttpClient();

            // Configure authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            // Add authorization
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

      
       
    }
}