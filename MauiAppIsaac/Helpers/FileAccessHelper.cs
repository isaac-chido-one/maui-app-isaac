using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Helpers;

public class FileAccessHelper
{
    public static string GetPathFile(string path)
    {
        return System.IO.Path.Combine(FileSystem.AppDataDirectory, path);
    }

    public static string GetAppDirctory(string path)
    {
        return FileSystem.AppDataDirectory;
    }
}
