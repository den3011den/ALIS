using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Наименование должно быть заполнено")]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "В архиве")]
        public bool? IsArchive { get; set; }

        public virtual ICollection<BooksToTag> BooksToTags { get; set; }
    }
}
