using BallastlaneAPI.Security;
using BallastlaneBLL;
using Microsoft.AspNetCore.Mvc;

namespace BallastlaneAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersAPI : ControllerBase
    {
        private readonly ILogger<UsersAPI> _logger;
        private readonly IConfiguration _configuration;

        public UsersAPI(ILogger<UsersAPI> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("init")]
        public IActionResult Init()
        {
            try
            {
                var response = new UserBLL(_configuration).InitUsers();
                return Ok(new
                {
                    Result = response
                });
            }
            catch (Exception  ex)
            {
                return Problem(
                    statusCode: 500,
                    title: "Error initalizing users",
                    detail: ex.Message
                );
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string userName, string password)
        {
            try
            {
                var response = new UserBLL(_configuration).Login(userName, password);
                return Ok(new
                {
                    Result = response
                });
            }
            catch (Exception ex)
            {
                return Problem(
                    statusCode: 500,
                    title: "Login failure",
                    detail: ex.Message
                );
            }
        }


        [HttpPost]
        [Route("create")]
        [BallastlaneAuthorize]
        public IActionResult Create(string Authorization, string userName, string firstName, string lastName, string password)
        {
            try
            {
                new UserBLL(_configuration).Create(userName, firstName, lastName, password);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(
                    statusCode: 500,
                    title: "Create failure",
                    detail: ex.Message
                );
            }
        }

        [HttpGet]
        [Route("read")]
        [BallastlaneAuthorize]
        public IActionResult Read(string Authorization)
        {
            try
            {
                var users = new UserBLL(_configuration).Read();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(
                    statusCode: 500,
                    title: "Read failure",
                    detail: ex.Message
                );
            }
        }

        [HttpPut]
        [Route("update")]
        [BallastlaneAuthorize]
        public IActionResult Update(string Authorization, string userName, string firstName, string lastName, string password)
        {
            try
            {
                new UserBLL(_configuration).Update(userName, firstName, lastName, password);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(
                    statusCode: 500,
                    title: "Update failure",
                    detail: ex.Message
                );
            }
        }

        [HttpDelete]
        [Route("delete")]
        [BallastlaneAuthorize]
        public IActionResult Delete(string Authorization, string userName)
        {
            try
            {
                new UserBLL(_configuration).Delete(userName);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(
                    statusCode: 500,
                    title: "Delete failure",
                    detail: ex.Message
                );
            }
        }

    }
}
