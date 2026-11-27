using System.Net.Http.Json;
using MecaControlRD.Blazor.Models;

namespace MecaControlRD.Blazor.Services
{
    public class ClientesApiService
    {
        private readonly HttpClient _http;

        public ClientesApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ClienteDTO>> ObtenerClientesAsync()
        {
            var respuesta = await _http.GetFromJsonAsync<
                RespuestaApi<List<ClienteDTO>>>("api/Clientes");

            return respuesta?.Value ?? new List<ClienteDTO>();
        }

        public async Task<bool> CrearClienteAsync(ClienteDTO cliente)
        {
            var response = await _http.PostAsJsonAsync(
                "api/Clientes", cliente);

            return response.IsSuccessStatusCode;
        }
    }
}
