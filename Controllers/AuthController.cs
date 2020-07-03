using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FileManager.Data;
using FileManager.Dtos;
using FileManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace FileManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _authrepo;
        public AuthController(IAuthRepository authrepo, IConfiguration config)
        {
            _config = config;
            _authrepo = authrepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            // [FromBody]UserForRegisterDto 
            // if(ModelState.IsValid) return BadRequest(ModelState);

            userForRegister.Username = userForRegister.Username.ToLower();

            if(await _authrepo.UserExists(userForRegister.Username)) return BadRequest("Username already Exists");

            var userToCreate = new User{
                Username = userForRegister.Username
            };

            var createdUser = await _authrepo.Register(userToCreate,userForRegister.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authrepo.Login(userForLoginDto.Username.ToLower(),userForLoginDto.Password);

            if(userFromRepo==null) return Unauthorized();

            var claims = new []{
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new{
                token = tokenHandler.WriteToken(token)
            });
        }
    
}
}