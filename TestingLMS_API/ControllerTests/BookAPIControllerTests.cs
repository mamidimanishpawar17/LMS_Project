using AutoMapper;
using LMS_API_ApplicationLayer.Controllers;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.DTO.Book;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

public class BookAPIControllerTests
{
    private readonly Mock<IBookRepository> _mockRepository;
    private readonly IMapper _mapper;
    private readonly BookAPIController _controller;

    public BookAPIControllerTests()
    {
        _mockRepository = new Mock<IBookRepository>();
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            //cfg.AddProfile<MappingProfile>();
        }));
        _controller = new BookAPIController(_mockRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetBooks_ReturnsOkResult()
    {
        // Arrange
        var filterAvailability = 1;
        var pageSize = 10;
        var pageNumber = 1;

        var books = new List<Book>()
        {
            new Book
                {

                    BookId = 1,
                    Title = "To Kill a Mockingbird",
                    Description = "A classic novel about racism and justice in a small Southern town.",
                    Publisher = "HarperCollins",
                    Language = "English",
                    ISBN = "9780061120084",
                    CallNumber = "F LEE",
                    MaxIssueDays = 14
                },
                new Book
                {

                    BookId = 2,
                    Title = "The Catcher in the Rye",
                    Description = "A coming-of-age novel about teenage angst and alienation in post-World War II America.",
                    Publisher = "Little, Brown and Company",
                    Language = "English",
                    ISBN = "9780316769488",
                    CallNumber = "F SAL",
                    MaxIssueDays = 10
                },
        };
        _mockRepository.Setup(r => r.GetAllAsync(null,null ,pageSize, pageNumber)).ReturnsAsync(books);

        // Act
        var result = await _controller.GetBooks(filterAvailability, pageSize, pageNumber);

        // Assert
        Assert.IsType<APIResponse>(result.Value);
        var apiResponse = (APIResponse)result.Value;
        Assert.Equal(HttpStatusCode.OK, apiResponse.StatusCode);
        
    }

    [Fact]
    public async Task GetBook_ReturnsNotFoundResult_WhenBookNotFound()
    {
        // Arrange
        var id = 1;
        Book book = null;
        _mockRepository.Setup(r => r.GetAsync(b => b.BookId == id,true,null)).ReturnsAsync(book);

        // Act
        var result = await _controller.GetBook(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
        var notFoundResult = (NotFoundObjectResult)result.Result;
        Assert.IsType<APIResponse>(notFoundResult.Value);
        var apiResponse = (APIResponse)notFoundResult.Value;
        Assert.Equal(HttpStatusCode.NotFound, apiResponse.StatusCode);
    }

    [Fact]
    public async Task CreateBook_ReturnsBadRequestResult_WhenBookCreateDTOIsNull()
    {
        // Arrange
        BookCreateDTO createDTO = null;

        // Act
        var result = await _controller.CreateBook(createDTO);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task DeleteBook_ReturnsNotFoundResult_WhenBookNotFound()
    {
        // Arrange
        var id = 1;
        Book book = null;
        

        // Act
        var result = await _controller.DeleteBook(id);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
