using AspTechTrader.Core.Domain.IdentityEntities;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public AuthenticationResponseDTO CreateJwtToken(ApplicationUser applicationUser)
        {
            // get the expiration-minote like (10minite) and convert it to dateTime in futer // eg = 2024/3/1 + 10minote = the expration-date
            DateTime expiration = DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:expiration_minutes"]));

            // the jwt-peyload
            // Create an array of Claim objects representing the user's claims, such as their ID, name, email, etc.
            Claim[] claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id.ToString()), // Subject (user id)

                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), // Jwt unique ID
                
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()), // Issued at (date and time of the token generation

                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id.ToString()), // Subject (user id)

                // optional
                new Claim(ClaimTypes.NameIdentifier,applicationUser.Email), // uniqu name identifire of user (Email)

                // optional
                new Claim(ClaimTypes.NameIdentifier,applicationUser.PersonName), // uniqu name identifire of user (PersonNAme)
       
            };

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
                Token = token,
                Expiration = expiration,
                Email = applicationUser.Email,
                PersonName = applicationUser.PersonName,
            };


        }
    }
}
