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

        

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTransaction([FromBody] TransactionDto transactionDto)
        {
            if (transactionDto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transaction = _mapper.Map<Transaction>(transactionDto);
            // Set the DateTime property based on the date string
            //transaction.Date = DateTime.Parse(transactionDto.Date);

            _transactionservice.AddTransaction(transaction);

            return CreatedAtAction(nameof(GetTransaction), new { transactionId = transaction.Id }, transaction);
        }


        




        //[HttpPut("{id}")]
        //public IActionResult UpdateTransaction(int id, [FromBody] updateTransaction uTransaction)
        //{
        //    if (uTransaction == null)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        _transactionservice.UpdateTransaction(id, uTransaction);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error and return an appropriate response
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(int id, [FromBody] updateTransaction update)
        {
            if (update == null)
            {
                return BadRequest();
            }

            var tr = _transactionservice.GetTransaction(id);

            if (tr == null)
            {
                return NotFound();
            }

            //_mapper.Map(coin, coinDto);
            // Update the coin object with values from the DTO
            tr.UserId = update.UserId;
            tr.Date = update.Date;
            


            var updated = _transactionservice.UpdateTransaction(id, update);

            if (!updated)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var tr = _transactionservice.GetTransaction(id);
            if (tr == null)
            {
                return NotFound();
            }
            _transactionservice.Delete(id);
            return NoContent();
        }














    }
}
