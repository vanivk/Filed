﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.DTOs
{
	public class PaymentDto
	{
		public string CreditCardNumber { get; set; }

		public string CardHolder { get; set; }

		public string ExpirationDate { get; set; }

		public string SecurityCode { get; set; }

		public decimal Amount { get; set; }
	}
}
