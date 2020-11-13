using System.Collections.Generic;
using System.Threading.Tasks;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.ShoppingCartService.Dto;

namespace DealerPlatformApiDemo.Service.ShoppingCartService {
    public interface IShoppingCart {
        List<ShoppingCartDto> ListShoppingCartDtoByCustomerNo (string cNo);
        (int, string) AddCarts (ShoppingCartInputDto shoppingCartInput);
        Task<bool> SetCartCheck (List<string> cartGuids, bool isSelected);
        Task<bool> SetCartNum (string cartGuid, int num);
        void RemoveCart (params string[] cartGuids);
        int GetCartNum(string customerNo);
    }
}