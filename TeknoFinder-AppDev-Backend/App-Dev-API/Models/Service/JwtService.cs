using AppDev.API.Data;
using AppDev.API.Handler;
using AppDev.API.Models.DataTransferObject.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppDev.API.Models.Service
{
    public class JwtService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IConfiguration _configuration;

        public JwtService(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            this.applicationDbContext = applicationDbContext;
            this._configuration = configuration;
        }
        public async Task<LoginResponseDTO> Authenticate(LoginUserDTO loginUserDTO)
        {
            if (string.IsNullOrWhiteSpace(loginUserDTO.Email) || string.IsNullOrWhiteSpace(loginUserDTO.Password)) {
                return null;
            }
            var userAccount = await applicationDbContext.Users.FirstOrDefaultAsync(x=>x.Email == loginUserDTO.Email);
            if (userAccount == null || !PasswordHashHandler.Verify(loginUserDTO.Password, userAccount.Password)) { 
                return null;
            }
            var issuer = _configuration["JWTConfig:Issuer"];
            var audience = _configuration["JWTConfig:Audience"];
            var key = _configuration["JWTConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JWTConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim(JwtRegisteredClaimNames.Email, loginUserDTO.Email)
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponseDTO
            { 
                AccessToken = accessToken,
                Email = loginUserDTO.Email,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
