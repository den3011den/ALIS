using ALIS_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ALIS_DataAccess.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorType> AuthorTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopiesCirculation> BookCopiesCirculations { get; set; }
        public DbSet<BookCopiesOperationType> BookCopiesOperationTypes { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<BooksToAuthor> BooksToAuthors { get; set; }
        public DbSet<BooksToTag> BooksToTags { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Grif> Grifs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Tag> Tags { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Alis_v2;Username=ALIS_v2_User;Password=Qwerty123");
             
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasComment("Справочник авторов (корректоров, редакторов, соавторов и тп)");

                entity.Property(e => e.Id)
                    .HasComment("Идентификатор автора")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.Name).HasComment("Наименование автора");
            });

            modelBuilder.Entity<AuthorType>(entity =>
            {
                entity.HasComment("Справочник типов авторства");

                entity.Property(e => e.Id)
                    .HasComment("Идентификатор типа автора")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.Name).HasComment("Наименование типа автора");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasComment("Книги");

                entity.Property(e => e.Id)
                    .HasComment("Уникальный идентификатор книги")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Bbk).HasComment("Библиотечно-библиографическая классификация");

                entity.Property(e => e.CopyrightMark).HasComment("Копирайт марк");

                entity.Property(e => e.Description).HasComment("Описание книги");

                entity.Property(e => e.GenreId).HasComment("id жанра книги (Genres)");

                entity.Property(e => e.GrifId).HasComment("id грифа (Grifs)");

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.Isbn).HasComment("ISBN");

                entity.Property(e => e.Name).HasComment("Наименование книги");

                entity.Property(e => e.NumberOfAvailableCopies).HasComment("Количество доступных (не выданных, остаток) экземпляров книги");

                entity.Property(e => e.NumberOfCopies).HasComment("Количество всего экземпляров книги на учёте в библиотеке");

                entity.Property(e => e.NumberOfPages).HasComment("Количество страниц в книге");

                entity.Property(e => e.PublicationYear).HasComment("Год публикации");

                entity.Property(e => e.PublisherId).HasComment("id издателя книги (Publishers)");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_Genre_Id_To_Genres_Id");

                entity.HasOne(d => d.Grif)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GrifId)
                    .HasConstraintName("FK_Books_Grif_Id_To_Grifs_Id");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_Publisher_Id_To_Publishers_Id");
            });

            modelBuilder.Entity<BookCopiesCirculation>(entity =>
            {
                entity.HasComment("Операции с экземплярами книг");

                entity.Property(e => e.Id)
                    .HasComment("Идентификатор операции")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BookCopiesOperationTypeId).HasComment("id типа операции (Book_Copies_Operation_Types)");

                entity.Property(e => e.BookCopyId).HasComment("id экземпляра книги, с которой произведена операция (Bood_Copies)");

                entity.Property(e => e.ForWhomId).HasComment("id контрагента операции (Persons)");

                entity.Property(e => e.OperationDate).HasComment("Дата/время операции");

                entity.Property(e => e.WhoDidId).HasComment("id кто зарегистрировал операцию (Persons)");

                entity.HasOne(d => d.BookCopiesOperationType)
                    .WithMany(p => p.BookCopiesCirculations)
                    .HasForeignKey(d => d.BookCopiesOperationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_Copies_Circulation_BookCopiesOperationType_id_To_Book_C");

                entity.HasOne(d => d.BookCopy)
                    .WithMany(p => p.BookCopiesCirculations)
                    .HasForeignKey(d => d.BookCopyId)
                    .HasConstraintName("FK_Book_Copies_Circulation_BookCopy_Id_To_Book_Copies_Id");

                entity.HasOne(d => d.ForWhom)
                    .WithMany(p => p.BookCopiesCirculationForWhoms)
                    .HasForeignKey(d => d.ForWhomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_Copies_Circulation_ForWhom_id_To_Persons_Id");

                entity.HasOne(d => d.WhoDid)
                    .WithMany(p => p.BookCopiesCirculationWhoDids)
                    .HasForeignKey(d => d.WhoDidId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_Copies_Circulation_WhoDid_id_To_Persons_Id");
            });

            modelBuilder.Entity<BookCopiesOperationType>(entity =>
            {
                entity.HasComment("Спрасочник типов (видов) операций с экземплярами книг");

                entity.Property(e => e.Id)
                    .HasComment("Идентификатор типа операции")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.IsOutOperation).HasComment("Признак является ли операция выдачей (выходом с библиотеки)");

                entity.Property(e => e.Name).HasComment("Наименование операции");
            });

            modelBuilder.Entity<BookCopy>(entity =>
            {
                entity.HasComment("Экземпляры книг");

                entity.Property(e => e.Id)
                    .HasComment("Идентификатор экземпляра книги")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Barcode).HasComment("Штрих-код");

                entity.Property(e => e.BookId).HasComment("id книги (Books)");

                entity.Property(e => e.CurrentHolderId).HasComment("Id текущего держателя экземпляра книги (Persons)");

                entity.Property(e => e.InventoryNumber).HasComment("Инвентарный номер");

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.IsObligatoryCopy).HasComment("Признак невыдаваемого за пределы библиотеки экземпляра");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookCopies)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_Copies_Book_id_To_Books_id");

                entity.HasOne(d => d.CurrentHolder)
                    .WithMany(p => p.BookCopies)
                    .HasForeignKey(d => d.CurrentHolderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_Copies_CurrentHolder_id_To_Persons_id");
            });

            modelBuilder.Entity<BooksToAuthor>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.AuthorId, e.AuthorTypeId })
                    .HasName("Books_To_Authors_pkey");

                entity.HasComment("Связь авторов и книг");

                entity.Property(e => e.BookId).HasComment("id книги (Books)");

                entity.Property(e => e.AuthorId).HasComment("id автора (Authors)");

                entity.Property(e => e.AuthorTypeId).HasComment("id типа автора (Author_Types)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BooksToAuthors)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_To_Authors_Author_Id_To_Authors_Id");

                entity.HasOne(d => d.AuthorType)
                    .WithMany(p => p.BooksToAuthors)
                    .HasForeignKey(d => d.AuthorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_To_Authors_AuthorType_Id_To_Author_Types_Id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BooksToAuthors)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_To_Authors_Book_Id_To_Books_Id");
            });

            modelBuilder.Entity<BooksToTag>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.TagId })
                    .HasName("Books_To_Tags_pkey");

                entity.HasComment("Привязка тэгов к книгам");

                entity.Property(e => e.BookId).HasComment("id книги (Books)");

                entity.Property(e => e.TagId).HasComment("id тэга (Tags)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BooksToTags)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_To_Tags_Book_Id_To_Books_Id");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.BooksToTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_To_Tags_Tag_Id_To_Tags_Id");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasComment("Справочник жанров");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("Идентификатор жанра");

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.Name).HasComment("Наименование жанра");
            });

            modelBuilder.Entity<Grif>(entity =>
            {
                entity.HasComment("Справочник грифов");

                entity.Property(e => e.Id)
                    .HasComment("Идентификатор грифа")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.Name).HasComment("Наименование грифа");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasComment("Персоны");

                entity.Property(e => e.Id)
                    .HasComment("Идентификатор персоны")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AltPhoneNumber).HasComment("Второй номер телефона");

                entity.Property(e => e.Barcode).HasComment("Штрих-код");

                entity.Property(e => e.Birthday).HasComment("День рождения");

                entity.Property(e => e.CreateDate).HasComment("Дата создания");

                entity.Property(e => e.Email).HasComment("e-mail");

                entity.Property(e => e.GroupNumber).HasComment("Номер учебной группы");

                entity.Property(e => e.HomeAddres).HasComment("Адрес фактического проживания");

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.Name).HasComment("Имя");

                entity.Property(e => e.PasportIssueDate).HasComment("Дата выдачи паспорта");

                entity.Property(e => e.PasportIssuedBy).HasComment("Кем выдан паспорт");

                entity.Property(e => e.PasportNumber).HasComment("Номер паспорта");

                entity.Property(e => e.PasportSerial).HasComment("Серия паспорта");

                entity.Property(e => e.Patronymic).HasComment("Отчество");

                entity.Property(e => e.PhoneNumber).HasComment("Номер телефона");

                entity.Property(e => e.Surname).HasComment("Фамилия");

                entity.Property(e => e.UserId).HasComment("User_id");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasComment("Издатели");

                entity.Property(e => e.Id)
                    .HasComment("Идентификатор издателя")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.Name).HasComment("Наименование издателя");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasComment("Справочник тэгов");

                entity.Property(e => e.Id)
                    .HasComment("Идентификатор тэга")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IsArchive).HasComment("Признак архива");

                entity.Property(e => e.Name).HasComment("Наименование тэга");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
