using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Iglesia_Dios_Web.Utilidad_Cliente
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CodigoFuncionalidadAttribute : Attribute
    {
        // Esta clase se encarga de recuperar el codigo de la funcionalidad
        public string Codigo { get; }
        public CodigoFuncionalidadAttribute(string codigo)
        {
            Codigo = codigo;
        }
    }
}