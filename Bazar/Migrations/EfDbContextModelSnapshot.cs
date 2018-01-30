using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Bazar.Models.Repository;

namespace Bazar.Migrations
{
    [DbContext(typeof(EfDbContext))]
    partial class EfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bazar.Models.Entities.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogCategoryId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("ImageAlt")
                        .HasMaxLength(100);

                    b.Property<string>("ImageFileName");

                    b.Property<string>("ImageSmallFileName");

                    b.Property<string>("ImageTitle")
                        .HasMaxLength(100);

                    b.Property<string>("LinkTitle")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ArticleId");

                    b.HasIndex("BlogCategoryId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Bazar.Models.Entities.BlogCategory", b =>
                {
                    b.Property<int>("BlogCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("LinkTitle")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("BlogCategoryId");

                    b.ToTable("BlogCategories");
                });

            modelBuilder.Entity("Bazar.Models.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("Message")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Theme");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Bazar.Models.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(4000);

                    b.Property<string>("IconAlt")
                        .HasMaxLength(100);

                    b.Property<string>("IconFileName");

                    b.Property<string>("IconTitle")
                        .HasMaxLength(100);

                    b.Property<string>("IconWhiteFileName");

                    b.Property<string>("ImageAlt")
                        .HasMaxLength(100);

                    b.Property<string>("ImageFileName");

                    b.Property<string>("ImageSmallFileName");

                    b.Property<string>("ImageTitle")
                        .HasMaxLength(100);

                    b.Property<string>("LinkUrl")
                        .HasMaxLength(100);

                    b.Property<int>("Order");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Bazar.Models.Entities.ProductImage", b =>
                {
                    b.Property<int>("ProductImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alt");

                    b.Property<string>("FileName");

                    b.Property<string>("Link");

                    b.Property<int>("ProductId");

                    b.Property<string>("Title");

                    b.HasKey("ProductImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Bazar.Models.Entities.Restoran", b =>
                {
                    b.Property<int>("RestoranId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BtnTitle")
                        .IsRequired();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("ContentTitle")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("RestoranId");

                    b.ToTable("Restoran");
                });

            modelBuilder.Entity("Bazar.Models.Entities.Slide", b =>
                {
                    b.Property<int>("SlideId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alt")
                        .HasMaxLength(100);

                    b.Property<string>("ImgFileName");

                    b.Property<int>("Order");

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("SlideId");

                    b.ToTable("Slides");
                });

            modelBuilder.Entity("Bazar.Models.Entities.Article", b =>
                {
                    b.HasOne("Bazar.Models.Entities.BlogCategory", "BlogCategory")
                        .WithMany("Articles")
                        .HasForeignKey("BlogCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bazar.Models.Entities.ProductImage", b =>
                {
                    b.HasOne("Bazar.Models.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
