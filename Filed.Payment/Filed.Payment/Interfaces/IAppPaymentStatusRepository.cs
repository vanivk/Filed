using Filed.Payment.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Interfaces
{
	public interface IAppPaymentStatusRepository
	{
		Task<AppPaymentStatus> GetPaymentStatusById(int id);
		void Add(AppPaymentStatus model);
	}
}
