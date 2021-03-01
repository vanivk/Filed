using AutoMapper;
using Filed.Payment.API.DTOs;
using Filed.Payment.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<PaymentDto, AppPayment>();
		}
	}
}
