using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Microsoft.Extensions.Configuration;

namespace CRMSystem
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddHangfire(configuration => configuration.UseSqlServerStorage(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            //app.MapGet("/", () => "Hello Hangfire!");

            RecurringJob.AddOrUpdate(() => Console.WriteLine("Hello Hangfire!"), Cron.Minutely);

            app.Run();
        }
    }
}