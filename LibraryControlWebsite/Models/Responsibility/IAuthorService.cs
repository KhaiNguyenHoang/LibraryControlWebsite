namespace LibaryControlWebsite.Models.Responsibility;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAuthors();
    Task<Author> GetAuthor(int? id);
    Task AddAuthor(Author author);
    Task UpdateAuthor(Author author);
    Task DeleteAuthor(int? id);
    bool AuthorExists(int id);
    Task<IEnumerable<Book>> GetBooksByAuthor(int? id);
    Task<IEnumerable<Author>> GetAuthorsByBook(int? id);
}