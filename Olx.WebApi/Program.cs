using Microsoft.EntityFrameworkCore;
using Olx.DataAccess.Contexts;
using Olx.DataAccess.Repositories;
using Olx.Domain.Configurations;
using Olx.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

optionsBuilder.UseNpgsql(Constants.CONNECTION_STRING, x => x.MigrationsAssembly("Olx.DataAccess"));

AppDbContext appDbContext = new AppDbContext(optionsBuilder.Options);

appDbContext.Database.Migrate();

builder.Services.AddDbContext<AppDbContext>();
// Add the following line in the ConfigureServices method of your Startup class
builder.Services.AddScoped<IRepository<User>, Repository<User>>();

// Add the following line in the ConfigureServices method of your Startup class
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
