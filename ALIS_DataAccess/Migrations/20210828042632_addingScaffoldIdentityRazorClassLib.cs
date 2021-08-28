using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ALIS_DataAccess.Migrations
{
    public partial class addingScaffoldIdentityRazorClassLib : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            /*
            migrationBuilder.CreateTable(
                name: "Author_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор типа автора")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying", nullable: false, comment: "Наименование типа автора"),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author_Types", x => x.Id);
                },
                comment: "Справочник типов авторства");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор автора")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying", nullable: false, comment: "Наименование автора"),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                },
                comment: "Справочник авторов (корректоров, редакторов, соавторов и тп)");

            migrationBuilder.CreateTable(
                name: "Book_Copies_Operation_Types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор типа операции")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying", nullable: false, comment: "Наименование операции"),
                    IsOutOperation = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак является ли операция выдачей (выходом с библиотеки)"),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Copies_Operation_Types", x => x.id);
                },
                comment: "Спрасочник типов (видов) операций с экземплярами книг");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор жанра"),
                    Name = table.Column<string>(type: "character varying", nullable: false, comment: "Наименование жанра"),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                },
                comment: "Справочник жанров");

            migrationBuilder.CreateTable(
                name: "Grifs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор грифа")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying", nullable: false, comment: "Наименование грифа"),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grifs", x => x.Id);
                },
                comment: "Справочник грифов");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор персоны")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying", nullable: false, comment: "Имя"),
                    Surname = table.Column<string>(type: "character varying", nullable: false, comment: "Фамилия"),
                    Patronymic = table.Column<string>(type: "character varying", nullable: true, comment: "Отчество"),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата создания"),
                    Birthday = table.Column<DateTime>(type: "date", nullable: true, comment: "День рождения"),
                    PhoneNumber = table.Column<string>(type: "character varying", nullable: true, comment: "Номер телефона"),
                    AltPhoneNumber = table.Column<string>(type: "character varying", nullable: true, comment: "Второй номер телефона"),
                    Email = table.Column<string>(type: "character varying", nullable: true, comment: "e-mail"),
                    Barcode = table.Column<string>(type: "character varying", nullable: false, comment: "Штрих-код"),
                    HomeAddres = table.Column<string>(type: "character varying", nullable: true, comment: "Адрес фактического проживания"),
                    GroupNumber = table.Column<string>(type: "character varying", nullable: true, comment: "Номер учебной группы"),
                    PasportSerial = table.Column<string>(type: "character varying", nullable: true, comment: "Серия паспорта"),
                    PasportNumber = table.Column<string>(type: "character varying", nullable: true, comment: "Номер паспорта"),
                    PasportIssuedBy = table.Column<string>(type: "character varying", nullable: true, comment: "Кем выдан паспорт"),
                    PasportIssueDate = table.Column<DateTime>(type: "date", nullable: true, comment: "Дата выдачи паспорта"),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива"),
                    User_id = table.Column<string>(type: "character varying", nullable: true, comment: "User_id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                },
                comment: "Персоны");

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор издателя")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying", nullable: false, comment: "Наименование издателя"),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                },
                comment: "Издатели");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор тэга")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying", nullable: false, comment: "Наименование тэга"),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                },
                comment: "Справочник тэгов");
            */

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            /*
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор книги")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying", nullable: false, comment: "Наименование книги"),
                    Genre_id = table.Column<int>(type: "integer", nullable: false, comment: "id жанра книги (Genres)"),
                    PublicationYear = table.Column<short>(type: "smallint", nullable: false, comment: "Год публикации"),
                    ISBN = table.Column<string>(type: "character varying", nullable: false, comment: "ISBN"),
                    NumberOfPages = table.Column<int>(type: "integer", nullable: false, comment: "Количество страниц в книге"),
                    BBK = table.Column<string>(type: "character varying", nullable: false, comment: "Библиотечно-библиографическая классификация"),
                    Grif_id = table.Column<int>(type: "integer", nullable: true, comment: "id грифа (Grifs)"),
                    CopyrightMark = table.Column<string>(type: "character varying", nullable: true, comment: "Копирайт марк"),
                    Publisher_id = table.Column<int>(type: "integer", nullable: false, comment: "id издателя книги (Publishers)"),
                    Description = table.Column<string>(type: "character varying", nullable: false, comment: "Описание книги"),
                    NumberOfCopies = table.Column<int>(type: "integer", nullable: true, comment: "Количество всего экземпляров книги на учёте в библиотеке"),
                    NumberOfAvailableCopies = table.Column<int>(type: "integer", nullable: true, comment: "Количество доступных (не выданных, остаток) экземпляров книги"),
                    isArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genre_Id_To_Genres_Id",
                        column: x => x.Genre_id,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Grif_Id_To_Grifs_Id",
                        column: x => x.Grif_id,
                        principalTable: "Grifs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Publisher_Id_To_Publishers_Id",
                        column: x => x.Publisher_id,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Книги");

            migrationBuilder.CreateTable(
                name: "Book_Copies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор экземпляра книги")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Barcode = table.Column<string>(type: "character varying", nullable: false, comment: "Штрих-код"),
                    InventoryNumber = table.Column<string>(type: "character varying", nullable: true, comment: "Инвентарный номер"),
                    IsObligatoryCopy = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак невыдаваемого за пределы библиотеки экземпляра"),
                    CurrentHolder_id = table.Column<int>(type: "integer", nullable: false, comment: "Id текущего держателя экземпляра книги (Persons)"),
                    Book_id = table.Column<int>(type: "integer", nullable: false, comment: "id книги (Books)"),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true, comment: "Признак архива")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Copies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Copies_Book_id_To_Books_id",
                        column: x => x.Book_id,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_Copies_CurrentHolder_id_To_Persons_id",
                        column: x => x.CurrentHolder_id,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Экземпляры книг");

            migrationBuilder.CreateTable(
                name: "Books_To_Authors",
                columns: table => new
                {
                    Book_id = table.Column<int>(type: "integer", nullable: false, comment: "id книги (Books)"),
                    Author_id = table.Column<int>(type: "integer", nullable: false, comment: "id автора (Authors)"),
                    AuthorType_id = table.Column<int>(type: "integer", nullable: false, comment: "id типа автора (Author_Types)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Books_To_Authors_pkey", x => new { x.Book_id, x.Author_id, x.AuthorType_id });
                    table.ForeignKey(
                        name: "FK_Books_To_Authors_Author_Id_To_Authors_Id",
                        column: x => x.Author_id,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_To_Authors_AuthorType_Id_To_Author_Types_Id",
                        column: x => x.AuthorType_id,
                        principalTable: "Author_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_To_Authors_Book_Id_To_Books_Id",
                        column: x => x.Book_id,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Связь авторов и книг");

            migrationBuilder.CreateTable(
                name: "Books_To_Tags",
                columns: table => new
                {
                    Book_id = table.Column<int>(type: "integer", nullable: false, comment: "id книги (Books)"),
                    Tag_id = table.Column<int>(type: "integer", nullable: false, comment: "id тэга (Tags)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Books_To_Tags_pkey", x => new { x.Book_id, x.Tag_id });
                    table.ForeignKey(
                        name: "FK_Books_To_Tags_Book_Id_To_Books_Id",
                        column: x => x.Book_id,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_To_Tags_Tag_Id_To_Tags_Id",
                        column: x => x.Tag_id,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Привязка тэгов к книгам");

            migrationBuilder.CreateTable(
                name: "Book_Copies_Circulation",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор операции")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    BookCopiesOperationType_id = table.Column<int>(type: "integer", nullable: false, comment: "id типа операции (Book_Copies_Operation_Types)"),
                    OperationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата/время операции"),
                    WhoDid_id = table.Column<int>(type: "integer", nullable: false, comment: "id кто зарегистрировал операцию (Persons)"),
                    ForWhom_id = table.Column<int>(type: "integer", nullable: false, comment: "id контрагента операции (Persons)"),
                    BookCopy_id = table.Column<int>(type: "integer", nullable: true, comment: "id экземпляра книги, с которой произведена операция (Bood_Copies)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Copies_Circulation", x => x.id);
                    table.ForeignKey(
                        name: "FK_Book_Copies_Circulation_BookCopiesOperationType_id_To_Book_C",
                        column: x => x.BookCopiesOperationType_id,
                        principalTable: "Book_Copies_Operation_Types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_Copies_Circulation_BookCopy_Id_To_Book_Copies_Id",
                        column: x => x.BookCopy_id,
                        principalTable: "Book_Copies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_Copies_Circulation_ForWhom_id_To_Persons_Id",
                        column: x => x.ForWhom_id,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_Copies_Circulation_WhoDid_id_To_Persons_Id",
                        column: x => x.WhoDid_id,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Операции с экземплярами книг");
            */
            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
            /*
            migrationBuilder.CreateIndex(
                name: "IX_Author_Types_name",
                table: "Author_Types",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_name",
                table: "Authors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Copies_Book_id",
                table: "Book_Copies",
                column: "Book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Copies_CurrentHolder_id",
                table: "Book_Copies",
                column: "CurrentHolder_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Copies_Circulation_BookCopiesOperationType_id",
                table: "Book_Copies_Circulation",
                column: "BookCopiesOperationType_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Copies_Circulation_BookCopy_id",
                table: "Book_Copies_Circulation",
                column: "BookCopy_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Copies_Circulation_ForWhom_id",
                table: "Book_Copies_Circulation",
                column: "ForWhom_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Copies_Circulation_WhoDid_id",
                table: "Book_Copies_Circulation",
                column: "WhoDid_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Genre_id",
                table: "Books",
                column: "Genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Grif_id",
                table: "Books",
                column: "Grif_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_name",
                table: "Books",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Publisher_id",
                table: "Books",
                column: "Publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_To_Authors_Author_id",
                table: "Books_To_Authors",
                column: "Author_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_To_Authors_AuthorType_id",
                table: "Books_To_Authors",
                column: "AuthorType_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_To_Tags_Tag_id",
                table: "Books_To_Tags",
                column: "Tag_id");
            */

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            /*
            migrationBuilder.DropTable(
                name: "Book_Copies_Circulation");

            migrationBuilder.DropTable(
                name: "Books_To_Authors");

            migrationBuilder.DropTable(
                name: "Books_To_Tags");
            */

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            /*
            migrationBuilder.DropTable(
                name: "Book_Copies_Operation_Types");

            migrationBuilder.DropTable(
                name: "Book_Copies");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Author_Types");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Grifs");

            migrationBuilder.DropTable(
                name: "Publishers");
            */
        }
    }
}
