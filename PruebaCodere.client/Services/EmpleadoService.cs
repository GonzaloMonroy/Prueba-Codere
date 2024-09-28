using BlazorCrud.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace PruebaCodere.client.Services
{
    public class EmpleadoService : EmpleadosIDService
    {
        private readonly HttpClient _http;

        public EmpleadoService(HttpClient http)
        {

            _http = http;
        }
        public async Task<List<EmpleadoDTO>> Lista()
        {
            var resultado = await _http.GetFromJsonAsync<ResponseAPI<List<EmpleadoDTO>>>("api/Empleados/Lista");
            if (resultado!.EsCorrecto)
            {

                return resultado.Valor!;

            }
            else
                throw new Exception(resultado.Mensaje);
        }
        public async Task<EmpleadoDTO> Buscar(int id)
        {
            var resultado = await _http.GetFromJsonAsync<ResponseAPI<EmpleadoDTO>>($"api/Empleados/Buscar/{id}");
            if (resultado!.EsCorrecto)
            {

                return resultado.Valor!;

            }
            else
                throw new Exception(resultado.Mensaje);
        }
        public async Task<int> Guardar(EmpleadoDTO empleado)
        {
            try
            {
                var resultado = await _http.PostAsJsonAsync("api/Empleados/Crear", empleado);
                resultado.EnsureSuccessStatusCode(); // Lanza una excepción si el código de estado no es 2xx
                var response = await resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();

                if (response!.EsCorrecto)
                {
                    return response.Valor!;
                }
                else
                {
                    throw new Exception(response.Mensaje);
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error o manejarlo de otra manera
                Console.WriteLine($"Error en Guardar: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Editar(EmpleadoDTO empleado)
        {
            try
            {
                var resultado = await _http.PutAsJsonAsync($"api/Empleados/Editar/{empleado.EmpleadoId}", empleado);
                resultado.EnsureSuccessStatusCode();
                var response = await resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();

                if (response!.EsCorrecto)
                {
                    return response.Valor!;
                }
                else
                {
                    throw new Exception(response.Mensaje);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Editar: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            var resultado = await _http.DeleteAsync($"api/Empleados/Eliminar/{id}");
            var response = await resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (response!.EsCorrecto)
            {

                return response.EsCorrecto!;

            }
            else
                throw new Exception(response.Mensaje);
        }

       

      
    }

}

