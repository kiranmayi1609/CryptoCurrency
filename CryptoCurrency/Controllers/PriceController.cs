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
    public class PriceController : ControllerBase
    {
        private readonly IPrice _PriceService;
        private readonly IMapper _mapper;
        public PriceController(IPrice PriceService , IMapper mapper)
        {
            _PriceService=PriceService;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Price>))]
        public IActionResult GetPrices()
        {
            try
            {
                var price = _mapper.Map<List<PriceDto>>(_PriceService.GetPrices());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(price);
            }
            catch (Exception ex)
            {
                // Log the exception here, e.g. using Serilog or another logging library
                // ...

                return StatusCode(500); // Return a 500 Internal Server Error response
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeletePrice(int id)
        {
            var p= _PriceService.GetPrice(id);

            if (p == null)
            {
                return NotFound();
            }

            var deletedPrice= _PriceService.DeletePrice(p);

            if (!deletedPrice)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreatePrice([FromBody] PriceDto priceDto)
        {

            
           if (priceDto == null)
           {
                    return BadRequest();
           }

           var price = _mapper.Map<Price>(priceDto);

           _PriceService.CreatePrice(price);

            return Ok();
        }
       
        [HttpPut("{id}")]
        public IActionResult UpdatePrice(int id, [FromBody] UpdatePrice updatePrice)
        {
            if (updatePrice == null)
            {
                return BadRequest();
            }

            var price = _PriceService.GetPrice(id);

            if (price == null)
            {
                return NotFound();
            }

            price.CoinId = updatePrice.CoinId;
            price.Value = updatePrice.Value;

           
            var priceExists = _PriceService.PriceExists(price.CoinId);

            if (!priceExists)
            {
                return NotFound("The specified coin does not exist.");
            }

            var updated = _PriceService.UpdatePrice(id,updatePrice);

            if (!updated)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }


    }
}
