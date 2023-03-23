using AutoMapper;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Models;
using LMS_API_DataLayer.Models.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Serilog;
using LMS_API_DataLayer.Models.DTO.Member;

namespace LMS_API_ApplicationLayer.Controllers
{
    [Route("api/MemberAPI")]
    [ApiController]

    public class MemberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMemberRepository _dbMember;
        private readonly IMapper _mapper;
        public MemberAPIController(IMemberRepository dbMember, IMapper mapper)
        {
            _dbMember = dbMember;
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
        public async Task<ActionResult<APIResponse>> GetMembers([FromQuery(Name = "filteravailability")] int IsActive,
            [FromQuery] int pageSize = 0, int pageNumber = 1)
        {
            {
                try
                {

                    IEnumerable<Member> List;

                    if (IsActive > 1)
                    {
                        List = await _dbMember.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }
                    else
                    {
                        List = await _dbMember.GetAllAsync(pageSize: pageSize,
                            pageNumber: pageNumber);
                    }

                    Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                    _response.Result = _mapper.Map<List<MemberDTO>>(List);
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

        [HttpGet("{id:int}", Name = "GetMember")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetMember(int id)
        {

            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var Member = await _dbMember.GetAsync(u => u.MemberId == id);
                if (Member == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<MemberDTO>(Member);
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
        public async Task<ActionResult<APIResponse>> CreateMember([FromBody] MemberCreateDTO createDTO)
        {
            try
            {

                if (await _dbMember.GetAsync(u => u.FirstName.ToLower() == createDTO.FirstName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Member already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Member Member = _mapper.Map<Member>(createDTO);


                await _dbMember.CreateAsync(Member);
                _response.Result = _mapper.Map<MemberDTO>(Member);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetMember", new { id = Member.MemberId }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteMember")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteMember(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var Member = await _dbMember.GetAsync(u => u.MemberId == id);
                if (Member == null)
                {
                    return NotFound();
                }

                await _dbMember.RemoveAsync(Member); // update record in the database
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
        [HttpPut("{id:int}", Name = "UpdateMember")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> UpdateMember(int id, [FromBody] MemberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.MemberId)
                {
                    return BadRequest();
                }

                Member model = _mapper.Map<Member>(updateDTO);

                await _dbMember.UpdateAsync(model);
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
        public async Task<IActionResult> GetAll() => Ok(await _dbMember.GetAll());
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id) => Ok(await _dbMember.GetById(id));
    }
}
