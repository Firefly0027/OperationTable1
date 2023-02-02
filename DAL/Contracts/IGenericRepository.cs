using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<bool> Add(T entity);
    Task<IEnumerable<T>> AddUser(T entity);
    Task<bool> Remove(int id);
    Task<bool> Update(T entity);
    Task<IEnumerable<T>> GetAllById(int id);
}
