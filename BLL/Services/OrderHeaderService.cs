using App.DAL.Models;
using DAL.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services;

public class OrderHeaderService : IGenericRepository<OrderTableModel>
{
    private readonly IConfiguration _configuration;

    public OrderHeaderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> Add(OrderTableModel orderTable)
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

                orderTable.Id = con.Query<int>(query, paramList).Single();

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
            return true;
        }
        catch
        {
            throw;
        }
    }

    public Task<IEnumerable<OrderTableModel>> AddUser(OrderTableModel entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderTableModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrderTableModel>> GetAllById(int id)
    {
        try
        {
            var orderTable = $"select * from dbo.OrderHeader where userid = {id}";

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                var result = await con.QueryAsync<OrderTableModel>(orderTable);
                return result;
            }
        }
        catch
        {
            throw;
        }
    }

    public Task<OrderTableModel?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Remove(int id)
    {
        try
        {
            var query = $"DELETE FROM [dbo].[OrderHeader] WHERE id = {id}";

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                con.Open();
                var result = await con.ExecuteAsync(query);
                return true;
            }
        }
        catch
        {
            throw;
        }
    }

    public Task<bool> Update(OrderTableModel entity)
    {
        throw new NotImplementedException();
    }
}
