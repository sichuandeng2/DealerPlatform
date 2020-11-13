using System;
using System.Collections.Generic;
using DealerPlatformApiDemo.Entity;

namespace DealerPlatformApiDemo.Service.OrderService.Dto {
    public class SaleOrderDto {
        public int Id { get; set; }
        public string SaleOrderNo { get; set; }
        public string CustomerNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InputDate { get; set; }
        public string StockNo { get; set; }
        public string EditUserNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Remark { get; set; }
        public List<SaleOrderDetails> OrderDetails { get; set; }
        public SaleOrderProgresses OrderProgresses { get; set; }
        public CustomerInvoices CustomerInvoices { get; set; }
    }
}