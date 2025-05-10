using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebApplication2.Services;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<WebApplication2Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication2Context") ?? throw new InvalidOperationException("Connection string 'WebApplication2Context' not found.")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WebApplication2Context>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
            builder.Services.AddSingleton<GameTimerService>();
       
            builder.Services.AddRazorPages();
       builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
           
            builder.Services.AddSignalR();
     builder.Services.AddAzureOpenAI(builder.Configuration);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
            }

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.MapControllers();
            app.UseAuthorization();
            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();
            app.MapHub<ChatHub>("/chathub");
            app.MapHub<GameHub>("/gamehub");
            app.Run();
        }
    }
}
