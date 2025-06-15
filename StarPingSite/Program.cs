using Microsoft.EntityFrameworkCore;
using StarPingData.Models.Context;
using StarPingSite.Services;

namespace StarPingSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<StarPingDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StarPingDBConnection")
                    ?? throw new InvalidOperationException("Connection string not found.")));

            //builder.Services.AddAuthorization();
            //builder.Services.AddAuthentication();
            //builder.Services.AddIdentityCore<UserModel>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
#if DEBUG
                options.IdleTimeout = TimeSpan.FromSeconds(40);
#else
                    options.IdleTimeout = TimeSpan.FromMinutes(40);
#endif
                options.Cookie.HttpOnly = true;

                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var baseUri = new Uri("https://localhost:7016");
            builder.Services.AddHttpClient<AddressService>(client => client.BaseAddress = baseUri);
            builder.Services.AddHttpClient<PaymentService>(client => client.BaseAddress = baseUri);
            builder.Services.AddHttpClient<OrderService>(client => client.BaseAddress = baseUri);
            builder.Services.AddHttpClient<DeviceService>(client => client.BaseAddress = baseUri);
            builder.Services.AddHttpClient<OfferService>(client => client.BaseAddress = baseUri);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient<WebCartService>(client => client.BaseAddress = baseUri);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();

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
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
