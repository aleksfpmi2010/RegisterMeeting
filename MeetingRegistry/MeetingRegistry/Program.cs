using MeetingRegistry;
using MeetingRegistry.EmailService;
using MeetingRegistry.MongoDbModels;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IMongoDataManager, MongoDataManager>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
//app.UseAuthentication();

app.MapControllers();

/*app.Run(async (context) =>
{
    string name = app.Configuration["ValidIssuer"];
});*/

app.Run();
