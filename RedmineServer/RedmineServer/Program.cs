using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:5165")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Configure DbContext with MySQL provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ConnectionString"), 
    new MariaDbServerVersion(new Version(10, 4, 32))));

var app = builder.Build();

// Test database connection during startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.OpenConnection(); // Open connection to test
        context.Database.CloseConnection(); // Close connection after testing
    }
    catch (Exception ex)
    {
        // Log error if database connection test fails
        app.Logger.LogError(ex, "An error occurred while testing the database connection.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting(); // Enable routing if using endpoints directly.

app.UseCors("BlazorPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();