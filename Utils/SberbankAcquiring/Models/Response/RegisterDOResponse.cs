using System;
namespace Utils.SberbankAcquiring.Models.Response
{

    public class RegisterDOResponse
    {
        public Model Model { get; set; }
        public bool Success { get; set; }
    }

    public class Model
    {
        public int ReasonCode { get; set; }
        public string PublicId { get; set; }
        public string TerminalUrl { get; set; }
        public int TransactionId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public int CurrencyCode { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentCurrency { get; set; }
        public int PaymentCurrencyCode { get; set; }
        public string AccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedDateIso { get; set; }
        public bool TestMode { get; set; }
        public string IpAddress { get; set; }
        public string IpCountry { get; set; }
        public string IpCity { get; set; }
        public string IpDistrict { get; set; }
        public double IpLatitude { get; set; }
        public double IpLongitude { get; set; }
        public string CardFirstSix { get; set; }
        public string CardLastFour { get; set; }
        public string CardExpDate { get; set; }
        public string CardType { get; set; }
        public string CardProduct { get; set; }
        public string CardCategory { get; set; }
        public string IssuerBankCountry { get; set; }
        public string Issuer { get; set; }
        public int CardTypeCode { get; set; }
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string CultureName { get; set; }
        public string Reason { get; set; }
        public string CardHolderMessage { get; set; }
        public int Type { get; set; }
        public bool Refunded { get; set; }
        public string Name { get; set; }
        public string GatewayName { get; set; }
        public bool ApplePay { get; set; }
        public bool AndroidPay { get; set; }
        public string WalletType { get; set; }
        public int TotalFee { get; set; }
    }

}
