using App.DAL.Models;
using DAL.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OperationTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services;

public class CategoryService : IGenericRepository<categoryModel>
{
    private readonly IConfiguration _configuration;

    public CategoryService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<bool> Add(categoryModel entity)
    {
        try
        {
            var ParamList = new
            {
                categoryName = entity.categoryName,
            };

            string sql = @"INSERT INTO [dbo].[category]
                    ([categoryName])
                    VALUES
                    (@categoryName)"
            ;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, ParamList);
                return true;
            }
        }
        catch (Exception ex)
        {
            Logger.Log(ex.Message);
            throw;
        }
    }

    public Task<IEnumerable<categoryModel>> AddUser(categoryModel entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<categoryModel>> GetAll()
    {
        try
        {
            var sql = @"select * from category";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                connection.Open();
                var result = await connection.QueryAsync<categoryModel>(sql);
                return result.ToList();
            }
        }
        catch (Exception ex)
        {
            Logger.Log(ex.Message);
            throw;
        }
    }

    public Task<IEnumerable<categoryModel>> GetAllById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<categoryModel?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(categoryModel entity)
    {
        throw new NotImplementedException();
    }
}
