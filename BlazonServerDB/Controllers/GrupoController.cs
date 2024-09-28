using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazonServerDB.Models;
using BlazorCrud.Shared;

using Microsoft.EntityFrameworkCore;

namespace BlazonServerDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
         private readonly PruebaTecnicaContext _dbContext;

        public GrupoController(PruebaTecnicaContext dbContext)
        {
                _dbContext = dbContext; 
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rensponseAPI = new ResponseAPI<List<GrupoDTO>>();

            var listaGrupoDTO = new List<GrupoDTO>();

            try
            {
                foreach (var item in await _dbContext.Grupos.ToListAsync())
                {
                    listaGrupoDTO.Add(new GrupoDTO
                    {
                        GrupoId = item.GrupoId,
                        NomOficina = item.NomOficina,
                        CodigoDep = item.CodigoDep,
                    });
                }
                rensponseAPI.EsCorrecto=true;
                rensponseAPI.Valor = listaGrupoDTO;
            }
            catch (Exception ex)
            {

                rensponseAPI.EsCorrecto = false;
                rensponseAPI.Mensaje = ex.Message;
            }

            return  Ok(rensponseAPI);
        }

    }
}
