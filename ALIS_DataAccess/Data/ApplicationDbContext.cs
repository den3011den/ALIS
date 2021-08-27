using ALIS_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALIS_DataAccess.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorType> AuthorTypes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopiesCirculation> BookCopiesCirculations { get; set; }
        public DbSet<BookCopiesOperationType> BookCopiesOperationTypes { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<BooksToAuthor> BooksToAuthors { get; set; }
        public DbSet<BooksToTag> BooksToTags { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Grif> Grifs { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
