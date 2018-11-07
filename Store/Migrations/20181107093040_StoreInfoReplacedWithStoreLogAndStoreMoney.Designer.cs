﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Context;

namespace Store.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20181107093040_StoreInfoReplacedWithStoreLogAndStoreMoney")]
    partial class StoreInfoReplacedWithStoreLogAndStoreMoney
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Store.Models.ProductTypes", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PropertyName");

                    b.HasKey("PropertyId");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("Store.Models.StoreLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SalesLog");

                    b.HasKey("Id");

                    b.ToTable("StoreLog");
                });

            modelBuilder.Entity("Store.Models.StoreMoney", b =>
                {
                    b.Property<decimal>("StoreCashSupply");

                    b.HasKey("StoreCashSupply");

                    b.ToTable("StoreMoney");
                });

            modelBuilder.Entity("Store.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand");

                    b.Property<int>("InStock");

                    b.Property<int>("MaxStock");

                    b.Property<decimal>("Overcharge");

                    b.Property<decimal>("Price");

                    b.Property<int>("Type");

                    b.HasKey("ProductID");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
