using AutoMapper;
using Filed.Payment.API.CommonUtils;
using Filed.Payment.API.DTOs;
using Filed.Payment.API.Entities;
using Filed.Payment.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Filed.Payment.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly ILogger<PaymentController> _logger;
		private readonly ICheapPaymentGateway _cheapPaymentGateway;
		private readonly IExpensivePaymentGateway _expensivePaymentGateway;
		private readonly IPremiumPaymentService _premiumPaymentService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;


		public PaymentController(ILogger<PaymentController> logger,  IMapper mapper, 
			ICheapPaymentGateway cheapPaymentGateway, 
			IExpensivePaymentGateway expensivePaymentGateway,
			IPremiumPaymentService premiumPaymentService, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_cheapPaymentGateway = cheapPaymentGateway;
			_expensivePaymentGateway = expensivePaymentGateway;
			_premiumPaymentService = premiumPaymentService;
			_mapper = mapper;
            _unitOfWork = unitOfWork;
		}

		[HttpPost("ProcessPayment")]
		public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
		{
			//return await NoContent();
			paymentDto.CreditCardNumber = Regex.Replace(paymentDto.CreditCardNumber, @"[^\d]", "");
			var validator = new PaymentValidator();
			var validate = await validator.ValidateAsync(paymentDto);

			if (!validate.IsValid)
			{
				var errors = validator.GetErrorList(validate.Errors);
				return BadRequest(new { message = "Bad request", errors });
			}
			AppPayment payment;
            try
            {
                payment = _mapper.Map<AppPayment>(paymentDto);

			    if (paymentDto.Amount < 20)
                    await _cheapPaymentGateway.ProcessPayment(payment);
                else if (paymentDto.Amount >= 20 && paymentDto.Amount <=500)
                {
                    try
                    {
                        await _expensivePaymentGateway.ProcessPayment(payment);
                    }
                    catch (Exception)
                    {
                        await _cheapPaymentGateway.ProcessPayment(payment);
                    }
                }
                else
                {
                    try
                    {
                        await _premiumPaymentService.ProcessPayment(payment);
                    }
                    catch (Exception)
                    {
                        int i = 1;
                        do
                        {
                            var response = await _cheapPaymentGateway.ProcessPayment(payment);
                            if (response)
                                break;
                            i++;
                        }
                        while (i < 4);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, new { message = "Internal error", errors = "Data access processing error" });
            }

            try
            {
                var result = await _unitOfWork.appPaymentStatusRepository.GetPaymentStatusById(payment.Id);
                return Ok(new { message = "Success", data = result });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, new { message = "Internal error", errors = "Data access processing error" });
            }          


		}
	}
}
