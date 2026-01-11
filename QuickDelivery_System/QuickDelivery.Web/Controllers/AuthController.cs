using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null) return BadRequest();

            // Verifică userul în baza de date Identity
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                return Ok(new { IsSuccess = true });
            }
            return Unauthorized();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel model)
        {
            if (model == null) return BadRequest();

            var user = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true }; // Confirmare automată
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Client"); // Adăugare rol automată
                return Ok(new { IsSuccess = true });
            }
            return BadRequest(result.Errors);
        }
    }
}