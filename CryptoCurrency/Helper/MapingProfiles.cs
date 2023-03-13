using AutoMapper;
using CryptoCurrency.Dto;
using CryptoCurrency.Models;

namespace CryptoCurrency.Helper
{
    public class MapingProfiles:Profile
    {
        public MapingProfiles()
        {
            CreateMap<Coin,CoinDTO>();
            CreateMap<Transaction ,TransactionDto>();
            CreateMap<PriceDto, Price>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))

             ;
            CreateMap<UserDto, User>();
            CreateMap<WalletDto, Wallet>();



        }
    }
}
