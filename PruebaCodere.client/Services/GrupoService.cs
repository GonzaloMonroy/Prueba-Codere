using BlazorCrud.Shared;
using System.ComponentModel.Design;
using System.Net.Http.Json;

namespace PruebaCodere.client.Services
{
    public class GrupoService : GrupoIDService
    {
        private readonly HttpClient _http;

        public GrupoService(HttpClient http)
        {

            _http = http;
        }

        public async Task<List<GrupoDTO>> Lista()
        {
            var resultado = await _http.GetFromJsonAsync<ResponseAPI<List<GrupoDTO>>>("api/Grupo/Lista");
            if (resultado!.EsCorrecto)
            {

                return resultado.Valor!;
                
            }
            else
                throw new Exception(resultado.Mensaje);

        }

    }

}
