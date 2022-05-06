using Api.Dtos;
using Api.Extensions;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBalanceService _balanceService;
        private readonly IClaimsRetriever _claimsRetriever;
        private readonly ICryptoWalletCallerService _cryptoWalletCallerService;

        public TransactionController(IUnitOfWork unitOfWork, IBalanceService balanceService, IClaimsRetriever claimsRetriever, ICryptoWalletCallerService cryptoWalletCallerService)
        {
            _unitOfWork = unitOfWork;
            _balanceService = balanceService;
            _claimsRetriever = claimsRetriever;
            _cryptoWalletCallerService = cryptoWalletCallerService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetTransactions()
        {
            var userId = _claimsRetriever.GetUserId(HttpContext);
            var transactions = await _unitOfWork.TransactionRepository.GetUserTransactions(userId);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        [Authorize]
        [HttpGet("address/{cryptoId}/{address}")]
        public async Task<ActionResult<List<Transaction>>> GetTransactions(int cryptoId, string address)
        {
            var userId = _claimsRetriever.GetUserId(HttpContext);
            var crypto = await _unitOfWork.CryptoCurrencyRepository.Get(cryptoId);
            if(crypto == null)
                return NotFound($"Crypto with id: {cryptoId} not found");
            var transactions = await _cryptoWalletCallerService.GetTransactionList(address, crypto, userId);
            return Ok(transactions);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionWithId(int id)
        {
            var userId = _claimsRetriever.GetUserId(HttpContext);
            var transaction = await _unitOfWork.TransactionRepository.GetForUser(userId, id);
            if(transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> AddTransaction([FromBody] TransactionDto transactionDto)
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
            var numberOfChanges = await _unitOfWork.SaveAsync();
            if (numberOfChanges > 0)
            {
                await _balanceService.HandleRebalance(transactionToAdd, userId);
                return Ok(true);
            }
            return BadRequest(false);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTransaction(int id, [FromBody] TransactionDto transactionDto)
        {
            var userId = HttpContext.GetUserId();
            var transaction = await _unitOfWork.TransactionRepository.Get(id);
            if (transaction == null)
                return NotFound();

            if(transaction.AppUserId != userId)
                return Unauthorized();

            transaction.Amount = transactionDto.Amount;
            transaction.UnitPrice = transactionDto.UnitPrice;
            transaction.CryptocurrencyId = transactionDto.CryptocurrencyId;

            var numberOfChanges = await _unitOfWork.SaveAsync();
            return Ok(numberOfChanges);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            var userId = HttpContext.GetUserId();
            var transaction = await _unitOfWork.TransactionRepository.GetForUser(userId, id);
            if (transaction != null)
            {
                _unitOfWork.TransactionRepository.Remove(transaction);
                transaction.Amount = -transaction.Amount;
                await _balanceService.HandleRebalance(transaction, userId);
            }
            else
            {
                return NotFound();
            }
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
