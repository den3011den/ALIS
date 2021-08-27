using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ALIS_Models
{
    public partial class BooksToAuthor
    {

        [Required]
        [DisplayName("Книга")]
        public int BookId { get; set; }

        [Required]
        [DisplayName("Автор")]
        public int AuthorId { get; set; }

        [Required]
        [DisplayName("Тип автора")]
        public int AuthorTypeId { get; set; }

        public virtual Author Author { get; set; }
        public virtual AuthorType AuthorType { get; set; }
        public virtual Book Book { get; set; }
    }
}
