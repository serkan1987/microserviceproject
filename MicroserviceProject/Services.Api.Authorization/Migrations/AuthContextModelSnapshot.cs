﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.Api.Infrastructure.Authorization.Configuration.Persistence;

namespace Services.Api.Infrastructure.Authorization.Migrations
{
    [DbContext(typeof(AuthContext))]
    partial class AuthContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClaimTypeId")
                        .HasColumnType("int")
                        .HasColumnName("CLAIMTYPEID");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CREATEDATE");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DELETEDATE");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("UPDATEDATE");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("VALUE");

                    b.HasKey("Id");

                    b.HasIndex("ClaimTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("CLAIMS");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.ClaimType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CREATEDATE");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DELETEDATE");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(500)")
                        .HasColumnName("NAME");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("UPDATEDATE");

                    b.HasKey("Id");

                    b.ToTable("CLAIMTYPES");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CREATEDATE");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DELETEDATE");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("NAME");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("UPDATEDATE");

                    b.HasKey("Id");

                    b.ToTable("POLICIES");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.PolicyRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CREATEDATE");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DELETEDATE");

                    b.Property<int>("PolicyId")
                        .HasColumnType("int")
                        .HasColumnName("POLICYID");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("ROLEID");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("UPDATEDATE");

                    b.HasKey("Id");

                    b.HasIndex("PolicyId");

                    b.HasIndex("RoleId");

                    b.ToTable("POLICYROLES");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CREATEDATE");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DELETEDATE");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("NAME");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("UPDATEDATE");

                    b.HasKey("Id");

                    b.ToTable("ROLES");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BeforeSessionId")
                        .HasColumnType("int")
                        .HasColumnName("BEFORESESSIONID");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CREATEDATE");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DELETEDATE");

                    b.Property<string>("GrantType")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("GRANTTYPE");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("IPADDRESS");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit")
                        .HasColumnName("ISVALID");

                    b.Property<int>("RefreshIndex")
                        .HasColumnType("int")
                        .HasColumnName("REFRESHINDEX");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("REFRESHTOKEN");

                    b.Property<string>("Region")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("REGION");

                    b.Property<string>("Scope")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("SCOPE");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TOKEN");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("UPDATEDATE");

                    b.Property<string>("UserAgent")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USERAGENT");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USERID");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2")
                        .HasColumnName("VALIDTO");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SESSIONS");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CREATEDATE");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DELETEDATE");

                    b.Property<string>("Email")
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Password")
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("PASSWORD");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("UPDATEDATE");

                    b.HasKey("Id");

                    b.ToTable("USERS");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CREATEDATE");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DELETEDATE");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("ROLEID");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("UPDATEDATE");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USERID");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("USERROLES");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Claim", b =>
                {
                    b.HasOne("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.ClaimType", "ClaimType")
                        .WithMany("Claims")
                        .HasForeignKey("ClaimTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.User", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClaimType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.PolicyRole", b =>
                {
                    b.HasOne("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Policy", "Policy")
                        .WithMany("PolicyRoles")
                        .HasForeignKey("PolicyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Role", "Role")
                        .WithMany("PolicyRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Policy");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Session", b =>
                {
                    b.HasOne("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.UserRole", b =>
                {
                    b.HasOne("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.ClaimType", b =>
                {
                    b.Navigation("Claims");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Policy", b =>
                {
                    b.Navigation("PolicyRoles");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.Role", b =>
                {
                    b.Navigation("PolicyRoles");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Services.Api.Infrastructure.Authorization.Entities.EntityFramework.User", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Sessions");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
