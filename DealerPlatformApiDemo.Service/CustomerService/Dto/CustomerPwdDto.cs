using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerPlatformApiDemo.Service.CustomerService.Dto
{
    public class CustomerPwdDto : EntityDto
    {
        public string CustomerNo { get; set; }
        public string CustomerPwd { get; set; }
        public static CustomerPwdDto ToDto(CustomerPwds customerPwds)
        {
            return new CustomerPwdDto
            {
                Id = customerPwds.Id,
                CustomerNo = customerPwds.CustomerNo,
                CustomerPwd = customerPwds.CustomerPwd
            };
        }
    }

}
