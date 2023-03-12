﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.CompanyAPI.Abstractions;
using ProjectManagement.CompanyAPI.Configuration;
using ProjectManagement.CompanyAPI.Data;
using ProjectManagement.CompanyAPI.Services;
using Winton.Extensions.Configuration.Consul;

namespace ProjectManagement.CompanyAPI.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddConsulKV(this IConfigurationBuilder builder, ConsulKVSettings settings)
    {
        builder.AddConsul(settings.Key, options =>
        {
            options.ConsulConfigurationOptions = config =>
            {
                config.Address = new Uri(settings.Url);
                config.Token = settings.Token;
            };

            options.Optional = false;
            options.ReloadOnChange = true;
        });
    }

    public static void AddServices(this IServiceCollection services, ApplicationSettings settings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddDbContext<ApplicationDbContext>(options => { options.UseNpgsql(settings.ConnectionString); });
    }
}