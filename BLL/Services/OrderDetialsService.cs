using App.DAL.Models;
using DAL.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OperationTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services;

public class OrderDetialsService : IGenericRepository<OrderDetialsModel>
{

    private readonly IConfiguration _configuration;

    public OrderDetialsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<bool> Add(OrderDetialsModel entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDetialsModel>> AddUser(OrderDetialsModel entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDetialsModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrderDetialsModel>> GetAllById(int id)
    {
        try
        {
            var detials = $@"SELECT o.*, i.company FROM OrderDetials o 
                              JOIN items i ON o.ItemID = i.ItemID where o.OrderID = {id}";

            await using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                var dd = con.Query<OrderDetialsModel>(detials);
                return dd;
            }
        }
        catch (Exception ex)
        {
            Logger.Log(ex.Message);
            throw;
        }
        return null;
    }

    public async Task<OrderDetialsModel?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(OrderDetialsModel entity)
    {
        throw new NotImplementedException();
    }
}
