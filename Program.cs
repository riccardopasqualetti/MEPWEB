using Microsoft.EntityFrameworkCore;
using Mep01Web.Infrastructure;
using Mep01Web.Service.Impl;
using Mep01Web.Validators;
using Mep01Web.Validators.Impl;
using Mep01Web.Service.Interface;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

			// [RP] for session variable.
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout= TimeSpan.FromMinutes(120);
            });


			builder.Services.AddDbContext<SataconsultingContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("Main")));
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<ICrrgService, CrrgService>();
            builder.Services.AddTransient<ICrrgValidator, CrrgValidator>();
            builder.Services.AddTransient<ITatvService, TatvService>();
            builder.Services.AddTransient<ITbcpService, TbcpService>();
			builder.Services.AddTransient<IAcliService, AcliService>();
			builder.Services.AddTransient<IOlcaService, OlcaService>();
			builder.Services.AddTransient<IMvxpa01Service, Mvxpa01Service>();
			builder.Services.AddTransient<ITbpnService, TbpnService>();

			var app = builder.Build();

            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); //[RP] defined in wwwroot.
            app.UseSession(); //[RP] for session variable.

			app.UseRouting();
            //app.UseAuthentication(); //[RP] 29:47 https://www.youtube.com/watch?v=hZ1DASYd9rk
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
// https://www.youtube.com/watch?v=hZ1DASYd9rk 02:10:40   Edit 02:25   Delete 02:36