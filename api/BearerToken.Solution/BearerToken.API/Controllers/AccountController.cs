﻿using BearerToken.API.Models.DataContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BearerToken.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly BearerDbContext db;

        public AccountController(BearerDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignIn(string username, string password)
        {

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Unauthorized(new
                {
                    error = true,
                    message = "Xahiş olunur xanaları doldurun!"
                });
            }

            if (username != "zakir" || password != "zakir007")
            {
                return Unauthorized(new
                {
                    error = true,
                    message = "İstifadəçi adı və ya şifrə yanlışdır!"
                });
            }

            var buffer = Encoding.UTF8.GetBytes("qubadli39.93ildabuq");

            var securityKey = new SymmetricSecurityKey(buffer);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                        "bearer",
                        "bearer",
                        expires: DateTime.UtcNow.AddHours(4).AddMinutes(5),
                        signingCredentials: credentials
                        );

            var tokenStr = new JwtSecurityTokenHandler()
                .WriteToken(token);

            if (username == "zakir" && password == "zakir007")
            {
                return Ok(new
                {
                    error = false,
                    message = "Xoş gəlmişsiniz!",
                    token = tokenStr
                });
            }

            return Ok();
        }
    }
}
