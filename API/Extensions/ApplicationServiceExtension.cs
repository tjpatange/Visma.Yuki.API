using System;
using Core.Interfaces;
using Infrastructure.Data;

namespace API.Extensions
{
	public static class ApplicationServiceExtension
	{
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                IConfiguration config)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}

