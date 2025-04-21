
using Account.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Model.Database.Configurations;

public static class UserEntityConfiguration
{
    
    public static void Configure(this EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("id");
        
        entity.Property(e => e.Nickname).HasColumnName("nickname").HasMaxLength(50).IsRequired();
        entity.HasIndex(e => e.Nickname).IsUnique();
        
        entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
        entity.HasIndex(e => e.Email).IsUnique();

        entity.Property(e => e.PasswordHash).HasColumnName("password_hash").HasMaxLength(60).IsRequired();
        entity.Property(e => e.Salt).HasColumnName("salt").HasMaxLength(60).IsRequired();

        entity.Property(e => e.CreatedAt).HasColumnName("created_at")
                .ValueGeneratedOnAdd().IsRequired();

        entity.Property(e => e.IsVerified)
                .HasColumnName("is_verified")
                .IsRequired()
                .HasDefaultValue(false);

    }
}