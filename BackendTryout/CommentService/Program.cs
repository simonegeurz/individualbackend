using Microsoft.EntityFrameworkCore;
using CommentService.Data.Repositories.Implementations;
using CommentService.Data.Repositories.Interfaces;
using CommentService.Data.Repositories;


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

    services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["ConnectionStrings:UserConn"],
            sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
    }, ServiceLifetime.Singleton);
    Console.WriteLine(builder.Configuration["ConnectionStrings:UserConn"]);
    services.AddAuthentication();
    services.AddCors(options =>
        options.AddPolicy("CorsPolicy", b =>
            b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

    services.AddSingleton<ICommentRepo, CommentRepo>();

}

