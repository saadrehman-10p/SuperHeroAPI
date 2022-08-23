using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI.Model
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public byte[] PasswordHash { get; set; }=new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
    }
    public enum UserRole { Admin, ApiUser }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasData(
                new User {Username = "test@gmail.com", Password = "1234",Role= UserRole.ApiUser},
                new User {Username = "abc@gmail.con", Password = "1234" ,Role=UserRole.Admin}
            );
        }
    }
}
