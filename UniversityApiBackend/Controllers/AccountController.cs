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
        private readonly UniversityDbContext _context;
        private readonly IStringLocalizer<AccountController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;
        public AccountController(JwtSettings jwtSettings,
            IStringLocalizer<AccountController> stringLocalizer,
            IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _jwtSettings = jwtSettings;
            _stringLocalizer = stringLocalizer;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        private IEnumerable<User> Logins = new List<User>()
         {
              new User()
              {
                  Id = 1,
                  Email="matin@imaginagroup.com",
                  Name="Admin",
                  Password="Admin"
              },
               new User()
              {
                  Id = 2,
                  Email="pepe@imaginagroup.com",
                  Name="User2",
                  Password="pepe"
              },

         };


        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                var valid = Logins.Any(user => user.Name.Equals(userLogins.Password, StringComparison.OrdinalIgnoreCase));
                if (valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
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


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }

}
