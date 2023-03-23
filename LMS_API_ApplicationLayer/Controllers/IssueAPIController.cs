using AutoMapper;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models;
using Serilog;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Issues;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using LMS_API_BusinessLayer.Repositories;
using LMS_API_DataLayer.Models.DTO.Issue;
using LMS_API_BusinessLayer.Messaging;

namespace LMS_API_ApplicationLayer.Controllers
{
    [Route("api/IssueAPI")]
    [ApiController]

    public class IssueAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IIssueRepository _dbIssue;
        private readonly IMapper _mapper;
        private readonly IReminderService _reminderService;
        private readonly IConfiguration _configuration;
        public IssueAPIController(IIssueRepository dbIssue, IMapper mapper,
              IConfiguration configuration, IReminderService reminderService)
        {
            _reminderService = reminderService;
            _configuration = configuration;
            _dbIssue = dbIssue;
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
        public async Task<ActionResult<APIResponse>> GetIssues([FromQuery(Name = "filteravailability")] int IsActive,
            [FromQuery] int pageSize = 0, int pageNumber = 1)
        {
            {
                try
                {

                    IEnumerable<Issue> List;

                    if (IsActive > 1)
                    {
                        List = await _dbIssue.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    else
                    {
                        List = await _dbIssue.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    //if (!string.IsNullOrEmpty(search))
                    //{
                    //    List = List.Where(u => u.IssueId.Equals(search));
                    //}
                    Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                    //await _dbIssue.SendOverdueEmailsAsync();

                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                    _response.Result = _mapper.Map<List<IssueDTO>>(List);
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

        [HttpGet("{id:int}", Name = "GetIssue")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetIssue(int id)
        {

            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var Issue = await _dbIssue.GetAsync(u => u.IssueId == id);
                if (Issue == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<IssueDTO>(Issue);
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
        public async Task<ActionResult<APIResponse>> CreateIssue([FromBody] IssueCreateDTO createDTO)
        {
            try
            {

                if (await _dbIssue.GetAsync(u => u.IssueId == createDTO.IssueId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Issue already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Issue Issue = _mapper.Map<Issue>(createDTO);


                await _dbIssue.CreateAsync(Issue);
                _response.Result = _mapper.Map<IssueDTO>(Issue);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetIssue", new { id = Issue.IssueId }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteIssue")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteIssue(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var Issue = await _dbIssue.GetAsync(u => u.IssueId == id);
                if (Issue == null)
                {
                    return NotFound();
                }

                await _dbIssue.RemoveAsync(Issue); // update record in the database
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
        [HttpPut("{id:int}", Name = "UpdateIssue")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateIssue(int id, [FromBody] IssueUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.IssueId)
                {
                    return BadRequest();
                }

                Issue model = _mapper.Map<Issue>(updateDTO);

                await _dbIssue.UpdateAsync(model);
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


        [HttpPost("sendreminder")]
        public async Task<ActionResult<APIResponse>> SendReminder()
        {
            try
            {
                await _reminderService.SendReminder();
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
        public async Task<IActionResult> GetAll() => Ok(await _dbIssue.GetAll());
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id) => Ok(await _dbIssue.GetById(id));
    }
}
