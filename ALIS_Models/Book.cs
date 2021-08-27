using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ALIS_Models
{
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

        [Required(ErrorMessage = "Наименование должно быть заполнено")]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать жанр")]
        [DisplayName("Жанр")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить год публикации")]
        [DisplayName("Год публикации")]
        public short PublicationYear { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить ISBN")]
        [DisplayName("ISBN")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить количество страниц")]
        [DisplayName("Количество страниц")]
        public int NumberOfPages { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить ББК")]
        [DisplayName("ББК")]
        public string Bbk { get; set; }
        public int? GrifId { get; set; }
        public string CopyrightMark { get; set; }

        [Required(ErrorMessage = "Издатель должен быть заполнен")]
        [DisplayName("Издатель")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Описание должно быть заполнен")]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Количество экземпляров")]
        public int? NumberOfCopies { get; set; }

        [DisplayName("Доступно экземпляров")]
        public int? NumberOfAvailableCopies { get; set; }

        [DisplayName("В архиве")]
        public bool? IsArchive { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Grif Grif { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookCopy> BookCopies { get; set; }
        public virtual ICollection<BooksToAuthor> BooksToAuthors { get; set; }
        public virtual ICollection<BooksToTag> BooksToTags { get; set; }
    }
}
