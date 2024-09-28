using BlazorCrud.Shared;

namespace PruebaCodere.client.Services
{
    public interface EmpleadosIDService
    {
        Task<List<EmpleadoDTO>> Lista();
        Task<EmpleadoDTO> Buscar(int id);
        Task<int> Guardar(EmpleadoDTO empleado);
        Task<int> Editar(EmpleadoDTO empleado);
        Task<bool> Eliminar(int id);
    }
}
