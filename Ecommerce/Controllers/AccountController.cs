using Dapper;
using Ecommerce.Models;
using Ecommerce.Services.JwtAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IJwtAuthService _authentication;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IJwtAuthService authentication)
        {
            _authentication = authentication;
            _logger = logger;
        }

        [HttpPost("/api/account/login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Login user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parameter is missing");
            }

            DynamicParameters dp_param = new DynamicParameters();
            dp_param.Add("email", user.email, DbType.String);
            dp_param.Add("password", user.password, DbType.String);
            dp_param.Add("returnValue", DbType.String, direction: ParameterDirection.Output);
            var serviceResponse = _authentication.Execute_Command<Account>("sp_loginUser", dp_param);

            if (serviceResponse.Code == 200)
            {
                var token = _authentication.GenerateJWT(serviceResponse.Data);
                return Ok(token);
            }

            return BadRequest("Unauthorized");
        }

        [HttpPost("/api/account/register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] Account user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parameter is missing");
            }

            DynamicParameters dp_param = new DynamicParameters();
            dp_param.Add("email", user.Email, DbType.String);
            dp_param.Add("password", user.Password, DbType.String);
            dp_param.Add("role", user.Role, DbType.String);
            dp_param.Add("returnValue", DbType.String, direction: ParameterDirection.Output);

            var serviceResponse = _authentication.Execute_Command<Account>("sp_registerUser", dp_param);

            if (serviceResponse.Code == 200)
            {
                return Ok(serviceResponse);
            }

            return BadRequest(serviceResponse);
        }

        [HttpPut("/api/account/changepassword")]
        [Authorize(Roles = "RegisteredCustomer")]
        public IActionResult ChangePassword([FromBody] Account user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parameter is missing");
            }

            DynamicParameters dp_param = new DynamicParameters();
            dp_param.Add("email", user.Email, DbType.String);
            dp_param.Add("password", user.Password, DbType.String);
            dp_param.Add("returnValue", DbType.String, direction: ParameterDirection.Output);

            var serviceResponse = _authentication.Execute_Command<Account>("sp_updatePassword", dp_param);

            if (serviceResponse.Code == 200)
            {
                return Ok(serviceResponse);
            }

            return BadRequest(serviceResponse);
        }
    }
}