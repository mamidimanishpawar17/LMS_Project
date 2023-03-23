using AutoMapper;
using LMS_Utility;
using LMS_WEB.Models;
using LMS_WEB.Models.DTO.Author;
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
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _AuthorService;
        private readonly IMapper _mapper;
        public AuthorsController(IAuthorService AuthorService, IMapper mapper)
        {
            _AuthorService = AuthorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAuthor()
        {
            try
            {
                Log.Information($"Running the IndexAuthor");
                List<AuthorDTO> list = new();

                var response = await _AuthorService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<AuthorDTO>>(Convert.ToString(response.Result));
                }
                return View(list);
            }
            catch (Exception e)
            {
                Log.Error(e.Message, e.StackTrace);
                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAuthor()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuthor(AuthorCreateDTO model)
        {
            try
            {
                Log.Information($"Running the CreateAuthor");
                if (ModelState.IsValid)
                {

                    var response = await _AuthorService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Author created successfully";
                        return RedirectToAction(nameof(IndexAuthor));
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
        public async Task<IActionResult> UpdateAuthor(int AuthorId)
        {
            try
            {
                var response = await _AuthorService.GetAsync<APIResponse>(AuthorId, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {

                    AuthorDTO model = JsonConvert.DeserializeObject<AuthorDTO>(Convert.ToString(response.Result));
                    return View(_mapper.Map<AuthorUpdateDTO>(model));
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
        public async Task<IActionResult> UpdateAuthor(AuthorDTO model)
        {
            try
            {
                Log.Information($"Running the UpdateAuthor");
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Author updated successfully";
                    var response = await _AuthorService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                    if (response != null && response.IsSuccess)
                    {
                        return RedirectToAction(nameof(IndexAuthor));
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
        public async Task<IActionResult> DeleteAuthor(int AuthorId)
        {
            try
            {

                var response = await _AuthorService.GetAsync<APIResponse>(AuthorId, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    AuthorDTO model = JsonConvert.DeserializeObject<AuthorDTO>(Convert.ToString(response.Result));
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
        public async Task<IActionResult> DeleteAuthor(AuthorDTO model)
        {

            try
            {
                Log.Information($"Running the DeleteAuthor");
                var response = await _AuthorService.DeleteAsync<APIResponse>(model.AuthorId, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Author deleted successfully";
                    return RedirectToAction(nameof(IndexAuthor));
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
