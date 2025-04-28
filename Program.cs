//using ECommerceWebsite.Models.DB;


using ECommerceWebsite.Models.DB;
using ECommerceWebsite.Services;
using Microsoft.AspNetCore.Identity;

namespace ECommerceWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ECommerceProjectContext>();
            //builder.Services.AddScoped<IProductManager<Product>, ProductManager>();
            //builder.Services.AddScoped<IUserManager<User>, UserManager>();



            builder.Services.AddScoped<ECommerceProjectContext>();
            builder.Services.AddScoped<UserManager>();
            builder.Services.AddScoped<ProductManager>();
            builder.Services.AddScoped<ProductImageManager>();
            builder.Services.AddScoped<CategoryManager>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();  // 👈 Important
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            //app.MapControllerRoute(
            // name: "Users",
            // pattern: "{area:exists}/{controller=Users}/{action=Index}/{id?}");
           
            app.MapControllerRoute(
             name: "Users",
             pattern: "{area:exists}/{controller=Users}/{action=Index}/{id?}");
           app.MapControllerRoute(
             name: "Users",
             pattern: "{area:exists}/{controller=Users}/{action=Create}");
           

            app.Run();
        }
    }
}
