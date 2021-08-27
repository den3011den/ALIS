using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_DataAccess
{
    [Table("Book_Copies")]
    public partial class BookCopy
    {
        public BookCopy()
        {
            BookCopiesCirculations = new HashSet<BookCopiesCirculation>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string Barcode { get; set; }
        [Column(TypeName = "character varying")]
        public string InventoryNumber { get; set; }
        public bool? IsObligatoryCopy { get; set; }
        [Column("CurrentHolder_id")]
        public int CurrentHolderId { get; set; }
        [Column("Book_id")]
        public int BookId { get; set; }
        public bool? IsArchive { get; set; }

        [ForeignKey(nameof(BookId))]
        [InverseProperty("BookCopies")]
        public virtual Book Book { get; set; }
        [ForeignKey(nameof(CurrentHolderId))]
        [InverseProperty(nameof(Person.BookCopies))]
        public virtual Person CurrentHolder { get; set; }
        [InverseProperty(nameof(BookCopiesCirculation.BookCopy))]
        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculations { get; set; }
    }
}
