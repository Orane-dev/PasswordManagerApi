using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PasswordManagerApi.Repository;
using PasswordManagerApi.Services;
using PasswordManagerApi.Entities;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string connection = builder.Configuration.GetConnectionString("SqliteConnecion");
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
        builder.Services.AddScoped<PasswordRepository>();

        var encryptionSettings = builder.Configuration.GetSection("EncryptionSettings");
        builder.Services.AddSingleton<EncryptionService>(x =>
        {
            return new EncryptionService(encryptionSettings["Key"], encryptionSettings["IV"]);
        });
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(setup => {
            setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Df Server API", Version = "v1" });
        });

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "swagger";
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.UseAuthorization();

        app.Run();
    }
}
