using System;
using Core.Interfaces;
using Infrastructure.Data;
using AutoMapper;

namespace API.Extensions
{
	public static class ApplicationServiceExtension
	{
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                IConfiguration config)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(System.AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

    }
}

