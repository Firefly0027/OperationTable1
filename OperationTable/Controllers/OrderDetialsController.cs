using App.DAL.Models;
using DAL.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OperationTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderDetialsController : ControllerBase
    {
        private readonly IGenericRepository<OrderDetialsModel> _repository;

        public OrderDetialsController(IGenericRepository<OrderDetialsModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ActionName("GetOrderDetials")]

        public async Task<IActionResult> GetOrderDetials(int id)
        {
            var result = await _repository.GetAllById(id);
            return Ok(result);
        }
    }
}
