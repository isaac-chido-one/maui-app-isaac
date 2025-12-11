using MauiAppIsaac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Interfaces;

public interface ICarParts
{
    public Task<List<CarPartModel>> GetAll();

    public Task<CarPartModel> GetById(int id);

    public Task<int> InsertCarPart(CarPartModel carPart);

    public Task<int> DeleteCarPart(CarPartModel carPart);

    public Task<int> UpdateCarPart(CarPartModel carPart);
}
