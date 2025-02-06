using LibaryControlWebsite.Models.Responsibility;

namespace LibaryControlWebsite.Models.Service;

public class AuthorService : IAuthorService
{
    public Task<IEnumerable<Author>> GetAuthors()
    {
        throw new NotImplementedException();
    }

    public Task<Author> GetAuthor(int? id)
    {
        throw new NotImplementedException();
    }

    public Task AddAuthor(Author author)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAuthor(Author author)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAuthor(int? id)
    {
        throw new NotImplementedException();
    }

    public bool AuthorExists(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Book>> GetBooksByAuthor(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Author>> GetAuthorsByBook(int? id)
    {
        throw new NotImplementedException();
    }
}