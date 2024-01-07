using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using MinervaApi.DataAccessLayer;
using MySqlConnector;
var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
builder.Services.AddTransient<IBusinessBL,BusinessBL>();
builder.Services.AddTransient<IBusinessRepository, BusinessRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IProjectsBL, ProjectsBL>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IClientBL, ClientBL>();
builder.Services.AddTransient<IStatesRepository, StatesRepositiory>();
builder.Services.AddTransient<IStatesBL, StatesBL>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000/",
                                              "https://dev.minerva.zyq.ai/",
                                              "*.minerva.zyq.ai/");
                      });
});

var app = builder.Build();
app.UseSwagger();
    app.UseSwaggerUI();
// startup.Configure(app, builder.Environment);
app.UseCors(MyAllowSpecificOrigins);



app.UseHttpsRedirection();

app.MapControllers();


app.Run();

