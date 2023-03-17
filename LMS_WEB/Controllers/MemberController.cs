using AutoMapper;
using LMS_WEB.Models;
using LMS_WEB.Models.DTO;
using LMS_WEB.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Serilog;

namespace LMS_WEB.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _MemberService;
        private readonly IMapper _mapper;
        public MemberController(IMemberService MemberService, IMapper mapper)
        {
            _MemberService = MemberService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexMember()
        {
            try
            {
                List<MemberDTO> list = new();

                var response = await _MemberService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<MemberDTO>>(Convert.ToString(response.Result));
                }
                return View(list);
            }
            catch (Exception e)
            {

                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateMember()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMember(MemberCreateDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var response = await _MemberService.CreateAsync<APIResponse>(model, await HttpContext.GetTokenAsync("access_token"));
                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Member created successfully";
                        return RedirectToAction(nameof(IndexMember));
                    }
                }
                TempData["error"] = "Error encountered.";
                return View(model);
            }
            catch (Exception e)
            {

                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateMember(int MemberId)
{
            try
            {
                var response = await _MemberService.GetAsync<APIResponse>(MemberId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {

                    MemberDTO model = JsonConvert.DeserializeObject<MemberDTO>(Convert.ToString(response.Result));
                    return View(_mapper.Map<MemberUpdateDTO>(model));
                }
                return NotFound();
            }
            catch (Exception e)
            {

                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMember(MemberUpdateDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Member updated successfully";
                    var response = await _MemberService.UpdateAsync<APIResponse>(model, await HttpContext.GetTokenAsync("access_token"));
                    if (response != null && response.IsSuccess)
                    {
                        return RedirectToAction(nameof(IndexMember));
                    }
                }
                TempData["error"] = "Error encountered.";
                return View(model);
            }
            catch (Exception e)
            {

                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteMember(int MemberId)
        {
            try
            {
                var response = await _MemberService.GetAsync<APIResponse>(MemberId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    MemberDTO model = JsonConvert.DeserializeObject<MemberDTO>(Convert.ToString(response.Result));
                    return View(model);
                }
                return NotFound();
            }
            catch (Exception e)
            {

                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMember(MemberDTO model)
        {
            try
            {

                var response = await _MemberService.DeleteAsync<APIResponse>(model.MemberId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Member deleted successfully";
                    return RedirectToAction(nameof(IndexMember));
                }
                TempData["error"] = "Error encountered.";
                return View(model);
            }
            catch (Exception e)
            {

                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }

    }
}
