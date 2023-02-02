using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using App.DAL.DataContext;
using App.DAL.Models;
using System.Transactions;
using DAL.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace OperationTable.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderTController : ControllerBase
    {
        private readonly IGenericRepository<OrderTableModel> _repository;

        public OrderTController(IGenericRepository<OrderTableModel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ActionName("GetOrders")]

        public async Task<IActionResult> GetOrders(int id)
        {
            var result = await _repository.GetAllById(id);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("AddOrder")]

        public async Task<IActionResult> AddOrder(OrderTableModel orderHeader)
        {
            await _repository.Add(orderHeader);
            return Ok();
        }

        [HttpDelete]
        [ActionName("DeleteOrder")]

        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _repository.Remove(id);
            return Ok(result);
        }

    }
 }

