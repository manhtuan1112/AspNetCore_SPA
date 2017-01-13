using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;
using Samples.AspCoreEF.DAL.EF.Models;

namespace Samples.AspCoreEF.DAL.EF.Migrations
{
    [DbContext(typeof(TaskSystemDbContext))]
    [Migration("20170110015842_InitDB")]
    partial class InitDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Samples.AspCoreEF.DAL.EF.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AddedDate");

                    b.Property<string>("IPAddress");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<bool?>("Status");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Samples.AspCoreEF.DAL.EF.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AddedDate");

                    b.Property<string>("Alias")
                        .IsRequired();

                    b.Property<long>("CategoryID");

                    b.Property<string>("Content");

                    b.Property<string>("Description");

                    b.Property<bool?>("HomeFlag");

                    b.Property<bool?>("HotFlag");

                    b.Property<string>("IPAddress");

                    b.Property<string>("Image");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("MoreImages")
                        .HasColumnType("xml");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("OriginalPrice");

                    b.Property<decimal>("Price");

                    b.Property<decimal?>("PromotionPrice");

                    b.Property<int>("Quantity");

                    b.Property<bool?>("Status");

                    b.Property<string>("Tags");

                    b.Property<int?>("ViewCount");

                    b.Property<int?>("Warranty");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Samples.AspCoreEF.DAL.EF.Models.ProductCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AddedDate");

                    b.Property<string>("Alias")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<int?>("DisplayOrder");

                    b.Property<string>("IPAddress");

                    b.Property<string>("Image");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long?>("ParentID");

                    b.Property<bool?>("Status");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("Samples.AspCoreEF.DAL.EF.Models.ProductTag", b =>
                {
                    b.Property<long>("ProductID");

                    b.Property<string>("TagID")
                        .HasMaxLength(50);

                    b.HasKey("ProductID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("ProductTag");
                });

            modelBuilder.Entity("Samples.AspCoreEF.DAL.EF.Models.Tag", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Samples.AspCoreEF.DAL.EF.Models.Task", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AddedDate");

                    b.Property<string>("Description");

                    b.Property<string>("IPAddress");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<long?>("PersonId");

                    b.Property<byte>("State");

                    b.Property<bool?>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Samples.AspCoreEF.DAL.EF.Models.Product", b =>
                {
                    b.HasOne("Samples.AspCoreEF.DAL.EF.Models.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Samples.AspCoreEF.DAL.EF.Models.ProductTag", b =>
                {
                    b.HasOne("Samples.AspCoreEF.DAL.EF.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Samples.AspCoreEF.DAL.EF.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Samples.AspCoreEF.DAL.EF.Models.Task", b =>
                {
                    b.HasOne("Samples.AspCoreEF.DAL.EF.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");
                });
        }
    }
}
