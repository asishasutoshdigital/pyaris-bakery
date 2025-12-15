using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<object> Get()
        {
            return Ok(new { controller = "HomeController", status = "active" });
        }
    }
}
