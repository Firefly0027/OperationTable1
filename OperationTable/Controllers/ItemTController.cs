using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.Models;
using App.DAL.DataContext;

namespace OperationTable.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemTController : ControllerBase
    {
        private readonly OperationDbContext _itemTableDbContext;

        public ItemTController(OperationDbContext itemTableDbContext)
        {
            _itemTableDbContext = itemTableDbContext;
        }
        [HttpGet]
        [ActionName("GetItemsTable")]
        public async Task<ActionResult<List<itemsTableModel>>> GetItemsTable()
        {
            var ItemTable = await _itemTableDbContext.Items
                .Include(c => c.category)
                .ToListAsync();

            return Ok(ItemTable);
        }
        [HttpPost]
        [ActionName("AddItemTable")]
        public async Task<IActionResult> AddItemTable([FromBody] itemsTableModel itemTableRequest)
        {
            await _itemTableDbContext.Items.AddAsync(itemTableRequest);
            await _itemTableDbContext.SaveChangesAsync();

            return Ok(itemTableRequest);
        }

        [HttpGet]
        [ActionName("GetItem")]
        [Route("{id}")]

        public async Task<IActionResult> GetItem([FromRoute] int id)
        {
            var item = await _itemTableDbContext.Items
                  .Include(c => c.category)
                  .FirstOrDefaultAsync(x => x.ItemID == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut]
        [ActionName("UpdateItem")]
        [Route("{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, itemsTableModel UpDateItemRequest)
        {
            var item = await _itemTableDbContext.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.company = UpDateItemRequest.company;
            item.availability = UpDateItemRequest.availability;
            item.price = UpDateItemRequest.price;
            item.discount = UpDateItemRequest.discount;
            item.tax = UpDateItemRequest.tax;
            item.description = UpDateItemRequest.description;
            item.categoryid = UpDateItemRequest.categoryid;

            await _itemTableDbContext.SaveChangesAsync();

            return Ok(item);

        }

        [HttpDelete]
        [ActionName("DeleteItem")]
        [Route("{id}")]

        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            var item = await _itemTableDbContext.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            _itemTableDbContext.Items.Remove(item);
            await _itemTableDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

