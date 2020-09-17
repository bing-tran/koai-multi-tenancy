using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Koai.WebApi.Models;
using Koai.WebApi.MultiTenancy;
using Koai.MultiTenancy;

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
            var tenantContext = this.HttpContext.GetMultiTenantContext<Tenant, int>();
            _logger.LogDebug($"Tenant provider {tenantContext?.Provider?.GetType()}");
            _logger.LogDebug($"Tenant Strategy {tenantContext?.Strategy?.GetType()}");
            _logger.LogDebug($"Tenant: Id {tenantContext?.Tenant?.Id}, Name: {tenantContext?.Tenant?.Name}");

            var books = new List<Book>
            {
                new Book { Id = 1, Name = "Brief History of Time ", Publisher = "Koai Books" }
            };

            return Ok(books);
        }
    }
}
