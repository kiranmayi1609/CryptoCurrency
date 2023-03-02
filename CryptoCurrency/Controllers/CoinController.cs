using AutoMapper;
using CryptoCurrency.Dto;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoin _coinService;
        private readonly IMapper _mapper;
        public CoinController(ICoin coinservice, IMapper mapper)
        {
            _coinService = coinservice;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Coin>))]

        public IActionResult GetCoins()
        {
            var coin = _mapper.Map<List<CoinDTO>>(_coinService.GetCoins());
            
            if (!ModelState.IsValid)

                return BadRequest(ModelState);
            return Ok(coin);
        }

        [HttpGet("{CoinId}")]
        [ProducesResponseType(200, Type = typeof(Coin))]
        [ProducesResponseType(400)]

        public IActionResult GetCoin(int CoinId)
        { 
            if(!_coinService.CoinExists(CoinId))
                return NotFound();

            var coins = _mapper.Map<CoinDTO>(_coinService.GetCoin(CoinId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(coins);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult CreateCoin([FromQuery] int transactionId, [FromBody] TransactionDto  coinCreate)
        {
            if (coinCreate == null)
            {
                return BadRequest(ModelState);
            }
            var filteredCoins = _coinService.GetCoins()
                .Where(c => c.MarketCap > 1000000);
            

            return NoContent();
        }
       [HttpDelete("{id}")]
       public IActionResult DeleteCoin(int id)
       {
           var coin = _coinService.GetCoin(id);

                if (coin == null)
                {
                    return NotFound();
                }

           var deletedCoin = _coinService.DeleteCoin(coin);

             if (!deletedCoin)
             {
                    return StatusCode(500, "A problem happened while handling your request.");
             }

           return NoContent();
       }
    }





}

