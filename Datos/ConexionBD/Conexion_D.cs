using System;
using System.Configuration;

namespace Datos.ConexionBD
{
    public class Conexion_D
    {
        #region Declaraciones
        public static string CadenaSQL = ConfigurationManager.ConnectionStrings["CadenaConexionSQL"].ToString();
        #endregion
    }
}
