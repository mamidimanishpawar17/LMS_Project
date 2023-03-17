using AutoMapper;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models;
using LMS_API_DataLayer.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using LMS_API_DataLayer.Models.Issues;
using Serilog;
namespace BuildAPI.Controllers
{
    [Route("api/IssueOverDueAPI")]
    [ApiController]

    public class IssueOverDueAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IIssueOverDueRepository _dbIssueOverDue;
        private readonly IMapper _mapper;
        public IssueOverDueAPIController(IIssueOverDueRepository dbIssueOverDue, IMapper mapper)
        {
            _dbIssueOverDue = dbIssueOverDue;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]

        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> GetIssueOverDues([FromQuery(Name = "filteravailability")] Boolean IsOverDue,
            [FromQuery] int pageSize = 0, int pageNumber = 1)
        {
            {
                try
                {

                    IEnumerable<IssueOverDue> List;

                    if (IsOverDue == true)
                    {
                        List = await _dbIssueOverDue.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    else
                    {
                        List = await _dbIssueOverDue.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    //if (!string.IsNullOrEmpty(search))
                    //{
                    //    List = List.Where(u => u.OverDueIssueNo.Equals(search));
                    //}
                    Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                    _response.Result = _mapper.Map<List<IssueOverDueDTO>>(List);
                    _response.StatusCode = HttpStatusCode.OK;
                    return _response;

                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message,ex.StackTrace); _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { ex.ToString() };
                }
                return _response;
            }


        }

        [HttpGet("{id:int}", Name = "GetIssueOverDue")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetIssueOverDue(int id)
        {

            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var IssueOverDue = await _dbIssueOverDue.GetAsync(u => u.OverDueIssueNo == id);
                if (IssueOverDue == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<IssueOverDueDTO>(IssueOverDue);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message,ex.StackTrace); _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> CreateIssueOverDue([FromBody] IssueOverDueCreateDTO createDTO)
        {
            try
            {

                if (await _dbIssueOverDue.GetAsync(u => u.OverDueIssueNo == createDTO.OverDueIssueNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "IssueOverDue already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                IssueOverDue IssueOverDue = _mapper.Map<IssueOverDue>(createDTO);


                await _dbIssueOverDue.CreateAsync(IssueOverDue);
                _response.Result = _mapper.Map<IssueOverDueDTO>(IssueOverDue);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetIssueOverDue", new { id = IssueOverDue.OverDueIssueNo }, _response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message,ex.StackTrace); _response.IsSuccess = false;
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
        [HttpDelete("{id:int}", Name = "DeleteIssueOverDue")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteIssueOverDue(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var IssueOverDue = await _dbIssueOverDue.GetAsync(u => u.OverDueIssueNo == id);
                if (IssueOverDue == null)
                {
                    return NotFound();
                }

                await _dbIssueOverDue.UpdateAsync(IssueOverDue); // update record in the database
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message,ex.StackTrace); _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpPut("{id:int}", Name = "UpdateIssueOverDue")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateIssueOverDue(int id, [FromBody] IssueOverDueUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.IssueId)
                {
                    return BadRequest();
                }

                IssueOverDue model = _mapper.Map<IssueOverDue>(updateDTO);

                await _dbIssueOverDue.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message,ex.StackTrace); _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        



    }
}
