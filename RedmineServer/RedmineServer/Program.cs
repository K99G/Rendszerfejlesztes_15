using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Cors Service
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:5165")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// Ensure your connection string name matches the one in your appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies()
        .UseMySql(builder.Configuration.GetConnectionString("ConnectionString"), 
    new MariaDbServerVersion(new Version(10, 4, 32))));

var app = builder.Build();

// Test database connection
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.OpenConnection();
        context.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while testing the database connection.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseRouting(); // Ensure routing is enabled if you use endpoints directly.

app.UseAuthorization();

app.MapControllers();


app.Run();
