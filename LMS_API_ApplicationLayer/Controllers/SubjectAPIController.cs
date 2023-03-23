using AutoMapper;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using LMS_API_DataLayer.Models.Books;
using Serilog;
using LMS_API_DataLayer.Models.DTO.Subjects;

namespace LMS_API_ApplicationLayer.Controllers
{
    [Route("api/SubjectAPI")]
    [ApiController]

    public class SubjectAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ISubjectRepository _dbSubject;
        private readonly IMapper _mapper;
        public SubjectAPIController(ISubjectRepository dbSubject, IMapper mapper)
        {
            _dbSubject = dbSubject;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]

        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> GetSubjects([FromQuery(Name = "filteravailability")] int IsActive,
            [FromQuery] int pageSize = 0, int pageNumber = 1)
        {
            {
                try
                {

                    IEnumerable<Subject> List;

                    if (IsActive > 1)
                    {
                        List = await _dbSubject.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    else
                    {
                        List = await _dbSubject.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }

                    Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                    _response.Result = _mapper.Map<List<SubjectDTO>>(List);
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

        [HttpGet("{id:int}", Name = "GetSubject")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetSubject(int id)
        {

            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var Subject = await _dbSubject.GetAsync(u => u.SubjectId == id);
                if (Subject == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<SubjectDTO>(Subject);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> CreateSubject([FromBody] SubjectCreateDTO createDTO)
        {
            try
            {

                if (await _dbSubject.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Subject already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Subject Subject = _mapper.Map<Subject>(createDTO);


                await _dbSubject.CreateAsync(Subject);
                _response.Result = _mapper.Map<SubjectDTO>(Subject);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetSubject", new { id = Subject.SubjectId }, _response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex.StackTrace); _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteSubject")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteSubject(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var Subject = await _dbSubject.GetAsync(u => u.SubjectId == id);
                if (Subject == null)
                {
                    return NotFound();
                }

                await _dbSubject.RemoveAsync(Subject); // update record in the database
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
        [HttpPut("{id:int}", Name = "UpdateSubject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateSubject(int id, [FromBody] SubjectUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.SubjectId)
                {
                    return BadRequest();
                }

                Subject model = _mapper.Map<Subject>(updateDTO);

                await _dbSubject.UpdateAsync(model);
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
        public async Task<IActionResult> GetAll() => Ok(await _dbSubject.GetAll());
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id) => Ok(await _dbSubject.GetById(id));
    }
}
