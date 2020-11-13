using System;

namespace DealerPlatformApiDemo.Service.OrderService.Dto
{
    public class OrderMasterInputDto
    {
        public string Remark  { get; set; }
        public DateTime DeliveryDate { get; set;}
        public string InvoiceNo { get; set; }
    }
}