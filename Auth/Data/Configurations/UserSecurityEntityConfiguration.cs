using Auth.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Model.Database.Configurations;

public static class UserSecurityEntityConfiguration
{

    public static void Configure(this EntityTypeBuilder<UserSecurityEntity> entity)
    {
        entity.ToTable("user_securities");

        entity.HasKey(e => e.JtiId);
        entity.Property(e => e.JtiId).HasColumnName("jti_id");

        entity.Property(e => e.ExpiresAt).HasColumnName("expires_at").IsRequired();

        entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();

        entity.HasOne(e => e.User).WithMany(u => u.UserSecurities)
           .HasForeignKey(e => e.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(e => e.ExpiresAt);
    }
}