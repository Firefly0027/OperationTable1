using App.DAL.DataContext;
using App.DAL.Models;
using Microsoft.IdentityModel.Tokens;
using OperationTable.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OperationTable.Repository;

public class JWTManagerRepository : IJWTManagerRepository<usersModel>
{
    private readonly OperationDbContext _context;
    private readonly IConfiguration _configuration;


    public JWTManagerRepository(OperationDbContext context , IConfiguration configuration)
    {
        _configuration = configuration;
        _context = context;
    }

    public Tokens Authenticate(usersModel users)
    {
        if(!_context.Users.Any(x => x.Email == users.Email && x.Password == users.Password))
        {
            return null;
        }

        var TokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
          {
              new Claim(ClaimTypes.Email, users.Email)
          }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = TokenHandler.CreateToken(tokenDescriptor);
        return new Tokens { Token = TokenHandler.WriteToken(token)};
    }
}
