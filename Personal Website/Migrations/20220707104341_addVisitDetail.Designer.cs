﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Personal_Website.Models;

namespace Personal_Website.Migrations
{
    [DbContext(typeof(PersonlWebsiteDbContext))]
    [Migration("20220707104341_addVisitDetail")]
    partial class addVisitDetail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Personal_Website.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ISO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InternationalName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Personal_Website.Models.IPRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BeginIPAddress")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<long>("EndIPAddress")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("IPRanges");
                });

            modelBuilder.Entity("Personal_Website.Models.VisitDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<long>("DateDetail")
                        .HasColumnType("bigint");

                    b.Property<long>("Minutes")
                        .HasColumnType("bigint");

                    b.Property<int>("VisitorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VisitorId");

                    b.ToTable("VisitDetails");
                });

            modelBuilder.Entity("Personal_Website.Models.Visitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("Personal_Website.Models.IPRange", b =>
                {
                    b.HasOne("Personal_Website.Models.Country", "Country")
                        .WithMany("IpRanges")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Personal_Website.Models.VisitDetail", b =>
                {
                    b.HasOne("Personal_Website.Models.Visitor", "Visitor")
                        .WithMany("VisitDetails")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Personal_Website.Models.Visitor", b =>
                {
                    b.HasOne("Personal_Website.Models.Country", "Country")
                        .WithMany("Visitors")
                        .HasForeignKey("CountryId");
                });
#pragma warning restore 612, 618
        }
    }
}
