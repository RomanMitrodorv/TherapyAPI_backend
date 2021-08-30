using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.Request
{
    public class CheckRequest
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime DateTime { get; set; }
        public string CardFirstSix { get; set; }
        public string CardLastFour { get; set; }
        public string CardType { get; set; }
        public string CardExpDate { get; set; }
        public bool TestMode { get; set; }
        public string Status { get; set; }
        public string OperationType { get; set; }
    }
}
