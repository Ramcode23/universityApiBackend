using UniversityApiBackend.DTOs.Account;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class DataSeed
    {
        private readonly IUserHelper _userHelper;
        public DataSeed(IUserHelper userHelper)
        {
            _userHelper=userHelper;
        }


        public async Task SeedAsync()
        {

           await CheckRoles();
          await  CheckUserAdmin();

        }

        private async Task  CheckUserAdmin()
        {
            var user = new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Admin",
                LastName = "Admin",
                Password = "Pass1234@"
            };


            var userExits = await _userHelper.GetUserByEmailAsync(user.Email);
            await _userHelper.CheckRoleAsync("Administrator");
            await _userHelper.CheckRoleAsync("User");




            if (userExits == null)
            {
                await _userHelper.CreateAdminAsync(new RegisterUser
                {
                    Email = user.Email,
                    FirstName = "User",
                    LastName = "Admin",
                    Password = user.Password

                });


                await _userHelper.AddUserToRoleAsync(user, "Admin");

            }
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("User");
        }

    }
}
