using Filed.Payment.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Services
{
	public class ExpensivePaymentGateway: CheapPaymentGateway, IExpensivePaymentGateway
	{
		public ExpensivePaymentGateway(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{

		}
	}
}
