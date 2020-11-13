using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DealerPlatformApiDemo.Api.Filters;
using DealerPlatformApiDemo.Common.SocketHelper;
using DealerPlatformApiDemo.Service.CustomerService;
using DealerPlatformApiDemo.Service.OrderService;
using DealerPlatformApiDemo.Service.OrderService.Dto;
using DealerPlatformApiDemo.Service.ProductService;
using DealerPlatformApiDemo.Service.ShoppingCartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealerPlatformApiDemo.Api.Controllers {
    [Authorize (AuthenticationSchemes = "Bearer")]
      public class OrderConfirmController : BaseController {
        private readonly IProduct _productService;
        private readonly IShoppingCart _cartSerivce;
        private readonly ICustomer _custormerService;
        private readonly IOrderService _orderService;
        public OrderConfirmController (IProduct productService, IShoppingCart cartSerivce, ICustomer custormerService, IOrderService orderService) {
            this._orderService = orderService;
            this._custormerService = custormerService;
            this._cartSerivce = cartSerivce;
            this._productService = productService;
        }
        /// <summary>
        /// 获取所有带确认的订单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
          [CtmAuthorizationFilter]
        public Dictionary<string, object> Get () {
            var carts = _cartSerivce.ListShoppingCartDtoByCustomerNo (UserNo).FindAll (m => m.CartSelected);
            var invoices = _custormerService.GetCustomerInvoicesByCustomerNo (UserNo);
            Dictionary<string, object> dic = new Dictionary<string, object> ();
            dic.Add ("orders", carts);
            dic.Add ("invoices", invoices);
            return dic;
        }

        [HttpPost]
          [CtmAuthorizationFilter]
        public string AddOrder (OrderMasterInputDto input) {
            var carts = _cartSerivce.ListShoppingCartDtoByCustomerNo (UserNo).FindAll (m => m.CartSelected);
            var sales = _productService.ListProductSalesByProductNos (carts.Select (c => c.ProductNo));
            var photos = _productService.ListProductPhotosByProductNos (carts.Select (c => c.ProductNo));
            var product = _productService.ListProductAll ().FindAll (m => carts.Select (c => c.ProductNo).Contains (m.ProductNo));
            var orderNo = _orderService.AddOrder(UserNo,input,carts,sales,photos,product);
            SendSocket(orderNo);
            return "success|"+orderNo;
        }

        private void SendSocket(string orderNo){
          Socketer socketer = new Socketer();
          string msg = $"{orderNo}需要审批";
          List<byte> byteList = new List<byte>();
          byteList.Add(1);
          byteList.AddRange(msg.Serialize());
          socketer.SendData(byteList.ToArray(),"127.0.0.1",6566);
        }
    }
}