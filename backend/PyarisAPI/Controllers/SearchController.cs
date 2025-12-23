using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<SearchController> _logger;

        public SearchController(IConfiguration configuration, ILogger<SearchController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<object> Get()
        {
            return Ok(new { controller = "SearchController", status = "active" });
        }
    }
}
