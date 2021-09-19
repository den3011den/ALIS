using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

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
        [Required(ErrorMessage = "Ввод наименования обязателен")]
        [Column(TypeName = "character varying")]
        [DisplayName("Наименование")]
        public string Name { get; set; }
        public bool? IsArchive { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [InverseProperty(nameof(Book.Grif))]
        public virtual ICollection<Book> Books { get; set; }
    }
}
