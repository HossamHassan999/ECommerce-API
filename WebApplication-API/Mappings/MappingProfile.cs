using AutoMapper;
using WebApplication_API.Data;
using WebApplication_API.DTOs;

namespace WebApplication_API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users
            CreateMap<User, UserDTO>();

            // Categories
            CreateMap<Category, CategoryDTO>();

            // Products
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            // Cart Items
            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price));

            // Order Items
            CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));


            // Orders
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));


            CreateMap<User, UserDTO>();




            CreateMap<RegisterDTO, User>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<ProductDTO, Product>();
            CreateMap<ProductDTOPost, Product>();

            CreateMap<UserDTO, User>();

            CreateMap<CartItemDTO, CartItem>();
            CreateMap<CartItemDTOPost, CartItem>();

            CreateMap<OrderDTO, Order>();
            CreateMap<OrderDTOPost, Order>();

            CreateMap<OrderItemDTO, OrderItem>();
        }
    }
}