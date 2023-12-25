using Library.Api.Identity;
using Library.Api.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public AuthController(ILogger<AuthController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            try
            {
                // validate body
                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Validation error occured while attempting to register.");
                    return BadRequest("Validation error occured while attempting to register.");
                }
                
                // set and save user
                var user = new IdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

                // if user is saved
                if (result.Succeeded)
                {
                    // add role to user
                    // admin is created in seeder. We create only visitor user for this method.
                    await userManager.AddToRoleAsync(user, CustomUserRoles.Visitor.ToString());

                    // save log message
                    logger.LogInformation("User registered successfully.");

                    return Ok();
                }

                // throw exception and save log if user is not saved
                logger.LogError("Registration is unsuccessfull.");
                throw new Exception("Registration is unsuccessfull.");
            }
            catch (Exception ex)
            {
                // throw global exception
                throw new Exception(ex.Message);
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            // validate body
            if (!ModelState.IsValid)
            {
                return BadRequest("Validation Error");
            }

            // get user
            var user = await userManager.FindByNameAsync(model.Username);

            // return invalid user message if user is not found
            if(user == null)
            {
                return NotFound(new
                {
                    message = "Invalid Username"
                });
            }
            
            // attempt login
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                var userRole = (await userManager.GetRolesAsync(user))[0];
                return Ok(new 
                { 
                    token = GenerateJWT(user, userRole),
                    role = userRole,
                    user = user.Id,
                });
            }

            // login attempt fail
            return BadRequest("Invalid Credentials");
        }

        [HttpGet]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        private object GenerateJWT(IdentityUser user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName.ToString()),
                        new Claim(ClaimTypes.Role, role)
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
