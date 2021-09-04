using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        [DisplayName("Наименование")]
        public int Id { get; set; }
        [Column(TypeName = "character varying")]
        [Required(ErrorMessage = "Имя обязательно для заполнения")]         
        [DisplayName("Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [DisplayName("Фамилия")]
        [Column(TypeName = "character varying")]
        public string Surname { get; set; }
        [DisplayName("Отчество")]
        [Column(TypeName = "character varying")]
        public string Patronymic { get; set; }
        [DisplayName("Дата создания")]
        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "date")]
        [DisplayName("Дата рождения")]
        public DateTime? Birthday { get; set; }
        [Column(TypeName = "character varying")]
        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "character varying")]
        [DisplayName("Телефон 2")]
        public string AltPhoneNumber { get; set; }
        [Column(TypeName = "character varying")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        [DisplayName("Штрих-код")]
        public string Barcode { get; set; }
        [Column(TypeName = "character varying")]
        [DisplayName("Домашний адрес")]
        public string HomeAddres { get; set; }
        [Column(TypeName = "character varying")]
        [DisplayName("Группа")]
        public string GroupNumber { get; set; }
        [DisplayName("Серия паспорта")]
        [Column(TypeName = "character varying")]
        public string PasportSerial { get; set; }
        [Column(TypeName = "character varying")]
        [DisplayName("Номер паспорта")]
        public string PasportNumber { get; set; }
        [Column(TypeName = "character varying")]
        [DisplayName("Кем выдан паспорт")]
        public string PasportIssuedBy { get; set; }
        [Column(TypeName = "date")]
        [DisplayName("Дата выдачи паспорта")]
        public DateTime? PasportIssueDate { get; set; }
        [DisplayName("Архив")]
        public bool? IsArchive { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }

        [DisplayName("Пользователь")]
        [Column("User_id", TypeName = "character varying")]
        public string UserId { get; set; }

        [NotMapped]
        [DisplayName("Роль пользователя")]
        public string RoleName { get; set; }

        [NotMapped]
        [DisplayName("Пользователь не активен")]
        public bool IsNotActive { get; set; }


        [InverseProperty(nameof(BookCopy.CurrentHolder))]
        public virtual ICollection<BookCopy> BookCopies { get; set; }
        [InverseProperty(nameof(BookCopiesCirculation.ForWhom))]
        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculationForWhoms { get; set; }
        [InverseProperty(nameof(BookCopiesCirculation.WhoDid))]
        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculationWhoDids { get; set; }
    }
}
