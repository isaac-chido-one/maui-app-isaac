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
        _rutaDB = FileAccessHelper.GetPathFile("yonque.db3");

        if (_connection != null)
        {
            return;
        }

        _connection = new SQLiteConnection(_rutaDB);

        // Crear las tablas necesarias aquí
        _connection.CreateTable<CarPartModel>();
    }

    /// <summary>
    /// Cierra la conexión actual (si existe) y elimina el fichero de base de datos asociado a esta instancia.
    /// </summary>
    public void CloseAndDeleteDatabase()
    {
        try
        {
            _connection?.Close();
            _connection?.Dispose();
            _connection = null;

            if (!string.IsNullOrEmpty(_rutaDB) && System.IO.File.Exists(_rutaDB))
            {
                System.IO.File.Delete(_rutaDB);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al eliminar la BD desde CloseAndDeleteDatabase: {ex}");
            throw;
        }
    }

    public static void DeleteDatabaseFile(string filename = "alumnos.db3")
    {
        var path = FileAccessHelper.GetPathFile(filename);
        try
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"No se pudo eliminar la BD {path}: {ex}");
            throw;
        }
    }
}
