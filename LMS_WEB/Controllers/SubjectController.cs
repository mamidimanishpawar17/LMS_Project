using AutoMapper;
using LMS_WEB.Models;
using LMS_WEB.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Serilog;
using LMS_WEB.Models.DTO.Subjects;

namespace LMS_WEB.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _SubjectService;
        private readonly IMapper _mapper;
        public SubjectController(ISubjectService SubjectService, IMapper mapper)
        {
            _SubjectService = SubjectService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexSubject()
        {
            try
            {
                Log.Information($"Running the IndexSubject");
                List<SubjectDTO> list = new();

                var response = await _SubjectService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<SubjectDTO>>(Convert.ToString(response.Result));
                }
                return View(list);
            }
            catch (Exception e)
            {

                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateSubject()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject(SubjectCreateDTO model)
        {
            try
            {
                Log.Information($"Running the CreateSubject");
                if (ModelState.IsValid)
                {

                    var response = await _SubjectService.CreateAsync<APIResponse>(model, await HttpContext.GetTokenAsync("access_token"));
                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Subject created successfully";
                        return RedirectToAction(nameof(IndexSubject));
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
        public async Task<IActionResult> UpdateSubject(int SubjectId)
{
            try
            {
                var response = await _SubjectService.GetAsync<APIResponse>(SubjectId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {

                    SubjectDTO model = JsonConvert.DeserializeObject<SubjectDTO>(Convert.ToString(response.Result));
                    return View(_mapper.Map<SubjectUpdateDTO>(model));
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
        public async Task<IActionResult> UpdateSubject(SubjectUpdateDTO model)
        {
            try
            {
                Log.Information($"Running the UpdateSubject");
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Subject updated successfully";
                    var response = await _SubjectService.UpdateAsync<APIResponse>(model, await HttpContext.GetTokenAsync("access_token"));
                    if (response != null && response.IsSuccess)
                    {
                        return RedirectToAction(nameof(IndexSubject));
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
        public async Task<IActionResult> DeleteSubject(int SubjectId)
        {
            try
            {
                var response = await _SubjectService.GetAsync<APIResponse>(SubjectId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    SubjectDTO model = JsonConvert.DeserializeObject<SubjectDTO>(Convert.ToString(response.Result));
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
        public async Task<IActionResult> DeleteSubject(SubjectDTO model)
        {
            try
            {
                Log.Information($"Running the DeleteSubject");
                var response = await _SubjectService.DeleteAsync<APIResponse>(model.SubjectId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Subject deleted successfully";
                    return RedirectToAction(nameof(IndexSubject));
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
