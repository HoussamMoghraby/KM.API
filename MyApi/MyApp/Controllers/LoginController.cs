using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyApp.Business.Exceptions;
using MyApp.Business.Helpers;
using MyApp.Business.Services;
using MyApp.Data.Models;
using MyApp.Data.Payloads;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserService _userService;

        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginPayload userLoginPayload)
        {
            try
            {
                var user = await Authenticate(userLoginPayload);

                var token = GenerateToken(user);
                return Ok(token);
            }
            catch (CustomBusinessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }



        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> Authenticate(UserLoginPayload userLoginPayload)
        {
            var users = await _userService.GetUsers();
                
            var currentUser = users.SingleOrDefault(o => o.Email.ToLower() == userLoginPayload.Email.ToLower());
            if (currentUser == null)
            {
                throw new CustomBusinessException("The email provided is invalid");
            }

            var hashedLoginPassword = StringHasher.HashString(userLoginPayload.Password);
            if (hashedLoginPassword != currentUser.Password)
            {
                throw new CustomBusinessException("The password provided is invalid");
            }

            return currentUser;
        }

    }
}
