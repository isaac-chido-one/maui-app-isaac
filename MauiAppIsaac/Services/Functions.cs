using MauiAppIsaac.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppIsaac.Services
{
    public class Functions : IFunctions
    {
        public string CambiarTexto(string text, int count)
        {
            return $"{text} : {count}";
        }
    }
}
