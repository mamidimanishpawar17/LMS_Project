
using AutoMapper;
using BuildAPI.Controllers;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace BuildAPI.Tests.Controllers
{
    public class AuthorAPIControllerTests
    {
        private readonly Mock<IAuthorRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AuthorAPIController _controller;

        public AuthorAPIControllerTests()
        {
            _mockRepository = new Mock<IAuthorRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new AuthorAPIController(_mockRepository.Object, _mockMapper.Object);
        }



        [Fact]
        public async Task GetAuthors_ReturnsListOfAuthors()
        {
            // Arrange
            IEnumerable<Author> authors = new List<Author>
            {
                new Author { AuthorId = 1, Name = "Author 1" },
                new Author { AuthorId = 2, Name = "Author 2" }
            };
            _mockMapper.Setup(mapper => mapper.Map<List<AuthorDTO>>(It.IsAny<IEnumerable<Author>>()))
                .Returns(new List<AuthorDTO>());

            // Act
            var result = await _controller.GetAuthors(true, "search", 10, 1);

            // Assert
            Assert.IsType<APIResponse>(result.Value);
            var response = result.Value as APIResponse;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            _mockMapper.Verify(mapper => mapper.Map<List<AuthorDTO>>(It.IsAny<IEnumerable<Author>>()), Times.Once);
        }

        [Fact]
        public async Task GetAuthor_ReturnsAuthor_WhenIdIsValid()
        {
            // Arrange
            int authorId = 1;
            var author = new Author { AuthorId = authorId, Name = "Author 1" };
            _mockMapper.Setup(mapper => mapper.Map<AuthorDTO>(It.IsAny<Author>()))
                .Returns(new AuthorDTO { AuthorId = authorId, Name = "Author 1" });

            // Act
            var result = await _controller.GetAuthor(authorId);

            // Assert
            Assert.IsType<APIResponse>(result.Value);
            var response = result.Value as APIResponse;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<AuthorDTO>(response.Result);
            _mockMapper.Verify(mapper => mapper.Map<AuthorDTO>(It.IsAny<Author>()), Times.Once);
        }


    }
}
