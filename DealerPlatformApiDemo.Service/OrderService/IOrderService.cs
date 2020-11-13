using System.Collections.Generic;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.OrderService.Dto;
using DealerPlatformApiDemo.Service.ShoppingCartService.Dto;

namespace DealerPlatformApiDemo.Service.OrderService {
    public interface IOrderService {
        string AddOrder (string custormerNo,
            OrderMasterInputDto inputDto,
            List<ShoppingCartDto> carts,
            List<ProductSales> productSales,
            List<ProductPhotos> productPhotos,
            List<Products> products);
        IEnumerable<SaleOrderProgresses> GetProgressByOrderNos (params string[] orderNo);

        int SetOrderProgress (SaleOrderProgresses progress);
        SaleOrderDto GetOrderInfoByNo (string saleOrderNo);
        int GetUnFinishedOrdersNum(string custormerNo);
        IEnumerable<SaleOrderDto> GetOrderInfoByCustomerNo(string customerNo);
        IEnumerable<SaleOrderProgresses> GetProgressByStepSn(string StepSn);
        List<SaleOrderDto> ListOrderInfoByStepSn(string StepSn);
        bool NexTStep(string orderNo);
    }
}