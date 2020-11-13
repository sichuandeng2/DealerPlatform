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
        private List<ProductPhotos> ListProductPhotoAll()
        {
            List<ProductPhotos> list = MemoryHelper.GetMemory(AppConst.productPhotoCacheKey) as List<ProductPhotos>;
            if (list == null)
            {
                list = _productPhotoRepository.ListAll().ToList();
                MemoryHelper.SetMemory(AppConst.productPhotoCacheKey, list);
            }
            return list;
        }
        public List<ProductPhotos> ListProductPhotosByProductNos(IEnumerable<string> productNos)
        {
            var productPhotos = ListProductPhotoAll();
            return productPhotos.FindAll(m => productNos.Contains(m.ProductNo));
        }
    }
}
