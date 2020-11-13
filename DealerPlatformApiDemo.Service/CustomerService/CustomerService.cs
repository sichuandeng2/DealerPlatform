using System;
using System.Collections.Generic;
using System.Text;
using DealerPlatformApiDemo.Core.Data;
using DealerPlatformApiDemo.Entity;

namespace DealerPlatformApiDemo.Service.CustomerService
{
    public partial class CustomerService:ICustomer
    {
        public IRepository<Customers> CustomerRepository { get; }
        public IRepository<CustomerPwds> CustomerPwdRepository { get; }
        public IRepository<CustomerInvoices> CustomerInvoiceRepository { get; }

        public  CustomerService(
            IRepository<Customers> customerRepository,
            IRepository<CustomerPwds> customerPwdRepository,
            IRepository<CustomerInvoices> CustomerInvoiceRepository)
        {
            this.CustomerRepository = customerRepository;
            CustomerPwdRepository = customerPwdRepository;
            this.CustomerInvoiceRepository = CustomerInvoiceRepository;
        }
    }
}
