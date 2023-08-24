﻿// <auto-generated />
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Infrastructure.Data.Context;

#nullable disable

namespace Shop.PublicApi.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    partial class WriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Latin1_General_CI_AI")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shop.Domain.Entities.CustomerAggregate.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("DATE");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerTypeId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Shop.Domain.Entities.CustomerTypeAggregate.CustomerType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerTypeCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("TenantId")
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CustomerTypes");
                });

            modelBuilder.Entity("Shop.Domain.Entities.CustomerAggregate.Customer", b =>
                {
                    b.HasOne("Shop.Domain.Entities.CustomerTypeAggregate.CustomerType", "CustomerType")
                        .WithMany()
                        .HasForeignKey("CustomerTypeId");

                    b.OwnsOne("Shop.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(254)
                                .IsUnicode(false)
                                .HasColumnType("varchar(254)")
                                .HasColumnName("Email")
                                .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                                .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                            b1.HasKey("CustomerId");

                            b1.HasIndex("Address")
                                .IsUnique()
                                .HasFilter("[Email] IS NOT NULL");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("CustomerType");

                    b.Navigation("Email");
                });
#pragma warning restore 612, 618
        }
    }
}
