using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.DTOs.Account;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ClaimsIdentity _claimsIdentity;
        private readonly UniversityDbContext _context;
        private readonly IConfiguration _configuration;
        public UserHelper(
         UniversityDbContext dataContext,
         UserManager<User> userManager,
         RoleManager<IdentityRole> roleManager,
         SignInManager<User> signInManager,
           IConfiguration configuration)
        {
            _context = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        public Task<UserTokens> BuildTokenAsync(UserLogins credentials)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAdminAsync(RegisterUser registeruser)
        {
            var user = new User
            {
                UserName = registeruser.Email,
                Email = registeruser.Email,
                Name = registeruser.FirstName,
                LastName = registeruser.LastName,
            };

            var rest = await _userManager.CreateAsync(user, registeruser.Password);
            if (rest.Succeeded)
                await _userManager.AddClaimAsync(user, new Claim("role", "admin"));

            return rest;
        }
        public async Task<IdentityResult> RegisterUserAsync(RegisterUser registeruser)
        {
            var user = new User
            {
                UserName = registeruser.Email,
                Email = registeruser.Email,
                Name = registeruser.FirstName,
                LastName = registeruser.LastName,
            };

            var rest = await _userManager.CreateAsync(user, registeruser.Password);
            if (rest.Succeeded)
                await _userManager.AddClaimAsync(user, new Claim("role", "user"));

            return rest;
        }




        public Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAuthenticaedUserAsync(ClaimsPrincipal User)
        {
            var authenticatedUser = User.Identities.Select(c => c.Claims).ToArray()[0].ToArray()[0].Value;

            return await _userManager.FindByEmailAsync(authenticatedUser);
        }

        public string GetAuthenticaedUserName(ClaimsPrincipal User)
        {
            return User.Identities.Select(c => c.Claims).ToArray()[0].ToArray()[0].Value;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user;
        }

        public async Task<SignInResult> PasswordSignInAsync(UserLogins credentials)
        {
            return await _signInManager.PasswordSignInAsync(credentials.UserName, credentials.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }
        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<IList<Claim>> GetRoleAsync(User user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterStudent registerStudent)
        {




            try
            {

                var user = new User
                {
                    UserName = registerStudent.Email,
                    Email = registerStudent.Email,
                    Name = registerStudent.FirstName,
                    LastName = registerStudent.LastName,
                };

                var address = new Address
                {
                    City = registerStudent.City,
                    Country = registerStudent.Country,
                    Street = registerStudent.Street,
                    ZipCode = registerStudent.ZipCode,
                    Comunity = registerStudent.Comunity,
                };


                var rest = await _userManager.CreateAsync(user, registerStudent.Password);
                if (rest.Succeeded)
                {
                    _context.Addresses.Add(address);


                    await _context.SaveChangesAsync();
                    await _userManager.AddClaimAsync(user, new Claim("role", "user"));

                    var student = new Student
                    {
                        User = user,
                        Address = address,
                        Dob = registerStudent.Dob,
                        CreatedAt=DateTime.Now,
                    };
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                }

                return rest;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
