using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppIsaac.Models;

namespace MauiAppIsaac.Helpers;

public class SQLLiteHelper<T> : SQLLiteBase where T : BaseModel, new()
{
    public List<T> GetAllData()
    {
        return _connection.Table<T>().ToList();
    }

    public int Add(T row)
    {
        _connection.Insert(row);

        return row.Id;
    }

    public int Update(T row)
    {
        return _connection.Update(row);
    }

    public int Delete(T row)
    {
        return _connection.Delete(row);
    }
    public T Get(int id)
    {
        return _connection.Table<T>().Where(w => w.Id == id).FirstOrDefault();
    }
}
