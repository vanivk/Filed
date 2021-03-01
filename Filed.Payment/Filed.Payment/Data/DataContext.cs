using Filed.Payment.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<AppPayment> Payments { get; set; }
		public DbSet<AppPaymentStatus> PaymentStatuses { get; set; }
	}
}
