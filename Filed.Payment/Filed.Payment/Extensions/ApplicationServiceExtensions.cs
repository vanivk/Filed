using Filed.Payment.API.Data;
using Filed.Payment.API.Helpers;
using Filed.Payment.API.Interfaces;
using Filed.Payment.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlite(config.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();
			services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
			services.AddScoped<IPremiumPaymentService, PremiumPaymentService>();
			return services;
		}
	}
}
