using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace BurgerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BurgerController : ControllerBase
    {
        private readonly ILogger<BurgerController> _logger;

        public BurgerController(ILogger<BurgerController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<Guid> Create(BurgerCommand command)
        {
            var sw = Stopwatch.StartNew();
            var burger = Burger.MakeBurger(command.Type, command.CheeseQuantity, command.MeatQuantity);
            sw.Stop();
            _logger.LogInformation($"Burger {burger.Id} with Cheese {burger.Cheese.ToString()} has made in {sw.ElapsedMilliseconds}");
            return Ok(burger.Id);
        }
    }

    public class BurgerCommand
    {
        public CheeseType Type { get; set; }
        public int CheeseQuantity { get; set; }
        public int MeatQuantity { get; set; }
    }
}
