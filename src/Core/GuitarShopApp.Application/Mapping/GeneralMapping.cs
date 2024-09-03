using AutoMapper;
using GuitarShopApp.Application.DTO;
using GuitarShopApp.Application.Models;
using GuitarShopApp.Domain.Entities;

namespace GuitarShopApp.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Order, OrderModel>().ReverseMap();
        CreateMap<Product, ProductViewModel>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<ProductDTO, ProductDTO>().ReverseMap();
        CreateMap<OrderDTO, OrderModel>().ReverseMap();
        CreateMap<OrderDTO, Order>().ReverseMap();
        CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
        CreateMap<User, LoginDTO>().ReverseMap();
        CreateMap<User, User>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<LoginDTO, LoginViewModel>().ReverseMap();
        CreateMap<UserDTO, RegisterViewModel>().ReverseMap();
    }
    
}