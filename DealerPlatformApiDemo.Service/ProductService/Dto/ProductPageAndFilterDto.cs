using DealerPlatformApiDemo.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerPlatformApiDemo.Service.ProductService.Dto
{
    public class ProductPageAndFilterDto
    {

            public string SearchText { get; set; }
            public string BType { get; set; }
            public string TypeName { get; set; }
            public Dictionary<string, string> ProductTypes { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
    }
}
