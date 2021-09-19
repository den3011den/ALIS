using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_Models
{
    [Microsoft.EntityFrameworkCore.Index(nameof(Name), Name = "IX_Books_name")]
    public partial class Book
    {
        public Book()
        {
            BookCopies = new HashSet<BookCopy>();
            BooksToAuthors = new HashSet<BooksToAuthor>();
            BooksToTags = new HashSet<BooksToTag>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Наименование книги является обязательным")]
        [DisplayName("Название книги")]
        [Column(TypeName = "character varying")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Выбор жанра обязателен")]
        [DisplayName("Жанр")]
        [Column("Genre_id")]
        public int? GenreId { get; set; }
        [DisplayName("Год публикации")]
        public short PublicationYear { get; set; }
        [Required(ErrorMessage = "ISBN обязателен")]
        [DisplayName("ISBN")]
        [Column("ISBN", TypeName = "character varying")]
        public string Isbn { get; set; }
        [DisplayName("Количество страниц")]
        public int NumberOfPages { get; set; }
        [Required(ErrorMessage = "BBK обязателен")]
        [DisplayName("BBK")]
        [Column("BBK", TypeName = "character varying")]
        public string Bbk { get; set; }
        [DisplayName("Гриф")]
        [Column("Grif_id")]
        public int? GrifId { get; set; }
        [Column(TypeName = "character varying")]
        [DisplayName("CopyrightMark")]
        public string CopyrightMark { get; set; }
        [Required(ErrorMessage = "Указание издателя обязательно")]
        [DisplayName("Издатель")]
        [Column("Publisher_id")]
        public int? PublisherId { get; set; }
        [Required(ErrorMessage = "Ввод описания обязателен")]
        [DisplayName("Описание")]
        [Column(TypeName = "character varying")]
        public string Description { get; set; }
        [DisplayName("Кол-во экземпляров")]
        public int? NumberOfCopies { get; set; }
        [DisplayName("Доступно экземпляров")]
        public int? NumberOfAvailableCopies { get; set; }
        [DisplayName("Архив")]
        [Column("isArchive")]
        public bool? IsArchive { get; set; }

        [NotMapped]
        [DisplayName("Выбор")]
        public bool IsSelected { get; set; }

        [NotMapped]
        [DisplayName("Авторы")]
        public string AuthorListString { get; set; }

        [NotMapped]
        [DisplayName("Тэги")]
        public string TagListString { get; set; }



        [ForeignKey(nameof(GenreId))]
        [InverseProperty("Books")]
        public virtual Genre Genre { get; set; }
        [ForeignKey(nameof(GrifId))]
        [InverseProperty("Books")]
        public virtual Grif Grif { get; set; }
        [ForeignKey(nameof(PublisherId))]
        [InverseProperty("Books")]
        public virtual Publisher Publisher { get; set; }
        [InverseProperty(nameof(BookCopy.Book))]
        public virtual ICollection<BookCopy> BookCopies { get; set; }
        [InverseProperty(nameof(BooksToAuthor.Book))]
        public virtual ICollection<BooksToAuthor> BooksToAuthors { get; set; }
        [InverseProperty(nameof(BooksToTag.Book))]
        public virtual ICollection<BooksToTag> BooksToTags { get; set; }
    }
}
