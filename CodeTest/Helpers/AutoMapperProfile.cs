using AutoMapper;
using CodeTest.Entities;
using CodeTest.Models.Product;
using CodeTest.Models.Users;

namespace CodeTest.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<ProductModel, Product>();
            CreateMap<CartItemModel, CartItem>();

        }
    }
}