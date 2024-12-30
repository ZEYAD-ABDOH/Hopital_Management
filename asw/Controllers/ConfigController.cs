using asw.Model;
using asw.Service;
using Jose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace asw.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<AttOpation> _attopation;

        public ConfigController(IConfiguration configuration, IOptionsMonitor<AttOpation> attopation)
        {
            _configuration = configuration;
            _attopation = attopation;
            var attopationd = attopation.CurrentValue;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetConfig()
        {
            Thread.Sleep(2000);
            var config = new
            {
                jwt = _configuration["Jwt:Issuer"],
                jwt2 = _configuration.GetConnectionString("Defaultconnection"),
                jwt3 = _configuration.GetConnectionString("Jwt:Aidoence"),
                jwtBd = _configuration["ConnectionStrings:Defaultconnection"],
                Defaultconnection = _configuration["Logging:LogLevel:Default"],
                TestKey = _configuration["TestKey"],
                singKey = _configuration["singKey"],
                Attopation = _attopation.CurrentValue,
            };

            return Ok(config);

        }
        [HttpPost]
        [Route("")]
        public IActionResult GetConfiga([FromQuery]User user, [FromQuery] User user1, [FromHeader (Name = "Log")]string Log)
        {
            return Ok(user);
        }
    }
}
