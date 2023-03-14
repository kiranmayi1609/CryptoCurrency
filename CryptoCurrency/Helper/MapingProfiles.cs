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
            CreateMap<CoinDTO,Coin>();
            CreateMap<TransactionDto,Transaction>();
            CreateMap<PriceDto, Price>();
            CreateMap<Price,PriceDto>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))

             ;
            CreateMap<UserDto, User>();
            CreateMap<WalletDto, Wallet>();
            CreateMap<Wallet, WalletDto>();
            CreateMap<Transaction, TransactionDto>();



        }
    }
}
