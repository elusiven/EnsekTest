using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnsekTest.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost("import")]
        public async Task<IActionResult> Import([FromForm] IFormFile file)
        {
            using (var sr = new StreamReader(file.OpenReadStream()))
            {
                var content = await sr.ReadToEndAsync();
                return Ok(content);
            }
        }
    }
}