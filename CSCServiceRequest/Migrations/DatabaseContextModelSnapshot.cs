﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CSCServiceRequest.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CSCServiceRequest.Models.ServiceRequest", b =>
                {
                    b.Property<long>("ServiceRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("TechnicalProblemsId")
                        .HasColumnType("bigint");

                    b.HasKey("ServiceRequestId");

                    b.HasIndex("TechnicalProblemsId");

                    b.ToTable("ServiceRequest");
                });

            modelBuilder.Entity("CSCServiceRequest.Models.TechnicalProblems", b =>
                {
                    b.Property<long>("TechnicalProblemsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TechnicalProblemsId");

                    b.ToTable("TechnicalProblems");
                });

            modelBuilder.Entity("CSCServiceRequest.Models.ServiceRequest", b =>
                {
                    b.HasOne("CSCServiceRequest.Models.TechnicalProblems", "TechnicalProblem")
                        .WithMany()
                        .HasForeignKey("TechnicalProblemsId");
                });
#pragma warning restore 612, 618
        }
    }
}
