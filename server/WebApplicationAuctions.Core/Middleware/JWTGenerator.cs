using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;

namespace WebApplicationAuctions.Core.Middleware
{
    public class JWTGenerator : IJwtGenerator
    {
        public string GenerateToken(Users user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurSuperSecretKeyForOurApi123456789hushhush"));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserID",user.UserId.ToString()),
                new Claim("Username",user.UserName)

            };

            //Admin?? if we have admin comment away and choose username for admin.

            /*if(users.UserName == "Admin")
             {
                 claims.Add(new Claim(ClaimTypes.Role, "Admin"));
             }

            else
             {
                 claims.Add(new Claim(ClaimTypes.Role, "User"));
             }*/



            //Move iisuer etc to appsettings.
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5120",
                audience: "http://localhost:5120",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
