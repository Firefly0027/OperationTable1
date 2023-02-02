using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.Models;
using App.DAL.DataContext;
using DAL.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace OperationTable.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ItemTController : ControllerBase
    {
        private IGenericRepository<itemsTableModel> _Repository;

        public ItemTController(IGenericRepository<itemsTableModel> repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        [ActionName("GetItems")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _Repository.GetAll();
            return Ok(items);
        }

        [HttpPost]
        [ActionName("AddItems")]

        public async Task<IActionResult> AddItems(itemsTableModel item)
        {
            await _Repository.Add(item);
            return Ok();
        }

        [HttpDelete]
        [ActionName("DeleteItem")]

        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _Repository.Remove(id);
            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetById")]

        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Repository.GetById(id);
            return Ok(result);
        }

        [HttpPut]
        [ActionName("UpdateItem")]

        public async Task<IActionResult> UpdateItem(itemsTableModel item)
        {
            var result = await _Repository.Update(item);
            return Ok(result);
        }
    }
    }

