

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
using WebApplication_scuffolding_reverse.Models;

namespace WebApplication_scuffolding_reverse.Controllers
{
    public class LoginController : ControllerBase
    {
        private JwtSettings _jwtsetting;

        public LoginController(JwtSettings jwtsetting)
        {
            _jwtsetting = jwtsetting;
        }

        [HttpPost("api/login")]
        public IActionResult Post([FromBody] Credentials credentials)
        {
            if (credentials != null)
            {
                // Lo username e la password, esistono nel mio db degli utenti?
                // se esiste, farò determinate azioni
                var token = GenerateJwtToken(credentials);
                return Ok(new { Token = token });
                // altrimenti
                // esco subito e mando l'errore/codice di utente sconosciuto
            }
            else
            {
                // Ritengo opportuno tracciare tutti i tentativi falliti di 
                // login al sistema
                Console.WriteLine("Login fallito, credenziali nulle (OKKIO!)");
                return BadRequest("Credenziali non valide");
            }
        }

        public string GenerateJwtToken(Credentials credentials)
        {
            // logica per generare il token JWT
            var secretKey = _jwtsetting.SecretKey;
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(secretKey!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, credentials.Username!)
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_jwtsetting.ExpirationMinutes),
                Issuer = _jwtsetting.Issuer,
                Audience = _jwtsetting.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;

        }
    }
}
