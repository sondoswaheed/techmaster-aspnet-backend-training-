using ApiRoutingDrills.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRoutingDrills.Controllers
{
    [Route("api/Converter")]
    [ApiController]
    public class TemperatureConversionController : ControllerBase
    {
        private readonly ConverterService _converterService;
        public TemperatureConversionController(ConverterService converterService)
        {
            _converterService = converterService;
        }

        [HttpGet("celsius-to-fahrenheit")]
        public IActionResult Convert([FromQuery]decimal cel)
        {
            decimal result= _converterService.ConvertCelsiusToFahrenheit(cel);

            return Ok(new{
                Celsius=cel,
                Fehrinhiet=result,
                formula = "(Celsius × 9 / 5) + 32"
            });
        }
    }
}
