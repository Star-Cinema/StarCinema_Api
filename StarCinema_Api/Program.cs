using StarCinema_Api.Data;
using StarCinema_Api.Data.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using StarCinema_Api.Repositories.UserRepository;
using StarCinema_Api.Services.UserService;
using StarCinema_Api.Services.AuthService;
using StarCinema_Api.Services.TokenService;
using StarCinema_Api.Services;
using StarCinema_Api.Repositories;
using StarCinema_Api.Repositories.ScheduleRepository;
using StarCinema_Api.Profiles;
using StarCinema_Api.Repositories.BookingRepository;
using StarCinema_Api.Services.BookingService;
using StarCinema_Api.Repositories.BookingDetailRepository;
using StarCinema_Api.Repositories.TicketsRepository;
using StarCinema_Api.Repositories.FilmsRepository;
using StarCinema_Api.Services.FilmsService;
using StarCinema_Api.Repositories.CategoriesRepository;
using StarCinema_Api.Services.CategoriesService;
using StarCinema_Api.Repositories.RoomRepository;
using StarCinema_Api.Repositories.ServiceRepository;
using StarCinema_Api.Services.VnPayService;
using StarCinema_Api.Repositories.PaymentRepository;
using StarCinema_Api.Services.PaymentService;
using StarCinema_Api.Services.EmailService;

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
            .WithOrigins("http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod()));

var connectionString = builder
    .Configuration.GetConnectionString("MyDB");
services.AddDbContext<MyDbContext>
(option =>
{
    option.UseSqlServer(connectionString);
});



// Add scoped repository
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<ISchedulesRepository, SchedulesRepository>();
services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
services.AddScoped<IBookingRepository, BookingRepository>();
services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();

services.AddScoped<ITicketsRepository, TicketsRespository>();
services.AddScoped<IFilmsRepository, FilmsRepository>();
services.AddScoped<ICategoriesRepository, CategoriesRepository>();
services.AddScoped<IPaymentRepository, PaymentRepository>();

// Add scoped services
services.AddScoped<IUserService, UserService>();
services.AddScoped<ITokenService, TokenService>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<ISchedulesService, SchedulesService>();
services.AddScoped<IFilmsService, FilmsService>();
services.AddScoped<ICategoriesService, CategoriesService>();
services.AddScoped<IBookingService, BookingService>();
services.AddScoped<IServiceRepository, ServiceRepository>();
services.AddScoped<IRoomRepository, RoomRepository>();
services.AddScoped<IVnPayService, VnPayService>();
services.AddScoped<IPaymentService, PaymentService>();
services.AddScoped<IEmailService , EmailService>();
services.AddAutoMapper(typeof(MapperProfile).Assembly);
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
builder.Services.AddEndpointsApiExplorer();
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

using var scope = app.Services.CreateScope();
var servicesProvider = scope.ServiceProvider;
try
{
    var context = servicesProvider.GetRequiredService<MyDbContext>();
    context.Database.Migrate();
    Seed.SeedUsers(context);
}
catch (Exception e)
{
    var logger = servicesProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "Migration failed");
}

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
