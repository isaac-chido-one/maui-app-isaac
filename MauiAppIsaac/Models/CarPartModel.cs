using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Models;

[Table("car_parts")]
public class CarPartModel : BaseModel
{
    public int Category { get; set; } = 0;

    public int Brand { get; set; } = 0;

    [MaxLength(255)]
    public string Model { get; set; } = string.Empty;

    public int Year { get; set; } = 0;

    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;
}
