using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.DataAccess.Repositories;
using Olx.Domain.Entities;

namespace Olx.WebApi.Startups
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configure DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Olx.DataAccess")));

            // Register repositories
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Post>, Repository<Post>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Property>, Repository<Property>>();
            services.AddScoped<IRepository<PropertyValue>, Repository<PropertyValue>>();
            services.AddScoped<IRepository<PostProperty>, Repository<PostProperty>>();
            services.AddScoped<IRepository<Message>, Repository<Message>>();
            services.AddScoped<IRepository<FavouritePost>, Repository<FavouritePost>>();

            // Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}