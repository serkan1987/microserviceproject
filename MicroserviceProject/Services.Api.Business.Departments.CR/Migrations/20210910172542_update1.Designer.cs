﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.Api.Business.Departments.CR.Configuration.Persistence;

namespace Services.Api.Business.Departments.CR.Migrations
{
    [DbContext(typeof(CRContext))]
    [Migration("20210910172542_update1")]
    partial class update1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Services.Api.Business.Departments.CR.Entities.EntityFramework.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsLegal")
                        .HasColumnType("bit")
                        .HasColumnName("ISLEGAL");

                    b.Property<string>("MiddleName")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("MIDDLENAME");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("NAME");

                    b.Property<string>("Surname")
                        .HasColumnType("NVARCHAR(100)")
                        .HasColumnName("SURNAME");

                    b.HasKey("Id");

                    b.ToTable("CR_CUSTOMERS");
                });

            modelBuilder.Entity("Services.Api.Business.Departments.CR.Entities.EntityFramework.RollbackEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRolledback")
                        .HasColumnType("bit")
                        .HasColumnName("ISROLLEDBACK");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("TRANSACTION_DATE");

                    b.Property<string>("TransactionIdentity")
                        .HasColumnType("NVARCHAR(100)")
                        .HasColumnName("TRANSACTION_IDENTITY");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int")
                        .HasColumnName("TRANSACTION_TYPE");

                    b.HasKey("Id");

                    b.ToTable("CR_TRANSACTIONS");
                });

            modelBuilder.Entity("Services.Api.Business.Departments.CR.Entities.EntityFramework.RollbackItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DataSet")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("DATA_SET");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Identity")
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("IDENTITY_");

                    b.Property<bool>("IsRolledback")
                        .HasColumnType("bit")
                        .HasColumnName("ISROLLEDBACK");

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("NAME");

                    b.Property<string>("NewValue")
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("NEW_VALUE");

                    b.Property<string>("OldValue")
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("OLD_VALUE");

                    b.Property<int?>("RollbackEntityId")
                        .HasColumnType("int");

                    b.Property<string>("RollbackType")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("ROLLBACK_TYPE");

                    b.Property<string>("TransactionIdentity")
                        .HasColumnType("NVARCHAR(100)")
                        .HasColumnName("TRANSACTION_IDENTITY");

                    b.HasKey("Id");

                    b.HasIndex("RollbackEntityId");

                    b.ToTable("CR_TRANSACTION_ITEMS");
                });

            modelBuilder.Entity("Services.Api.Business.Departments.CR.Entities.EntityFramework.RollbackItemEntity", b =>
                {
                    b.HasOne("Services.Api.Business.Departments.CR.Entities.EntityFramework.RollbackEntity", null)
                        .WithMany("RollbackItems")
                        .HasForeignKey("RollbackEntityId");
                });

            modelBuilder.Entity("Services.Api.Business.Departments.CR.Entities.EntityFramework.RollbackEntity", b =>
                {
                    b.Navigation("RollbackItems");
                });
#pragma warning restore 612, 618
        }
    }
}
