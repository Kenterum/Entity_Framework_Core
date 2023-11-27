﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Seeding.Migrations
{
    [DbContext(typeof(ApplicationsDbContext))]
    partial class ApplicationsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Blogs", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Url = "www.google.com/blog"
                        },
                        new
                        {
                            Id = 2,
                            Url = "www.com.com/deneme"
                        });
                });

            modelBuilder.Entity("Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BlogId = 1,
                            Content = "....",
                            Title = "A"
                        },
                        new
                        {
                            Id = 2,
                            BlogId = 1,
                            Content = "....",
                            Title = "B"
                        });
                });

            modelBuilder.Entity("Post", b =>
                {
                    b.HasOne("Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("Blog", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
