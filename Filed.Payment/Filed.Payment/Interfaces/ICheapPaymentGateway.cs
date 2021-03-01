using Filed.Payment.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Interfaces
{
	public interface ICheapPaymentGateway
	{
		Task<bool> ProcessPayment(AppPayment model);
	}
}
