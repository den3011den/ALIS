using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALIS_Models.ViewModels
{
    public class BookListVM
    {
        public IEnumerable<Book> BookList { get; set; }
        public IEnumerable<SelectListItem> GenreList { get; set; }
        public string Genre { get; set; }
        public IEnumerable<SelectListItem> GrifList { get; set; }
        public string Grif { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; }        
        public string Publisher { get; set; }
        public IEnumerable<SelectListItem> AuthorList { get; set; }
        public string Author { get; set; }
        public IEnumerable<SelectListItem> AuthorTypeList { get; set; }
        public string AuthorType { get; set; }
        public IEnumerable<SelectListItem> TagList { get; set; }
        public string Tag { get; set; }

    }
}
