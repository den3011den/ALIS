using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ALIS_Models
{
    public partial class BookCopiesOperationType
    {
        public BookCopiesOperationType()
        {
            BookCopiesCirculations = new HashSet<BookCopiesCirculation>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование операции должно быть заполнено")]
        [DisplayName("Наименование операции")]
        public string Name { get; set; }

        [DisplayName("Признак выдачи экземпляра")]
        public bool? IsOutOperation { get; set; }

        [DisplayName("В архиве")]
        public bool? IsArchive { get; set; }

        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculations { get; set; }
    }
}
