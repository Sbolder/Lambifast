using Lambifast.Sample.Entities;

namespace Lambifast.Sample.Services;

public interface IBookService
{
    Task<IList<Book>> GetAllAsync();
}