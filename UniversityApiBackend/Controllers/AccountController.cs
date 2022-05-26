using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTOs;
using UniversityApiBackend.Entities;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
   
        private readonly IStringLocalizer<AccountController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;
        private readonly IUserHelper _userHelper;

        public AccountController(JwtSettings jwtSettings,
            IStringLocalizer<AccountController> stringLocalizer,
            IStringLocalizer<SharedResource> sharedResourceLocalizer,
            IUserHelper userHelper
            
            )
        {
            _jwtSettings = jwtSettings;
            _stringLocalizer = stringLocalizer;
            _sharedResourceLocalizer = sharedResourceLocalizer;
            _userHelper = userHelper;
        }

     


        [HttpPost("login")]
        public async Task<ActionResult<UserLogins>> Login([FromBody] UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                //var valid = Logins.Any(user => user.Name.Equals(userLogins.Password, StringComparison.OrdinalIgnoreCase));
                var valid = await _userHelper.PasswordSignInAsync(userLogins);
            
                if (valid.Succeeded)
                {
             
              
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = userLogins.UserName,
                        EmailId = userLogins.UserName,
    
                        GuiId = Guid.NewGuid(),

                    },
                    _jwtSettings);
                }
                else
                {
                    var Wrongpassword = _stringLocalizer.GetString("Wrongpassword").Value ?? String.Empty;
                    return BadRequest(Wrongpassword);
                }

                var msj = _stringLocalizer.GetString("Welcome").Value ?? String.Empty;
                
                
                return Ok( new { 
                
                Token=Token,
                Msj= msj
                });
            }
            catch (Exception ex)
            {

                throw new Exception("GetToken Error", ex);
            }

        }

        [HttpPost("register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<ActionResult<UserLogins>> Register([FromBody] RegisterUser registeruser)
        {
            var Token = new UserTokens();
            var isExits = await _userHelper.GetUserByEmailAsync(registeruser.Email);
            if (isExits == null)
            {
                var rest = await _userHelper.RegisterUserAsync(registeruser);
                var credencials = new UserLogins { Password = registeruser.Password };
                if (rest.Succeeded)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = registeruser.Email,
                        EmailId = registeruser.Email,
                        GuiId = Guid.NewGuid(),

                    },
                 _jwtSettings);


                    var msj = _stringLocalizer.GetString("Welcome").Value ?? String.Empty;


                    return Ok(new
                    {

                        Token = Token,
                        Msj = msj
                    });
                }
                else
                {
                    return BadRequest(rest.Errors);
                }
            }
            return BadRequest("Email aready exist");


        }

        [HttpPost("createmanager")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<UserLogins>> createAdmin([FromBody] RegisterUser registeruser)
        {
            var Token = new UserTokens();
            var isExits = await _userHelper.GetUserByEmailAsync(registeruser.Email);
            if (isExits == null)
            {
                var rest = await _userHelper.CreateAdminAsync(registeruser);
                var credencials = new UserLogins { Password = registeruser.Password };
                if (rest.Succeeded)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = registeruser.Email,
                        EmailId = registeruser.Email,
                        GuiId = Guid.NewGuid(),

                    },
                 _jwtSettings);


                    var msj = _stringLocalizer.GetString("Welcome").Value ?? String.Empty;


                    return Ok(new
                    {

                        Token = Token,
                        Msj = msj
                    });
                }
                else
                {
                    return BadRequest(rest.Errors);
                }
            }
            return BadRequest("Email aready exist");


        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok();
        }
    }

}
