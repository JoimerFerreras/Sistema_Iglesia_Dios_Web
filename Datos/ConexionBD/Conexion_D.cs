using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
