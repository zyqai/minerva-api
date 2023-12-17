using Minerva.BusinessLayer;
using Minerva.BusinessLayer.Interface;
using Minerva.DataAccessLayer;
using Minerva.IDataAccessLayer;
using MySqlConnector;
var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();
// startup.Configure(app, builder.Environment);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();

