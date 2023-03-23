// Import necessary namespaces
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Net;
using AutoMapper;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models.Books;
using LMS_API_ApplicationLayer.Controllers;
using LMS_API_DataLayer.Models.DTO.Author;
using System.Linq.Expressions;
using LMS_API_DataLayer.Models;

// Make sure to replace "AuthorController" with the actual name of the controller class
public class AuthorAPIControllerTests
{
    private readonly Mock<IAuthorRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;

    private readonly AuthorAPIController _controller;
   
    public AuthorAPIControllerTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.CreateMap<Author, AuthorDTO>();
            opts.CreateMap<AuthorCreateDTO, Author>();
        });
        _mockRepo = new Mock<IAuthorRepository>();
        _mockMapper = new Mock<IMapper>();
        _controller = new AuthorAPIController(_mockRepo.Object, _mockMapper.Object);
    }


    [Fact]

    public void GetAll_ActionExecutes_NotReturnsViewForGetAll()
    {
        //Act
        var result = _controller.GetAll();
        //Assert
        Assert.IsNotType<ViewResult>(result);
    }
   
    
    


    [Fact]
    public async Task CreateAuthor_ReturnsBadRequest_WhenModelIsNull()
    {
        // Arrange
        AuthorCreateDTO createDTO = null;

        // Act
        var result = await _controller.CreateAuthor(createDTO);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }


    [Fact]
    public async Task GetAuthors_ReturnsOKResponse()
    {
        // Arrange
        var controller = new AuthorAPIController(_mockRepo.Object, _mockMapper.Object);
        _mockRepo.Setup(repo => repo.GetAllAsync(null, null, 0, 1)).ReturnsAsync(GetTestAuthors());

        // Act
        var result = await controller.GetAuthors(1, 0, 1);

        // Assert
        var okResult = Assert.IsType<ActionResult<APIResponse>>(result);
        Assert.IsType<List<AuthorDTO>>(okResult.Value.Result);
        Assert.Equal(HttpStatusCode.OK, okResult.Value.StatusCode);
    }

    [Fact]
    public async Task CreateAuthor_WithDuplicateAuthor_ReturnsBadRequestResponse()
    {
        // Arrange
        var controller = new AuthorAPIController(_mockRepo.Object, _mockMapper.Object);
        var author = new AuthorCreateDTO { AuthorId = 1 };
        _mockRepo.Setup(repo => repo.GetById( author.AuthorId)).ReturnsAsync(new Author());

        // Act
        var result = await controller.CreateAuthor(author);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var errorMessages = Assert.IsType<SerializableError>(badRequestResult.Value)["ErrorMessages"] as string[];
        Assert.Single(errorMessages);
        Assert.Equal("Author already Exists!", errorMessages[0]);
    }

    [Fact]
    public async Task DeleteAuthor_WithValidId_ReturnsNoContentResponse()
    {
        // Arrange
        var controller = new AuthorAPIController(_mockRepo.Object, _mockMapper.Object);
        var author = new Author { AuthorId = 1, Name = "Test Author" };
        _mockRepo.Setup(repo => repo.GetById( author.AuthorId)).ReturnsAsync(author);
        _mockRepo.Setup(repo => repo.UpdateAsync(author)).Returns((Task<Author>)Task.CompletedTask);

        // Act
        var result = await controller.DeleteAuthor(author.AuthorId);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result.Result);
        Assert.Equal(HttpStatusCode.NoContent, (HttpStatusCode)noContentResult.StatusCode);
    }




    private List<Author> GetTestAuthors()
        {
            return new List<Author>
        {
            new Author { AuthorId = 1, Name = "Author 1" },
            new Author { AuthorId = 2, Name = "Author 2" },
            new Author { AuthorId = 3, Name = "Author 3" }
        };
        }
    }




