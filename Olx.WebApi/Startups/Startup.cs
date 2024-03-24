using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.IRepositories;
using Olx.DataAccess.Repositories;
using Olx.Domain.Configurations;
using Olx.Domain.Entities;
using Olx.Service.Interfaces;
using Olx.Service.Services;

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

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(Constants.CONNECTION_STRING, x => x.MigrationsAssembly("Olx.DataAccess"));

            AppDbContext appDbContext = new AppDbContext(optionsBuilder.Options);

            appDbContext.Database.Migrate();

            services.AddDbContext<AppDbContext>();

            // Register repositories
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Post>, Repository<Post>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Property>, Repository<Property>>();
            services.AddScoped<IRepository<PropertyValue>, Repository<PropertyValue>>();
            services.AddScoped<IRepository<PostProperty>, Repository<PostProperty>>();
            services.AddScoped<IRepository<Message>, Repository<Message>>();
            services.AddScoped<IRepository<FavouritePost>, Repository<FavouritePost>>();
            services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();

            // Register services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPropertyValueService, PropertyValueService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostPropertyService, PostPropertyService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IFavouritePostService, FavouritePostService>();
            services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();

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