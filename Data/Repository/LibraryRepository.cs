using LibreriaArqui.Data.Entities;
using LibreriaArqui.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace LibreriaArqui.Data.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private List<Book> books = new List<Book>();
        private LibraryDBcontext libraryDbContext;
        public LibraryRepository(LibraryDBcontext libraryDBcontext)
        {
            this.libraryDbContext = libraryDBcontext;

            books.Add(new Book()
            {
                AuthorId = 1,
                Genre = "Novel",
                Pages = 80,
                Title = "The little prince",
                Id = 1
            });

            books.Add(new Book()
            {
                AuthorId = 2,
                Genre = "Fantasy",
                Pages = 364,
                Title = "The Silmarillon",
                Id = 2
            });

            books.Add(new Book()
            {
                AuthorId = 2,
                Genre = "Fantasy",
                Pages = 400,
                Title = "The lord of the rings",
                Id = 3
            });

            books.Add(new Book()
            {
                AuthorId = 3,
                Genre = "Horror",
                Pages = 464,
                Title = "The call of Cthulu",
                Id = 4
            });

            books.Add(new Book()
            {
                AuthorId = 3,
                Genre = "Horror",
                Pages = 500,
                Title = "Dagon",
                Id = 5
            });
        }
        public void CreateAuhtor(AuthorEntity author)
        {
            var savedAuhtor = libraryDbContext.Authors.Add(author);
        }

        public Book CreateBook(Book book)
        {
            var lastAuthorBook = books.Where(bookv => bookv.AuthorId == book.AuthorId).OrderBy(bookv => bookv.Id).FirstOrDefault();
            int nextId = lastAuthorBook == null ? 1 : lastAuthorBook.Id + 1;
            book.Id = nextId;
            books.Add(book);
            return book;
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await libraryDbContext.Authors.SingleAsync(authorv => authorv.Id == id);
            libraryDbContext.Authors.Remove(author);
        }

        public bool DeleteBook(int id)
        {
            var book = books.Single(bookv => bookv.Id == id);
            books.Remove(book);
            return true; 
        }

        public void DetachEntity<T>(T entity) where T : class
        {
            libraryDbContext.Entry(entity).State = EntityState.Detached;
        }

        public async Task<AuthorEntity> GetAuthorAsync(int id, bool showBooks = false)
        {
            IQueryable<AuthorEntity> query = libraryDbContext.Authors;
            if (showBooks)
            {
                query = query.Include(authorv => authorv.Books);
            }
            return await query.SingleOrDefaultAsync(author => author.Id == id);
        }

        public async Task<IEnumerable<AuthorEntity>> GetAuthors(string orderBy = "id", bool showBooks = false)
        {
            IQueryable<AuthorEntity> query = libraryDbContext.Authors;
            if (showBooks)
            {
                query = query.Include(authorv => authorv.Books);
            }
            switch (orderBy)
            {
                case "id":
                    query = query.OrderBy(authorv => authorv.Id);
                        break;
                case "name":
                    query = query.OrderBy(authorv => authorv.Name);
                    break;
                case "lastname":
                    query = query.OrderBy(authorv => authorv.LastName);
                    break;
                case "nationallity":
                    query = query.OrderBy(authorv => authorv.Nationallity);
                    break;
                default:
                    break;
            }
            return await query.ToArrayAsync();
        }

        public Book GetBook(int id)
        {
            return books.SingleOrDefault(bookv => bookv.Id == id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return books;
        }

        public async  Task<bool> SaveChangesAsync()
        {
            return (await libraryDbContext.SaveChangesAsync()) > 0;
        }

        public void UpdateAuthor(AuthorEntity author)
        {
            libraryDbContext.Authors.Update(author);
        }

        public Book UpdateBook(Book book)
        {
            var bookToUpdate = books.Single(bookv => bookv.Id == book.Id);
            bookToUpdate.Genre = book.Genre;
            bookToUpdate.Pages = book.Pages;
            bookToUpdate.Title = book.Title;
            return book;
        }
    }
}
