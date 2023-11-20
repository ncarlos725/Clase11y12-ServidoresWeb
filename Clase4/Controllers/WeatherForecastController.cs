using Microsoft.AspNetCore.Mvc;

namespace Clase4.Controllers
{
    [ApiController] // Indica que esto es un controlador, y permiten que sus metodos sean publicados en el servidor
    [Route("[controller]")]// Le da una ruta a todos los metodos del controlador , si el valor dentro del route  es "[controller]", entonces
                           // el valor del Route va a ser reemplazado  por el nombre del controlador , sin la palabra controller en este caso 
                           // WhatherForecast 
    public class WeatherForecastController : ControllerBase  // Todo controlador hereda de Controller Base
    {
        //---------Atributos-------------------------------------------------------------------------------
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };// un controlador es un objeto, y pueden tener metodos y atributos que sean necesario

        private readonly ILogger<WeatherForecastController> _logger; // los atributos de un controlador , generalmente son privados

        public WeatherForecastController(ILogger<WeatherForecastController> logger) // Como Objeto puede tener un constructor
        {
            _logger = logger;
        }


        //--------------Metodos-------------------------------------------------------------------------------
        [HttpGet(Name = "prueba")] // anotador de servicios web: indica que este metodo puede ser llamado por otra computadora
                                               // HttpGet: Este metodo debe ser llamado por Get
                                               // Name: Para llamar el metodo , debo utilizar el nombre "GetWeatherForecast" en la URL
        public int Get(int id)
        {
            return id;
        }
    }
}