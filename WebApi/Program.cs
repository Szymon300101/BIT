using BackgroundLogic.Helpers;
using BackgroundLogic.Helpers.Interfaces;
using BackgroundLogic.InputOutput;
using BackgroundLogic.InputOutput.Interfaces;
using WebApi.Helpers;
using WebApi.Helpers.Interfaces;
using WebApi.Hubs;
using WebApi.Models.Factories;
using WebApi.Models.Factories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            //.WithOrigins("http://localhost:3000")
            //.AllowCredentials()
            .AllowAnyOrigin();
    });
});

builder.Services.AddScoped<IInitiativeIO, InitiativeIO>();
builder.Services.AddScoped<IInitiativeConverter, InitiativeConverter>();
builder.Services.AddScoped<IInitiativeCRUDFactory, InitiativeCRUDFactory>();
builder.Services.AddSingleton<IPathMenager, PathLookup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ClientPermission");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//SignalR
app.MapHub<InitiativeHub>("/hubs/initiative");


//wczytywanie ścieżki progDataPath i zapisywanie jej w FileIO
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
FileIO.LoadPath(configuration["ProgDataDirectory"]);
PathLookup.StaticSetProgData(configuration["ProgDataDirectory"]);

app.Run();
