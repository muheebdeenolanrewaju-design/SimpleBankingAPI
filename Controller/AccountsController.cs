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
}