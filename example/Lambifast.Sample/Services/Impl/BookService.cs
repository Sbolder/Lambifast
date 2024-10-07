using Lambifast.Sample.Dtos;
using Lambifast.Sample.Entities;
using AWS.Lambda.Powertools.Tracing;
using Lambifast.Sample.Utils;
using Lambifast.Services;

namespace Lambifast.Sample.Services.Impl;

public class BookService : IBookService
{
    private ILogger<BookService> Logger { get; }
    private ILocalizationService LocalizationService { get; }


    public BookService(ILogger<BookService> logger,
        ILocalizationService localizationService)
    {
        Logger = logger;
        LocalizationService = localizationService;
    }

    [Tracing(SegmentName = "Start sub segment")]
    public async Task<IList<Book>> GetAllAsync()
    {
        Logger.LogInformation("Custom logging");
        return new List<Book>()
        {
            new()
            {
                Title = LocalizationUtils.localizeResponse(LocalizationService, "TITLE1"),
                Author = "Herman Melville",
                ReleaseDate = new DateTime(1851, 01, 01)
            },
            new()
            {
                Title = LocalizationUtils.localizeResponse(LocalizationService, "TITLE2"),
                Author = "Ernest Hemingway",
                ReleaseDate = new DateTime(1951, 01, 01)
            }
        };
    }
}