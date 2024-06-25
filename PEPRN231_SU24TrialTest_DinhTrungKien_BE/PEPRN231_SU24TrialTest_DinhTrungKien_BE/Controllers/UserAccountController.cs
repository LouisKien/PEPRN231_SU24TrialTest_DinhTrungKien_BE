using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.ModelViews.UserAccount;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Interfaces;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(LoginDtoRequest request)
        {
            try
            {
                if (request == null) {
                    return BadRequest("Login info cannot null");
                }
                if( request.UserEmail == null)
                {
                    return BadRequest("Email cannot null");
                }
                if (request.UserPassword == null)
                {
                    return BadRequest("Password cannot null");
                }
                IActionResult response = Unauthorized();
                var user = await _userAccountService.AuthenticateUser(request);
                if (user != null)
                {
                    var accessToken = await _userAccountService.GenerateAccessToken(user);
                    response = Ok(new { accessToken = accessToken });
                    return response;
                }
                return NotFound("Invalid email or password");
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
