using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealerPlatformApiDemo.Common;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.CustomerService.Dto;

namespace DealerPlatformApiDemo.Service.CustomerService
{
    public partial class CustomerService: ICustomer
    {
        public CustomerPwdDto CheckLogin(UserCheckInput userCheckInput)
        {
            var customerName = userCheckInput.CustomerName;
            var password = userCheckInput.Pwd;
            var customer = GetCustomerByName(customerName);
            Dictionary<string,string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("CustomerNo", customer.CustomerNo);
            keyValuePairs.Add("CustomerPwd", password.ToMd5());
            var customerPwd = CustomerPwdRepository.ListByCustoms(keyValuePairs).FirstOrDefault();
            return CustomerPwdDto.ToDto(customerPwd);
        }
    }
}
