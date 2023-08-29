using ItExpertTestTask.Data;
using ItExpertTestTask.Middleware;
using ItExpertTestTask.Services;
using ItExpertTestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItExpertTestTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("localdb")
                    ?? throw new InvalidOperationException("Connection string 'localdb' not found."))
            );

            builder.Services.AddScoped<IItemSaveService, ItemSaveService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Item}/{action=Data}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}