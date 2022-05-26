using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UniversityApiBackend.DTOs;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetAuthenticaedUserAsync(ClaimsPrincipal User);
        string GetAuthenticaedUserName(ClaimsPrincipal User);
       
        Task<User> GetUserByEmailAsync(string email);
        Task<IdentityResult> RegisterUserAsync(RegisterUser registeruser);
        Task<IdentityResult> CreateAdminAsync(RegisterUser registeruser);
   
        Task<SignInResult> PasswordSignInAsync(UserLogins credentials);
        Task<UserTokens> BuildTokenAsync(UserLogins credentials);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
        Task<string> GeneratePasswordResetTokenAsync(User user);


    }
}
