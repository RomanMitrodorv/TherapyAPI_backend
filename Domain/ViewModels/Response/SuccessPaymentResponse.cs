using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.Response
{
    public class SuccessPaymentResponse : ResponseModel
    {
        public string RedirectUrl { get; set; }
    }
}
