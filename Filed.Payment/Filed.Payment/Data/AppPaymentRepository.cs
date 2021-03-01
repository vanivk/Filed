using AutoMapper;
using Filed.Payment.API.Entities;
using Filed.Payment.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Data
{
	public class AppPaymentRepository : IAppPaymentRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		public AppPaymentRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async void Add(AppPayment model)
		{
			await _context.Payments.AddAsync(model);
			
		}
	}
}
