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
            CreateMap<Price, PriceDto>();


        }
    }
}
