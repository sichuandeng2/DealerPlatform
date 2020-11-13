using AutoMapper;
using DealerPlatformApiDemo.Entity;
using DealerPlatformApiDemo.Service.OrderService.Dto;
using DealerPlatformApiDemo.Service.ProductService.Dto;
using DealerPlatformApiDemo.Service.ShoppingCartService.Dto;
using DealerPlatformApiDemo.Service.UserService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerPlatformApiDemo.Service.Common
{
    public class ServiceProfile:Profile
    {
        public ServiceProfile() {
            CreateMap<Products, ProductDto>().ReverseMap();
            CreateMap<ShoppingCarts, ShoppingCartDto>().ReverseMap();
            CreateMap<SaleOrderMasters, SaleOrderDto>().ReverseMap();
            CreateMap<Users, UserDto>().ReverseMap();
        }
    }
}
