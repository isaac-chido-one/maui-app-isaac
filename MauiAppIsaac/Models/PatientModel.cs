using CommunityToolkit.Mvvm.DependencyInjection;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Models;

[Table("patients")]
public class PatientModel : BaseModel
{
    public static string[] LEVELS = new[]
    {
        "Seleccionar ...",
        "Rara vez",
        "1 a 3 días por semana",
        "3 a 5 días por semana",
        "6 a 7 días por semana",
        "Diariamente",
    };

    public static string[] GENDERS = new[]
    {
        "Seleccionar ...",
        "Femenino",
        "Masculino",
    };

    const int FEMALE = 1;

    const int MALE = 2;


    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string LastName { get; set; } = string.Empty;

    public int Age { get; set; } = 0;

    public double Weight { get; set; } = 0.0;

    public double Height { get; set; } = 0.0;

    public int Gender { get; set; } = 0;

    public int Level { get; set; } = 0;


    public string LevelName => Level > 0 && Level < LEVELS.Length ? LEVELS[Level] : string.Empty;

    public string GenderName => Gender > 0 && Gender < GENDERS.Length ? GENDERS[Gender] : string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public string Imc
    {
        get
        {
            double imc = Height == 0 ? 0 : Weight / (Height * Height);
            string status = "Sin información";

            if (imc >= 0 && imc < 18.5)
            {
                status = "Bajo peso";
            }
            else if (imc >= 18.5 && imc < 25.0)
            {
                status = "Bajo normal";
            }
            else if (imc >= 25.0 && imc < 30.0)
            {
                status = "Pre-obesidad o Sobrepeso";
            }
            else if (imc >= 30.0 && imc < 35.0)
            {
                status = " Obesidad clase I";
            }
            else if (imc >= 35.0 && imc < 40.0)
            {
                status = " Obesidad clase II";
            }
            else if (imc >= 40)
            {
                status = " Obesidad clase III";
            }

            return $"IMC: {imc:F2} ({status})";
        }
    }

    public string Gc
    {
        get
        {
            double imc = Height == 0 ? 0 : Weight / (Height * Height);
            double gc = 1.2 * imc + 0.23 * Age - (Gender == MALE ? 10.8 : 0) - 5.4;
            string status = "Sin información";

            if (Gender == FEMALE)
            {
                if (gc >= 10 && gc < 14)
                {
                    status = "Grasa esencial";
                }
                else if (gc >= 14 && gc < 21)
                {
                    status = "Atletas";
                }
                else if (gc >= 21 && gc < 25)
                {
                    status = "Fitness";
                }
                else if (gc >= 25 && gc < 32)
                {
                    status = "Aceptable";
                }
                else if (gc >= 32)
                {
                    status = "Obesidad";
                }
            }
            else if (Gender == MALE)
            {
                if (gc >= 2 && gc < 6)
                {
                    status = "Grasa esencial";
                }
                else if (gc >= 6 && gc < 14)
                {
                    status = "Atletas";
                }
                else if (gc >= 14 && gc < 18)
                {
                    status = "Fitness";
                }
                else if (gc >= 18 && gc < 25)
                {
                    status = "Aceptable";
                }
                else if (gc >= 25)
                {
                    status = "Obesidad";
                }
            }

            return $"%GC: {gc:F2} ({status})";
        }
    }

    public string IdealWeight
    {
        get
        {
            double cm = Height * 100;
            double idealWeight = cm - 100 - (cm - 150) / (Gender == MALE ? 4 : 2.5);

            return $"Peso ideal: {idealWeight:F2}";
        }
    }

    public string Tdee
    {
        get
        {
            double tdee = 0, bmr = 0, cm = Height * 100;

            if (Gender == MALE)
            {
                bmr = (cm * 6.25) + (Weight * 9.99) - (Age * 4.92) + 5;
            }
            else if (Gender == FEMALE)
            {
                bmr = (cm * 6.25) + (Weight * 9.99) - (Age * 4.92) - 161;
            }

            switch (Level)
            {
                case 1:
                    tdee = bmr * 12;
                    break;
                case 2:
                    tdee = bmr * 1.375;
                    break;
                case 3:
                    tdee = bmr * 1.55;
                    break;
                case 4:
                    tdee = bmr * 1.725;
                    break;
                case 5:
                    tdee = bmr * 1.9;
                    break;
            }

            return $"TDEE: {tdee:F2}";
        }
    }
}
