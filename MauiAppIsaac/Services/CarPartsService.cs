using MauiAppIsaac.Helpers;
using MauiAppIsaac.Interfaces;
using MauiAppIsaac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Services;

public class CarPartsService : ICarParts
{
    private readonly SQLLiteHelper<CarPartModel> db;

    public CarPartsService()
    {
        db = new();
    }

    Task<int> ICarParts.DeleteCarPart(CarPartModel carPart)
    {
        return Task.FromResult(db.Delete(carPart));
    }

    Task<List<CarPartModel>> ICarParts.GetAll()
    {
        return Task.FromResult(db.GetAllData());
    }

    Task<CarPartModel> ICarParts.GetById(int id)
    {
        return Task.FromResult(db.Get(id));
    }

    Task<int> ICarParts.InsertCarPart(CarPartModel carPart)
    {
        return Task.FromResult(db.Add(carPart));
    }

    Task<int> ICarParts.UpdateCarPart(CarPartModel carPart)
    {
        return Task.FromResult(db.Update(carPart));
    }
}
