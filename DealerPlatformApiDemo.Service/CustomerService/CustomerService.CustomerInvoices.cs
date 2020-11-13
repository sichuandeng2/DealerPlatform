using System;
using System.Collections.Generic;
using System.Text;
using DealerPlatformApiDemo.Entity;

namespace DealerPlatformApiDemo.Service.CustomerService
{
    public partial class CustomerService: ICustomer
    {
        public IEnumerable<CustomerInvoices> GetCustomerInvoicesByCustomerNo(string customerNo)
        {
            var ci = CustomerInvoiceRepository.ListByCustom("CustomerNo", customerNo);
            return ci;
        }
    }
}
