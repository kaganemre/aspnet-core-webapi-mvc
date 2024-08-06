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
    }
    
}