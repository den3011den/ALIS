using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ALIS_Models
{
    public partial class BooksToTag
    {

        [Required]
        [DisplayName("Книга")]
        public int BookId { get; set; }

        [Required]
        [DisplayName("Тэг")]

        public int TagId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
