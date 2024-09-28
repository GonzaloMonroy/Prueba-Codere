using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace BlazorCrud.Shared
{



    public class EmpleadoDTO
    {
        public int EmpleadoId { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El campo Puesto de Trabajo es requerido.")]
        public string PuestoTrabajo { get; set; } = null!;
        [Required(ErrorMessage = "El campo Salario Base es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int SalarioBase { get; set; } // Asegúrate de que este sea un int
        public int? SupervisorId { get; set; } // Puede ser nulo
        [Required(ErrorMessage = "El campo Código de Empleado es requerido.")]
        public int CodigoEmpleado { get; set; }
        [Required(ErrorMessage = "El campo Grupo es requerido.")]
        public int GrupoId { get; set; }
        public GrupoDTO? Grupo { get; set; } // Asegúrate de que GrupoDTO esté bien definido
    }


}

