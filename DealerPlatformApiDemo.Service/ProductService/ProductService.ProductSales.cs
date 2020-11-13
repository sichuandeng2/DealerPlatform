using DealerPlatformApiDemo.Common;
using DealerPlatformApiDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealerPlatformApiDemo.Service.ProductService
{
    public partial class ProductService
    {
        private List<ProductSales> ListProductSaleAll()
        {
            List<ProductSales> list = MemoryHelper.GetMemory(AppConst.productSaleCacheKey) as List<ProductSales>;
            if (list == null)
            {
                list = _productSaleRepository.ListAll().ToList();
                MemoryHelper.SetMemory(AppConst.productPhotoCacheKey, list);
            }
            return list;
        }
        public List<ProductSales> ListProductSalesByProductNos(IEnumerable<string> productNos) {
            var productSales = ListProductSaleAll();
            //_productSaleRepository.ListByCustomWhereIn("ProductNo", productNos.ToArray());
            return productSales.FindAll(m => productNos.Contains(m.ProductNo));
        }
    }
}
