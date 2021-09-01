using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_Models
{
    public partial class Tag
    {
        public Tag()
        {
            BooksToTags = new HashSet<BooksToTag>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string Name { get; set; }
        public bool? IsArchive { get; set; }

        [InverseProperty(nameof(BooksToTag.Tag))]
        public virtual ICollection<BooksToTag> BooksToTags { get; set; }
    }
}
