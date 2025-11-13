using System.Security.Claims;
using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.Orders.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerSpace.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ICheckoutCommand _checkoutCommand;
        public OrdersController(ICheckoutCommand checkoutCommand)
        {
            _checkoutCommand = checkoutCommand;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutDto checkoutDto)
        {
            var userId = GetUserIdFromToken();
            var order = await _checkoutCommand.Execute(checkoutDto, userId);

            return Ok(order);
        }

        private long GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token.");

            return long.Parse(userIdClaim.Value);
        }
    }
}