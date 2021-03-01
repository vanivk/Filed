using AutoMapper;
using Filed.Payment.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public UnitOfWork(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public IAppPaymentRepository appPaymentRepository => new AppPaymentRepository(_context, _mapper);

		public IAppPaymentStatusRepository appPaymentStatusRepository => new AppPaymentStatusRepository(_context, _mapper);

		public async Task<bool> Complete()
		{
			return await _context.SaveChangesAsync() > 0;
		}

		public bool HasChanges()
		{
			return _context.ChangeTracker.HasChanges();
		}
	}
}
