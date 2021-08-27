using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ALIS_Models
{
    public partial class BookCopiesCirculation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Тип операции с экземпляром должен быть заполнен")]
        [DisplayName("Тип операции с экземпляром")]
        public int BookCopiesOperationTypeId { get; set; }

        [Required(ErrorMessage = "Дата операции не может быть путой")]
        [DisplayName("Дата операции")]
        public DateTime? OperationDate { get; set; }

        [Required(ErrorMessage = "Исполнитель операции должен быть заполнен")]
        [DisplayName("Исполнитель операции")]
        public int WhoDidId { get; set; }

        [Required(ErrorMessage = "Клиент должен быть заполнен")]
        [DisplayName("Клиент")]
        public int ForWhomId { get; set; }

        [Required(ErrorMessage = "Экземпляр не может быть пустым")]
        [DisplayName("Экземпляр книги")]
        public int? BookCopyId { get; set; }

        public virtual BookCopiesOperationType BookCopiesOperationType { get; set; }
        public virtual BookCopy BookCopy { get; set; }
        public virtual Person ForWhom { get; set; }
        public virtual Person WhoDid { get; set; }
    }
}
