using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using MySqlConnector;

namespace Minerva;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddRazorPages();
        
        services.AddSingleton<IConfiguration>(_configuration);
        services.AddMySqlDataSource(_configuration.GetConnectionString("Default")!);
        services.AddTransient<IAuthenticationBusinessLayer, AuthenticationBusinessLayer>();
        services.AddTransient<IAdminUserRepository, AdminUserRepository>();
        // services.Configure<ConnectionStringSettings>(_configuration.GetSection("ConnectionStrings"));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseDeveloperExceptionPage();
        app.UseStaticFiles();

        app.UseRouting();
       

        static void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/MapGet", () => "MapGet");

            endpoints.MapControllers();
            // endpoints.MapControllerRoute(
            //     Guid.NewGuid().ToString(),
            //     "{controller=Home}/{action=Index}/{id?}");

            // endpoints.MapRazorPages();
        }

        // app.UseEndpoints(builder =>
        // {
        //     ConfigureEndpoints(builder);
        //     var group = builder.MapGroup("/group");
        //     ConfigureEndpoints(group);
        // });
    }

    public static void Main(string[] args)
    {
        var host = CreateWebHostBuilder(args)
            .Build();

        host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        new WebHostBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureLogging(factory =>
            {
                factory
                    .AddConsole()
                    .AddDebug();
            })
            .UseKestrel()
            .UseStartup<Startup>();
}