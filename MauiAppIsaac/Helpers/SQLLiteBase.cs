using MauiAppIsaac.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Helpers;

public class SQLLiteBase
{
    private string _rutaDB;

    public SQLiteConnection? _connection;

    public SQLLiteBase()
    {
        _rutaDB = FileAccessHelper.GetPathFile("alumnos.db3");

        if (_connection != null)
        {
            return;
        }

        _connection = new SQLiteConnection(_rutaDB);
        _connection.CreateTable<AlumnoModel>();
    }
}
