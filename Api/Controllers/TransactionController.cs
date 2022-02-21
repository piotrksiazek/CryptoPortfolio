using Api.Dtos;
using Api.Extensions;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetPaged()
        {
            //add paging
            var userId = HttpContext.GetUserId();
            var transactions = await _unitOfWork.TransactionRepository.Find(x => x.AppUserId == userId);
            return Ok(transactions);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetWithId(int id)
        {
            var userId = HttpContext.GetUserId();
            var transactions = await _unitOfWork.TransactionRepository.Find(x => x.AppUserId == userId && x.Id == id);
            return Ok(transactions);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> Add([FromBody] TransactionDto transactionDto)
        {
            var userId = HttpContext.GetUserId();
            var transactionToAdd = new Transaction()
            {
                AppUserId = userId,
                UnitPrice = transactionDto.UnitPrice,
                Amount = transactionDto.Amount,
                CryptocurrencyId = transactionDto.CryptocurrencyId
            };

            await _unitOfWork.TransactionRepository.Add(transactionToAdd);
            var numberOfChanges = _unitOfWork.Save();
            if (numberOfChanges > 0)
            {
                return Ok(true);
            }
            return BadRequest(false);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<bool>> Update(TransactionWIthIDDto transactionDto)
        {
            var userId = HttpContext.GetUserId();
            var transaction = await _unitOfWork.TransactionRepository.Get(transactionDto.Id);
            if (transaction == null || transaction.AppUserId != userId)
                return BadRequest(false);

            transaction.Amount = transactionDto.Amount;
            transaction.UnitPrice = transactionDto.UnitPrice;
            transaction.CryptocurrencyId = transactionDto.CryptocurrencyId;

            _unitOfWork.Save();
            if(transaction.Amount > 0)
                return Ok(true);
            return BadRequest(false);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = HttpContext.GetUserId();
            var transaction = await _unitOfWork.TransactionRepository.Get(id);
            if(transaction.AppUserId == userId)
            {
                _unitOfWork.TransactionRepository.Remove(transaction);
            }
            else
            {
                return BadRequest("This transaction doesn't belong to you");
            }
            var amountOfChanges = _unitOfWork.Save();
            if (amountOfChanges > 0)
                return Ok();
            return BadRequest();
        }
    }
}
