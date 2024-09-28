using BlazorCrud.Shared;

namespace PruebaCodere.client.Services
{
    public interface GrupoIDService
    {
        Task<List<GrupoDTO>> Lista();   
    }
}
