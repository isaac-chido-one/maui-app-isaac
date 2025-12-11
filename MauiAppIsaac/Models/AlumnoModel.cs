using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Models;

[Table("alumnos")]
public class AlumnoModel : BaseModel
{
    [MaxLength(30)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(30)]
    public string Apellido { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Nombre} {Apellido}";
    }
}
