using Microsoft.AspNetCore.Mvc;
using SimpleBankingApi.DTOs.Requests;
using SimpleBankingApi.DTOs.Responses;
using SimpleBankingApi.Repository.Interface;

namespace SimpleBankingApi.Controller;

[ApiController]
[Route("api/[controller]")]

public class AccountsController: ControllerBase
{
     private readonly IBankingService _bankingService;
        public AccountsController(IBankingService bankingService)
        {
          _bankingService = bankingService;   
        }
    
    
        [HttpPost("OpenAccount")]
    
        public async Task<ActionResult<ApiResponse<AccountResponse>>> OpenAccountAsync(CreateAccountRequest request)
        {
            var result = await _bankingService.CreateAccountAsync(request);
            if(!result.IsSuccess)
            {
                return BadRequest(result);
            }
    
            return Ok(result);
        }
        
        [HttpGet("GetAccount/{accountNumber}")]
            public async Task<ActionResult<ApiResponse<AccountResponse>>> GetAccountAsync(string accountNumber)
            {
                var result = await _bankingService.GetAccountAsync(accountNumber);
                if (!result.IsSuccess) return NotFound(result);
                return Ok(result);
            }
        
            [HttpPut("UpdateAccount/{accountNumber}")]
            public async Task<ActionResult<ApiResponse<AccountResponse>>> UpdateAccountAsync(string accountNumber, UpdateAccountRequest request)
            {
                var result = await _bankingService.UpdateAccountAsync(accountNumber, request);
                if (!result.IsSuccess) return BadRequest(result);
                return Ok(result);
            }
        
            [HttpDelete("DeleteAccount/{accountNumber}")]
            public async Task<ActionResult<ApiResponse<bool>>> DeleteAccountAsync(string accountNumber)
            {
                var result = await _bankingService.DeleteAccountAsync(accountNumber);
                if (!result.IsSuccess) return BadRequest(result);
                return Ok(result);
            }
        
            [HttpGet("GetAllAccounts")]
            public async Task<ActionResult<ApiResponse<IEnumerable<AccountResponse>>>> GetAllAccountsAsync()
            {
                var result = await _bankingService.GetAllAccountsAsync();
                if (!result.IsSuccess) return BadRequest(result);
                return Ok(result);
            }

            [HttpGet("CheckBalance/{accountNumber}")]
            public async Task<ActionResult<ApiResponse<BalanceResponse>>> CheckBalance(string accountNumber)
            {
                var result = await _bankingService.CheckBalanceAsync(accountNumber);
                if (!result.IsSuccess) return BadRequest(result);
                return Ok(result);
            }




            [HttpPost("Deposit")]

        public async Task<ActionResult<ApiResponse<decimal>>> Deposit(DepositRequest request)
        {
            var result = await _bankingService.DepositAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("Withdraw")]
        public async Task<ActionResult<decimal>> Withdraw(WithdrawalRequest request)
        {
            var result = await _bankingService.WithdrawAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("FundTransfer")]
        public async Task<ActionResult<TransferSummary>> Transfer(TransferRequest request)
        {
            var result = await _bankingService.TransferFundsAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
}