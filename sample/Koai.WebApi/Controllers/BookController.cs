using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Koai.WebApi.Models;

namespace Koai.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetBooksAsync()
        {
            var books = new List<Book>
            {
                new Book { Id = 1, Name = "Brief History of Time ", Publisher = "Koai Books" }
            };

            return Ok(books);
        }
    }
}
