using System;
using System.Collections.Generic;

namespace DealerPlatformApiDemo.Service.ShoppingCartService.Dto {
    public class ShoppingCartInputDto {
        public List<string> CartGuids { get; set; }
        public string CustomerNo { get; set; }
        public string ProductNo { get; set; }
        public int BuyNum { get; set; }
        public bool? CartCheck { get; set; }

    }
}