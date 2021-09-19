﻿// <auto-generated />
using System;
using ALIS_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ALIS_DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Russian_Russia.1251")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ALIS_Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Идентификатор автора")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasComment("Признак архива");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Наименование автора");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_Authors_name");

                    b.ToTable("Authors");

                    b
                        .HasComment("Справочник авторов (корректоров, редакторов, соавторов и тп)");
                });

            modelBuilder.Entity("ALIS_Models.AuthorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Идентификатор типа автора")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasComment("Признак архива");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Наименование типа автора");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_Author_Types_name");

                    b.ToTable("Author_Types");

                    b
                        .HasComment("Справочник типов авторства");
                });

            modelBuilder.Entity("ALIS_Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Уникальный идентификатор книги")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("Bbk")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("BBK")
                        .HasComment("Библиотечно-библиографическая классификация");

                    b.Property<string>("CopyrightMark")
                        .HasColumnType("character varying")
                        .HasComment("Копирайт марк");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Описание книги");

                    b.Property<int?>("GenreId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("Genre_id")
                        .HasComment("id жанра книги (Genres)");

                    b.Property<int?>("GrifId")
                        .HasColumnType("integer")
                        .HasColumnName("Grif_id")
                        .HasComment("id грифа (Grifs)");

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasColumnName("isArchive")
                        .HasComment("Признак архива");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasColumnName("ISBN")
                        .HasComment("ISBN");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Наименование книги");

                    b.Property<int?>("NumberOfAvailableCopies")
                        .HasColumnType("integer")
                        .HasComment("Количество доступных (не выданных, остаток) экземпляров книги");

                    b.Property<int?>("NumberOfCopies")
                        .HasColumnType("integer")
                        .HasComment("Количество всего экземпляров книги на учёте в библиотеке");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("integer")
                        .HasComment("Количество страниц в книге");

                    b.Property<short>("PublicationYear")
                        .HasColumnType("smallint")
                        .HasComment("Год публикации");

                    b.Property<int?>("PublisherId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("Publisher_id")
                        .HasComment("id издателя книги (Publishers)");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("GrifId");

                    b.HasIndex("PublisherId");

                    b.HasIndex(new[] { "Name" }, "IX_Books_name");

                    b.ToTable("Books");

                    b
                        .HasComment("Книги");
                });

            modelBuilder.Entity("ALIS_Models.BookCopiesCirculation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasComment("Идентификатор операции")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<int>("BookCopiesOperationTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("BookCopiesOperationType_id")
                        .HasComment("id типа операции (Book_Copies_Operation_Types)");

                    b.Property<int?>("BookCopyId")
                        .HasColumnType("integer")
                        .HasColumnName("BookCopy_id")
                        .HasComment("id экземпляра книги, с которой произведена операция (Bood_Copies)");

                    b.Property<int>("ForWhomId")
                        .HasColumnType("integer")
                        .HasColumnName("ForWhom_id")
                        .HasComment("id контрагента операции (Persons)");

                    b.Property<DateTime?>("OperationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата/время операции");

                    b.Property<int>("WhoDidId")
                        .HasColumnType("integer")
                        .HasColumnName("WhoDid_id")
                        .HasComment("id кто зарегистрировал операцию (Persons)");

                    b.HasKey("Id");

                    b.HasIndex("BookCopiesOperationTypeId");

                    b.HasIndex("BookCopyId");

                    b.HasIndex("ForWhomId");

                    b.HasIndex("WhoDidId");

                    b.ToTable("Book_Copies_Circulation");

                    b
                        .HasComment("Операции с экземплярами книг");
                });

            modelBuilder.Entity("ALIS_Models.BookCopiesOperationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasComment("Идентификатор типа операции")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasComment("Признак архива");

                    b.Property<bool?>("IsOutOperation")
                        .HasColumnType("boolean")
                        .HasComment("Признак является ли операция выдачей (выходом с библиотеки)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Наименование операции");

                    b.HasKey("Id");

                    b.ToTable("Book_Copies_Operation_Types");

                    b
                        .HasComment("Спрасочник типов (видов) операций с экземплярами книг");
                });

            modelBuilder.Entity("ALIS_Models.BookCopy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Идентификатор экземпляра книги")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Штрих-код");

                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("Book_id")
                        .HasComment("id книги (Books)");

                    b.Property<int>("CurrentHolderId")
                        .HasColumnType("integer")
                        .HasColumnName("CurrentHolder_id")
                        .HasComment("Id текущего держателя экземпляра книги (Persons)");

                    b.Property<string>("InventoryNumber")
                        .HasColumnType("character varying")
                        .HasComment("Инвентарный номер");

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasComment("Признак архива");

                    b.Property<bool?>("IsObligatoryCopy")
                        .HasColumnType("boolean")
                        .HasComment("Признак невыдаваемого за пределы библиотеки экземпляра");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("CurrentHolderId");

                    b.ToTable("Book_Copies");

                    b
                        .HasComment("Экземпляры книг");
                });

            modelBuilder.Entity("ALIS_Models.BooksToAuthor", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("Book_id")
                        .HasComment("id книги (Books)");

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer")
                        .HasColumnName("Author_id")
                        .HasComment("id автора (Authors)");

                    b.Property<int>("AuthorTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("AuthorType_id")
                        .HasComment("id типа автора (Author_Types)");

                    b.HasKey("BookId", "AuthorId", "AuthorTypeId")
                        .HasName("Books_To_Authors_pkey");

                    b.HasIndex("AuthorId");

                    b.HasIndex("AuthorTypeId");

                    b.ToTable("Books_To_Authors");

                    b
                        .HasComment("Связь авторов и книг");
                });

            modelBuilder.Entity("ALIS_Models.BooksToTag", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("Book_id")
                        .HasComment("id книги (Books)");

                    b.Property<int>("TagId")
                        .HasColumnType("integer")
                        .HasColumnName("Tag_id")
                        .HasComment("id тэга (Tags)");

                    b.HasKey("BookId", "TagId")
                        .HasName("Books_To_Tags_pkey");

                    b.HasIndex("TagId");

                    b.ToTable("Books_To_Tags");

                    b
                        .HasComment("Привязка тэгов к книгам");
                });

            modelBuilder.Entity("ALIS_Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Идентификатор жанра")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasComment("Признак архива");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Наименование жанра");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b
                        .HasComment("Справочник жанров");
                });

            modelBuilder.Entity("ALIS_Models.Grif", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Идентификатор грифа")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasComment("Признак архива");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Наименование грифа");

                    b.HasKey("Id");

                    b.ToTable("Grifs");

                    b
                        .HasComment("Справочник грифов");
                });

            modelBuilder.Entity("ALIS_Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Идентификатор персоны")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<string>("AltPhoneNumber")
                        .HasColumnType("character varying")
                        .HasComment("Второй номер телефона");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Штрих-код");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("date")
                        .HasComment("День рождения");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasComment("Дата создания");

                    b.Property<string>("Email")
                        .HasColumnType("character varying")
                        .HasComment("e-mail");

                    b.Property<string>("GroupNumber")
                        .HasColumnType("character varying")
                        .HasComment("Номер учебной группы");

                    b.Property<string>("HomeAddres")
                        .HasColumnType("character varying")
                        .HasComment("Адрес фактического проживания");

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasComment("Признак архива");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Имя");

                    b.Property<DateTime?>("PasportIssueDate")
                        .HasColumnType("date")
                        .HasComment("Дата выдачи паспорта");

                    b.Property<string>("PasportIssuedBy")
                        .HasColumnType("character varying")
                        .HasComment("Кем выдан паспорт");

                    b.Property<string>("PasportNumber")
                        .HasColumnType("character varying")
                        .HasComment("Номер паспорта");

                    b.Property<string>("PasportSerial")
                        .HasColumnType("character varying")
                        .HasComment("Серия паспорта");

                    b.Property<string>("Patronymic")
                        .HasColumnType("character varying")
                        .HasComment("Отчество");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("character varying")
                        .HasComment("Номер телефона");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Фамилия");

                    b.Property<string>("UserId")
                        .HasColumnType("character varying")
                        .HasColumnName("User_id")
                        .HasComment("User_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Persons");

                    b
                        .HasComment("Персоны");
                });

            modelBuilder.Entity("ALIS_Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Идентификатор издателя")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasComment("Признак архива");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Наименование издателя");

                    b.HasKey("Id");

                    b.ToTable("Publishers");

                    b
                        .HasComment("Издатели");
                });

            modelBuilder.Entity("ALIS_Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasComment("Идентификатор тэга")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<bool?>("IsArchive")
                        .HasColumnType("boolean")
                        .HasComment("Признак архива");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying")
                        .HasComment("Наименование тэга");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b
                        .HasComment("Справочник тэгов");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ALIS_Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("ALIS_Models.Book", b =>
                {
                    b.HasOne("ALIS_Models.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_Books_Genre_Id_To_Genres_Id")
                        .IsRequired();

                    b.HasOne("ALIS_Models.Grif", "Grif")
                        .WithMany("Books")
                        .HasForeignKey("GrifId")
                        .HasConstraintName("FK_Books_Grif_Id_To_Grifs_Id");

                    b.HasOne("ALIS_Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .HasConstraintName("FK_Books_Publisher_Id_To_Publishers_Id")
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Grif");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("ALIS_Models.BookCopiesCirculation", b =>
                {
                    b.HasOne("ALIS_Models.BookCopiesOperationType", "BookCopiesOperationType")
                        .WithMany("BookCopiesCirculations")
                        .HasForeignKey("BookCopiesOperationTypeId")
                        .HasConstraintName("FK_Book_Copies_Circulation_BookCopiesOperationType_id_To_Book_C")
                        .IsRequired();

                    b.HasOne("ALIS_Models.BookCopy", "BookCopy")
                        .WithMany("BookCopiesCirculations")
                        .HasForeignKey("BookCopyId")
                        .HasConstraintName("FK_Book_Copies_Circulation_BookCopy_Id_To_Book_Copies_Id");

                    b.HasOne("ALIS_Models.Person", "ForWhom")
                        .WithMany("BookCopiesCirculationForWhoms")
                        .HasForeignKey("ForWhomId")
                        .HasConstraintName("FK_Book_Copies_Circulation_ForWhom_id_To_Persons_Id")
                        .IsRequired();

                    b.HasOne("ALIS_Models.Person", "WhoDid")
                        .WithMany("BookCopiesCirculationWhoDids")
                        .HasForeignKey("WhoDidId")
                        .HasConstraintName("FK_Book_Copies_Circulation_WhoDid_id_To_Persons_Id")
                        .IsRequired();

                    b.Navigation("BookCopiesOperationType");

                    b.Navigation("BookCopy");

                    b.Navigation("ForWhom");

                    b.Navigation("WhoDid");
                });

            modelBuilder.Entity("ALIS_Models.BookCopy", b =>
                {
                    b.HasOne("ALIS_Models.Book", "Book")
                        .WithMany("BookCopies")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_Book_Copies_Book_id_To_Books_id")
                        .IsRequired();

                    b.HasOne("ALIS_Models.Person", "CurrentHolder")
                        .WithMany("BookCopies")
                        .HasForeignKey("CurrentHolderId")
                        .HasConstraintName("FK_Book_Copies_CurrentHolder_id_To_Persons_id")
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("CurrentHolder");
                });

            modelBuilder.Entity("ALIS_Models.BooksToAuthor", b =>
                {
                    b.HasOne("ALIS_Models.Author", "Author")
                        .WithMany("BooksToAuthors")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("FK_Books_To_Authors_Author_Id_To_Authors_Id")
                        .IsRequired();

                    b.HasOne("ALIS_Models.AuthorType", "AuthorType")
                        .WithMany("BooksToAuthors")
                        .HasForeignKey("AuthorTypeId")
                        .HasConstraintName("FK_Books_To_Authors_AuthorType_Id_To_Author_Types_Id")
                        .IsRequired();

                    b.HasOne("ALIS_Models.Book", "Book")
                        .WithMany("BooksToAuthors")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_Books_To_Authors_Book_Id_To_Books_Id")
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("AuthorType");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("ALIS_Models.BooksToTag", b =>
                {
                    b.HasOne("ALIS_Models.Book", "Book")
                        .WithMany("BooksToTags")
                        .HasForeignKey("BookId")
                        .HasConstraintName("FK_Books_To_Tags_Book_Id_To_Books_Id")
                        .IsRequired();

                    b.HasOne("ALIS_Models.Tag", "Tag")
                        .WithMany("BooksToTags")
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_Books_To_Tags_Tag_Id_To_Tags_Id")
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("ALIS_Models.Person", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ALIS_Models.Author", b =>
                {
                    b.Navigation("BooksToAuthors");
                });

            modelBuilder.Entity("ALIS_Models.AuthorType", b =>
                {
                    b.Navigation("BooksToAuthors");
                });

            modelBuilder.Entity("ALIS_Models.Book", b =>
                {
                    b.Navigation("BookCopies");

                    b.Navigation("BooksToAuthors");

                    b.Navigation("BooksToTags");
                });

            modelBuilder.Entity("ALIS_Models.BookCopiesOperationType", b =>
                {
                    b.Navigation("BookCopiesCirculations");
                });

            modelBuilder.Entity("ALIS_Models.BookCopy", b =>
                {
                    b.Navigation("BookCopiesCirculations");
                });

            modelBuilder.Entity("ALIS_Models.Genre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("ALIS_Models.Grif", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("ALIS_Models.Person", b =>
                {
                    b.Navigation("BookCopies");

                    b.Navigation("BookCopiesCirculationForWhoms");

                    b.Navigation("BookCopiesCirculationWhoDids");
                });

            modelBuilder.Entity("ALIS_Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("ALIS_Models.Tag", b =>
                {
                    b.Navigation("BooksToTags");
                });
#pragma warning restore 612, 618
        }
    }
}
