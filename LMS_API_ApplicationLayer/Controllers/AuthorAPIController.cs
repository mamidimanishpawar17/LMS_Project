using AutoMapper;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models;

using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.DTO.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;
using System.Text.Json;

namespace LMS_API_ApplicationLayer.Controllers
{
    [Route("api/AuthorAPI")]
    [ApiController]

    public class AuthorAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IAuthorRepository _dbAuthor;
        private readonly IMapper _mapper;

        public AuthorAPIController(IAuthorRepository dbAuthor, IMapper mapper)
        {

            _dbAuthor = dbAuthor;
            _mapper = mapper;
            _response = new();
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<APIResponse>> GetAuthors([FromQuery(Name = "filteravailability")] int Availability,
            [FromQuery] int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                Log.Information("Entered in GetAuthors");
                IEnumerable<Author> List;

                if (Availability > 1)
                {
                    List = await _dbAuthor.GetAllAsync(pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                else
                {
                    List = await _dbAuthor.GetAllAsync(pageSize: pageSize,
                        pageNumber: pageNumber);
                }
                //if (!string.IsNullOrEmpty(search))
                //{
                //    List = List.Where(u => u.Name.ToLower().Contains(search));
                //}
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<AuthorDTO>>(List);
                _response.StatusCode = HttpStatusCode.OK;
                return _response;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.StackTrace);
                Log.Error(ex.Message, ex.StackTrace); _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetAuthor")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetAuthor(int id)
        {
            try
            {
                Log.Information("Entered in GetAuthor");
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var Author = await _dbAuthor.GetAsync(u => u.AuthorId == id);
                if (Author == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<AuthorDTO>(Author);
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
        public async Task<ActionResult<APIResponse>> CreateAuthor([FromBody] AuthorCreateDTO createDTO)
        {
            try
            {
                Log.Information("Entered in CreateAuthors");
                if (await _dbAuthor.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Author already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Author Author = _mapper.Map<Author>(createDTO);


                await _dbAuthor.CreateAsync(Author);
                _response.Result = _mapper.Map<AuthorDTO>(Author);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetAuthor", new { id = Author.AuthorId }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteAuthor")]
        public async Task<ActionResult<APIResponse>> DeleteAuthor(int id)
        {
            try
            {
                Log.Information("Entered in DeleteAuthors");
                if (id == 0)
                {
                    return BadRequest();
                }
                var Author = await _dbAuthor.GetAsync(u => u.AuthorId == id);
                if (Author == null)
                {
                    return NotFound();
                }

                await _dbAuthor.UpdateAsync(Author); // update record in the database
                _response.StatusCode = HttpStatusCode.NoContent;

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
        [HttpPut("{id:int}", Name = "UpdateAuthor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> UpdateAuthor(int id, [FromBody] AuthorUpdateDTO updateDTO)
        {
            try
            {
                Log.Information("Entered in UpdateAuthors");
                if (updateDTO == null || id != updateDTO.AuthorId)
                {
                    return BadRequest();
                }

                Author model = _mapper.Map<Author>(updateDTO);

                await _dbAuthor.UpdateAsync(model);
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
        public async Task<IActionResult> GetAll() => Ok(await _dbAuthor.GetAll());
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id) => Ok(await _dbAuthor.GetById(id));
    }
}
