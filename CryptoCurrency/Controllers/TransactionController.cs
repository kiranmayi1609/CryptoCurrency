using AutoMapper;
using CryptoCurrency.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CryptoCurrency.Models;
using CryptoCurrency.Dto;

namespace CryptoCurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransaction _transactionservice;
        private readonly IMapper _mapper;
        public TransactionController(ITransaction TransactionService, IMapper mapper)
        {
            _transactionservice = TransactionService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]

        public IActionResult GetTransactions()
        {
            var transactions = _mapper.Map<List<TransactionDto>>(_transactionservice.GetTransactions());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(transactions);
        }

        [HttpGet ("{transactionId}") ]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Transaction>))]
        [ProducesResponseType(400)]

        public IActionResult GetTransaction(int transactionId)
        {
            if(!_transactionservice.TransactionExists(transactionId))
                return NotFound();

            var transaction = _mapper.Map<TransactionDto>(_transactionservice.GetTransaction(transactionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(transaction);
        }

        [HttpGet("coin/{transactionId}")]
        [ProducesResponseType (200, Type = typeof(IEnumerable<Coin>))]
        [ProducesResponseType(400)]

        public IActionResult GetTransactionsByCoins(int transactionId)
        {
            var transaction = _mapper.Map<List<CoinDTO>>(_transactionservice.GetTransactionsByCoins(transactionId));
            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(transaction);
        }
    }
}
