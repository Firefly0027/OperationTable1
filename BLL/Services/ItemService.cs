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

public class ItemService : IGenericRepository<itemsTableModel>
{
    private readonly IConfiguration _configuration;
    public ItemService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<bool> Add(itemsTableModel entity)
    {
        try
        {
            var ParamList = new
            {
                company = entity.company,
                availability = entity.availability,
                price = entity.price,
                discount = entity.discount,
                tax = entity.tax,
                description = entity.description,
                categoryid = entity.categoryid
            };

            string sql = @"INSERT INTO [dbo].[Items]
           ([company]
           ,[availability]
           ,[price]
           ,[discount]
           ,[tax]
           ,[description]
           ,[categoryid])
            VALUES
            (@company,
            @availability,
            @price,
            @discount,
            @tax,
            @description,
            @categoryid)"
            ;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, ParamList);
                return true;
            }
        }
        catch
        {
            throw;
        }
    }

    public Task<IEnumerable<itemsTableModel>> AddUser(itemsTableModel entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<itemsTableModel>> GetAll()
    {
        try
        {
            var sql = "select * from Items";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                connection.Open();
                var result = await connection.QueryAsync<itemsTableModel>(sql);
                return result.ToList();
            }
        }
        catch
        {
            throw;
        }
    }

    public Task<IEnumerable<itemsTableModel>> GetAllById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<itemsTableModel?> GetById(int id)
    {
        try
        {
            var sql = "SELECT * FROM Items WHERE ItemID = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<itemsTableModel>(sql, new { Id = id });
                return result;
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> Remove(int id)
    {
        try
        {
            var sql = "DELETE FROM Items WHERE ItemID = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return true;
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> Update(itemsTableModel entity)
    {
        var paramList = new
        {
            Company = entity.company, Availability = entity.availability , Price = entity.price , Discount = entity.discount,
            Tax = entity.tax , Description = entity.description , Categoryid = entity.categoryid , ItemID = entity.ItemID
        };

       string sql = @"UPDATE [dbo].[Items]
       SET
       [company] = @Company
      ,[availability] = @Availability
      ,[price] = @Price
      ,[discount] = Discount
      ,[tax] = @Tax
      ,[description] = @Description
      ,[categoryid] = @Categoryid
       WHERE
       [ItemID] = @ItemID";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
        {
            connection.Open();
            await connection.ExecuteAsync(sql, paramList);
            return true;
        }
    }
}
