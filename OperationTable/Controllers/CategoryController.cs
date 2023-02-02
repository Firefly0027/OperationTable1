using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.DataContext;
using App.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace OperationTable.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly OperationDbContext _categoriesDbContext;

        public CategoryController(OperationDbContext categoriesDbContext)
        {
            _categoriesDbContext = categoriesDbContext;
        }

        [HttpGet]
        [ActionName("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var Categories = await _categoriesDbContext.category.ToListAsync();
                return Ok(Categories);

            }
            catch ( Exception ex)
            {
                Logger.Log(ex.Message);
                throw;
            }
            
        }

        [HttpPost]
        [ActionName("AddCategories")]

        public async Task<IActionResult> AddCategories([FromBody] categoryModel categoriesRequest)
        {
            try
            {
                await _categoriesDbContext.category.AddAsync(categoriesRequest);
                await _categoriesDbContext.SaveChangesAsync();
                object x = null;
                string e = (int.Parse(x.ToString())).ToString() + "ddddddddddd";
                return Ok(categoriesRequest); ;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                return BadRequest(ex);

            }
          
        }
    }
}
