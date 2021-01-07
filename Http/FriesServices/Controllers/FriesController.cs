using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace FriesServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriesController : ControllerBase
    {


        private readonly ILogger<FriesController> _logger;

        public FriesController(ILogger<FriesController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<Guid> Create(FriesCommand command)
        {
            var sw = Stopwatch.StartNew();
            var fries = Fries.MakeFries(command.Type);
            sw.Stop();
            _logger.LogInformation($"Fries {fries.Type.ToString()} has made in {sw.ElapsedMilliseconds}");
            return Ok(fries.Id);
        }
    }
    public class FriesCommand
    {
        public FriesType Type { get; set; }
    }
}
