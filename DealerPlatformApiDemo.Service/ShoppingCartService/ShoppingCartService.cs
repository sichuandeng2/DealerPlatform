using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DealerPlatformApiDemo.Common;
using DealerPlatformApiDemo.Core.Data;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.ProductService;
using DealerPlatformApiDemo.Service.ShoppingCartService.Dto;

namespace DealerPlatformApiDemo.Service.ShoppingCartService {
    public partial class ShoppingCartService:IShoppingCart {
        private readonly IRepository<ShoppingCarts> _cartRepository;
        private readonly IProduct _productService;
        private readonly IMapper _mapper;

        public ShoppingCartService (
            IRepository<ShoppingCarts> cartRepository,
            IProduct productService,
            IMapper mapper) {
           
            this._cartRepository = cartRepository;
            this._productService = productService;
            this._mapper = mapper;
        }
    }
}