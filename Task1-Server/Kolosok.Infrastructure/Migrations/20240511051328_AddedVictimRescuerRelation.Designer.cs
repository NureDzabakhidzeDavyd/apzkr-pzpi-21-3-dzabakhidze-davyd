﻿// <auto-generated />
using System;
using Kolosok.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kolosok.Infrastructure.Migrations
{
    [DbContext(typeof(KolosokDbContext))]
    [Migration("20240511051328_AddedVictimRescuerRelation")]
    partial class AddedVictimRescuerRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kolosok.Domain.Entities.Action", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ActionPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ActionTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("BrigadeRescuerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("VictimId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BrigadeRescuerId");

                    b.HasIndex("VictimId");

                    b.ToTable("Action");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Brigade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Brigades");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.BrigadeRescuer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BrigadeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BrigadeId");

                    b.HasIndex("ContactId")
                        .IsUnique();

                    b.ToTable("BrigadeRescuers");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Diagnosis", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DetectionTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("VictimId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VictimId");

                    b.ToTable("Diagnoses");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Victim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BrigadeRescuerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BrigadeRescuerId");

                    b.HasIndex("ContactId")
                        .IsUnique();

                    b.ToTable("Victims");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Action", b =>
                {
                    b.HasOne("Kolosok.Domain.Entities.BrigadeRescuer", "BrigadeRescuer")
                        .WithMany("Actions")
                        .HasForeignKey("BrigadeRescuerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kolosok.Domain.Entities.Victim", "Victim")
                        .WithMany("Actions")
                        .HasForeignKey("VictimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BrigadeRescuer");

                    b.Navigation("Victim");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.BrigadeRescuer", b =>
                {
                    b.HasOne("Kolosok.Domain.Entities.Brigade", "Brigade")
                        .WithMany("BrigadeRescuers")
                        .HasForeignKey("BrigadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kolosok.Domain.Entities.Contact", "Contact")
                        .WithOne("BrigadeRescuer")
                        .HasForeignKey("Kolosok.Domain.Entities.BrigadeRescuer", "ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brigade");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Diagnosis", b =>
                {
                    b.HasOne("Kolosok.Domain.Entities.Victim", "Victim")
                        .WithMany("Diagnoses")
                        .HasForeignKey("VictimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Victim");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Victim", b =>
                {
                    b.HasOne("Kolosok.Domain.Entities.BrigadeRescuer", "BrigadeRescuer")
                        .WithMany("Victims")
                        .HasForeignKey("BrigadeRescuerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kolosok.Domain.Entities.Contact", "Contact")
                        .WithOne("Victim")
                        .HasForeignKey("Kolosok.Domain.Entities.Victim", "ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BrigadeRescuer");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Brigade", b =>
                {
                    b.Navigation("BrigadeRescuers");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.BrigadeRescuer", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Victims");
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Contact", b =>
                {
                    b.Navigation("BrigadeRescuer")
                        .IsRequired();

                    b.Navigation("Victim")
                        .IsRequired();
                });

            modelBuilder.Entity("Kolosok.Domain.Entities.Victim", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Diagnoses");
                });
#pragma warning restore 612, 618
        }
    }
}
