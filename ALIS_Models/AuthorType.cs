using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_Models
{
    [Table("Author_Types")]
    [Microsoft.EntityFrameworkCore.Index(nameof(Name), Name = "IX_Author_Types_name")]
    public partial class AuthorType
    {
        public AuthorType()
        {
            BooksToAuthors = new HashSet<BooksToAuthor>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ввод наименования обязателен")]
        [Column(TypeName = "character varying")]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        public bool? IsArchive { get; set; }

        [InverseProperty(nameof(BooksToAuthor.AuthorType))]
        public virtual ICollection<BooksToAuthor> BooksToAuthors { get; set; }
    }
}
