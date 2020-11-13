using System.Collections.Generic;
using System.Net;
using DealerPlatformApiDemo.Api.Filters;
using DealerPlatformApiDemo.Service.OrderService;
using DealerPlatformApiDemo.Service.OrderService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealerPlatformApiDemo.Api.Controllers {
    [Authorize (AuthenticationSchemes = "Bearer")]
    public class OrderInfoController : BaseController {
        private readonly IOrderService _orderService;
        public OrderInfoController (IOrderService orderService) {
            this._orderService = orderService;
        }

        [HttpGet ("{orderNo}")]
        public SaleOrderDto GetSale (string orderNo) {
            return _orderService.GetOrderInfoByNo (orderNo);
        }
        [HttpGet]
        [CtmAuthorizationFilter]
        public IEnumerable<SaleOrderDto> GetOrders () {
            return _orderService.GetOrderInfoByCustomerNo (UserNo);
        }

        [HttpGet ("unFinNum")]
        [CtmAuthorizationFilter]
        public int GetUnFinished () {
            return _orderService.GetUnFinishedOrdersNum (UserNo);
        }
    }
}