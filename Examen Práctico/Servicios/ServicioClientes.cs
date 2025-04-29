using Newtonsoft.Json;
using System.Net.Http.Headers;
using Examen_Práctico.Modelos;

namespace Examen_Práctico.Servicios
{
    public class ServicioClientes
    {
        private readonly HttpClient _httpClient;

        public ServicioClientes()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Version", "2.0.6.0");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", "Y2hyaXN0b3BoZXJAZGV2ZWxvcC5teDpUZXN0aW5nRGV2ZWxvcDEyM0AuLi4=");
        }

        public async Task<List<Cliente>> ObtenerCliente()
        {
            /*
            var url = "https://examentecnico.azurewebsites.net/v3/api/Test/Customer";

            var respuesta = await _httpClient.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<List<Cliente>>(json);

                if (clientes == null)
                {
                    return new List<Cliente>();
                }
                return clientes;
            }

            return new List<Cliente>(); 
            */

            return new List<Cliente>
            {
                new Cliente
                {
                    customerId = 1,
                    Email = "crismata@gmail.com",
                    Telefono = "643-123-4567",
                    Nombre = "Cris",
                    Apellido = "Mata",
                    FechaCumpleaños = new DateTime(2005, 9, 16),
                    address = new List<Direccion>
                    {
                        new Direccion { Address1 = "Calle 1", CreationDate = new DateTime(2023,1,1), Preferida = false, CodigoPostal = "12345" },
                        new Direccion { Address1 = "Calle 2", CreationDate = new DateTime(2022,5,10), Preferida = true, CodigoPostal = "54321" },
                        new Direccion { Address1 = "Calle 3", CreationDate = new DateTime(2024,3,20), Preferida = false, CodigoPostal = "32245" },
                    }
                },
                new Cliente
                {
                    customerId = 2,
                    Email = "paolamata@gmail.com",
                    Telefono = "235-987-6543",
                    Nombre = "Paola",
                    Apellido = "Mata",
                    FechaCumpleaños = new DateTime(1999, 10, 9),
                    address = new List<Direccion>
                    {
                        new Direccion { Address1 = "Calle 4", CreationDate = new DateTime(2021,11,5), Preferida = true, CodigoPostal = "67890" },
                        new Direccion { Address1 = "Calle 5", CreationDate = new DateTime(2022,1,20), Preferida = false, CodigoPostal = "4213" },
                    }
                },
                new Cliente
                {
                    customerId = 3,
                    Email = "carlos.gomez@correo.com",
                    Telefono = "555-555-5555",
                    Nombre = "Carlos",
                    Apellido = "Gómez",
                    FechaCumpleaños = new DateTime(1995, 5, 10),
                    address = new List<Direccion>
                    {
                        new Direccion { Address1 = "Calle 6", CreationDate = new DateTime(2020,7,14), Preferida = false, CodigoPostal = "5125" },
                        new Direccion { Address1 = "Calle 7", CreationDate = new DateTime(2021,6,18), Preferida = true, CodigoPostal = "3215" },
                    }
                }
            };
        }

        // 1.- Obtener solo la información importante del cliente
        public async Task<List<ClienteInfo>> ObtenerInformacionClienteImportante()
        {
            var clientes = await ObtenerCliente();

            var clienteInfo = clientes.Select(c => new ClienteInfo
            {
                customerId = c.customerId,
                Email = c.Email,
                Telefono = c.Telefono,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                FechaCumpleaños = c.FechaCumpleaños,
            }).ToList();

            return clienteInfo;
        }

        // 2.- Ordenar las direcciones del cliente
        public async Task<List<Direccion>> ObtenerDireccionesOrdenadas(int customerId, string ordenarPor = "Address1", bool ascendente = true)
        {
            var clientes = await ObtenerCliente();
            var cliente = clientes.FirstOrDefault(c => c.customerId == customerId);

            if (cliente == null)
            {
                return new List<Direccion>();
            }

            var direcciones = cliente.address;

            // Ordenar por la propiedad seleccionada
            if (ordenarPor.ToLower() == "creationdate")
            {
                if (ascendente)
                {
                    return direcciones.OrderBy(d => d.CreationDate).ToList();
                }
                else
                {
                    return direcciones.OrderByDescending(d => d.CreationDate).ToList();
                }
            }

            // Default: Ordenar por Address1
            if (ascendente)
            {
                return direcciones.OrderBy(d => d.Address1).ToList();
            }
            else
            {
                return direcciones.OrderByDescending(d => d.Address1).ToList();
            }
        }

        // 3.- Obtener la dirección preferida del cliente
        public async Task<Direccion> ObtenerDireccionPreferida(int customerId)
        {
            var clientes = await ObtenerCliente();
            var cliente = clientes.FirstOrDefault(c => c.customerId == customerId);

            if (cliente == null)
            {
                return null;
            }

            return cliente.address.FirstOrDefault(d => d.Preferida);
        }

        // 4.- Buscar direcciones por código postal
        public async Task<List<Direccion>> BuscarDireccionesPorCodigoPostal(int customerId, string codigoPostal)
        {
            var clientes = await ObtenerCliente();
            var cliente = clientes.FirstOrDefault(c => c.customerId == customerId);

            if (cliente == null)
            {
                return new List<Direccion>();
            }

            return cliente.address.Where(d => d.CodigoPostal == codigoPostal).ToList();
        }
    }
}
