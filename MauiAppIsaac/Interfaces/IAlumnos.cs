using MauiAppIsaac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Interfaces;

public interface IAlumnos
{
    public Task<List<AlumnoModel>> GetAll();

    public Task<AlumnoModel> GetById(int id);

    public Task<int> InsertarAlumno(AlumnoModel alumno);

    public Task<int> DeleteAlumno(AlumnoModel alumno);

    public Task<int> UpdateAlumno(AlumnoModel alumno);
}
