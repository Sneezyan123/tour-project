using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using TourProject.Infrastructure.Config;
using TourProject.Infrastructure.Funcs;
using TourProject.Persistence;
using TourProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
var connectionString = builder.Configuration.GetConnectionString("DbConnect");
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
var jwtOptions = new JwtOptions();
var jwtOptionsConfig = builder.Configuration.GetSection(nameof(JwtOptions));
jwtOptionsConfig.Bind(jwtOptions);
builder.Services.Configure<JwtOptions>(jwtOptionsConfig);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer=false,
            ValidateAudience=false,
            ValidateIssuerSigningKey=true,
            ValidateLifetime=true,
            IssuerSigningKey= new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TourService>();

builder.Services.AddScoped<JwtProvider>();
builder.Services.AddScoped<ApiUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
