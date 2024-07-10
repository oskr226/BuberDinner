using BuberDinner.API;
using BuberDinner.API.Common.Errors;
using BuberDinner.API.Common.Mapping;
using BuberDinner.API.Filters;
using BuberDinner.API.Middleware;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    //builder.Services.AddControllers(option => option.Filters.Add<ErrorHandlingFilterAttribute>());
    //builder.Services.AddControllers(); -> LLevado al archivo DependencyInjection.cs
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddPresentationApi();
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructure(builder.Configuration);    
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseAuthorization();

    //app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}
