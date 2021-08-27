using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
// экземпляры книг
namespace ALIS_Models
{
    public partial class BookCopy
    {
        public BookCopy()
        {
            BookCopiesCirculations = new HashSet<BookCopiesCirculation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Штрих-код")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "Инвентарный номер должен быть заполнен")]
        [DisplayName("Наименование операции")]
        public string InventoryNumber { get; set; }

        [DisplayName("Невыдаваемый экземпляр")]
        public bool? IsObligatoryCopy { get; set; }

        [Required(ErrorMessage = "Текущий держатель экземпляра не должен быть пустым")]
        [DisplayName("Текущий держатель экземпляра")]
        public int CurrentHolderId { get; set; }

        [Required(ErrorMessage = "Книга должна быть заполнена")]
        [DisplayName("Книга")]
        public int BookId { get; set; }

        [DisplayName("В архиве")]
        public bool? IsArchive { get; set; }

        public virtual Book Book { get; set; }
        public virtual Person CurrentHolder { get; set; }
        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculations { get; set; }
    }
}
