using ProjectManagement.Company.Api.Configuration;
using ProjectManagement.Company.Api.Extensions;
using ProjectManagement.Company.Api.Mapping;
using Steeltoe.Discovery.Client;

namespace ProjectManagement.Company.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        // Settings for docker
        builder.Configuration.AddJsonFile("hostsettings.json", true);

        // Settings for consul kv
        ConsulKVSettings consulKvSettings = new ();
        builder.Configuration.GetRequiredSection("ConsulKV").Bind(consulKvSettings);
        builder.Configuration.AddConsulKV(consulKvSettings);

        ApplicationSettings applicationSettings = new ();
        builder.Configuration.GetRequiredSection("ApplicationSettings").Bind(applicationSettings);

        // Add services to the container.
        builder.Services.AddDiscoveryClient();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(CompanyProfile));
        
        builder.Services.AddServices(applicationSettings);

        WebApplication app = builder.Build();

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
    }
}