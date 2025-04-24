using Account.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Model.Database.Configurations;

public static class RefreshTokenEntityConfiguration
{

    public static void Configure(this EntityTypeBuilder<RefreshTokenEntity> entity)
    {
        entity.ToTable("refresh_tokens");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Token).HasColumnName("token").HasMaxLength(512).IsRequired();

        entity.Property(e => e.Expires).HasColumnName("expires").IsRequired();

        entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();

        entity.HasOne(e => e.User).WithMany(u => u.RefreshTokens)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(e => e.Expires)
            .IsUnique();
    }
}