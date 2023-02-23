﻿// <auto-generated />
using System;
using CryptoCurrency.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CryptoCurrency.Migrations
{
    [DbContext(typeof(CryptoDbContext))]
    partial class CryptoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CryptoCurrency.Models.Coin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Change24h")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Volume24h")
                        .HasColumnType("decimal(18, 6)");

                    b.HasKey("Id");

                    b.ToTable("coin");
                });

            modelBuilder.Entity("CryptoCurrency.Models.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoinId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18, 6)");

                    b.HasKey("Id");

                    b.HasIndex("CoinId");

                    b.ToTable("prices");
                });

            modelBuilder.Entity("CryptoCurrency.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("CryptoCurrency.Models.TransactionCoin", b =>
                {
                    b.Property<int>("CoinID")
                        .HasColumnType("int");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 6)");

                    b.HasKey("CoinID", "TransactionId");

                    b.HasIndex("TransactionId");

                    b.ToTable("transactionCoins");
                });

            modelBuilder.Entity("CryptoCurrency.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("CryptoCurrency.Models.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("wallets");
                });

            modelBuilder.Entity("CryptoCurrency.Models.Price", b =>
                {
                    b.HasOne("CryptoCurrency.Models.Coin", "Coin")
                        .WithMany("Prices")
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coin");
                });

            modelBuilder.Entity("CryptoCurrency.Models.Transaction", b =>
                {
                    b.HasOne("CryptoCurrency.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoCurrency.Models.TransactionCoin", b =>
                {
                    b.HasOne("CryptoCurrency.Models.Coin", "Coin")
                        .WithMany("TransactionCoins")
                        .HasForeignKey("CoinID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoCurrency.Models.Transaction", "Transaction")
                        .WithMany("TransactionCoins")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coin");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("CryptoCurrency.Models.Wallet", b =>
                {
                    b.HasOne("CryptoCurrency.Models.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoCurrency.Models.Coin", b =>
                {
                    b.Navigation("Prices");

                    b.Navigation("TransactionCoins");
                });

            modelBuilder.Entity("CryptoCurrency.Models.Transaction", b =>
                {
                    b.Navigation("TransactionCoins");
                });

            modelBuilder.Entity("CryptoCurrency.Models.User", b =>
                {
                    b.Navigation("Transactions");

                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
