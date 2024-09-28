using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
   public class GrupoDTO
    {
        public int GrupoId { get; set; }

        public string NomOficina { get; set; } = null!;

        public int CodigoDep { get; set; }
    }
}
