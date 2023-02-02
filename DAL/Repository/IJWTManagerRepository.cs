using App.DAL.Models;
using OperationTable.Models;

namespace OperationTable.Repository;

public interface IJWTManagerRepository<T>
{
    Tokens Authenticate(usersModel users);
}
