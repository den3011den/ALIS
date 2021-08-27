using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Наименование должно быть заполнено")]
        [DisplayName("Наименование грифа")]
        public string Name { get; set; }

        [DisplayName("В архиве")]
        public bool? IsArchive { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
