using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ALIS_Models
{
    public partial class Person
    {
        public Person()
        {
            BookCopies = new HashSet<BookCopy>();
            BookCopiesCirculationForWhoms = new HashSet<BookCopiesCirculation>();
            BookCopiesCirculationWhoDids = new HashSet<BookCopiesCirculation>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя должно быть заполнено")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Фамилия должна быть заполнена")]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [DisplayName("Дата создания")]
        public DateTime CreateDate { get; set; }

        [DisplayName("День рождения")]
        public DateTime? Birthday { get; set; }

        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [DisplayName("Телефон дополнительный")]
        public string AltPhoneNumber { get; set; }

        [DisplayName("Почта")]
        public string Email { get; set; }

        [DisplayName("Штрих-код")]
        public string Barcode { get; set; }

        [DisplayName("Домашний адрес")]
        public string HomeAddres { get; set; }

        [DisplayName("Группа")]
        public string GroupNumber { get; set; }

        [DisplayName("Серия паспорта")]
        public string PasportSerial { get; set; }

        [DisplayName("Номер паспорта")]
        public string PasportNumber { get; set; }

        [DisplayName("Кем выдан")]
        public string PasportIssuedBy { get; set; }

        [DisplayName("Дата выдачи")]
        public DateTime? PasportIssueDate { get; set; }

        [DisplayName("В архиве")]
        public bool? IsArchive { get; set; }

        [DisplayName("Пользователь")]
        public string UserId { get; set; }

        public virtual ICollection<BookCopy> BookCopies { get; set; }
        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculationForWhoms { get; set; }
        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculationWhoDids { get; set; }
    }
}
