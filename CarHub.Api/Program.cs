using DAL.SqlServer;
using Application;
using CarHub.Api.Infrastructure.Middlewares;
//using CarHub.Api.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddProblemDetails();

var conn = builder.Configuration.GetConnectionString("MyConn");
builder.Services.AddSqlServerServices(conn!);
builder.Services.AddApplicationServices();
//builder.Services.AddAuthenticationService(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();