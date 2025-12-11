using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppIsaac.Helpers;
using MauiAppIsaac.Interfaces;
using MauiAppIsaac.Models;

namespace MauiAppIsaac.Services;

public class AlumnosService : IAlumnos
{
    private readonly SQLLiteHelper<AlumnoModel> db;

    public AlumnosService()
    {
        db = new();
    }

    public Task<List<AlumnoModel>> GetAll()
    {
        return Task.FromResult(db.GetAllData());
    }

    public Task<AlumnoModel> GetById(int id)
    {
        return Task.FromResult(db.Get(id));
    }

    public Task<int> InsertarAlumno(AlumnoModel alumno)
    {
        return Task.FromResult(db.Add(alumno));
    }

    public Task<int> DeleteAlumno(AlumnoModel alumno)
    {
        return Task.FromResult(db.Delete(alumno));
    }

    public Task<int> UpdateAlumno(AlumnoModel alumno)
    {
        return Task.FromResult(db.Update(alumno));
    }
}
