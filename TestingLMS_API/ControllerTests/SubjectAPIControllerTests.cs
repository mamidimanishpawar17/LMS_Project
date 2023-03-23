using AutoMapper;
using LMS_API_ApplicationLayer.Controllers;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.DTO.Subjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using Moq;

public class SubjectControllerTests
{
    private readonly Mock<ISubjectRepository> _mockRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly SubjectAPIController _subjectController;

    public SubjectControllerTests()
    {
        _mockRepository = new Mock<ISubjectRepository>();
        _mapper = new Mock<IMapper>();

        _subjectController = new SubjectAPIController(_mockRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task CreateSubject_ShouldReturnBadRequest_WhenCreateDTOIsNull()
    {
        // Arrange
        SubjectCreateDTO createDTO = null;

        // Act
        var result = await _subjectController.CreateSubject(createDTO);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(createDTO, badRequestResult.Value);
    }

    
   

}
