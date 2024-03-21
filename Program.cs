using Microsoft.EntityFrameworkCore;
using Mep01Web.Infrastructure;
using Mep01Web.Service.Impl;
using Mep01Web.Validators;
using Mep01Web.Validators.Impl;
using Mep01Web.Service.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using MepWeb.Service;
using MepWeb.Service.Interface;
using MepWeb.Service.Impl;
using MepWeb.Controllers;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = "auth_mepscweb";
                //options.Cookie.Path = "/Login";
                options.LoginPath = "/login/login";
                options.LogoutPath = "/login/login";
                //options.AccessDeniedPath = "/Login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(240);
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.SlidingExpiration = true;
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<SessionTimeoutFilterAttribute>();
            });

            // [RP] for session variable.
            builder.Services.AddScoped<UserScope>();
            builder.Services.AddScoped<GetConfig>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();


            builder.Services.AddDbContext<SataconsultingContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("Main")));
            builder.Services.AddRazorPages();
            builder.Services.AddTransient<ICrrgService, CrrgService>();
            builder.Services.AddTransient<ICrrgValidator, CrrgValidator>();
            builder.Services.AddTransient<ITatvService, TatvService>();
            builder.Services.AddTransient<ITbcpService, TbcpService>();
            builder.Services.AddTransient<IAcliService, AcliService>();
            builder.Services.AddTransient<IOlcaService, OlcaService>();
            builder.Services.AddTransient<IPscCo02Service, PscCo02Service>();
            builder.Services.AddTransient<IMvxpa01Service, Mvxpa01Service>();
            builder.Services.AddTransient<ITbpnService, TbpnService>();
            builder.Services.AddTransient<ILoginService, LoginService>();
            builder.Services.AddTransient<IOreQualificaService, OreQualificaService>();
            builder.Services.AddTransient<IRegistroRicaricheService, RegistroRicaricheService>();
            builder.Services.AddTransient<IOreQualificaService, OreQualificaService>();
            builder.Services.AddTransient<ITcdlService, TcdlService>();
            builder.Services.AddTransient<IPscQualService, PscQualService>();
            builder.Services.AddTransient<IVsCommAperteXCliService, VsCommAperteXCliService>();
            builder.Services.AddTransient<IMaccService, MaccService>();
            builder.Services.AddTransient<IQualificheService, QualificheService>();
            builder.Services.AddTransient<IPsc001AService, Psc001AService>();


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

            app.UseRouting();
            //app.UseAuthentication(); //[RP] 29:47 https://www.youtube.com/watch?v=hZ1DASYd9rk
            app.UseAuthentication();
            app.UseSession(); //[RP] for session variable.
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
// https://www.youtube.com/watch?v=hZ1DASYd9rk 02:10:40   Edit 02:25   Delete 02:36