using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookRecords.Data;
using BookRecords.Data.Entities;
using BookRecords.Interfaces;
using BookRecords.Responses.Categories;
using BookRecords.Responses.Books;
using BookRecords.Services;

namespace BookRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly bookrecordsContext _context;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var getCategoriesResponse = await _categoryService.GetCategoriesAsync();
            if (!getCategoriesResponse.Success)
            {
                return NotFound();
            }

            var categoriesResponse = getCategoriesResponse.Categories.ConvertAll(o => new CategoryResponse
            {
                Idcategory = o.Idcategory,
                CategoryName = o.CategoryName,

            });

            return Ok(categoriesResponse);

           
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var categoryResponse = await _categoryService.GetCategoryByIdAsync(id);

            if (!categoryResponse.Success)
            {
                return NotFound();
            }

            return Ok(new CategoryResponse
            {
                Idcategory = categoryResponse.Idcategory,
                CategoryName = categoryResponse.CategoryName,
            });
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Idcategory)
            {
                return BadRequest();
            }

            var categoryResponse = await _categoryService.UpdateCategoryAsync(id, category);

            if (!categoryResponse.Success)
            {
                return BadRequest(category);
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCategory(Category category)
        {
            var categoryResponse = await _categoryService.CreateCategoryAsync(category);

            if (!categoryResponse.Success)
            {
                return UnprocessableEntity(category);
            }

            return CreatedAtAction(nameof(GetCategory), new { id = category.Idcategory }, category);
          
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryResponse = await _categoryService.DeleteCategoryAsync(id);
            if (!categoryResponse.Success)
            {
                return BadRequest();
            }

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Idcategory == id);
        }
        private bool CategoryNameExists(int id, string name)
        {
            return _context.Categories.Any(e => e.CategoryName == name && e.Idcategory == id);
        }
        private bool CategoryExistsByName(string name)
        {
            return _context.Categories.Any(e => e.CategoryName == name);
        }
    }
}
