using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace CityNest
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
        public async Task<IActionResult> Register([FromBody] UsersDto request)
        {
                var response = usersServices.Register(request.Name, request.Email, request.Password, request.PhoneNumber);
                return Ok(response.Id);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsersLoginDto request)
        {
            var token = await usersServices.Login(request.password, request.email);

            HttpContext.Response.Cookies.Append("testy-cookies", token);
            return Ok(new { Token = token });
        }

        [HttpDelete("{id.guid}")]
        public async Task<IActionResult> DeleteUser([FromBody] Guid Id)
        {
            await usersServices.Delete(Id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            await usersServices.Update(user);
            return Ok();
        }

    }
}
