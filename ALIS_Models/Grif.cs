using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_Models
{
    public partial class Grif
    {
        public Grif()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string Name { get; set; }
        public bool? IsArchive { get; set; }

        [InverseProperty(nameof(Book.Grif))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
