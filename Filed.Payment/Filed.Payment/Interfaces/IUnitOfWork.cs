using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Interfaces
{
	public interface IUnitOfWork
	{
		IAppPaymentRepository appPaymentRepository { get; }
		IAppPaymentStatusRepository appPaymentStatusRepository { get; }

		Task<bool> Complete();

		bool HasChanges();
	}
}
