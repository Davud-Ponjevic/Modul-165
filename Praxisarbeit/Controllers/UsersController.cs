using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Praxisarbeit.Dto;
using Praxisarbeit.Model;
using Praxisarbeit.Services;

namespace Praxisarbeit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public UsersController(AppDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
        


        [HttpPost("login")]
        public IActionResult Login(LoginDto model)
        {
            var user = _dbContext.Users.Find(u => u.userName == model.UserName).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (user.password.Equals(model.Password))
            {
                var token = _tokenService.CreateToken(model.UserName);
                return Ok(
                    new JsonResult(new { token = token, username = user.userName })
                );
            }

            return BadRequest("Invalid login data");
        }

    }
}
