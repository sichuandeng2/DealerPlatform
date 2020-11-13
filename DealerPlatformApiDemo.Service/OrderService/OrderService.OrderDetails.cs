using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using DealerPlatformApiDemo.Entity;

namespace DealerPlatformApiDemo.Service.OrderService {
    public partial class OrderService {
        private void AddOrderDetail (
            string custormerNo,
            List<ShoppingCarts> carts,
            List<ProductSales> productSales,
            List<ProductPhotos> productPhotos,
            List<Products> products,
            DateTime inputDate,
            string saleOrderNo) {
            foreach (var cart in carts) {
                SaleOrderDetails detail = new SaleOrderDetails () {
                    SaleOrderGuid = Guid.NewGuid ().ToString (),
                    SaleOrderNo = saleOrderNo,
                    ProductNo = cart.ProductNo,
                    ProductName = products.Find (m => m.ProductNo == cart.ProductNo)?.ProductName,
                    ProductPhotoUrl = productPhotos.Find (m => m.ProductNo == cart.ProductNo)?.ProductPhotoUrl,
                    CustomerNo = custormerNo,
                    InputDate = inputDate,
                    OrderNum = cart.ProductNum,
                    BasePrice = productSales.Find (m => m.ProductNo == cart.ProductNo)?.SalePrice??0,
                    DiffPrice = 0,
                    SalePrice = productSales.Find (m => m.ProductNo == cart.ProductNo)?.SalePrice??0,
                };
                _orderDetailRepository.Insert (detail);
            }
            _cartService.RemoveCart (carts.Select (m => m.CartGuid).ToArray ());
        }
        public List<SaleOrderDetails> GetOrderDetailByOrderNo (string saleOrderNo) {
            return _orderDetailRepository.ListByCustom("SaleOrderNo",saleOrderNo).ToList();
        }
    }
}