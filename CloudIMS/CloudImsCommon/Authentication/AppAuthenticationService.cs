using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace CloudImsCommon.Authentication
{
    public interface IAppAuthenticationService
    {
        UserAccount Authenticate(string username, string password);
        Boolean ValidateUserToken(string userId, string token);
    }

    public class AppAuthenticationService : IAppAuthenticationService
    {
        private readonly AppSettings _appSettings;
        private readonly AppDbContext dbContext;

        public AppAuthenticationService(IOptions<AppSettings> appSettings, AppDbContext dbContext)
        {
            this._appSettings = appSettings.Value;
            this.dbContext = dbContext;
        }

        public UserAccount Authenticate(string username, string password)
        {
            var user = dbContext.UserAccounts.SingleOrDefault(x => x.UserID == username && x.Password == password);
            
            // return null if user not found
            if (user == null)
                return null;

            // remove password to prevent security breach
            user.Password = "";

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.UserID.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public bool ValidateUserToken(string userId, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                var tokenOwner = securityToken.Claims.First(claim => claim.Type == "UserId").Value;

                if (userId != tokenOwner)
                {
                    return false;
                }
                else
                {
                    return _VerifyToken(token);
                }
            }
            catch (Exception)
            {

                return false;
            }

        }


        private bool _VerifyToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var signingKey = new SymmetricSecurityKey(key);

            //var myIssuer = "http://mysite.com";
            //var myAudience = "http://myaudience.com";

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidIssuer = myIssuer,
                    //ValidAudience = myAudience,
                    IssuerSigningKey = signingKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
