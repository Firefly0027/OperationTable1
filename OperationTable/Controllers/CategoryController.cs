using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.DataContext;
using App.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using DAL.Contracts;

namespace OperationTable.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private IGenericRepository<categoryModel> _Repository;

        public CategoryController(IGenericRepository<categoryModel> repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        [ActionName("GetCategory")]

        public async Task<IActionResult> GetCategory()
        {
            var result = await _Repository.GetAll();
            return Ok(result);
        }

        [HttpPost]
        [ActionName("AddCategory")]

        public async Task<IActionResult> AddCategory(categoryModel category)
        {
            await _Repository.Add(category);
            return Ok();
        }
    }
    }
