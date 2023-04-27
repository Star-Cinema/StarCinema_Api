//using StarCinema_Api.Data;
//using StarCinema_Api.Profiles;
//using StarCinema_Api.Repositories;
//using StarCinema_Api.Services;
//using StarCinema_Api.Data.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()));

//var connectionString = builder
//    .Configuration.GetConnectionString("MyDB");
//services.AddDbContext<MyDbContext>
//(option =>
//{
//    option.UseSqlServer(connectionString);
//}, ServiceLifetime.Transient);


// Add scoped repository

// Add scoped services


//services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]))
        };
    });
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger Star Cinema",
        Version = "v1",
        Description = "An ASP.NET Core Web API for project Star Cinema",
        Contact = new OpenApiContact
        {
            Name = "Nguyen Trong Anh",
            Url = new Uri("https://www.facebook.com/anhnguyen53")
        },
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Authorization header using the Bearer scheme. 
                Enter 'Bearer' [space] and then your token in the text input below.
                Example: 'Bearer 12345abcdef",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
                {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
                },
                In = ParameterLocation.Header,
            },
            new List<string>()
            }
        });
});

var app = builder.Build();

// Add seed data

//using var scope = app.Services.CreateScope();
//var servicesProvider = scope.ServiceProvider;
//try
//{
//    var context = servicesProvider.GetRequiredService<MyDbContext>();
//    context.Database.Migrate();
//    Seed.SeedUsers(context);
//}
//catch (Exception e)
//{
//    var logger = servicesProvider.GetRequiredService<ILogger<Program>>();
//    logger.LogError(e, "Migration failed");
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
