using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

#nullable disable

namespace ALIS_Models
{
    [Microsoft.EntityFrameworkCore.Index(nameof(Name), Name = "IX_Authors_name")]
    public partial class Author
    {
        public Author()
        {
            BooksToAuthors = new HashSet<BooksToAuthor>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        public bool? IsArchive { get; set; }

        [InverseProperty(nameof(BooksToAuthor.Author))]
        public virtual ICollection<BooksToAuthor> BooksToAuthors { get; set; }
    }
}
