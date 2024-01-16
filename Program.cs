using FluentValidation;
using FluentValidation.AspNetCore;
using MeetupAPI.ActionFilters;
using MeetupAPI.Authorization;
using MeetupAPI.Controllers;
using MeetupAPI.DTOs;
using MeetupAPI.Entities;
using MeetupAPI.Identity;
using MeetupAPI.Profilers;
using MeetupAPI.Repositories;
using MeetupAPI.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Text;

namespace MeetupAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            AddServices(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            SetupPipelineConfiguration(app);
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


            SetupFluentValidationService(builder);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "MeetupAPI", Version = "1" });
            });

            SetupJwtAuthenticationService(builder);

            builder.Services.AddScoped<TimeTrackFilter>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiler));

            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            builder.Services.AddDbContext<MeetupContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("MeetupConnectionString"));
                    options.EnableSensitiveDataLogging();
                }
            );

            builder.Services.AddScoped<IMeetupRepository, MeetupRepository>();

            builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", 
                    builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

            });
        }

        private static void SetupFluentValidationService(WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserValidator>();
            builder.Services.AddScoped<IValidator<MeetupQuery>, MeetupQueryValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        }

        private static void SetupJwtAuthenticationService(WebApplicationBuilder builder)
        {
            JwtOptions jwtOptions = new();
            builder.Configuration.GetSection("jwt").Bind(jwtOptions);

            builder.Services.AddSingleton(jwtOptions);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtOptions.JwtIssuer,
                    ValidAudience = jwtOptions.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JwtKey)),
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("HasNationality", optionBuilder => optionBuilder.RequireClaim("Nationality", "German", "English"));
                options.AddPolicy("AtLeast18", optionBuilder => optionBuilder.AddRequirements(new MinimumAgeRequirement(18)));
            });


            builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, MeetupResourceOperationHandler>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        }

        private static void SetupPipelineConfiguration(WebApplication app)
        {
            app.UseResponseCaching();
            app.UseStaticFiles();
            app.UseCors("FrontEndClient");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}