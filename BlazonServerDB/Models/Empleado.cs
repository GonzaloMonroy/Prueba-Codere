using System;
using System.Collections.Generic;

namespace BlazonServerDB.Models;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string PuestoTrabajo { get; set; } = null!;

    public int? SalarioBase { get; set; }

    public int? SupervisorId { get; set; }

    public int CodigoEmpleado { get; set; }

    public int GrupoId { get; set; }

    public virtual Grupo Grupo { get; set; } = null!;

    public virtual ICollection<Empleado> InverseSupervisor { get; set; } = new List<Empleado>();

    public virtual Empleado? Supervisor { get; set; }
}
