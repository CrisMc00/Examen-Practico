using Microsoft.AspNetCore.Mvc;
using Examen_Práctico.Servicios;
using System.Collections.Generic;
using Examen_Práctico.Modelos;

namespace Examen_Práctico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ServicioClientes _servicioClientes;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _servicioClientes = new ServicioClientes();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("clientes")]
        public async Task<ActionResult> ObtenerClientes()
        {
            var clientes = await _servicioClientes.ObtenerCliente();

            if (clientes == null || clientes.Count == 0)
            {
                return NotFound("No se encontraron clientes.");
            }

            return Ok(clientes);
        }

        [HttpGet("informacion")]
        public async Task<ActionResult> ObtenerInformacionClienteImportante()
        {
            var clientes = await _servicioClientes.ObtenerInformacionClienteImportante();

            if (clientes == null || clientes.Count == 0)
            {
                return NotFound("No se encontraron clientes.");
            }

            return Ok(clientes); 
        }
        
        [HttpGet("direcciones/ordenadas")]
        public async Task<ActionResult> ObtenerDireccionesOrdenadas(int customerId, string ordenarPor = "Address1", bool ascendente = true)
        {
            var direcciones = await _servicioClientes.ObtenerDireccionesOrdenadas(customerId, ordenarPor, ascendente);

            if (direcciones == null || direcciones.Count == 0)
            {
                return NotFound("No se encontraron direcciones.");
            }

            return Ok(direcciones);
        }
        
        [HttpGet("direcciones/preferida")]
        public async Task<ActionResult> ObtenerDireccionPreferida(int customerId)
        {
            var direccion = await _servicioClientes.ObtenerDireccionPreferida(customerId);

            if (direccion == null)
            {
                return NotFound("No se encontron direccion preferida.");
            }

            return Ok(direccion);
        }
        
        [HttpGet("direcciones/busqueda")]
        public async Task<ActionResult> BuscarDireccionesPorCodigoPostal(int customerId, string codigoPostal)
        {
            var direcciones = await _servicioClientes.BuscarDireccionesPorCodigoPostal(customerId, codigoPostal);

            if (direcciones == null || direcciones.Count == 0)
            {
                return NotFound("No se encontron direccion con ese codigo postal.");
            }

            return Ok(direcciones);
        }
    }
}