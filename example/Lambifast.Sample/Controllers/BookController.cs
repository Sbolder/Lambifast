using AutoMapper;
using Lambifast.Sample.Dtos;
using Lambifast.Sample.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lambifast.Sample.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private IBookService PeopleService { get; }
    private IMapper Mapper { get; }

    public BookController(IBookService peopleService, IMapper mapper)
    {
        PeopleService = peopleService;
        Mapper = mapper;
    }

    [HttpGet]
    public async Task<IList<BookDto>> GetAllAsync()
    {
        var entities = await PeopleService.GetAllAsync();
        return Mapper.Map<IList<BookDto>>(entities);
    }
}