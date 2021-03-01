using Filed.Payment.API.Entities;
using Filed.Payment.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Services
{
	public class CheapPaymentGateway : ICheapPaymentGateway
	{
		
		private readonly IUnitOfWork _unitOfWork;

		public CheapPaymentGateway(IUnitOfWork unitOfWork)
		{
		
			_unitOfWork = unitOfWork;
		}
		public async Task<bool> ProcessPayment(AppPayment model)
		{
			_unitOfWork.appPaymentRepository.Add(model);
			var paymentAdded = await _unitOfWork.Complete();
			if (paymentAdded) { 
				var paymentStatus = new AppPaymentStatus
				{
					PaymentId = model.Id,
					Status = Enums.PaymentStatus.processed
				};
				_unitOfWork.appPaymentStatusRepository.Add(paymentStatus);
			}
			return await _unitOfWork.Complete();
		}
	}
}
