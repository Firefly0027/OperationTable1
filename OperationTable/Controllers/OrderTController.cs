using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using App.DAL.DataContext;
using App.DAL.Models;
using System.Transactions;

namespace OperationTable.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderTController : ControllerBase
    {
        private readonly OperationDbContext _ordersDbContext;
        private readonly IConfiguration _configuration;

        public OrderTController(OperationDbContext usersANDordersDbContext, IConfiguration configuration)
        {
            _ordersDbContext = usersANDordersDbContext;
            _configuration = configuration;
        }

        [HttpGet]
        [ActionName("GetOrderTable")]
        public async Task<ActionResult<List<OrderTableModel>>> GetOrderTable(int id)
        {
            var orderTable = $"select * from dbo.OrderHeader where userid = {id}";

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                var result = await con.QueryAsync<OrderTableModel>(orderTable);
                return result.ToList();
            }
        }

        [HttpPost]
        [ActionName("AddOrderTable")]
        public async Task<ActionResult<List<OrderTableModel>>> AddOrderTable([FromBody] OrderTableModel orderTable)
        {

            using (var scope = new TransactionScope())
            {
                try
                {
                    await using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
                    {
                        con.Open();
                        var paramList = new
                        {
                            OperationType = orderTable.OperationType,
                            Date = orderTable.Date,
                            Address = orderTable.Address,
                            Custname = orderTable.Custname,
                            NetTotal = orderTable.NetTotal,
                            GrossTotal = orderTable.GrossTotal,
                            DiscountTotal = orderTable.DiscountTotal,
                            TaxTotal = orderTable.TaxTotal,
                            QuantityTotal = orderTable.QuantityTotal,
                            UserID = orderTable.UserID
                        };

                        string query = @"
                                               INSERT INTO [dbo].[OrderHeader]                                                
                                              ([OperationType]
                                              ,[Date] 
                                              ,[Address]
                                              ,[Custname] 
                                              ,[NetTotal] 
                                              ,[GrossTotal]
                                              ,[DiscountTotal]
                                              ,[TaxTotal]
                                              ,[QuantityTotal]
                                              ,[UserID])
                                        VALUES
                                            (@OperationType
                                              ,@Date
                                              ,@Address
                                              ,@Custname
                                              ,@NetTotal
                                              ,@GrossTotal
                                              ,@DiscountTotal
                                              ,@TaxTotal
                                              ,@QuantityTotal
                                              ,@UserID)
                                               SELECT SCOPE_IDENTITY()";

                        orderTable.Id =  con.Query<int>(query, paramList).Single();

                        var details = orderTable.orderDetials.ToList();

                        for (int i = 0; i < details.Count; i++)
                        {
                            var paramList2 = new
                            {
                                OrderID = orderTable.Id,
                                ItemID = details[i].ItemID,
                                discount = details[i].discount,
                                Quantity = details[i].Quantity,
                                tax = details[i].tax,
                                Total = details[i].Total,
                                GrossTotal = details[i].GrossTotal,
                                Price = details[i].Price,
                            };

                            string query2 = @"INSERT INTO OrderDetials ([OrderID],[ItemID] , [discount], [Quantity],[tax],[Total], [GrossTotal] , [Price])
                                                           VALUES (@OrderID ,@ItemID , @discount , @Quantity, @tax , @Total , @GrossTotal , @Price)";

                            con.Execute(query2, paramList2); 
                        }
                    }
                    scope.Complete();
                    }
                catch (Exception e)
                {
                    string exception = e.Message;
                    throw;
                }
                return Ok();
            }
           
        }

        [HttpDelete]
        [ActionName("DeleteOrder")]
        [Route("{id}")]

        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var query = $"DELETE FROM [dbo].[OrderHeader] WHERE id = {id}";

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                con.Open();
                var result = await con.ExecuteAsync(query);
                return Ok(result.ToString());
            }

        }


        [HttpGet]
        [ActionName("GetOrderDetials")]
        public async Task<IActionResult> GetOrderDetials(int id)
        {
            try
            {
                var detials = $@"SELECT o.*, i.company FROM OrderDetials o 
                              JOIN items i ON o.ItemID = i.ItemID where o.OrderID = {id}";

                await using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
                {
                    var dd = con.Query<OrderDetialsModel>(detials);
                    return Ok(dd);
                }
            }
            catch (Exception ex)
            {
                string v = ex.InnerException.Message;
            }
            return null;
        }

    }
 }

