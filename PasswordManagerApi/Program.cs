using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PasswordManagerApi.Services;
using PasswordManagerApi.Entities;
using PasswordManagerApi.Repository.Implementation;
using PasswordManagerApi.Repository.Interfaces;
using PasswordManagerApi.BL.Interfaces;
using PasswordManagerApi.BL.Implementation;

internal class Program
{
    private static void Main(string[] args)
    {
        // TO DO
        // Переменные окружения
        var builder = WebApplication.CreateBuilder(args);

        var environment = builder.Environment.EnvironmentName;

        string connection = builder.Configuration.GetConnectionString("SqliteConnecion");
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));

        builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
        builder.Services.AddTransient<IPasswordBL, PasswordBL>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IServiceBL, ServiceBL>();
        builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
        builder.Services.AddTransient<IManagmentBL, ManagmentBL>();
        builder.Services.AddSingleton<IEncryptionService, EncryptionService>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(setup => {
            setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Password manager API", Version = "v1" });
        });

        if (environment == "Production")
        {
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(80);
                serverOptions.ListenAnyIP(443, listenOptions =>
                {
                    var certPath = builder.Configuration["Kestrel:Certificates:Default:Path"];
                    var certPassword = builder.Configuration["Kestrel:Certificates:Default:Password"];
                    listenOptions.UseHttps(certPath, certPassword);
                });
            });
        }

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
