using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.HttpOverrides;
using RedmineServer.WebSocket;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers(options => 
options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddWebSocketManager();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression();

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
    new MariaDbServerVersion(new Version(10, 4, 32)))
    );

// Add Token Authentication 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero  // for immediate token expiration handling
    };
}); 

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
    app.UseDeveloperExceptionPage(); // Use developer exception page to see detailed errors
}
else {  
    app.UseHttpsRedirection(); 
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting(); // Enable routing if using endpoints directly.
app.UseCors("BlazorPolicy");

// Configures the HTTP request pipeline to use authentication & authorization.
app.UseAuthentication();
app.UseAuthorization();

var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();//A WebSocketManager használatához szükséges
var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;//A WebSocketManager használatához szükséges
app.UseWebSockets();//Websocketek használatához szükséges
app.MapWebSocketManager("/ws", serviceProvider.GetService<HelloWorldHandler>());//A WebSocketManager-t használjuk a HelloWorldHandlerrel

app.MapControllers();

app.Run();