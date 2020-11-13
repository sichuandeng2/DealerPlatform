using DealerPlatformApiDemo.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerPlatformApiDemo.Service.CustomerService.Dto
{
    public class UserCheckInput
    {
        public string CustomerName { get; set; }
        public string Pwd { get; set; }
    }
}
