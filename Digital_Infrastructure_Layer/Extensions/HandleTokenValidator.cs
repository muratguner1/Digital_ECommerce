using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.Models;
using Microsoft.IdentityModel.Tokens;

namespace Digital_Infrastructure_Layer.Extensions;

public static class HandleTokenValidator
{
    public static TokenModel HandleToken(IList<string> roles, User user, string secretKey)
    {
        TokenModel tokenModel = new();
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(secretKey);

        if (user.Email != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            tokenModel.Token = tokenHandler.WriteToken(token);
            tokenModel.Expiration = tokenDescriptor.Expires.Value;
        }
        return tokenModel;
    }
}