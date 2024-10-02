using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TreeNodes.API.Mapping.Profiles;
using TreeNodes.API.Services;
using TreeNodes.Data.Context;
using TreeNodes.Data.Interfaces;

namespace TreeNodes.API.Extensions
{
    public static class ServicesConfigurationExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Db
            services.AddDbContext<TreeNodesContext>(options => options.
                UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Automapper
            services.AddAutoMapper(typeof(JournalProfile));
            services.AddAutoMapper(typeof(TreeNodeProfile));


            // Services
            services.AddTransient<IJournalRepository, JournalRepository>();
            services.AddTransient<ITreeRepository, TreeRepository>();

            // Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);

            return services;
        }

        public async static Task EnsureDbCreated(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<TreeNodesContext>())
                await context.Database.EnsureCreatedAsync();
        }
    }
}
