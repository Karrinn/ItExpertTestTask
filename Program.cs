using NLog;
using NLog.Web;
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
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try { 
                var builder = WebApplication.CreateBuilder(args);

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

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

                app.UseMiddleware<ExceptionMiddleware>();
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Item}/{action=Data}/{id?}");

                app.MapFallbackToFile("index.html");

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}