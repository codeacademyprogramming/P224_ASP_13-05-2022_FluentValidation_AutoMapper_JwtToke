using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P224FirstApi.DAL;
using P224FirstApi.DAL.Entities;
using P224FirstApi.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P224FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryPostDto categoryPostDto)
        {
            Category category = _mapper.Map<Category>(categoryPostDto);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, categoryPostDto);
        }

        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();

            Category category = await _context.Categories.Include(c=>c.Products).FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            CategoryGetDto categoryGetDto = _mapper.Map<CategoryGetDto>(category);

            return Ok(categoryGetDto);

        }
    }
}
