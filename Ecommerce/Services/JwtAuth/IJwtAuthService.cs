using Dapper;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.JwtAuth
{
    public interface IJwtAuthService
    {
        ServiceResponse<string> GenerateJWT(Account user);
        ServiceResponse<T> Execute_Command<T>(string query, DynamicParameters store_procedure_params);
        ServiceResponse<List<T>> getUserList<T>();
    }
}
