using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FranchiseController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<FranchiseController> _logger;

        public FranchiseController(IConfiguration configuration, ILogger<FranchiseController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<object> Get()
        {
            return Ok(new { controller = "FranchiseController", status = "active" });
        }
    }
}
