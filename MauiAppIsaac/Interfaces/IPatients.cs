using MauiAppIsaac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Interfaces;

public interface IPatients
{
    public Task<List<PatientModel>> GetAll();

    public Task<PatientModel> GetById(int id);

    public Task<int> InsertPatient(PatientModel patient);

    public Task<int> DeletePatient(PatientModel patient);

    public Task<int> UpdatePatient(PatientModel patient);
}
