using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace P224FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> CreateToken()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "Hamid"),
                new Claim("FullName", "Hamid Mammaod"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"SuperAdmin")
            };

            string seckretKey = "c0f851a4-75db-4f0c-986a-26456e5496b6";

            byte[] keys = System.Text.Encoding.UTF8.GetBytes(seckretKey);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(keys);
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                issuer: "http://localhost:57925",
                audience: "http://localhost:57925",
                expires: DateTime.Now.AddDays(3)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(token);
        }
    }
}
