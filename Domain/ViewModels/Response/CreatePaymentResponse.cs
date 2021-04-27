using System;
namespace Domain.ViewModels.Response
{
    public class CreatePaymentResponse : ResponseModel
    {
        public Guid OrderId { get; set; }
    }
}
