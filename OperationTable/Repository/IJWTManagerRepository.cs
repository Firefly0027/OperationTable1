using App.DAL.Models;
using Microsoft.VisualBasic.ApplicationServices;
using OperationTable.Models;

namespace OperationTable.Repository;

public interface IJWTManagerRepository<T>
{
    Tokens Authenticate(usersModel users);
}
