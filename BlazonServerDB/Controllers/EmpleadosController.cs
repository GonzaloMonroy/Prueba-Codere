using BlazonServerDB.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazonServerDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly PruebaTecnicaContext _dbContext;

        public EmpleadosController(PruebaTecnicaContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rensponseAPI = new ResponseAPI<List<EmpleadoDTO>>();

            var listaEmpleadoDTO = new List<EmpleadoDTO>();

            try
            {
                // Consulta para obtener todos los empleados e incluir el grupo relacionado
                var empleados = await _dbContext.Empleados
                                                .Include(e => e.Grupo)
                                                .ToListAsync();

                // Mapeo de Empleado a EmpleadoDTO
                foreach (var empleado in empleados)
                {
                    listaEmpleadoDTO.Add(new EmpleadoDTO
                    {
                        EmpleadoId = empleado.EmpleadoId,
                        Nombre = empleado.Nombre,
                        PuestoTrabajo = empleado.PuestoTrabajo,
                        SalarioBase = empleado.SalarioBase ?? 0,
                        SupervisorId = empleado.SupervisorId,
                        CodigoEmpleado = empleado.CodigoEmpleado,
                        GrupoId = empleado.GrupoId,
                        Grupo = new GrupoDTO
                        {
                            GrupoId = empleado.Grupo.GrupoId,
                            NomOficina = empleado.Grupo.NomOficina,
                            CodigoDep = empleado.Grupo.CodigoDep
                        }
                    });
                }

                rensponseAPI.EsCorrecto = true;
                rensponseAPI.Valor = listaEmpleadoDTO;
            }
            catch (Exception ex)
            {
                // Captura de excepción y retorno de mensaje de error
                rensponseAPI.EsCorrecto = false;
                rensponseAPI.Mensaje = ex.Message;
            }

            // Devolver la respuesta
            return Ok(rensponseAPI);
        }

        // Método de buscar empleado
        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseAPI = new ResponseAPI<EmpleadoDTO>();

            try
            {
                var dbEmpleado = await _dbContext.Empleados
                                                 .Include(e => e.Grupo) // Incluye el grupo si es necesario
                                                 .FirstOrDefaultAsync(x => x.EmpleadoId == id);

                if (dbEmpleado == null)
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Empleado no encontrado";
                    return NotFound(responseAPI);
                }

                var empleadoDTO = new EmpleadoDTO
                {
                    EmpleadoId = dbEmpleado.EmpleadoId,
                    Nombre = dbEmpleado.Nombre,
                    PuestoTrabajo = dbEmpleado.PuestoTrabajo,
                    SalarioBase = dbEmpleado.SalarioBase ?? 0,
                    SupervisorId= dbEmpleado.SupervisorId,
                    CodigoEmpleado = dbEmpleado.CodigoEmpleado,
                    GrupoId = dbEmpleado.GrupoId
                };

                // Si incluyes el grupo
                if (dbEmpleado.Grupo != null)
                {
                    empleadoDTO.Grupo = new GrupoDTO
                    {
                        GrupoId = dbEmpleado.Grupo.GrupoId,
                        NomOficina = dbEmpleado.Grupo.NomOficina,
                        CodigoDep = dbEmpleado.Grupo.CodigoDep
                    };
                }

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = empleadoDTO;
            }
            catch (Exception ex)
            {
                // Captura de excepción y retorno de mensaje de error
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            // Devolver la respuesta
            return Ok(responseAPI);
        }

        // POST: api/Empleados/Crear
        [HttpPost]
        [Route("Crear")]
        public async Task<IActionResult> Crear([FromBody] EmpleadoDTO empleadoDTO)
        {
            var responseAPI = new ResponseAPI<EmpleadoDTO>();

            try
            {
                // Asegúrate de que el DTO contenga todos los campos necesarios
                if (!ModelState.IsValid)
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Datos de entrada no válidos.";
                    return BadRequest(responseAPI);
                }

                var nuevoEmpleado = new Empleado
                {
                    Nombre = empleadoDTO.Nombre,
                    PuestoTrabajo = empleadoDTO.PuestoTrabajo,
                    SalarioBase = empleadoDTO.SalarioBase,
                    // Asegúrate de manejar SupervisorId correctamente
                    SupervisorId = empleadoDTO.SupervisorId, // Puedes establecerlo a null si no se proporciona
                    CodigoEmpleado = empleadoDTO.CodigoEmpleado,
                    GrupoId = empleadoDTO.GrupoId
                };

                // Aquí puedes manejar la lógica de crear un grupo si no existe
                if (empleadoDTO.Grupo != null)
                {
                    // Considera validar y agregar el grupo aquí
                    var nuevoGrupo = new Grupo
                    {
                        GrupoId = empleadoDTO.Grupo.GrupoId,
                        NomOficina = empleadoDTO.Grupo.NomOficina,
                        CodigoDep = empleadoDTO.Grupo.CodigoDep
                    };

                    // Agrega lógica para manejar el grupo, si es necesario
                    _dbContext.Grupos.Add(nuevoGrupo);
                    await _dbContext.SaveChangesAsync(); // Guardar el grupo primero, si es nuevo
                }

                // Agregar el nuevo empleado a la base de datos
                _dbContext.Empleados.Add(nuevoEmpleado);
                await _dbContext.SaveChangesAsync();

                // Devolver el empleado creado con su ID generado
                empleadoDTO.EmpleadoId = nuevoEmpleado.EmpleadoId;
                responseAPI.EsCorrecto = true;
                responseAPI.Valor = empleadoDTO;
            }
            catch (JsonException jsonEx)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = "Error en la deserialización del JSON: " + jsonEx.Message;
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        // PUT: api/Empleados/Actualizar
        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] EmpleadoDTO empleadoDTO)
        {
            var responseAPI = new ResponseAPI<EmpleadoDTO>();

            try
            {
                var empleadoExistente = await _dbContext.Empleados.FindAsync(id);

                if (empleadoExistente == null)
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Empleado no encontrado";
                    return NotFound(responseAPI);
                }

                // Actualizar los campos del empleado
                empleadoExistente.Nombre = empleadoDTO.Nombre;
                empleadoExistente.PuestoTrabajo = empleadoDTO.PuestoTrabajo;
                empleadoExistente.SalarioBase = empleadoDTO.SalarioBase;
                empleadoExistente.CodigoEmpleado = empleadoDTO.CodigoEmpleado;
                empleadoExistente.GrupoId = empleadoDTO.GrupoId;

                await _dbContext.SaveChangesAsync();

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = empleadoDTO;
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }

        // DELETE: api/Empleados/Eliminar
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseAPI = new ResponseAPI<bool>();

            try
            {
                var empleado = await _dbContext.Empleados.FindAsync(id);

                if (empleado == null)
                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.Mensaje = "Empleado no encontrado";
                    return NotFound(responseAPI);
                }

                _dbContext.Empleados.Remove(empleado);
                await _dbContext.SaveChangesAsync();

                responseAPI.EsCorrecto = true;
                responseAPI.Valor = true;
            }
            catch (Exception ex)
            {
                responseAPI.EsCorrecto = false;
                responseAPI.Mensaje = ex.Message;
            }

            return Ok(responseAPI);
        }
    }
}
    