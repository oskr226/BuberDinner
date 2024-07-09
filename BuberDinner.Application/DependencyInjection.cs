//using BuberDinner.Application.Services.Authentication.Commands;
//using BuberDinner.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace BuberDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        //Con la implementación de MediatR ya no se usará más la carpeta Services 
        //services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        //services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();

        return services;
    }
}
