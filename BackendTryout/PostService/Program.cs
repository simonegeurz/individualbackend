using Microsoft.EntityFrameworkCore;
using PostService.Data.Repositories.Implementations;
using PostService.Data.Repositories.Interfaces;
using PostService.Data.Repositories;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    );
app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddCors();

    services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["ConnectionStrings:UserConn"],
            sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
    }, ServiceLifetime.Singleton);
    Console.WriteLine(builder.Configuration["ConnectionStrings:UserConn"]);
    services.AddOptions();
    // Add our Config object so it can be injected
    services.Configure<RabbitMq>(builder.Configuration.GetSection("RabbitMq"));
    services.AddAuthentication();
    services.AddCors(options =>
        options.AddPolicy("CorsPolicy", b =>
            b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

    services.AddSingleton<IPostRepo, PostRepo>();


}