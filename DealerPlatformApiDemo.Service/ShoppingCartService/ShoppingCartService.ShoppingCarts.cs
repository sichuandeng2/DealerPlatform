using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealerPlatformApiDemo.Common;
using DealerPlatformApiDemo.Common.RedisHelper;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.ShoppingCartService.Dto;

namespace DealerPlatformApiDemo.Service.ShoppingCartService {
    public partial class ShoppingCartService {
        /// <summary>
        /// ��ȡ���й��ﳵ�����뻺��
        /// </summary>
        /// <returns></returns>
        //private List<ShoppingCarts> ListShoppingCartAll () {
        //    //�ӻ����л�ȡ����
        //    List<ShoppingCarts> carts = MemoryHelper.GetMemory (AppConst.ShoppingCartCacheKey) as List<ShoppingCarts>;
        //    if (carts == null) {
        //        //��������л�û���������ȴ����ݿ��л�ȡ���ٴ��뻺��
        //        carts = _cartRepository.ListAll ().ToList ();
        //        MemoryHelper.SetMemory (AppConst.productCacheKey, carts);
        //    }
        //    return carts;
        //}
        /// <summary>
        /// 根据用户编号获取购物车
        /// </summary>
        /// <param name="cNo"></param>
        /// <returns></returns>
        private List<ShoppingCarts> ListShoppingCartByCustomerNo (string cNo) {
            //return _cartRepository.ListByCustom ("CustomerNo", cNo).ToList ();
            return RedisHelper.GetHashMemory<ShoppingCarts> ($"carts:{cNo}:*");
        }
        public List<ShoppingCartDto> ListShoppingCartDtoByCustomerNo (string cNo) {
            var carts = ListShoppingCartByCustomerNo (cNo);
            var dtos = _mapper.Map<List<ShoppingCartDto>> (carts);
            var sales = _productService.ListProductSalesByProductNos (dtos.Select (m => m.ProductNo));
            var photos = _productService.ListProductPhotosByProductNos (dtos.Select (m => m.ProductNo));
            var products = _productService.ListProductByNos (dtos.Select (m => m.ProductNo));
            dtos.ForEach (m => {
                var url = photos.FirstOrDefault (p => p.ProductNo == m.ProductNo)?.ProductPhotoUrl;
                m.ProductPhotoUrl = string.IsNullOrEmpty (url) ? "/img/productPhotos/1.jpg" : url;
                m.SalePrice = sales.FirstOrDefault (p => p.ProductNo == m.ProductNo)?.SalePrice??0;
                m.TypeName = products.FirstOrDefault (p => p.ProductNo == m.ProductNo)?.TypeName;
                m.ProductName = products.FirstOrDefault (p => p.ProductNo == m.ProductNo)?.ProductName;
            });
            return dtos;
        }
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="shoppingCartInput"></param>
        /// <returns></returns>
        public (int, string) AddCarts (ShoppingCartInputDto shoppingCartInput) {
            int rowCount = 1;
            string addType = null;
            try {
                //var carts = ListShoppingCartByCustomerNo (shoppingCartInput.CustomerNo);
                //var currCart = carts.Find (m => m.ProductNo == shoppingCartInput.ProductNo);
                var carts = RedisHelper.GetHashMemory<ShoppingCarts> ($"carts:{shoppingCartInput.CustomerNo}:*");
                var currCart = carts.Find (m => m.ProductNo == shoppingCartInput.ProductNo);
                //如果存在购物车则说明是添加数量 ，如果不存在 则添加新的购物车信息 
                if (currCart != null) {
                    currCart.ProductNum += shoppingCartInput.BuyNum;
                    //rowCount = _cartRepository.Update (currCart);
                    addType = "u"; //"u"代表update
                } else {
                    currCart = new ShoppingCarts {
                        CartGuid = Guid.NewGuid ().ToString (),
                        CustomerNo = shoppingCartInput.CustomerNo,
                        ProductNo = shoppingCartInput.ProductNo,
                        ProductNum = shoppingCartInput.BuyNum,
                        CartSelected = false
                    };
                    //rowCount = _cartRepository.Insert (currCart);
                    addType = "i"; //"i"代表insert
                }
                RedisHelper.SetHashMemory ($"carts:{currCart.CustomerNo}:{currCart.CartGuid}", currCart);
                return (rowCount, addType);
            } catch (System.Exception) {
                return (0, addType);
                throw;
            }

        }
        /// <summary>
        /// 设置购物车物品选中状态
        /// </summary>
        /// <param name="cartGuids"></param>
        /// <param name="isSelected"></param>
        /// <returns></returns>
        public async Task<bool> SetCartCheck (List<string> cartGuids, bool isSelected) {
            bool isSuccess = await Task.Run (() => {
                try {
                    foreach (var cartGuid in cartGuids) {
                        /*
                         **从数据库中修改
                         */
                        // var cart = _cartRepository.ListByCustom ("CartGuid", cartGuid).FirstOrDefault ();
                        // cart.CartSelected = isSelected;
                        // _cartRepository.Update (cart);
                        /*
                         **从Redis中修改
                         */
                        var cart = RedisHelper.GetHashMemory<ShoppingCarts> ($"*:{cartGuid}").FirstOrDefault ();
                        cart.CartSelected = isSelected;
                        RedisHelper.SetHashMemory ($"carts:{cart.CustomerNo}:{cart.CartGuid}", cart);
                    }
                    return true;
                } catch (System.Exception) {
                    return false;
                }
            });
            return isSuccess;
        }
        /// <summary>
        /// 设置购物车物品数量
        /// </summary>
        /// <param name="cartGuid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public async Task<bool> SetCartNum (string cartGuid, int num) {
            var isSuccess = await Task.Run (() => {
                /*
                 **从数据库中修改
                 */
                // var cart = _cartRepository.ListByCustom ("CartGuid", cartGuid).FirstOrDefault ();
                // cart.ProductNum = num;
                // var rowNum = _cartRepository.Update (cart);
                // return rowNum > 0;
                /*
                 **从Redis中修改
                 */
                try {
                    var cart = RedisHelper.GetHashMemory<ShoppingCarts> ($"*:{cartGuid}").FirstOrDefault ();
                    cart.ProductNum = num;
                    RedisHelper.SetHashMemory ($"carts:{cart.CustomerNo}:{cart.CartGuid}", cart);
                    return true;
                } catch (System.Exception) {
                    return false;
                    throw;
                }

            });
            return isSuccess;
        }
        /// <summary>
        /// 移除购物车
        /// </summary>
        /// <param name="cartGuids"></param>
        public void RemoveCart (params string[] cartGuids) {
            for (int i = 0; i < cartGuids.Length; i++) {
                var cartGuid = cartGuids[i];
                var cart = _cartRepository.DeleteByCustomer ("CartGuid", cartGuid);
            }
        }

        public int GetCartNum(string customerNo){
            return RedisHelper.GetKeys($"carts:{customerNo}:*").Count();
        }
    }
}