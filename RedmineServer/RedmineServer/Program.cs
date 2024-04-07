using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:5000", "https://localhost:5001") // Adjust the port to match your Blazor app's
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

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

app.UseCors("BlazorPolicy");

app.Run();
