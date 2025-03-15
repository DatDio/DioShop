using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DioShop.Application.DTOs.Brand;
using DioShop.Application.DTOs.Cart;
using DioShop.Application.DTOs.CartItem;
using DioShop.Application.DTOs.Category;
using DioShop.Application.DTOs.ChatMessage;
using DioShop.Application.DTOs.Coupon;
using DioShop.Application.DTOs.Order;
using DioShop.Application.DTOs.OrderItem;
using DioShop.Application.DTOs.Product;
using DioShop.Application.DTOs.ProductItem;
using DioShop.Application.DTOs.ShippingMethod;
using DioShop.Application.DTOs.User;
using DioShop.Domain.Entities;

namespace DioShop.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));


            CreateMap<Coupon, CouponDto>().ReverseMap();
            CreateMap<Coupon, CreateCouponDto>().ReverseMap();
            CreateMap<Coupon, UpdateCouponDto>().ReverseMap();

            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<ChatMessage, CreateMessageDto>().ReverseMap();
            CreateMap<ChatMessage, MessageDto>().ReverseMap();

            CreateMap<ShippingMethod, ShippingMethodDto>().ReverseMap();
            CreateMap<ShippingMethod, CreateShippingMethodDto>().ReverseMap();
            CreateMap<ShippingMethod, UpdateShippingMethodDto>().ReverseMap();

       

            CreateMap<CartItemDto, CartItem>();
            CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductItem, opt => opt.MapFrom(src => src.ProductItem))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductItemId, opt => opt.MapFrom(src => src.ProductItemId))
            .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<CartItem, CreateCartItemDto>().ReverseMap();
            CreateMap<CartItem, UpdateCartItemDto>().ReverseMap();
            CreateMap<Cart, CartDto>()
                 .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));
            CreateMap<CartDto, Cart>();

            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>()
              .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
              .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));


            CreateMap<Order, OrderClientDto>().
                ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderStatusDto>().ReverseMap();


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductItem, opt => opt.MapFrom(src => src.ProductItem));
            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();

            
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>()
              .ForMember(dest => dest.ProductItems, opt => opt.MapFrom(src => src.ProductItems.Select(item => new ProductItemDto
              {
                  Id = item.Id,
                  ProductId = item.ProductId,
                  Name = item.Name,
                  ImageUrl = item.ImageUrl,
                  QuantityInStock = item.QuantityInStock,
                  Price = item.Price,
              })));

          

            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();

            CreateMap<ProductItem, ProductItemDto>().ReverseMap();
            CreateMap<ProductItem, CreateProductItemDto>().ReverseMap();
            CreateMap<ProductItem, UpdateProductItemDto>().ReverseMap();
        }
    }
}
