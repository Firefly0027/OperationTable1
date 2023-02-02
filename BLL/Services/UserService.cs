using App.DAL.DataContext;
using App.DAL.Models;
using DAL.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BLL.Services;

public class UserService : IGenericRepository<usersModel>
{
    private readonly IConfiguration _configuration;
    private readonly OperationDbContext _context;

    public UserService(IConfiguration configuration , OperationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public Task<bool> Add(usersModel entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<usersModel>> AddUser(usersModel users)
    {
        try
        {
           
            var paramList = new { Email = users.Email, Password = users.Password };

            string sql = @"SELECT * FROM Users WHERE Email = @Email AND Password = @Password";

          await using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                connection.Open();
                var user = connection.Query<usersModel>(sql, paramList);
                return user.ToList();
            }
            
        }
        catch (SqlException ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            throw;

        }
    }

    public Task<IEnumerable<usersModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<usersModel>> GetAllById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<usersModel?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Remove(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(usersModel users)
    {
        try
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("sqlServerConnStrr")))
            {
                var emailExists = connection.QueryFirstOrDefault<bool>(
                  "SELECT 1 FROM Users WHERE Email = @Email",
                    new { Email = users.Email });
                if (emailExists)
                {
                    return false;
                }
                else
                {
                    var paramList = new { Email = users.Email, Password = users.Password, UserName = users.UserName };

                    string sql = @"INSERT INTO [dbo].[Users]
                              ([Email]
                             ,[Password]
                             ,[UserName])
                         VALUES
                              (@Email
                             ,@Password
                             ,@UserName)";

                    var result = await connection.ExecuteAsync(sql, paramList);
                    return true;
                }
            }
        }
        catch
        {
            throw;
        }
    }
}
