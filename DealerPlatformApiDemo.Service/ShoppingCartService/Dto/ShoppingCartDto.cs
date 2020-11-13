namespace DealerPlatformApiDemo.Service.ShoppingCartService.Dto {
    public class ShoppingCartDto {
        public string ProductName { get; set; }
        public System.String CartGuid {get;set;}
        public System.String CustomerNo{get;set;}
        public System.String ProductNo{get;set;}
        public System.Int32 ProductNum{get;set;}
        public System.Boolean CartSelected{get;set;}
        public string TypeName { get; set; }
        public double SalePrice { get; set; }
        public string ProductPhotoUrl { get; set; }
    }
}