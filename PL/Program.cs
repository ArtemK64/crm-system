using BLL;
using DAL.Interfaces;
using Hangfire;

namespace PL
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
            builder.Services.AddScoped<IHomeRepository, HomeRepository>();
            builder.Services.AddScoped<IHomeService, HomeService>();

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

            RecurringJob.AddOrUpdate(() => WriteHelloHangfire(), Cron.Minutely);

            app.Run();
        }

        public static void WriteHelloHangfire()
        {
            Console.WriteLine("Hello Hangfire!");
        }
    }
}