﻿using BuberDinner.API.Common.Errors;
using BuberDinner.API.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationApi(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddMapping();
            services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();

            return services;
        }
    }
}
