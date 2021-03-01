using Filed.Payment.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Entities
{
	public class AppPaymentStatus
	{
		public int Id { get; set; }
		public PaymentStatus Status { get; set; }
		public int PaymentId { get; set; }
		public AppPayment Payment { get; set; }
	}
}
