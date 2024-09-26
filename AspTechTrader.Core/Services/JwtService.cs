using AspTechTrader.Core.Domain.IdentityEntities;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AspTechTrader.Core.Services
{
    public class JwtService : IJwtService
    {

        private readonly IConfiguration _configuration;

        // to read the env from app.settings json , inject the IConfiguration
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Generates a JWT token using the given user's information and the configuration settings.
        /// </summary>
        /// <param name="user">ApplicationUser object</param>
        /// <returns>AuthenticationResponse that includes token</returns>
        public AuthenticationResponseDTO CreateJwtToken(ApplicationUser applicationUser, IList<string> roles)
        {
            // get the expiration-minote like (10minite) and convert it to dateTime in futer // eg = 2024/3/1 + 10minote = the expration-date
            DateTime expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:expiration_minutes"]));

            // the jwt-peyload
            // Create an array of Claim objects representing the user's claims, such as their ID, name, email, etc.
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id.ToString()), // Subject (user id)

                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), // Jwt unique ID
                
                new Claim(ClaimTypes.Email,applicationUser.Email),


                // this code created a bug //motherFucker
                //new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()), // Issued at (date and time of the token generation


                ////// optional
                //new Claim(ClaimTypes.NameIdentifier,applicationUser.Email), // uniqu name identifire of user (Email)

                ////// optional
                //new Claim(ClaimTypes.NameIdentifier,applicationUser.PersonName), // uniqu name identifire of user (PersonNAme)

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create a SymmetricSecurityKey object using the key specified in the configuration.// get the sucret key
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            // Create a SigningCredentials object with the security key and the HMACSHA256 algorithm.// hash the args
            SigningCredentials signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256 // this is a hashing algoritem // the most popular
                );

            // Create a JwtSecurityToken object with the given issuer, audience, claims, expiration, and signing credentials.
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"], // host of backend
                _configuration["Jwt:Audience"], // host of front
                claims, // the peyload of jwt
                expires: expiration, // the expration date
                signingCredentials: signingCredentials // get the hashed data
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // generate the token // Create a JwtSecurityTokenHandler object and use it to write the token as a string.
            string token = tokenHandler.WriteToken(tokenGenerator);

            // return the response with newly generated jwt-token based on the given information
            return new AuthenticationResponseDTO()
            {
                AccessToken = token,
                AccessTokenExpirationTime = expiration,
                Email = applicationUser.Email,
                PersonName = applicationUser.PersonName,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpirationDateTime = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:expiration_minutes"]))
            };


        }


        // create base-64 random refreshToken
        private string GenerateRefreshToken()
        {
            Byte[] bytes = new Byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);

        }


        public ClaimsPrincipal? GetPrincipalFormJwtToken(string? token)
        {
            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                LogValidationExceptions = true,

                // validate issuer
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],

                // validate Audiance
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],

                //becuz the token is expired when we exicud this method so dont validate exprationDate
                ValidateLifetime = false,

                // validate signature
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            };


            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken securityToken
                );

            // validation of token
            if (
                securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
                )
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }


    }
}
