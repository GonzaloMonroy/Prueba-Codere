using System;
using System.Collections.Generic;

namespace BlazonServerDB.Models;

public partial class Grupo
{
    public int GrupoId { get; set; }

    public string NomOficina { get; set; } = null!;

    public int CodigoDep { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
