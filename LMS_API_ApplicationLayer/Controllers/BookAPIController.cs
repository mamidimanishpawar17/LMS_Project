using AutoMapper;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.DTO.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;
using System.Text.Json;

namespace LMS_API_ApplicationLayer.Controllers
{
    [Route("api/BookAPI")]
    [ApiController]


    public class BookAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IBookRepository _dbBook;
        private readonly IMapper _mapper;
        public BookAPIController(IBookRepository dbBook, IMapper mapper)
        {
            _dbBook = dbBook;
            _mapper = mapper;
            _response = new();
        }

        //[Authorize(Roles = "admin")]
        [HttpGet(Name ="GetBooks")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        
        public async Task<ActionResult<APIResponse>> GetBooks([FromQuery(Name = "filteravailability")] int availability,
            [FromQuery] int pageSize = 0, int pageNumber = 1)
        {
            {
                try
                {
                    Log.Information("Entered in GetBooks");
                    IEnumerable<Book> List;

                    if (availability > 1)
                    {
                        List = await _dbBook.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    else
                    {
                        List = await _dbBook.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    //if (!string.IsNullOrEmpty(search))
                    //{
                    //    List = List.Where(u => u.Title.ToLower().Contains(search));
                    //}
                    Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                    _response.Result = _mapper.Map<List<BookDTO>>(List);
                    _response.StatusCode = HttpStatusCode.OK;
                    return _response;

                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex.StackTrace); _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { ex.ToString() };
                }
                return _response;
            }


        }

        [HttpGet("{id:int}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetBook(int id)
        {
            try
            {
                Log.Information("Entered in GetBooks");
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var Book = await _dbBook.GetAsync(u => u.BookId == id);
                if (Book == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<BookDTO>(Book);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.StackTrace); _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    
        public async Task<ActionResult<APIResponse>> CreateBook([FromBody] BookCreateDTO createDTO)
        {
            try
            {
                Log.Information("Entered in CreateBook");
                if (await _dbBook.GetAsync(u => u.Title.ToLower() == createDTO.Title.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Book already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Book Book = _mapper.Map<Book>(createDTO);


                await _dbBook.CreateAsync(Book);
                _response.Result = _mapper.Map<BookDTO>(Book);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetBook", new { id = Book.BookId }, _response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.StackTrace); _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteBook")]
        
        public async Task<ActionResult<APIResponse>> DeleteBook(int id)
        {
            try
            {
                Log.Information("Entered in DeleteBook");
                if (id == 0)
                {
                    return BadRequest();
                }
                var Book = await _dbBook.GetAsync(u => u.BookId == id);
                if (Book == null)
                {
                    return NotFound();
                }

                await _dbBook.RemoveAsync(Book); // Delete record in the database
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.StackTrace); _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        //[Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateBook(int id, [FromBody] BookUpdateDTO updateDTO)
        {
            try
            {
                Log.Information("Entered in UpdateBook");
                if (updateDTO == null || id != updateDTO.BookId)
                {
                    return BadRequest();
                }

                Book model = _mapper.Map<Book>(updateDTO);

                await _dbBook.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.StackTrace); _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _dbBook.GetAll());
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id) => Ok(await _dbBook.GetById(id));
    }
}
