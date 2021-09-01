using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_Models
{
    [Table("Book_Copies_Circulation")]
    public partial class BookCopiesCirculation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("BookCopiesOperationType_id")]
        public int BookCopiesOperationTypeId { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime? OperationDate { get; set; }
        [Column("WhoDid_id")]
        public int WhoDidId { get; set; }
        [Column("ForWhom_id")]
        public int ForWhomId { get; set; }
        [Column("BookCopy_id")]
        public int? BookCopyId { get; set; }

        [ForeignKey(nameof(BookCopiesOperationTypeId))]
        [InverseProperty("BookCopiesCirculations")]
        public virtual BookCopiesOperationType BookCopiesOperationType { get; set; }
        [ForeignKey(nameof(BookCopyId))]
        [InverseProperty("BookCopiesCirculations")]
        public virtual BookCopy BookCopy { get; set; }
        [ForeignKey(nameof(ForWhomId))]
        [InverseProperty(nameof(Person.BookCopiesCirculationForWhoms))]
        public virtual Person ForWhom { get; set; }
        [ForeignKey(nameof(WhoDidId))]
        [InverseProperty(nameof(Person.BookCopiesCirculationWhoDids))]
        public virtual Person WhoDid { get; set; }
    }
}
