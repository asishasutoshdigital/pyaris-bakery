using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagesController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<PagesController> _logger;

        public PagesController(IConfiguration configuration, ILogger<PagesController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<object> Get()
        {
            return Ok(new { controller = "PagesController", status = "active" });
        }
    }
}
