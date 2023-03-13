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

        public ActionResult<WalletDto> Create(WalletDto walletCreateDto)
        {
            try
            {
                var wallet = _mapper.Map<Wallet>(walletCreateDto);
                _walletService.Create(wallet);
                var walletDto = _mapper.Map<Wallet>(wallet);
                return CreatedAtAction(nameof(GetById), new { id = walletDto.Id }, walletDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the wallet: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
            public ActionResult<WalletDto> Update(int id, WalletDto walletUpdateDto)
            {
                var wallet = _walletService.GetById(id);
                if (wallet == null)
                {
                    return NotFound();
                }
                _mapper.Map(walletUpdateDto, wallet);
                _walletService.Update(wallet);
                var walletDto = _mapper.Map<WalletDto>(wallet);
                return Ok(walletDto);
            }

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
    }



    
}
