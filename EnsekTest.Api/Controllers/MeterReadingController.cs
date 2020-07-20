using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnsekTest.Service.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnsekTest.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingController(
            IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            return Ok(await _meterReadingService.ImportMeterReadingsFromCSV(file));
        }
    }
}