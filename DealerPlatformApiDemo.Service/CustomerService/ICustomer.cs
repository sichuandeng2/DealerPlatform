using System;
using System.Collections.Generic;
using System.Text;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.CustomerService.Dto;

namespace DealerPlatformApiDemo.Service.CustomerService
{
    public interface ICustomer
    {
        IEnumerable<Customers> ListCustomerAll();

        Customers GetCustomerByName(string name);

        Customers GetCustomerByNo(string no);
        CustomerPwdDto CheckLogin(UserCheckInput userCheckInput);
        IEnumerable<CustomerInvoices> GetCustomerInvoicesByCustomerNo(string customerNo);
    }
}
