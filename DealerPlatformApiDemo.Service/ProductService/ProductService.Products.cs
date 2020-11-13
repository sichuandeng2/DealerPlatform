using DealerPlatformApiDemo.Common;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.ProductService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealerPlatformApiDemo.Service.ProductService
{
    public partial class ProductService
    {
        public List<Products> ListProductAll() {
            List<Products> products = MemoryHelper.GetMemory(AppConst.productCacheKey) as List<Products>;
            if (products == null)
            {
                products = _productRepository.ListAll().ToList();
                MemoryHelper.SetMemory(AppConst.productCacheKey, products);
            }
            return products;
        }
        public List<Products> ListProductByNos(IEnumerable<string> pNos) {
           var products = ListProductAll().Where(m=>pNos.Contains(m.ProductNo)).ToList();
            return products;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bType">板材/五金/地板</param>
        /// <param name="typeName">商品类型</param>
        /// <param name="productType">筛选类型</param>
        /// <param name="pageIndex">当前页码 </param>
        /// <param name="pageSize">每页数据量</param>
        /// <returns></returns>
        public List<ProductDto> ListProductByPage(ProductPageAndFilterDto ppfd) {
            var products = ListProductAll();
            if (ppfd.ProductTypes != null && ppfd.ProductTypes.Count>0)
            {
                var bzgg = ppfd.ProductTypes.ContainsKey("ProductBZGG") ? ppfd.ProductTypes["ProductBZGG"] : null;
                ppfd.ProductTypes.TryGetValue("ProductCD", out string cd);
                ppfd.ProductTypes.TryGetValue("ProductPP", out string pp);
                ppfd.ProductTypes.TryGetValue("ProductXH", out string xh);
                ppfd.ProductTypes.TryGetValue("ProductCZ", out string cz);
                ppfd.ProductTypes.TryGetValue("ProductHB", out string hb);
                ppfd.ProductTypes.TryGetValue("ProductHD", out string hd);
                ppfd.ProductTypes.TryGetValue("ProductGY", out string gy);
                ppfd.ProductTypes.TryGetValue("ProductHS", out string hs);
                ppfd.ProductTypes.TryGetValue("ProductMC", out string mc);
                ppfd.ProductTypes.TryGetValue("ProductDJ", out string dj);
                ppfd.ProductTypes.TryGetValue("ProductGG", out string gg);
                ppfd.ProductTypes.TryGetValue("ProductYS", out string ys);
                products = products.FindAll(m => (bzgg == null || m.ProductBZGG == bzgg)
                    && (cd == null || m.ProductCD == cd)
                    && (pp == null || m.ProductPP == pp)
                    && (xh == null || m.ProductXH == xh)
                    && (cz == null || m.ProductCZ == cz)
                    && (hb == null || m.ProductHB == hb)
                    && (hd == null || m.ProductHD == hd)
                    && (gy == null || m.ProductGY == gy)
                    && (hs == null || m.ProductHS == hs)
                    && (mc == null || m.ProductMC == mc)
                    && (dj == null || m.ProductDJ == dj)
                    && (gg == null || m.ProductGG == gg)
                    && (ys == null || m.ProductYS == ys));
            }
            return GetFullProductDto(products
                .FindAll(m=>m.BelongTypeNo == ppfd.BType
                && (m.TypeName == ppfd.TypeName
                || String.IsNullOrEmpty(ppfd.TypeName) 
                && m.ProductName.Contains(ppfd.SearchText==null?"": ppfd.SearchText)))
                .Skip((ppfd.PageIndex - 1) * ppfd.PageSize)
                .Take(ppfd.PageSize).ToList());
        }

        private List<ProductDto> GetFullProductDto(List<Products> products) {
            //获取物品编号的集合
            var productNos = products.Select(m => m.ProductNo);
            //获取属于这些物品的照片和价格
            var photos = ListProductPhotosByProductNos(productNos);
            var sales = ListProductSalesByProductNos(productNos);
            //AutoMapper转换
            var dtos = _mapper.Map<List<ProductDto>>(products);
            dtos.ForEach(m =>
            {
                m.productPhoto = photos.FirstOrDefault(s=>s.ProductNo == m.ProductNo);
                m.productSale = sales.FirstOrDefault(s=>s.ProductNo == m.ProductNo);
                if (m.productSale == null) m.productSale = new ProductSales { SalePrice = 0 };
                if (m.productPhoto == null) m.productPhoto = new ProductPhotos { ProductPhotoUrl = "/img/productPhotos/1.jpg" };
            });
            return dtos;
        }

        public List<string> ListTypeName(string bType) {
            return ListProductAll().FindAll(m => m.BelongTypeNo == bType).Select(m => m.TypeName).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList();
        }
        public Dictionary<string, List<string>> ListProductTypes(string bType, string typeName) {
            Dictionary<string, List<string>> dicProductType = new Dictionary<string, List<string>>();
            var product = ListProductAll().FindAll(m => m.BelongTypeNo == bType && (m.TypeName == typeName || string.IsNullOrEmpty(typeName)));
            dicProductType.Add("ProductBZGG|包装规格", product.Select(m => m.ProductBZGG).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductCD|产地", product.Select(m => m.ProductCD).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductPP|品牌", product.Select(m => m.ProductPP).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductXH|型号", product.Select(m => m.ProductXH).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductCZ|材质", product.Select(m => m.ProductCZ).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductHB|环保", product.Select(m => m.ProductHB).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductHD|厚度", product.Select(m => m.ProductHD).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductGY|工艺", product.Select(m => m.ProductGY).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductHS|花色", product.Select(m => m.ProductHS).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductMC|面材", product.Select(m => m.ProductMC).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductDJ|等级", product.Select(m => m.ProductDJ).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductGG|规格", product.Select(m => m.ProductGG).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
             dicProductType.Add("ProductYS|颜色", product.Select(m => m.ProductYS).Distinct().Where(m => !string.IsNullOrEmpty(m)).ToList());
            return dicProductType;
        }
    }
}
