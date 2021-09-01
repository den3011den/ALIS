using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_Models
{
    [Table("Book_Copies_Operation_Types")]
    public partial class BookCopiesOperationType
    {
        public BookCopiesOperationType()
        {
            BookCopiesCirculations = new HashSet<BookCopiesCirculation>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string Name { get; set; }
        public bool? IsOutOperation { get; set; }
        public bool? IsArchive { get; set; }

        [InverseProperty(nameof(BookCopiesCirculation.BookCopiesOperationType))]
        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculations { get; set; }
    }
}
