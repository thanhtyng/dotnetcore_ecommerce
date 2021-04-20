using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Utils
{
    public interface IUtilsService
    {
        Account ParseJwtToAccountObject(string authSt);
    }
}
