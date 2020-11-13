using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealerPlatformApiDemo.Service.ProductService;
using DealerPlatformApiDemo.Service.ProductService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DealerPlatformApiDemo.Api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProduct _productServer;

        public ProductController(IProduct productServer)
        {
            this._productServer = productServer;
        }
        [HttpGet]
        public List<ProductDto> GetAll(string bType, string typeName, string productTypes, string searchText, int pageIndex, int pageSize)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (productTypes != null && !string.IsNullOrEmpty(productTypes))
            {
                foreach (var productType in productTypes.Split("%5E"))
                {
                    dic.Add(productType.Split('_')[0], productType.Split('_')[1]);
                }
            }
            var ppfd = new ProductPageAndFilterDto
            {
                BType = bType,
                ProductTypes = dic,
                TypeName = typeName == "空" ? null : typeName,
                SearchText = searchText,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return _productServer.ListProductByPage(ppfd);
        }
        [HttpGet("{bType}/GetTypeName")]
        public List<string> GetTypeName(string bType)
        {
            return _productServer.ListTypeName(bType);
        }
        [HttpGet("{bType}/{typeName}/GetProductTypes")]
        public List<ProductTypeDto> GetProductTypes(string bType, string typeName)
        {
            var dic = _productServer.ListProductTypes(bType, typeName == "空" ? null : typeName);
            List<ProductTypeDto> productTypeDtos = new List<ProductTypeDto>();
            foreach (var kv in dic)
            {
                if (kv.Value.Count <= 0) continue;
                ProductTypeDto productTypeDto = new ProductTypeDto();
                productTypeDto.TypeName = kv.Key.Split('|')[0];
                productTypeDto.TypeNameCn = kv.Key.Split('|')[1];
                kv.Value.ForEach(m =>
                {
                    var typeItem = string.IsNullOrEmpty(m) ? "未命名" : m;
                    productTypeDto.TypeItems.Add(typeItem);
                });
                productTypeDtos.Add(productTypeDto);
            }
            return productTypeDtos;
        }
    }
}
