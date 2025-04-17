using DAL.SqlServer;
using Application;

using CarHub.Api.Infrastructure.Middlewares;
using CarHub.Api.Security;

using Application.Security;
using CarHub.Api.Infrastructure;
using SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllWithCredentials", policy =>
    {
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials() 
              .WithOrigins("http://localhost:5173"); 
    });
});


builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSwaggerService();
builder.Services.AddScoped<IUserContext, HttpUserContext>();

var conn = builder.Configuration.GetConnectionString("MyConn");
builder.Services.AddSqlServerServices(conn!);
builder.Services.AddApplicationServices();
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllWithCredentials",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllWithCredentials"); 

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<MessageHub>("/messageHub");

//app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
