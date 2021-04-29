using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Config;
using BusinessLogic.Interfaces;
using Domain.Enums;
using Domain.Models;
using Domain.ViewModels.Request;
using Domain.ViewModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.SberbankAcquiring;
using Utils.SberbankAcquiring.Models;
using Utils.SberbankAcquiring.Models.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TherapyAPI.Controllers
{
    [Route("api/payments")]
    public class PaymentsController : Controller
    {
        private IUserService UserService { get; set; }
        private IPaymentService PaymentService { get; set; }
        private IUserWalletService UserWalletService { get; set; }
        private ISessionService SessionService { get; set; }

        public PaymentsController([FromServices]
            IUserService userService,
            IPaymentService paymentService,
            IUserWalletService userWalletService,
            ISessionService sessionService)
        {
            UserService = userService;
            PaymentService = paymentService;
            UserWalletService = userWalletService;
            SessionService = sessionService;
        }

        private RedirectResult RedirectResult(string redirectPath)
        {
            return Redirect($"{AppSettings.ClientAppUrl}/{redirectPath}");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var user = UserService.Get(long.Parse(User.Identity.Name));

            if (user == null)
                return NotFound(new ResponseModel
                {
                    Success = false,
                    Message = "Пользователь не найден"
                });

            var wallet = UserWalletService.GetUserWallet(user);

            if (wallet == null)
                return NotFound(new ResponseModel
                {
                    Success = false,
                    Message = "Кошелек не найден"
                });

            var payment = new Payment
            {
                Wallet = wallet,
                Amount = request.Amount,
                Type = request.Type,
                Status = PaymentStatus.New,
                OrderID = Guid.NewGuid()
            };

            PaymentService.Create(payment);

            return Ok(new CreatePaymentResponse
            {
                OrderId = payment.OrderID
            });
        }

        [HttpPost("success")]
        [Authorize]
        public IActionResult SuccessPayment([FromBody] RegisterDOWebhook data)
        {
            var payment = PaymentService.GetPaymentByOrderID(data.OrderId);

            if (payment == null || payment.Status != PaymentStatus.New)
                return NotFound(new ResponseModel
                {
                    Success = false,
                    Message = "Платеж не найден или закрыт"
                });

            payment.Status = PaymentStatus.Completed;
            PaymentService.Update(payment);

            payment.Wallet.Balance += payment.Amount;
            UserWalletService.Update(payment.Wallet);
            if (data.SessionId != 0)
            {
                var session = SessionService.Get(data.SessionId);
                if (session == null)
                    return NotFound(new ResponseModel
                    {
                        Success = false,
                        Message = "Платеж выполнен, но указанная сессия не найдена"
                    });

                session.Date = DateTime.UtcNow;
                session.Status = SessionStatus.Started;

                SessionService.Update(session);
            }

            return Ok(new SuccessPaymentResponse
            {
                RedirectUrl = $"{AppSettings.ClientAppUrl}/profile?deposit=success"
            });

        }


        [HttpPost("fail/{orderId}")]
        [Authorize]
        public IActionResult FailPayment(Guid orderId)
        {
            var payment = PaymentService.GetPaymentByOrderID(orderId);

            payment.Status = PaymentStatus.Canceled;
            PaymentService.Update(payment);

            return BadRequest(new ResponseModel
            {
                Success = false,
                Message = "Платеж отменен."
            });

        }

        [HttpGet("failUrl")]
        public IActionResult FailUrl()
        {
            Console.WriteLine("failtestpay");
            return Ok();
        }
    }
}
