using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using MinervaApi.BusinessLayer;
using MinervaApi.BusinessLayer.Interface;
using MinervaApi.DataAccessLayer;
using MinervaApi.ExternalApi;
using MinervaApi.IDataAccessLayer;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Extensions.Http;
using Polly.Registry;
using System.Net;
using System.Security.Claims;
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
builder.Services.AddTransient<IPersonaRepository,PersonaRepository>();
builder.Services.AddTransient<IPersona, PersonaBL>();

builder.Services.AddTransient<ICBRelationRepository, CBRelationRepository>();
builder.Services.AddTransient<ICBRelation, CBRelationBL>();
builder.Services.AddTransient<IFileTypeRepository, FileTypeRepository>();
builder.Services.AddTransient<IFileTypeBL, FileTypeBL>();
builder.Services.AddTransient<IDocumentClassificationRepository, DocumentClassificationRepository>();
builder.Services.AddTransient<IDocumentClassificationBL, DocumentClassificationBL>();
builder.Services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
builder.Services.AddTransient<IDocumentTypeBL, DocumentTypeBL>();
builder.Services.AddTransient<IReminderRepository, ReminderRepository>();
builder.Services.AddTransient<IReminderBL, ReminderBL>();
builder.Services.AddTransient<IRequestTemplateRepository, RequestTemplateRepository>();
builder.Services.AddTransient<IRequestTemplateBL, RequestTemplateBL>();
builder.Services.AddTransient<IRequestTemplateDetailsRepository, RequestTemplateDetailsRepository>();
builder.Services.AddTransient<IRequestTemplateDetailsBL, RequestTemplateDetailsBL>();

builder.Services.AddTransient<IMasterRepository, MasterRepository>();
builder.Services.AddTransient<IMasterBL, MasterBL>();

builder.Services.AddTransient<ILenderRepository, LenderRepository>();
builder.Services.AddTransient<ILenderBL , LenderBL>();

builder.Services.AddTransient<IprojectPeopleRelation,projectPeopleRelationBL>();
builder.Services.AddTransient<IprojectPeopleRelationRepository,projectPeopleRelationRepository>();

builder.Services.AddTransient<IprojectBusinessesRelation, projectBusinessesRelation>();
builder.Services.AddTransient<IprojectBusinessesRelationRepository, projectBusinessesRelationRepository>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

var configuration = builder.Configuration;
var authority = configuration.GetValue<string>("OIDC_AUTHORITY");
var metada_address = configuration.GetValue<string>("OIDC_METADATA");
var client_id = configuration.GetValue<string>("OIDC_CLIENT_ID");
var client_secret = configuration.GetValue<string>("OIDC_CLIENT_SECRET");
var tokenEndpoint = configuration.GetValue<string>("OIDC_TOKEN_ENDPOINT");
var baseURL = configuration.GetValue<string>("KC_API_URL");

var registry = new PolicyRegistry()
{
    { "defaultretrystrategy", HttpPolicyExtensions.HandleTransientHttpError()
    .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
    .WaitAndRetryAsync(
        retryCount: 3,
        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
    ) },
    { "defaultcircuitbreaker", HttpPolicyExtensions.HandleTransientHttpError()
    .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
    .CircuitBreakerAsync(
        handledEventsAllowedBeforeBreaking: 3,
        durationOfBreak: TimeSpan.FromSeconds(30)
    ) },
};
builder.Services.AddPolicyRegistry(registry);
// Add AccessTokenManagement
builder.Services.AddClientAccessTokenManagement()
                .ConfigureBackchannelHttpClient()
                .AddTransientHttpErrorPolicy(builder =>
                builder.WaitAndRetryAsync(
                    retryCount: 3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                ));
// Add keycloak client
builder.Services.AddHttpClient<IKeycloakApiService, KeycloakApiService>(client =>
{
    client.BaseAddress = new Uri(baseURL);
})
                .AddClientAccessTokenHandler()
                .AddPolicyHandlerFromRegistry("defaultretrystrategy")
                .AddPolicyHandlerFromRegistry("defaultcircuitbreaker");


const string openIdConnectAuthenticationScheme = OpenIdConnectDefaults.AuthenticationScheme;
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = openIdConnectAuthenticationScheme;
})
.AddOpenIdConnect(openIdConnectAuthenticationScheme, options =>
{
    options.Authority = authority;
    options.MetadataAddress = metada_address;
    options.ClientId = client_id;
    options.ClientSecret = client_secret;
})


            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration.GetValue<string>("OIDC_AUTHORITY");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                };
                // options.Audience = authorizationConfig["Audience"];
                options.IncludeErrorDetails = true;

                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = ctx =>
                    {
                        string? clientId = ctx.Principal?.FindFirstValue("azp");

                        ClaimsIdentity claimsIdentity = (ClaimsIdentity)ctx.Principal!.Identity!;

                        var resource_access = claimsIdentity.FindFirst((claim) => claim.Type == "resource_access")?.Value;
                        var realm_access = claimsIdentity.FindFirst((claim) => claim.Type == "realm_access")?.Value;

                        JObject resourceObj = JObject.Parse(resource_access!);
                        // JObject realmObj = JObject.Parse(realm_access!);

                        var resource_roles = resourceObj.GetValue(clientId)!.ToObject<JObject>()!.GetValue("roles");
                        foreach (JToken role in resource_roles!)
                        {
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                        }

                        // var realm_roles = resourceObj.GetValue("roles");
                        return Task.CompletedTask;
                    }

                };
            });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TenantAdminPolicy",
        builder =>
        {
            builder.AuthenticationSchemes = new List<string>
            {
                JwtBearerDefaults.AuthenticationScheme
            };
            builder.RequireAuthenticatedUser();
            builder.RequireRole("TenantAdmin");
        }
        );
    options.AddPolicy("AdminPolicy",
        builder =>
        {
            builder.AuthenticationSchemes = new List<string>
            {
                JwtBearerDefaults.AuthenticationScheme
            };
            builder.RequireAuthenticatedUser();
            builder.RequireRole("admin");
        }
        );
    options.AddPolicy("AdminPolicy",
        builder =>
        {
            builder.AuthenticationSchemes = new List<string>
            {
                JwtBearerDefaults.AuthenticationScheme
            };
            builder.RequireAuthenticatedUser();
            builder.RequireRole("Admin");
        }
        );
    options.AddPolicy("StaffPolicy",
        builder =>
        {
            builder.AuthenticationSchemes = new List<string>
            {
                JwtBearerDefaults.AuthenticationScheme
            };
            builder.RequireAuthenticatedUser();
            builder.RequireRole("Staff");
        }
        );

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

builder.Services.AddHttpContextAccessor();

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

builder.WebHost.UseUrls("https://localhost:7166");

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

