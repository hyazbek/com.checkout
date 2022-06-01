﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using com.checkout.data;

#nullable disable

namespace com.checkout.data.Migrations
{
    [DbContext(typeof(CKODBContext))]
    partial class CKODBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("com.checkout.data.Model.CardDetails", b =>
                {
                    b.Property<int>("CardDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardDetailsID"), 1L, 1);

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cvv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpiryMonth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpiryYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardDetailsID");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("com.checkout.data.Model.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currencies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrencyCode = "QAR",
                            CurrencyName = "Qatari Riyal"
                        },
                        new
                        {
                            Id = 2,
                            CurrencyCode = "USD",
                            CurrencyName = "US Dollar"
                        },
                        new
                        {
                            Id = 3,
                            CurrencyCode = "GBP",
                            CurrencyName = "British Pound"
                        });
                });

            modelBuilder.Entity("com.checkout.data.Model.Merchant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Merchants", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4fa68f3b-d4e7-4245-8786-31e8a66566fa"),
                            Country = "USA",
                            Name = "Github"
                        },
                        new
                        {
                            Id = new Guid("88703fb9-aa3b-4718-8a73-21a4eb5e7f6c"),
                            Country = "USA",
                            Name = "Amazon"
                        },
                        new
                        {
                            Id = new Guid("20f96956-e5df-4bf8-ac24-65c9f76fbe16"),
                            Country = "UK",
                            Name = "Farfetch"
                        });
                });

            modelBuilder.Entity("com.checkout.data.Model.PaymentRequest", b =>
                {
                    b.Property<int>("PaymentRequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentRequestID"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CardDetailsID")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<string>("MerchantID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentRequestID");

                    b.HasIndex("CardDetailsID");

                    b.ToTable("PaymentRequests");
                });

            modelBuilder.Entity("com.checkout.data.Model.Transaction", b =>
                {
                    b.Property<Guid>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("CardDetailsID")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyID")
                        .HasColumnType("int");

                    b.Property<Guid>("MerchantID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionID");

                    b.HasIndex("CardDetailsID");

                    b.HasIndex("CurrencyID");

                    b.HasIndex("MerchantID");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("com.checkout.data.Model.PaymentRequest", b =>
                {
                    b.HasOne("com.checkout.data.Model.CardDetails", "Card")
                        .WithMany()
                        .HasForeignKey("CardDetailsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("com.checkout.data.Model.Transaction", b =>
                {
                    b.HasOne("com.checkout.data.Model.CardDetails", "CardDetails")
                        .WithMany()
                        .HasForeignKey("CardDetailsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("com.checkout.data.Model.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("com.checkout.data.Model.Merchant", "Merchant")
                        .WithMany()
                        .HasForeignKey("MerchantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardDetails");

                    b.Navigation("Currency");

                    b.Navigation("Merchant");
                });
#pragma warning restore 612, 618
        }
    }
}
