using System;
using Core.Interfaces;
using Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
	public static class ApplicationServiceExtension
	{
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
                IConfiguration config)
        {
            services.AddDbContext<StoreContext>(x =>
               x.UseSqlite(config.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(System.AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

    }
}

