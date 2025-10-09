using RB_Ärendesystem.Datalayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dbProvider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "SqlServer";

if (dbProvider == "PostgreSQL")
{
    builder.Services.AddDbContext<RB_context>(options =>
        options.UseNpgsql(connectionString));
}
else if (dbProvider == "SQLite")
{
    builder.Services.AddDbContext<RB_context>(options =>
        options.UseSqlite(connectionString));
}
else
{
    builder.Services.AddDbContext<RB_context>(options =>
        options.UseSqlServer(connectionString));
}

// Register UnitOfWork for dependency injection
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();

// Add CORS for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",    // React dev server
                "http://localhost:80",      // React production build local
                "http://frontend:80"        // Docker container
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Initialize database with test data on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RB_context>();
    context.Database.EnsureCreated();
    TestData.SeedData();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Comment out HTTPS redirection for Docker HTTP-only setup
// app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
