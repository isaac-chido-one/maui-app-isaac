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
    public static string[] BRANDS = new[]
    {
        "Seleccionar ...",
        "Audi",
        "BMW",
        "Daimler",
        "Dodge",
        "Ford",
        "General Motors (Chevrolet)",
        "Honda",
        "Hyundai",
        "Jeep",
        "Kia",
        "Mazda",
        "MG Motor",
        "Nissan",
        "Peugeot",
        "Porsche",
        "Ram",
        "Toyota",
        "Volkswagen",
    };

    public static string[] CATEGORIES = new[]
    {
        "Seleccionar ...",
        "Carrocería",
        "Chasis y suspensión",
        "Motor y transmisión",
        "Partes interiores",
        "Sistema de frenos y escape",
        "Sistema eléctrico y ECU",
    };

    public int Category { get; set; } = 0;

    public int Brand { get; set; } = 0;

    [MaxLength(255)]
    public string Model { get; set; } = string.Empty;

    public int Year { get; set; } = 2025;

    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;

    public string BrandName => Brand > 0 && Brand < BRANDS.Length ? BRANDS[Brand] : string.Empty;

    public string CategoryName => Category > 0 && Category < CATEGORIES.Length ? CATEGORIES[Category] : string.Empty;
}
