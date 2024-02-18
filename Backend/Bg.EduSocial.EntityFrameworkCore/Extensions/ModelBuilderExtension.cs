using Bg.EduSocial.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.EntityFrameworkCore.Extensions
{
    public static class ModelBuilderExtension
    {
        
        public static void SeedUser(ModelBuilder modelBuilder)
        {
            IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "",
                PasswordHash = "",
                PhoneNumber = "1234567890",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");
            modelBuilder.Entity<User>().HasData(
                user
                ) ;
        }
    }
}
