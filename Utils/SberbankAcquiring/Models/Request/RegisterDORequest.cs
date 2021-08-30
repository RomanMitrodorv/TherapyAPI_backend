using BusinessLogic.Config;
using System;
namespace Utils.SberbankAcquiring.Models.Request
{
    public class RegisterDORequest
    {
        public string Name { get; set; }
        public string CardCryptogramPacket { get; set; } 
        public string OrderNumber { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; } = "RUB";
        public string InvoiceId { get; set; }
        public string AccountId { get; set; }

    }
}
