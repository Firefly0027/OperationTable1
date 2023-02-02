using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.DataContext;
using App.DAL.Models;
using OperationTable.Repository;
using Microsoft.AspNetCore.Authorization;

namespace OperationTable.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersControllers : ControllerBase
    {
        private readonly OperationDbContext _usersDbContext;
        private readonly IJWTManagerRepository<usersModel> _repository;

        public UsersControllers(OperationDbContext usersANDordersDbContext , IJWTManagerRepository<usersModel> repository)
        {
            _usersDbContext = usersANDordersDbContext;
            _repository = repository;
        }

        [HttpGet]
         public async Task<IActionResult> GetUser()
        {
            var result = await _usersDbContext.Users.ToListAsync();
            Logger.Log("authorized to fetch the user Emails and Passwords");
            return Ok(result);
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
            Logger.Log("User logging in");
            return Ok(Token);
            //if (users == null)
            //{
            //    return BadRequest();
            //}

            //var user = await _usersDbContext.Users
            //    .FirstOrDefaultAsync(x => x.Email == users.Email && x.Password == users.Password);
            //if (user == null)
            //    return NotFound(new { Message = "User Not Found" });

            //return Ok
            //    (  
            //   user
            //    );
        }
        [HttpPost]
        [ActionName("Register")]

        public async Task<IActionResult> Register([FromBody] usersModel users)
        {
            if (users == null)
            {
                return BadRequest();
            }

            var user = _usersDbContext.Users.Where(e => e.Email == users.Email).FirstOrDefault();
            if (user != null)
            {
                return BadRequest("Email Already Exists!");
            }

            await _usersDbContext.Users.AddAsync(users);
            await _usersDbContext.SaveChangesAsync();
            return Ok
                (new { Message = " Registeration Success!" });
        }
    }
}
