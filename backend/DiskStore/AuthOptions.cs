using DiskStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DiskStore.Data
{
    public static class AuthOptions
    {
        public static string ISSUER = "MyAuthServer";
        public static string AUDIENCE = "MyAuthClient";
        public static string KEY = "mysupersecret_secretsecretsecretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
