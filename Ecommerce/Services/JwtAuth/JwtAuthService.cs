using Dapper;
using Ecommerce.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.JwtAuth
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly IConfiguration _configuration;

        public JwtAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Execute SQL store procedure which is passed as a parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="store_procedure_params"></param>
        /// <returns></returns>
        public ServiceResponse<T> Execute_Command<T>(string query, DynamicParameters store_procedure_params)
        {
            ServiceResponse<T> serviceResponse = new ServiceResponse<T>();
            using (IDbConnection ecomDbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (ecomDbConnection.State == ConnectionState.Closed)
                {
                    ecomDbConnection.Open();
                }

                using var transaction = ecomDbConnection.BeginTransaction();
                try
                {
                    serviceResponse.Data = ecomDbConnection.Query<T>(query, store_procedure_params, commandType: CommandType.StoredProcedure, transaction: transaction).FirstOrDefault();
                    serviceResponse.Code = store_procedure_params.Get<int>("returnValue"); // this is an output parameter value from SQL store_produce
                    serviceResponse.Message = "It is success";
                    transaction.Commit();

                } catch(Exception e)
                {
                    transaction.Rollback();
                    serviceResponse.Code = 500;
                    serviceResponse.Message = e.StackTrace;
                }
            }
            return serviceResponse;
        }
        
        /// <summary>
        /// Generating a token using an user object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ServiceResponse<string> GenerateJWT(Account user)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //use claim as an identity part participating to generate a JWT token
            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim("roles", user.Role),
                 new Claim("Date", DateTime.Now.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(_configuration["JwtAuth:Issuer"],
              _configuration["JwtAuth:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            // return an JWT token
            serviceResponse.Data = new JwtSecurityTokenHandler().WriteToken(token); 
            serviceResponse.Code = 200;
            serviceResponse.Message = "An token is generated";
            return serviceResponse;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ServiceResponse<List<T>> getUserList<T>()
        {
            throw new NotImplementedException();
        }
    }
}
