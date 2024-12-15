using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerAPI.Data;
using PersonalFinanceTrackerAPI.Models;

namespace PersonalFinanceTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionRepository _repository;

        public TransactionController(TransactionRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Transaction
        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> Get()
        {
            var transactions = _repository.GetAllTransactions();
            return Ok(transactions);
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public ActionResult<Transaction> Get(int id)
        {
            var transaction = _repository.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        // POST: api/Transaction
        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }

            _repository.InsertTransaction(transaction);
            return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Transaction transaction)
        {
            if (transaction == null || transaction.Id != id)
            {
                return BadRequest();
            }

            var existingTransaction = _repository.GetTransactionById(id);
            if (existingTransaction == null)
            {
                return NotFound();
            }

            _repository.UpdateTransaction(transaction);
            return NoContent();
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var transaction = _repository.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _repository.DeleteTransaction(id);
            return NoContent();
        }
    }
}
