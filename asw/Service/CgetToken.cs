using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace asw.Service
{
    public class CgetToken : GetToken
    {
        private readonly string _issues;
        private readonly string _aidoence;
        private readonly string _secreKey;

        public CgetToken(IConfiguration configuration)
        {
            _issues = configuration["Jwt : Issuer"];
            _aidoence = configuration["Jwt:Aidoence"];
            _secreKey = configuration["Jwt:SecretKey"];


        }

        public string CreateToken(string token , string Role)
        {
            var tockendHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(_secreKey);

            var tokendDesceip = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.Name, token),
                        new Claim(ClaimTypes.Role, Role),
                }

                ),
                Issuer = _issues,
                Audience = _aidoence,
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)

            };
            var tok = tockendHandler.CreateToken(tokendDesceip);

            return tockendHandler.WriteToken(tok);
        }

      
    }
}
