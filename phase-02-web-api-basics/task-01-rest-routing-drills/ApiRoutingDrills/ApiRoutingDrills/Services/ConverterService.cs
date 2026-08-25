namespace ApiRoutingDrills.Services
{
    public class ConverterService
    {
        public decimal ConvertCelsiusToFahrenheit(decimal celsius)
        {
            return (celsius * 9 / 5) + 32;
        }
    }
}
