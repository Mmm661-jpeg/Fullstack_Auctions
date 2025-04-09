using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Domain.DomainModels;

namespace WebApplicationAuctions.Core.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateToken(Users user);
    }
}
