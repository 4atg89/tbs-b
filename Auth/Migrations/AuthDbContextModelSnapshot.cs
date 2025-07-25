﻿// <auto-generated />
using System;
using Auth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Auth.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    partial class AuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Auth.Data.Entity.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<DateTime>("CreatedAt"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("is_verified");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nickname");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("password_hash");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("salt");

                    b.Property<Guid>("SecurityStamp")
                        .HasColumnType("char(36)")
                        .HasColumnName("security_stamp");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Nickname")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Auth.Data.Entity.UserSecurityEntity", b =>
                {
                    b.Property<Guid>("JtiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("jti_id");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("expires_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)")
                        .HasColumnName("user_id");

                    b.HasKey("JtiId");

                    b.HasIndex("ExpiresAt");

                    b.HasIndex("UserId");

                    b.ToTable("user_securities", (string)null);
                });

            modelBuilder.Entity("Auth.Data.Entity.UserSecurityEntity", b =>
                {
                    b.HasOne("Auth.Data.Entity.UserEntity", "User")
                        .WithMany("UserSecurities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Auth.Data.Entity.UserEntity", b =>
                {
                    b.Navigation("UserSecurities");
                });
#pragma warning restore 612, 618
        }
    }
}
