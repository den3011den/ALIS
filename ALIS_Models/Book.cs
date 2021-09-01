using System;
using System.Collections.Generic;
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
        [Required]
        [Column(TypeName = "character varying")]
        public string Name { get; set; }
        [Column("Genre_id")]
        public int GenreId { get; set; }
        public short PublicationYear { get; set; }
        [Required]
        [Column("ISBN", TypeName = "character varying")]
        public string Isbn { get; set; }
        public int NumberOfPages { get; set; }
        [Required]
        [Column("BBK", TypeName = "character varying")]
        public string Bbk { get; set; }
        [Column("Grif_id")]
        public int? GrifId { get; set; }
        [Column(TypeName = "character varying")]
        public string CopyrightMark { get; set; }
        [Column("Publisher_id")]
        public int PublisherId { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string Description { get; set; }
        public int? NumberOfCopies { get; set; }
        public int? NumberOfAvailableCopies { get; set; }
        [Column("isArchive")]
        public bool? IsArchive { get; set; }

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
