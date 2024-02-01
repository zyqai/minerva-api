using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using MinervaApi.DataAccessLayer;
using MySqlConnector;
var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// var startup = new Startup(builder.Configuration);
// startup.ConfigureServices(builder.Services); // calling ConfigureServices method

builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);
builder.Services.AddTransient<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddTransient<IAuthenticationBusinessLayer, AuthenticationBusinessLayer>();
builder.Services.AddTransient<IUserBL, UserBL>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBusinessBL, BusinessBL>();
builder.Services.AddTransient<IBusinessRepository, BusinessRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IProjectsBL, ProjectsBL>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IClientBL, ClientBL>();
builder.Services.AddTransient<IStatesRepository, StatesRepositiory>();
builder.Services.AddTransient<IStatesBL, StatesBL>();
builder.Services.AddTransient<ITenantRepositiry, TenantRepositiry>();
builder.Services.AddTransient<ITenant, TenantBL>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration.GetValue<string>("OIDC_AUTHORITY");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };
                options.RequireHttpsMetadata = false;

                options.Events = new JwtBearerEvents()
                {
                    
                    OnMessageReceived = msg =>
                    {
                        var token = msg?.Request.Headers.Authorization.ToString();
                        string path = msg?.Request.Path ?? "";
                        if (!string.IsNullOrEmpty(token))

                        {
                            Console.WriteLine("Access token");
                            Console.WriteLine($"URL: {path}");
                            Console.WriteLine($"Token: {token}\r\n");
                        }
                        else
                        {
                            Console.WriteLine("Access token");
                            Console.WriteLine("URL: " + path);
                            Console.WriteLine("Token: No access token provided\r\n");
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = authfailedmsg =>
                    {
                        var stackTrace = authfailedmsg.Exception.StackTrace?.ToString();
                        if (!string.IsNullOrEmpty(stackTrace))
                        {
                            Console.WriteLine("Error Message");
                            Console.WriteLine($"stackTrace: {stackTrace}\r\n");
                        }
                        else
                        {
                            Console.WriteLine("Error Message");
                            Console.WriteLine("stackTrace: no data\r\n");
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = tokenValidated =>
                    {
                        var stackTrace = tokenValidated.Response?.ToString();
                        if (!string.IsNullOrEmpty(stackTrace))
                        {
                            Console.WriteLine("Error Message");
                            Console.WriteLine($"stackTrace: {stackTrace}\r\n");
                        }
                        else
                        {
                            Console.WriteLine("Error Message");
                            Console.WriteLine("stackTrace: no data\r\n");
                        }
                        return Task.CompletedTask;
                    }
                };
            });


//builder.Services.AddAuthorization(options =>
//{
//options.AddPolicy("TenantAdmin",
//    policy => policy.RequireRole("TenentAdmin")
//    );
//});

builder.Services.AddSwaggerGen((c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebApi", Version = "v1" });

    // Key cloak Identity code -- start
    //First we define the security scheme
    c.AddSecurityDefinition("Bearer", //Name the security scheme
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
            Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
    // Key cloak Identity code -- end

}));



builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000/",
                                              "https://dev.minerva.zyq.ai/",
                                              "*.minerva.zyq.ai/");
                      });
});

builder.WebHost.UseUrls("http://localhost:7166");

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// startup.Configure(app, builder.Environment);
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();


app.Run();

