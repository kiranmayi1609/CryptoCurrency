﻿using CryptoCurrency.Dto;
using CryptoCurrency.Models;

namespace CryptoCurrency.Interfaces
{
    public interface IPrice
    {
        ICollection<Price> GetPrices();
        Price GetPrice(int priceId);
        bool CreatePrice(Price price);
        bool UpdatePrice(int id,UpdatePrice updatePrice);
        bool DeletePrice(Price price);
        ICollection<Price> GetPricesFromCoin(int coinId);

        ICollection<Coin> GetCoinsFromPrice(int priceId);
        bool PriceExists(int coinId);
    }
}
