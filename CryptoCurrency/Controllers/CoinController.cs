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
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateCoin([FromBody] CoinDTO coinCreate)
        {
            if (coinCreate == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coin = _mapper.Map<Coin>(coinCreate);

            _coinService.CreateCoin(coin);

            return CreatedAtAction(nameof(GetCoin), new { coinId = coin.Id }, coin);
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

        [HttpPut("{id}")]
        public IActionResult UpdateCoin(int id, [FromBody] CoinDTO coinDto)
        {
            if (coinDto == null)
            {
                return BadRequest();
            }

            var coin = _coinService.GetCoin(id);

            if (coin == null)
            {
                return NotFound();
            }

            _mapper.Map(coin, coinDto);

            var updated = _coinService.UpdateCoin(id, coin);

            if (!updated)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok();
        }

    }





}

