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
    }
}
