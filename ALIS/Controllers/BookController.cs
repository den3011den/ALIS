using ALIS_DataAccess.Repository;
using ALIS_Models;
using ALIS_Models.ViewModels;
using ALIS_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALIS.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IGrifRepository _grifRepo;
        private readonly IPublisherRepository _publisherRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IAuthorTypeRepository _authorTypeRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IBooksToAuthorRepository _booksToAuthorRepo;



        public IWebHostEnvironment _env { get; set; }

        public BookController(IBookRepository bookRepo,
            IGenreRepository genreRepo,
            IGrifRepository grifRepo,
            IPublisherRepository publisherRepo,
            IAuthorRepository authorRepo,
            IAuthorTypeRepository authorTypeRepo,
            ITagRepository tagRepo,
            IBooksToAuthorRepository booksToAuthorRepo,
            IWebHostEnvironment env)
        {
            _bookRepo = bookRepo;
            _genreRepo = genreRepo;
            _grifRepo = grifRepo;
            _publisherRepo = publisherRepo;
            _authorRepo = authorRepo;
            _authorTypeRepo = authorTypeRepo;
            _tagRepo = tagRepo;
            _booksToAuthorRepo = booksToAuthorRepo;
            _env = env;
        }
        public IActionResult Index()
        {
            BookListVM bookListVM = new BookListVM()
            {
                BookList = _bookRepo.GetAll(includeProperties: "Genre,Grif,Publisher,BooksToAuthors,BooksToTags"),
                //GenreList = (IEnumerable<SelectListItem>)_genreRepo.GetAll(u=>u.IsArchive!=true),
                //GrifList = (IEnumerable<SelectListItem>)_grifRepo.GetAll(u => u.IsArchive != true),
                //PublisherList = (IEnumerable<SelectListItem>)_publisherRepo.GetAll(u => u.IsArchive != true),
                //AuthorList = (IEnumerable<SelectListItem>)_authorRepo.GetAll(u => u.IsArchive != true),
                //AuthorTypeList = (IEnumerable<SelectListItem>)_authorTypeRepo.GetAll(u => u.IsArchive != true),
                //TagList = (IEnumerable<SelectListItem>)_tagRepo.GetAll(u => u.IsArchive != true)
            };

            foreach(Book book in bookListVM.BookList)
            {
                book.AuthorListString = "";
                IEnumerable<BooksToAuthor> booksToAuthorsList = book.BooksToAuthors.OrderBy(p => p.AuthorTypeId);
                foreach (BooksToAuthor booksToAuthor in booksToAuthorsList)
                {
                    var authorType = _authorTypeRepo.FirstOrDefault(u => u.Id == booksToAuthor.AuthorTypeId);
                    var author = _authorRepo.FirstOrDefault(u => u.Id == booksToAuthor.AuthorId);
                    if (!String.IsNullOrEmpty(book.AuthorListString))
                        {
                            book.AuthorListString = book.AuthorListString + Environment.NewLine;
                        }
                    book.AuthorListString = book.AuthorListString + authorType.Name + ": " + author.Name;
                }
                //IEnumerable<BooksToTag> booksToTagList = book.BooksToTags.OrderBy(p => p.Tag);
                foreach (BooksToTag booksToTag in book.BooksToTags)
                {
                    var tag = _tagRepo.FirstOrDefault(u => u.Id == booksToTag.TagId);                    
                    book.TagListString = book.TagListString + tag.Name +";";
                    
                }
            }

            return View(bookListVM);
        }
    }
}
