using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityNest.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices usersServices;
        private readonly IJwtProvider jwtProvider;
        public UsersController(IUsersServices usersServices, IJwtProvider jwtProvider)
        {
            this.usersServices = usersServices;
            this.jwtProvider = jwtProvider;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsersRegisterRequest request)
        {
            var response = await usersServices.Register(request.name, request.email, request.passwordHash, request.phoneNumber);
            return Ok(response.Id);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsersLoginRequest request)
        {
            var token = await usersServices.Login(request.password, request.email);

            HttpContext.Response.Cookies.Append("testy-cookies", token);
            return Ok(new { Token = token });
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] Guid id)
        {
            await usersServices.Delete(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UsersRegisterRequest user)
        {
            await usersServices.Update(user);
            return Ok();
        }

    }
}
