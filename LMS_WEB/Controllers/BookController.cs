using AutoMapper;
using LMS_WEB.Models;
using LMS_WEB.Models.DTO.Book;
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
    public class BookController : Controller
    {
        private readonly IBookService _BookService;
        private readonly IMapper _mapper;
        public BookController(IBookService BookService, IMapper mapper)
        {
            _BookService = BookService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexBook()
        {
            try
            {
                Log.Information($"Running the IndexBook");
                List<BookDTO> list = new();

                var response = await _BookService.GetAllAsync<APIResponse>(await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
                }
                return View(list);
            }
            catch (Exception e)
            {

                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateBook()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(BookCreateDTO model)
        {
            try
            {
                Log.Information($"Running the CreateBook");
                if (ModelState.IsValid)
                {

                    var response = await _BookService.CreateAsync<APIResponse>(model, await HttpContext.GetTokenAsync("access_token"));
                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Book created successfully";
                        return RedirectToAction(nameof(IndexBook));
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
        public async Task<IActionResult> UpdateBook(int BookId)
        {
            try
            {
                var response = await _BookService.GetAsync<APIResponse>(BookId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {

                    BookUpdateDTO model = JsonConvert.DeserializeObject<BookUpdateDTO>(Convert.ToString(response.Result));
                    return View(_mapper.Map<BookUpdateDTO>(model));
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
        public async Task<IActionResult> UpdateBook(BookUpdateDTO model)
        {
            try
            {
                Log.Information($"Running the UpdateBook");
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Book updated successfully";
                    var response = await _BookService.UpdateAsync<APIResponse>(model, await HttpContext.GetTokenAsync("access_token"));
                    if (response != null && response.IsSuccess)
                    {
                        return RedirectToAction(nameof(IndexBook));
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
        public async Task<IActionResult> DeleteBook(int BookId)
        {
            try
            {
                var response = await _BookService.GetAsync<APIResponse>(BookId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
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
        public async Task<IActionResult> DeleteBook(BookDTO model)
        {

            try
            {
                Log.Information($"Running the DeleteBook");
                var response = await _BookService.DeleteAsync<APIResponse>(model.BookId, await HttpContext.GetTokenAsync("access_token"));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Book deleted successfully";
                    return RedirectToAction(nameof(IndexBook));
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
