using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend
{
    public  static class AddJwtTokenServicesExtensions
    {

      public static void AddJwtTokenServices(this IServiceCollection services ,IConfiguration configuration)
        {
            ///Add  jwt Settings 
            var bindJwtSettings = new JwtSettings();
            configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

            //Add Singleton fo JWT settings
            services.AddSingleton(bindJwtSettings);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
              .AddJwtBearer(options =>
              {

                  options.RequireHttpsMetadata = false;
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {

                      ValidateIssuerSigningKey = bindJwtSettings.validateIsUsersSigningKey,
                      IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                      ValidateIssuer = bindJwtSettings.ValidateIssuer,
                      ValidateAudience = bindJwtSettings.ValidateAudience,
                      ValidAudience = bindJwtSettings.ValidAudience,
                      RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                      ValidateLifetime = bindJwtSettings.ValidateLifeTime,
                      ClockSkew = TimeSpan.FromDays(1),
                  };
              });
            
        }
    }
}
