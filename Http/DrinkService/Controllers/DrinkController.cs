using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace DrinkService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrinkController : ControllerBase
    {

        private readonly ILogger<DrinkController> _logger;

        public DrinkController(ILogger<DrinkController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<Guid> Create(DrinkCommand command)
        {
            var sw = Stopwatch.StartNew();
            var drink = Drink.MakeDrink(command.Type, command.Flavor, command.Size);
            sw.Stop();
            _logger.LogInformation($"Drink {drink.Type.ToString()} with flavor {drink.Flavor.ToString()} sized {drink.Size.ToString()} has made in {sw.ElapsedMilliseconds}");
            return Ok(drink.Id);
        }
    }

    public class DrinkCommand
    {
        public DrinkType Type { get; set; }
        public DrinkFlavor Flavor { get; set; }
        public DrinkSize Size { get; set; }
    }
}
