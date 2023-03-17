using AutoMapper;
using LMS_WEB.Models;
using LMS_WEB.Models.DTO;
using LMS_WEB.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Reflection;

namespace LMS_WEB.Controllers
{
    public class IssueController : Controller
    {
        private readonly IIssueService _IssueService;
        private readonly IMapper _mapper;
        public IssueController(IIssueService IssueService, IMapper mapper)
        {
            _IssueService = IssueService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexIssue()
        {
            try
            {
                List<IssueDTO> list = new();

                var response = await _IssueService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<IssueDTO>>(Convert.ToString(response.Result));
                }
                return View(list);
            }
            catch (Exception e)
            {

                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateIssue()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIssue(IssueCreateDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var response = await _IssueService.CreateAsync<APIResponse>(model, await HttpContext.GetTokenAsync("access_token"));
                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Issue created successfully";
                        return RedirectToAction(nameof(IndexIssue));
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
        public async Task<IActionResult> UpdateIssue(int IssueId)
{
            try
            {
                var response = await _IssueService.GetAsync<APIResponse>(IssueId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {

                    IssueDTO model = JsonConvert.DeserializeObject<IssueDTO>(Convert.ToString(response.Result));
                    return View(_mapper.Map<IssueUpdateDTO>(model));
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
        public async Task<IActionResult> UpdateIssue(IssueUpdateDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Issue updated successfully";
                    var response = await _IssueService.UpdateAsync<APIResponse>(model, await HttpContext.GetTokenAsync("access_token"));
                    if (response != null && response.IsSuccess)
                    {
                        return RedirectToAction(nameof(IndexIssue));
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
        public async Task<IActionResult> DeleteIssue(int IssueId)
        {
            try
            {
                var response = await _IssueService.GetAsync<APIResponse>(IssueId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    IssueDTO model = JsonConvert.DeserializeObject<IssueDTO>(Convert.ToString(response.Result));
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
        public async Task<IActionResult> DeleteIssue(IssueDTO model)
        {

            try
            {
                var response = await _IssueService.DeleteAsync<APIResponse>(model.IssueId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Issue deleted successfully";
                    return RedirectToAction(nameof(IndexIssue));
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
