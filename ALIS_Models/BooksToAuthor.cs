using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_Models
{
    [Table("Books_To_Authors")]
    public partial class BooksToAuthor
    {
        [Key]
        [Column("Book_id")]
        public int BookId { get; set; }
        [Key]
        [Column("Author_id")]
        public int AuthorId { get; set; }
        [Key]
        [Column("AuthorType_id")]
        public int AuthorTypeId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        [InverseProperty("BooksToAuthors")]
        public virtual Author Author { get; set; }
        [ForeignKey(nameof(AuthorTypeId))]
        [InverseProperty("BooksToAuthors")]
        public virtual AuthorType AuthorType { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey(nameof(BookId))]
        [InverseProperty("BooksToAuthors")]
        public virtual Book Book { get; set; }
    }
}
