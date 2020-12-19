﻿// <auto-generated />
using System;
using ContactsApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContactsApi.Database.Migrations
{
    [DbContext(typeof(AddressBookContext))]
    [Migration("20201219203550_ContactDataWithSeed")]
    partial class ContactDataWithSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ContactsApi.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("AddressNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("address_number");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("country");

                    b.Property<DateTime>("CreatedOrUpdated")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_or_updated");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("first_name");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("postcode");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("street");

                    b.Property<string>("Surname")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("surname");

                    b.HasKey("Id")
                        .HasName("pk_contacts");

                    b.HasIndex("FirstName", "Surname", "Street", "AddressNumber", "Postcode", "City", "Country")
                        .IsUnique()
                        .HasDatabaseName("contact_uidx");

                    b.ToTable("contacts");

                    b
                        .HasComment("Basic information about contacts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressNumber = "8106",
                            City = "Schenectady",
                            Country = "New York, US",
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 814, DateTimeKind.Local).AddTicks(8038),
                            DateOfBirth = new DateTime(1964, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Keanu",
                            Postcode = "12302",
                            Street = "Linda Ave.",
                            Surname = "Reeves"
                        },
                        new
                        {
                            Id = 2,
                            AddressNumber = "7201",
                            City = "Easton",
                            Country = "Pennsylvania, US",
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 818, DateTimeKind.Local).AddTicks(6608),
                            DateOfBirth = new DateTime(1981, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Roger",
                            Postcode = "18042",
                            Street = "N. Roehampton Ave.",
                            Surname = "Federer"
                        },
                        new
                        {
                            Id = 3,
                            AddressNumber = "57",
                            City = "Bern",
                            Country = "Switzerland",
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 818, DateTimeKind.Local).AddTicks(6654),
                            DateOfBirth = new DateTime(1971, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Mark",
                            Postcode = "3027",
                            Street = "Erlenweg",
                            Surname = "Wahlberg"
                        },
                        new
                        {
                            Id = 4,
                            AddressNumber = "3357",
                            City = "Schenectady",
                            Country = "New York, US",
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 818, DateTimeKind.Local).AddTicks(6660),
                            FirstName = "Superman",
                            Postcode = "12303",
                            Street = "Golden Ridge Road"
                        },
                        new
                        {
                            Id = 5,
                            AddressNumber = "4597",
                            City = "Fort Lauderdale",
                            Country = "Florida, US",
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 818, DateTimeKind.Local).AddTicks(6665),
                            DateOfBirth = new DateTime(1955, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Bill",
                            Postcode = "33308",
                            Street = "Pointe Lane",
                            Surname = "Gates"
                        });
                });

            modelBuilder.Entity("ContactsApi.Models.ContactData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ContactDataStatus")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("contact_data_status")
                        .HasComment("Flag which tells if a contact data is active or marked as deleted");

                    b.Property<string>("ContactDataType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("contact_data_type")
                        .HasComment("Code which designates type of contact data (e.g. phone, mail, ...)");

                    b.Property<string>("ContactDataValue")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("contact_data_value")
                        .HasComment("Concrete value of contact data (e.g. contact's phone number)");

                    b.Property<int>("ContactId")
                        .HasColumnType("integer")
                        .HasColumnName("contact_id");

                    b.Property<DateTime>("CreatedOrUpdated")
                        .IsConcurrencyToken()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_or_updated");

                    b.HasKey("Id")
                        .HasName("pk_contact_data");

                    b.HasAlternateKey("ContactId", "ContactDataType", "ContactDataValue")
                        .HasName("ak_contact_data_contact_id_contact_data_type_contact_data_value");

                    b.ToTable("contact_data");

                    b
                        .HasComment("Additional information about contacts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactDataStatus = "Y",
                            ContactDataType = "PHONE",
                            ContactDataValue = "0900000000",
                            ContactId = 1,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(1841)
                        },
                        new
                        {
                            Id = 2,
                            ContactDataStatus = "Y",
                            ContactDataType = "PHONE",
                            ContactDataValue = "0900000001",
                            ContactId = 1,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2537)
                        },
                        new
                        {
                            Id = 3,
                            ContactDataStatus = "Y",
                            ContactDataType = "MAIL",
                            ContactDataValue = "keanu.reeves@mail.com",
                            ContactId = 1,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2555)
                        },
                        new
                        {
                            Id = 4,
                            ContactDataStatus = "Y",
                            ContactDataType = "PHONE",
                            ContactDataValue = "0900000100",
                            ContactId = 2,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2560)
                        },
                        new
                        {
                            Id = 5,
                            ContactDataStatus = "Y",
                            ContactDataType = "MAIL",
                            ContactDataValue = "roger.federer@mail.com",
                            ContactId = 2,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2565)
                        },
                        new
                        {
                            Id = 6,
                            ContactDataStatus = "Y",
                            ContactDataType = "MAIL",
                            ContactDataValue = "roger.federer@anothermail.com",
                            ContactId = 2,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2569)
                        },
                        new
                        {
                            Id = 7,
                            ContactDataStatus = "Y",
                            ContactDataType = "PHONE",
                            ContactDataValue = "0900000200",
                            ContactId = 3,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2573)
                        },
                        new
                        {
                            Id = 8,
                            ContactDataStatus = "Y",
                            ContactDataType = "MAIL",
                            ContactDataValue = "mark.wahlberg@mail.com",
                            ContactId = 3,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2577)
                        },
                        new
                        {
                            Id = 9,
                            ContactDataStatus = "Y",
                            ContactDataType = "MAIL",
                            ContactDataValue = "superman@mail.com",
                            ContactId = 4,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2581)
                        },
                        new
                        {
                            Id = 10,
                            ContactDataStatus = "Y",
                            ContactDataType = "PHONE",
                            ContactDataValue = "0900000002",
                            ContactId = 4,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2585)
                        },
                        new
                        {
                            Id = 11,
                            ContactDataStatus = "Y",
                            ContactDataType = "PHONE",
                            ContactDataValue = "0901000100",
                            ContactId = 5,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2589)
                        },
                        new
                        {
                            Id = 12,
                            ContactDataStatus = "Y",
                            ContactDataType = "MAIL",
                            ContactDataValue = "bill.gates@mail.com",
                            ContactId = 5,
                            CreatedOrUpdated = new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2593)
                        });
                });

            modelBuilder.Entity("ContactsApi.Models.ContactData", b =>
                {
                    b.HasOne("ContactsApi.Models.Contact", null)
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .HasConstraintName("fk_contact_data_contacts_contact_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}