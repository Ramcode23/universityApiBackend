namespace UniversityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        public bool validateIsUsersSigningKey { get; set; }
        public string IssuerSigningKey { get; set; }= string.Empty;

        public bool ValidateIssuer { get; set; } = true;
        public string? ValidIsssuer { get; set; }

        public bool ValidateAudience { get; set; }
        public string? ValidAudience { get; set; }

        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifeTime { get; set; }
    }
}
