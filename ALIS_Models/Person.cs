using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string Surname { get; set; }
        [Column(TypeName = "character varying")]
        public string Patronymic { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }
        [Column(TypeName = "character varying")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "character varying")]
        public string AltPhoneNumber { get; set; }
        [Column(TypeName = "character varying")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string Barcode { get; set; }
        [Column(TypeName = "character varying")]
        public string HomeAddres { get; set; }
        [Column(TypeName = "character varying")]
        public string GroupNumber { get; set; }
        [Column(TypeName = "character varying")]
        public string PasportSerial { get; set; }
        [Column(TypeName = "character varying")]
        public string PasportNumber { get; set; }
        [Column(TypeName = "character varying")]
        public string PasportIssuedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PasportIssueDate { get; set; }
        public bool? IsArchive { get; set; }
        [Column("User_id", TypeName = "character varying")]
        public string UserId { get; set; }

        [InverseProperty(nameof(BookCopy.CurrentHolder))]
        public virtual ICollection<BookCopy> BookCopies { get; set; }
        [InverseProperty(nameof(BookCopiesCirculation.ForWhom))]
        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculationForWhoms { get; set; }
        [InverseProperty(nameof(BookCopiesCirculation.WhoDid))]
        public virtual ICollection<BookCopiesCirculation> BookCopiesCirculationWhoDids { get; set; }
    }
}
