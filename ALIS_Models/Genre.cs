using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_Models
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string Name { get; set; }
        public bool? IsArchive { get; set; }

        [InverseProperty(nameof(Book.Genre))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
