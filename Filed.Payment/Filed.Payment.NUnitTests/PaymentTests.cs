using AutoMapper;
using Filed.Payment.API.Controllers;
using Filed.Payment.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Http.Results;
using System.Text.Json;
using Filed.Payment.API.Helpers;

namespace Filed.Payment.NUnitTests
{
	public class PaymentTests
	{
		private Mock<ICheapPaymentGateway> _cheapPayment;
		private Mock<IExpensivePaymentGateway> _expensivePayment;
		private Mock<IPremiumPaymentService> _premiumPayment;
		private PaymentController controller;
		private Mock<IUnitOfWork> _unitOfWorkMock;
		private Mock<ILogger<PaymentController>> _logger;
		private Mock<IMapper> _mapper;


		[SetUp]
		public void Setup()
		{
			_cheapPayment = new Mock<ICheapPaymentGateway>();
			_expensivePayment = new Mock<IExpensivePaymentGateway>();
			_premiumPayment = new Mock<IPremiumPaymentService>();
			_unitOfWorkMock = new Mock<IUnitOfWork>();
			_logger = new Mock<ILogger<PaymentController>>();
			_mapper = new Mock<IMapper>();

			var config = new MapperConfiguration(opts =>
			{
				opts.AddProfile<AutoMapperProfiles>();
			});


			var mapper = config.CreateMapper();
			controller = new PaymentController(_logger.Object, mapper, _cheapPayment.Object, _expensivePayment.Object, _premiumPayment.Object, _unitOfWorkMock.Object);
		}

		[Test]
		public void CheapPaymentTest()
		{
			var paymentDto = new API.DTOs.PaymentDto {
				Amount = 20,
				CardHolder = "Vani", 
				CreditCardNumber = "5522771104439587",
				ExpirationDate = "02/03/2025", 
				SecurityCode = "908" 
			};

			var result = controller.ProcessPayment(paymentDto);
			Assert.IsNotNull(result);
			
		}

		[Test]
		public void ExpensivePaymentTest()
		{
			var paymentDto = new API.DTOs.PaymentDto
			{
				Amount = 220,
				CardHolder = "Vani",
				CreditCardNumber = "5522771104439587",
				ExpirationDate = "02/03/2025",
				SecurityCode = "908"
			};

			var result = controller.ProcessPayment(paymentDto);
			Assert.IsNotNull(result);

		}

		[Test]
		public void PremiumPaymentTest()
		{
			var paymentDto = new API.DTOs.PaymentDto
			{
				Amount = 2230,
				CardHolder = "Vani",
				CreditCardNumber = "5522771104439587",
				ExpirationDate = "02/03/2025",
				SecurityCode = "908"
			};

			var result = controller.ProcessPayment(paymentDto);
			Assert.IsNotNull(result);

		}

		[Test]
		public void ValidDateTest()
		{
			DateTime date = DateTime.Now.AddDays(1);
			var result = API.CommonUtils.Utilities.IsDateInThePast(date);
			Assert.True(result);
		}

		[Test]
		public void InValidDateTest()
		{
			DateTime date = DateTime.Now.AddDays(-1);
			var result = API.CommonUtils.Utilities.IsDateInThePast(date);
			Assert.False(result);
		}

		[Test]
		public void ValidCCNTest()
		{
			string cardNumber = "5522771104439587";
			var result = API.CommonUtils.Utilities.IsCardNumberValid(cardNumber);
			Assert.True(result);
		}

		[Test]
		public void InvalidCCNTest()
		{
			string cardNumber = "8111111111111445";
			var result = API.CommonUtils.Utilities.IsCardNumberValid(cardNumber);
			Assert.False(result);
		}

		[Test]
		public void ValidDateFormatTest()
		{
			string date = "02/02/2024";
			var result = API.CommonUtils.Utilities.DateFormatValidator(date);
			Assert.True(result);
		}

		[Test]
		public void InValidDateFormatTest()
		{
			string date = "2/02/2024";
			var result = API.CommonUtils.Utilities.DateFormatValidator(date);
			Assert.False(result);
		}
	}
}
