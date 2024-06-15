using BallastlaneBLL;
using Microsoft.AspNetCore.Mvc;

namespace BallastlaneAPI.Controllers
{
    [ApiController]
    [Route("/books")]
    public class BooksAPI : ControllerBase
    {
        private readonly ILogger<BooksAPI> _logger;
        private readonly IConfiguration _configuration;

        public BooksAPI(ILogger<BooksAPI> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(string name, string author, int year)
        {
            try
            {
                new BookBLL(_configuration).Create(name, author, year);
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
        public IActionResult Read()
        {
            try
            {
                var Books = new BookBLL(_configuration).Read();
                return Ok(Books);
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
        public IActionResult Update(string bookId, string name, string author, int year)
        {
            try
            {
                new BookBLL(_configuration).Update(bookId, name, author, year);
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
        public IActionResult Delete(string bookId)
        {
            try
            {
                new BookBLL(_configuration).Delete(bookId);
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
