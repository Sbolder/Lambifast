using Lambifast.Sample.Dtos;
using Lambifast.Sample.Entities;
using AWS.Lambda.Powertools.Tracing;

namespace Lambifast.Sample.Services.Impl;

public class BookService : IBookService
{
    private ILogger<BookService> Logger { get; }

    public BookService(ILogger<BookService> logger)
    {
        Logger = logger;
    }

    [Tracing(SegmentName = "Start sub segment")]
    public async Task<IList<Book>> GetAllAsync()
    {
        Logger.LogInformation("Custom logging");
        return new List<Book>()
        {
            new()
            {
                Title = "Moby Dick",
                Author = "Herman Melville",
                ReleaseDate = new DateTime(1851, 01, 01)
            },
            new()
            {
                Title = "The old man and the sea",
                Author = "Ernest Hemingway",
                ReleaseDate = new DateTime(1951, 01, 01)
            }
        };
    }
}