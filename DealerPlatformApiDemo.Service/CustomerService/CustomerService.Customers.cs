using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealerPlatformApiDemo.Common;
using DealerPlatformApiDemo.Entity;

namespace DealerPlatformApiDemo.Service.CustomerService
{
    public partial class CustomerService: ICustomer
    {
        public IEnumerable<Customers> ListCustomerAll()
        {
            List<Customers> customers;
            if (MemoryHelper.GetMemory(AppConst.customerCacheKey) == null)
            {
                customers = CustomerRepository.ListAll().ToList();
                MemoryHelper.SetMemory(AppConst.customerCacheKey, customers, TimeSpan.FromHours(1));
            }
            else customers = MemoryHelper.GetMemory(AppConst.customerCacheKey) as List<Customers>;

            return customers;
        }

        public Customers GetCustomerByName(string name)
        {
            return ListCustomerAll().FirstOrDefault(m => m.CustomerName == name);
        }
        public Customers GetCustomerByNo(string no)
        {
            return ListCustomerAll().FirstOrDefault(m => m.CustomerNo == no);
        }
    }
}
