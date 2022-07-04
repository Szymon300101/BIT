using BackgroundLogic.Helpers;
using BackgroundLogic.InputOutput;
using WebApi.Hubs;

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
PathLookup.ProgData = configuration["ProgDataDirectory"];

app.Run();
