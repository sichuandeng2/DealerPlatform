using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.ProductService.Dto;
using System.Collections.Generic;

namespace DealerPlatformApiDemo.Service.ProductService
{
    public interface IProduct
    {
        List<Products> ListProductAll() ;
        List<ProductDto> ListProductByPage(ProductPageAndFilterDto ppfd);
        List<Products> ListProductByNos(IEnumerable<string> pNos);
        List<string> ListTypeName(string bType);
        Dictionary<string, List<string>> ListProductTypes(string bType, string typeName);
        List<ProductSales> ListProductSalesByProductNos(IEnumerable<string> productNos);
        List<ProductPhotos> ListProductPhotosByProductNos(IEnumerable<string> productNos);
    }
}