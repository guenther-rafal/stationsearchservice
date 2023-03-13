using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using StationSearchService.StationSearch.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddValidatorsFromAssemblyContaining<SearchRequestValidator>();
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        var applicationAssembly = Assembly.Load("StationSearchService.Application");
        var mediatrConfig = MediatRConfigurationBuilder
            // ReSharper disable once HeapView.ObjectAllocation
            .Create(applicationAssembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build();
        containerBuilder.RegisterMediatR(mediatrConfig);
        containerBuilder.RegisterAssemblyModules(applicationAssembly);
    });

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