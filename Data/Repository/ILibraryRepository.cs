using LibreriaArqui.Data.Entities;
using LibreriaArqui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaArqui.Data.Repository
{
    public interface ILibraryRepository
    {
        //Author
        Task<AuthorEntity> GetAuthorAsync(int id, bool showBooks = false);
        Task<IEnumerable<AuthorEntity>> GetAuthors(string orderBy = "id", bool showBooks = false);
        Task DeleteAuthorAsync(int id);
        void UpdateAuthor(AuthorEntity author);
        void CreateAuhtor(AuthorEntity author);

        //Book
        Book GetBook(int id);
        IEnumerable<Book> GetBooks();
        bool DeleteBook(int id);
        Book UpdateBook(Book book);
        Book CreateBook(Book book);

        //general
        Task<bool> SaveChangesAsync();
        void DetachEntity<T>(T entity) where T : class;

    }
}
