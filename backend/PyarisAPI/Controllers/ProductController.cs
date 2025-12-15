using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using PyarisAPI.Models;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IConfiguration configuration, ILogger<ProductController> logger)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
        }

        /// <summary>
        /// Get products by subgroup (converted from spark.aspx.cs LoadForSubGroup)
        /// </summary>
        [HttpGet("subgroup/{subgroup}")]
        public ActionResult<IEnumerable<ProductModel>> GetBySubGroup(string subgroup, [FromQuery] string? grp = null, [FromQuery] bool isFlavour = false)
        {
            try
            {
                var productsList = new List<ProductModel>();
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    SqlCommand cmdx;
                    
                    if (grp != null)
                    {
                        cmdx = new SqlCommand($"SELECT [id],[menu name],[sell price],[Group],[active] FROM [XMaster Menu] WHERE [Group]='{grp.Replace("'", "''")}' AND [Sub group] LIKE '%{subgroup.Replace("'", "''")}%' AND [active] = 1", cn);
                    }
                    else
                    {
                        cmdx = new SqlCommand($"SELECT [id],[menu name],[sell price],[Group],[active] FROM [XMaster Menu] WHERE [Sub group] LIKE '%{subgroup.Replace("'", "''")}%' AND [active] = 1", cn);
                    }
                    
                    var drx = cmdx.ExecuteReader();
                    if (drx.HasRows)
                    {
                        while (drx.Read())
                        {
                            if (drx[3].ToString() != "CAKES")
                            {
                                var product = new ProductModel()
                                {
                                    Id = drx[0].ToString() ?? "",
                                    MenuName = drx[1].ToString() ?? "",
                                    SellPrice = drx[2].ToString() ?? "",
                                    Group = drx[3].ToString() ?? "",
                                    Active = drx[4].ToString() ?? "",
                                };
                                productsList.Add(product);
                            }
                        }
                    }
                }
                
                return Ok(productsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by subgroup");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get product by ID (converted from sparkdetails.aspx.cs)
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<ProductModel> GetById(string id)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand($"SELECT [id],[menu name],[Barcode],[Group],[Sub Group],[Feature 1],[Feature 2],[Feature 3],[Feature 4],[Cost Price],[Sell Price],[Tax],[Stock],[Discount Type],[Discount Value],[Min Weight],[Flavour],[active],[Modified Date] FROM [XMaster Menu] WHERE [id]='{id.Replace("'", "''")}'", cn);
                    var dr = cmd.ExecuteReader();
                    
                    if (dr.Read())
                    {
                        var product = new ProductModel
                        {
                            Id = dr[0].ToString() ?? "",
                            MenuName = dr[1].ToString() ?? "",
                            Barcode = dr[2].ToString() ?? "",
                            Group = dr[3].ToString() ?? "",
                            SubGroup = dr[4].ToString() ?? "",
                            Feature1 = dr[5].ToString() ?? "",
                            Feature2 = dr[6].ToString() ?? "",
                            Feature3 = dr[7].ToString() ?? "",
                            Feature4 = dr[8].ToString() ?? "",
                            CostPrice = dr[9].ToString() ?? "",
                            SellPrice = dr[10].ToString() ?? "",
                            Tax = dr[11].ToString() ?? "",
                            Stock = dr[12].ToString() ?? "",
                            DiscountType = dr[13].ToString() ?? "",
                            DiscountValue = dr[14].ToString() ?? "",
                            MinWeight = dr[15].ToString() ?? "",
                            Flavour = dr[16].ToString() ?? "",
                            Active = dr[17].ToString() ?? "",
                            ModifiedDate = dr[18].ToString() ?? ""
                        };
                        return Ok(product);
                    }
                    
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product by ID");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> GetAll([FromQuery] string? group = null, [FromQuery] bool activeOnly = true)
        {
            try
            {
                var productsList = new List<ProductModel>();
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    string query = "SELECT [id],[menu name],[sell price],[Group],[Sub Group],[active] FROM [XMaster Menu]";
                    
                    if (group != null || activeOnly)
                    {
                        query += " WHERE ";
                        var conditions = new List<string>();
                        
                        if (group != null)
                            conditions.Add($"[Group]='{group.Replace("'", "''")}'");
                        if (activeOnly)
                            conditions.Add("[active] = 1");
                        
                        query += string.Join(" AND ", conditions);
                    }
                    
                    var cmd = new SqlCommand(query, cn);
                    var dr = cmd.ExecuteReader();
                    
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var product = new ProductModel
                            {
                                Id = dr[0].ToString() ?? "",
                                MenuName = dr[1].ToString() ?? "",
                                SellPrice = dr[2].ToString() ?? "",
                                Group = dr[3].ToString() ?? "",
                                SubGroup = dr[4].ToString() ?? "",
                                Active = dr[5].ToString() ?? ""
                            };
                            productsList.Add(product);
                        }
                    }
                }
                
                return Ok(productsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get distinct groups
        /// </summary>
        [HttpGet("groups")]
        public ActionResult<IEnumerable<string>> GetGroups()
        {
            try
            {
                var groups = new List<string>();
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand("SELECT DISTINCT [Group] FROM [XMaster Menu] WHERE [active] = 1 ORDER BY [Group]", cn);
                    var dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        groups.Add(dr[0].ToString() ?? "");
                    }
                }
                
                return Ok(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product groups");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get distinct subgroups for a group
        /// </summary>
        [HttpGet("subgroups")]
        public ActionResult<IEnumerable<string>> GetSubGroups([FromQuery] string? group = null)
        {
            try
            {
                var subGroups = new List<string>();
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    string query = "SELECT DISTINCT [Sub Group] FROM [XMaster Menu] WHERE [active] = 1";
                    
                    if (group != null)
                        query += $" AND [Group]='{group.Replace("'", "''")}'";
                    
                    query += " ORDER BY [Sub Group]";
                    
                    var cmd = new SqlCommand(query, cn);
                    var dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        subGroups.Add(dr[0].ToString() ?? "");
                    }
                }
                
                return Ok(subGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product subgroups");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
