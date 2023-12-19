using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace StudentAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class JwtController : Controller
    {
        public readonly IConfiguration _config;
            public JwtController(IConfiguration configuration)
            {
                _config = configuration;
            }
            private static Student AuthenticateUser(JwtGenRequest req)
            {
                Student _student = new();
                if (req.Name == "Gaurav" && req.Phone == 1000)
                {
                    _student = new Student { Name = "Admin" };
                }
                return _student;
            }
            private string GenerateToken(Student st)
            {
                /*var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                            new Claim("UserName", user.UserName),
                            new Claim("Password", user.Password)
                        };*/
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            [AllowAnonymous]
            [HttpPost]
            public IActionResult Login(JwtGenRequest s)
            {
                IActionResult response = Unauthorized();
                //Student stud = AuthenticateUser(s);
                var stud = AuthenticateUser(s);
                if (stud.Name == "Admin")
                {
                    var token = GenerateToken(stud);
                    response = Ok(new { token });
                }
                return response;
            }

            [Authorize]
            [HttpGet]
            public IActionResult getAll()
            {

                return Ok("Ok Checked Autherization"); ;
            }
        }
    }