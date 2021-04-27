using System;
namespace Utils.SberbankAcquiring.Models
{
    public class RegisterDOWebhook
    {
        public Guid OrderId { get; set; }
        public long SessionId { get; set; }
    }
}
