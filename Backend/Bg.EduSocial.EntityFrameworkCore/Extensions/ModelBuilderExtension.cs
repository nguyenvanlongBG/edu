using Microsoft.EntityFrameworkCore;

namespace Bg.EduSocial.EntityFrameworkCore.Extensions
{
    public static class ModelBuilderExtension
    {
        
        public static void SeedUser(ModelBuilder modelBuilder)
        {
            //IPasswordHasher<UserEntity> passwordHasher = new PasswordHasher<UserEntity>();
            //var user = new UserEntity()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    UserName = "",
            //    PasswordHash = "",
            //    PhoneNumber = "1234567890",
            //    NormalizedUserName = "ADMIN",
            //    NormalizedEmail = "ADMIN@GMAIL.COM",
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //};
            //user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");
            //modelBuilder.Entity<UserEntity>().HasData(
            //    user
            //    ) ;
        }
    }
}
