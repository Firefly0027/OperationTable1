using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.DataContext;
using App.DAL.Models;
using OperationTable.Repository;
using Microsoft.AspNetCore.Authorization;
using DAL.Contracts;
using Microsoft.VisualBasic.ApplicationServices;

namespace OperationTable.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly OperationDbContext _usersDbContext;
        private readonly IJWTManagerRepository<usersModel> _repository;
        private readonly IGenericRepository<usersModel> _repositoryy;

        public UsersControllers(OperationDbContext usersANDordersDbContext , IJWTManagerRepository<usersModel> repository)
        {
            _usersDbContext = usersANDordersDbContext;
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Login")]

        public async Task<IActionResult> Login([FromBody] usersModel users)
        {
            var Token = _repository.Authenticate(users);
            
            if(Token == null)
            {
                return Unauthorized();
            }
            return Ok(Token);
        
        }
        [HttpPost]
        [ActionName("Register")]

        public async Task<IActionResult> Register(usersModel user)
        {
            var result = await _repositoryy.Add(user);
            return Ok(result);
        }
    }
}
