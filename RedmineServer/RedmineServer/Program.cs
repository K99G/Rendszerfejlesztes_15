using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("YourConnectionString"), 
    new MariaDbServerVersion(new Version(10, 4, 32))));


var app = builder.Build();


//SQLserver tester
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        // Ellenőrizzük, hogy a kapcsolat él-e
        context.Database.OpenConnection();
        context.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Hiba történt az adatbázis-kapcsolat tesztelése közben.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
