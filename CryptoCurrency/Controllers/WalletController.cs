using AutoMapper;
using CryptoCurrency.Dto;
using CryptoCurrency.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CryptoCurrency.Repositories;
using CryptoCurrency.Models;

namespace CryptoCurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        
            private readonly IWallet _walletService;
            private readonly IMapper _mapper;

            public WalletController(IWallet walletService, IMapper mapper)
            {
                _walletService = walletService;
                _mapper = mapper;
            }

            [HttpGet("{id}")]
            public ActionResult<WalletDto> GetById(int id)
            {
                var wallet = _walletService.GetById(id);
                if (wallet == null)
                {
                    return NotFound();
                }
                var walletDto = _mapper.Map<WalletDto>(wallet);
                return Ok(walletDto);
            }

            [HttpGet]
            public ActionResult<IEnumerable<WalletDto>> GetAll()
            {
                var wallets = _walletService.GetAll();
                var walletDtos = _mapper.Map<IEnumerable<Wallet>>(wallets);
                return Ok(walletDtos);
            }

            [HttpPost]
        //public ActionResult<WalletDto> Create(WalletDto walletCreateDto)
        //{
        //    var wallet = _mapper.Map<Wallet>(walletCreateDto);
        //    _walletService.Create(wallet);
        //    var walletDto = _mapper.Map<WalletDto>(wallet);
        //    return CreatedAtAction(nameof(GetById), new { id = walletDto.Id }, walletDto);
        //}

        //public ActionResult<WalletDto> Create(WalletDto walletCreateDto)
        //{
        //    try
        //    {
        //        var wallet = _mapper.Map<Wallet>(walletCreateDto);
        //        _walletService.Create(wallet);
        //        var walletDto = _mapper.Map<Wallet>(wallet);
        //        return CreatedAtAction(nameof(GetById), new { id = walletDto.Id }, walletDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while creating the wallet: {ex.Message}");
        //    }
        //}




        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateWallet([FromBody] WalletDto walletCreate)
        {
            if (walletCreate == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var wallet = _mapper.Map<Wallet>(walletCreate);

            _walletService.CreateWallet(wallet);

            return CreatedAtAction(nameof(GetAll), new { walletId = wallet.Id }, wallet);
        }

        //[HttpPut("{id}")]
        //    public ActionResult<WalletDto> Update(int id, WalletDto walletUpdateDto)
        //    {
        //        var wallet = _walletService.GetById(id);
        //        if (wallet == null)
        //        {
        //            return NotFound();
        //        }
        //        _mapper.Map(walletUpdateDto, wallet);
        //        _walletService.Update(wallet);
        //        var walletDto = _mapper.Map<WalletDto>(wallet);
        //        return Ok(walletDto);
        //    }



        //public IActionResult UpdateWallet(int id, [FromBody] updateWallet uWallet)
        //{
        //    if(id!=uWallet.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        var wallet =_walletService.GetById(id);
        //        if(wallet==null)
        //        {
        //            return NotFound();
        //        }
        //        //update the wallet properties 
        //        wallet.UserId=uWallet.UserId;
        //        wallet.Balance=uWallet.Balance;

        //        _walletService.Update(wallet);
        //        return Ok();
        //    }
        //    catch(Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //    //if (id! == uWallet.UserId)
        //    //{
        //    //    return BadRequest();
        //    //}

        //    //try
        //    //{
        //    //    _walletService.Update(id, uWallet);
        //    //    return Ok();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    // Log the error and return an appropriate response
        //    //    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    //}

        //    // Update the wallet

        //}


        //[HttpPut("{id}")]
        //public IActionResult UpdateTransaction(int id, [FromBody] updateWallet uWallet)
        //{
        //    if (uWallet == null)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        _walletService.Update(id, uWallet);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error and return an appropriate response
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}


        [HttpDelete("{id}")]
            public ActionResult Delete(int id)
            {
                var wallet = _walletService.GetById(id);
                if (wallet == null)
                {
                    return NotFound();
                }
                _walletService.Delete(id);
                return NoContent();
            }

        [HttpPut("{id}")]

        public IActionResult UpdateWallet(int id, [FromBody] updateWallet update)
        {
            if (update == null)
            {
                return BadRequest();
            }

            var wallet = _walletService.GetById(id);

            if (wallet == null)
            {
                return NotFound();
            }

            //_mapper.Map(coin, coinDto);
            // Update the coin object with values from the DTO
            wallet.UserId = update.UserId;
            wallet.Balance = update.Balance;    
            


            var updated = _walletService.UpdateWallet(id, update);

            if (!updated)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok();
        }



    }



    
}
