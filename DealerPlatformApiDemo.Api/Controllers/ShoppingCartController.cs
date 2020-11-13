using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DealerPlatformApiDemo.Api.Filters;
using DealerPlatformApiDemo.Api.Models;
using DealerPlatformApiDemo.Service.ShoppingCartService;
using DealerPlatformApiDemo.Service.ShoppingCartService.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealerPlatformApiDemo.Api.Controllers {
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShoppingCartController : BaseController {
        private readonly IShoppingCart _cartService;
        public ShoppingCartController (IShoppingCart cartService) {
            this._cartService = cartService;
        }

        [HttpPost]
        [CtmAuthorizationFilter]
        public string AddCart (ShoppingCartInputDto shoppingCartInput) {
            shoppingCartInput.CustomerNo = UserNo;
            var addType = _cartService.AddCarts (shoppingCartInput);
            if (addType.Item1 > 0) return addType.Item2;
            else {
                HttpContext.Response.StatusCode = 204;
                return "";
            }
        }

        [HttpGet]
        [CtmAuthorizationFilter]
        public Dictionary<string, object> GetCarts () {
            //获取购物车数据
            var dtos = _cartService.ListShoppingCartDtoByCustomerNo (UserNo);
            //创建typeName仅供上端使用的视图模型 
            List<TypeNameViewModel> typeNames = new List<TypeNameViewModel> ();
            //给typeName赋值
            var types = dtos.Select (m => m.TypeName).Distinct ().Where (m => !string.IsNullOrEmpty (m)).ToList ();
            types.ForEach (t => {
                typeNames.Add (new TypeNameViewModel {
                    Name = t,
                        IsChecked = false
                });
            });
            Dictionary<string, object> dic = new Dictionary<string, object> ();
            dic.Add ("carts", dtos);
            dic.Add ("typeNames", typeNames);
            return dic;
        }

        [HttpPut]
        public async Task<bool> SetCart (ShoppingCartInputDto input) {
            if (input.CartCheck != null && input.BuyNum == 0)
                return await SetCartChecked (input);
            else
                return await SetCartNum (input);
        }
        private async Task<bool> SetCartChecked (ShoppingCartInputDto input) {
            return await _cartService.SetCartCheck (input.CartGuids, input.CartCheck??false);
        }
        private async Task<bool> SetCartNum (ShoppingCartInputDto input) {
            return await _cartService.SetCartNum (input.CartGuids.FirstOrDefault (), input.BuyNum);
        }
        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="cartGuid"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool RemoveCart (string cartGuid) {
            try {
                _cartService.RemoveCart (cartGuid);
                return true;
            } catch (System.Exception) {
                return false;
            }
        }
        [HttpGet("/num")]
        [CtmAuthorizationFilter]
        public int GetCartNum(){
           return _cartService.GetCartNum(UserNo);
        }
    }
}