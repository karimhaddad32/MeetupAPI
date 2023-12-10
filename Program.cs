using MeetupAPI.Entities;
using MeetupAPI.Profilers;
using MeetupAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

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
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "MeetupAPI", Version = "1" });         
            });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiler));

            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            builder.Services.AddDbContext<MeetupContext>( options => {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("MeetupConnectionString"));
                    options.EnableSensitiveDataLogging();
                }
            );

            builder.Services.AddScoped<IMeetupRepository, MeetupRepository>();
        }

        private static void SetupPipelineConfiguration(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}