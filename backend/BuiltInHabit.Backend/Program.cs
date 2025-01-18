using MongoDB.Driver;
using BuiltInHabit.Backend.Models;
using BuiltInHabit.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Load the MongoDB connection string from appsettings.json
var mongoConnectionString = builder.Configuration.GetValue<string>("MongoDb:ConnectionString");

if (string.IsNullOrEmpty(mongoConnectionString))
{
    throw new InvalidOperationException("MongoDB connection string is not provided.");
}

// Register MongoDB client
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    return new MongoClient(mongoConnectionString);
});

// Register MongoDbContext as a service, passing IConfiguration for MongoDbContext constructor
builder.Services.AddScoped<MongoDbContext>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HabitService
builder.Services.AddScoped<IHabitService, HabitService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Frontend origin
              .AllowAnyMethod()  // Allow GET, POST, PUT, DELETE, etc.
              .AllowAnyHeader()  // Allow all headers
              .AllowCredentials();  // Allow cookies/authentication if needed
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS middleware
app.UseCors("AllowFrontend");

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();