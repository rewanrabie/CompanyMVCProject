using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVC_3.Mapping.Employees;
using MVC_3.Serives;
using MVC_3_BLL;
using MVC_3_BLL.Interfaces;
using MVC_3_BLL.Repositories;
using MVC_3_DAL.Data.Contexts;
using MVC_3_DAL.Models;

namespace MVC_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Configure Services That Allow Dependancy Injection
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<AppDbContext>();  //Allow DI For AppDbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });    //Allow DI For AppDbContext

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>(); // Allow DI For DepartmentRepositry
                                                                                        //builder.Services.AddScoped<DepartmentRepository>(); // Allow DI For DepartmentRepositry
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));


            /* //Life Time
             builder.Services.AddScoped();    // Per Request , UnReachable Object 
             builder.Services.AddSingleton(); // Per App
             builder.Services.AddTransient(); // Per Opertions
             */

            builder.Services.AddScoped<IScopedSerives, ScopedSerives>();
            builder.Services.AddSingleton<ISingletonSerives, SingletonSerives>();
            builder.Services.AddTransient<ITransientServies, TransientServies>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                           .AddEntityFrameworkStores<AppDbContext>()
                           .AddDefaultTokenProviders();
            // builder.Services.AddScoped<UserManager<ApplicationUser>>();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });



            #endregion

            var app = builder.Build();

            #region Configure Http Request Pipeline Or Middlewares


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
            #endregion

        }
    }
}
