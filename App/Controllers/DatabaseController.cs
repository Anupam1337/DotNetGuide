using Common.Connection;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly ConnectionManager _connectionManager;
        private readonly DbConnectionFactory _dbConnectionFactory;

        public DatabaseController(DatabaseConfig config)
        {
            _connectionManager = new ConnectionManager(config);
            _dbConnectionFactory = new DbConnectionFactory(config);
        }

        [HttpPost("checkConnectionWithoutDb")]
        public async Task<IActionResult> CheckConnectionWithoutDb([FromBody] DatabaseConfig config)
        {
            var databases = await _connectionManager.CheckConnectionWithoutDb(config);
            return Ok(new { success = true, databases });
        }

        [HttpPost("checkConnectionWithDb")]
        public async Task<IActionResult> CheckConnectionWithDb([FromBody] DatabaseConfig config)
        {
            var isConnected = await _connectionManager.CheckConnectionWithDb(config);
            return Ok(new { success = isConnected });
        }
    }

}
