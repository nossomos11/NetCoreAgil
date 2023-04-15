using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProAgil.API.Dtos;
using ProAgil.Domain.Identity;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration Config { get; }
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
        public IMapper Mapper { get; }
        
        public UserController(IConfiguration config, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IMapper mapper)
        {
            Config = config;
            UserManager = userManager;
            SignInManager = signInManager;
            Mapper = mapper;
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser(UserDTO userDTO)
        {
            return Ok(userDTO);
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                var user = Mapper.Map<User>(userDTO);
                var result = await UserManager.CreateAsync(user, userDTO.Password);
                
                if(result.Succeeded)
                {
                    userDTO = Mapper.Map<UserDTO>(user);
                    return Created("GetUser", userDTO);
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode (StatusCodes.Status500InternalServerError, $"Ocorreu um erro durante o registro do usuário. {ex.Message}");
            }

        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {

                var user = await UserManager.FindByNameAsync(loginDTO.UserName);
                var result = await SignInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

                if(result.Succeeded)
                {
                    user = await UserManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == loginDTO.UserName.ToUpper());

                    var userDTO = Mapper.Map<UserDTO>(user);

                    return Ok(new {
                        token = GenerateJWTToken(user).Result,
                        user = userDTO
                    });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode (StatusCodes.Status500InternalServerError, $"Ocorreu um erro durante o login do usuário. {ex.Message}");
            }

        }

        private async Task<String> GenerateJWTToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await UserManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(Config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}