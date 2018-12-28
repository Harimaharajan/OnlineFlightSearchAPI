using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineFlightSearchAPI.Models;
using OnlineFlightSearchAPI.Services.AuthenticationServices;

namespace OnlineFlightSearchAPI.Controllers
{
    [Route("api/Auth/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        [ActionName("RequestToken")]
        public IActionResult RequestToken([FromBody]UserLoginModel userLoginModel)
        {
            try
            {
                if (_userService.IsValidUser(userLoginModel))
                {
                    return Ok(GenerateJwtToken());
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
        }

        private object GenerateJwtToken()
        {
            var jwtToken = JwtTokenBuilder();
            return jwtToken;
        }

        private object JwtTokenBuilder()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParameter:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["JWTParameter:Issuer"],
                audience: _configuration["JWTParameter:Audience"],
                signingCredentials: credentials,
                expires: DateTime.Now.AddHours(1)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}