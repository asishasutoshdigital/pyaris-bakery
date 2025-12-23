using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IConfiguration configuration, ILogger<CustomerController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<object> Get()
        {
            return Ok(new { controller = "CustomerController", status = "active" });
        }
    }
}
