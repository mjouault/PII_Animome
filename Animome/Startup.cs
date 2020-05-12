using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Animome.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Animome.Models;

namespace Animome
{
        public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection")));
                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
                services.AddControllersWithViews();
                services.AddRazorPages();
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseDatabaseErrorPage();
                }
                else
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

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
               CreateRoles(services).Wait();
            }

            private async Task CreateRoles(IServiceProvider serviceProvider)
            {
                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                IdentityResult roleResult;
                //Ajout de rôles
                var roleCheck = await RoleManager.RoleExistsAsync("Admin");
                var roleCheck2 = await RoleManager.RoleExistsAsync("Utilisateur");
            if (!roleCheck)
                {
                    //création du rôle d'admin si inexistant
                    roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
                }
            if (!roleCheck2)
            {
                //Création du rôle Utilisateur si inexistant
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Utilisateur"));
            }

            ApplicationUser user3 = await UserManager.FindByEmailAsync("test@gmail.com");


            if (user3 != null && user3.Role == null)
            {
                await UserManager.AddToRoleAsync(user3, "Admin");
                user3.Role = "Admin";
            }

            ApplicationUser user2 = await UserManager.FindByEmailAsync("testUtilisateur@gmail.com");

            //Attribution du rôle d utilisateur
            if (user2 != null && user2.Role == null)
            {
                await UserManager.AddToRoleAsync(user2, "Utilisateur");
                user2.Role = "Utilisateur";
            }
        }
    }
}