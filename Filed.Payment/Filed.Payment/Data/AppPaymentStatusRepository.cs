using AutoMapper;
using Filed.Payment.API.Entities;
using Filed.Payment.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Data
{
	public class AppPaymentStatusRepository : IAppPaymentStatusRepository
	{

		private readonly DataContext _context;
		private readonly IMapper _mapper;
		public AppPaymentStatusRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async void Add(AppPaymentStatus model)
		{
			await _context.PaymentStatuses.AddAsync(model);
		}

		public async Task<AppPaymentStatus> GetPaymentStatusById(int id)
		{
			return await _context.PaymentStatuses.Include(x => x.Payment).FirstOrDefaultAsync(x => x.PaymentId == id);
		}
	}
}
