using MauiAppIsaac.Helpers;
using MauiAppIsaac.Interfaces;
using MauiAppIsaac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Services;

public class PatientsService : IPatients
{
    private readonly SQLLiteHelper<PatientModel> db;

    public PatientsService()
    {
        db = new();
    }

    Task<int> IPatients.DeletePatient(PatientModel patient)
    {
        return Task.FromResult(db.Delete(patient));
    }

    Task<List<PatientModel>> IPatients.GetAll()
    {
        return Task.FromResult(db.GetAllData());
    }

    Task<PatientModel> IPatients.GetById(int id)
    {
        return Task.FromResult(db.Get(id));
    }

    Task<int> IPatients.InsertPatient(PatientModel patient)
    {
        return Task.FromResult(db.Add(patient));
    }

    Task<int> IPatients.UpdatePatient(PatientModel patient)
    {
        return Task.FromResult(db.Update(patient));
    }
}
