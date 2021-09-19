using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ALIS_Models
{
    [Table("Books_To_Tags")]
    public partial class BooksToTag
    {
        [Key]
        [Column("Book_id")]
        public int BookId { get; set; }
        [Key]
        [Column("Tag_id")]
        public int TagId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey(nameof(BookId))]
        [InverseProperty("BooksToTags")]
        public virtual Book Book { get; set; }
        [ForeignKey(nameof(TagId))]
        [InverseProperty("BooksToTags")]
        public virtual Tag Tag { get; set; }
    }
}
