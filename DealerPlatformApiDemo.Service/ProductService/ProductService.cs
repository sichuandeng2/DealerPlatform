using AutoMapper;
using DealerPlatformApiDemo.Core.Data;
using DealerPlatformApiDemo.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerPlatformApiDemo.Service.ProductService
{
    public partial class ProductService : IProduct
    {
        private readonly IRepository<Products> _productRepository;
        private readonly IRepository<ProductPhotos> _productPhotoRepository;
        private readonly IRepository<ProductSales> _productSaleRepository;
        private readonly IMapper _mapper;

        public ProductService(
           IRepository<Products> productRepository,
           IRepository<ProductPhotos> productPhotoRepository,
           IRepository<ProductSales> productSaleRepository,
           IMapper mapper)
        {
            this._productRepository = productRepository;
            this._productPhotoRepository = productPhotoRepository;
            this._productSaleRepository = productSaleRepository;
            this._mapper = mapper;
        }
    }
}
