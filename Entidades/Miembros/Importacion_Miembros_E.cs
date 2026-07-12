using System.Collections.Generic;
using System.Data;

namespace Entidades.Miembros
{
    /// <summary>
    /// Contiene los seis DataTable enviados a SQL Server y los errores
    /// encontrados al leer el archivo Excel.
    /// </summary>
    public class Importacion_Miembros_E
    {
        public DataTable Miembros { get; set; }
        public DataTable Informacion_Familiar_1 { get; set; }
        public DataTable Informacion_Familiar_2 { get; set; }
        public DataTable Informacion_Laboral { get; set; }
        public DataTable Nivel_Academico { get; set; }
        public DataTable Pasatiempos { get; set; }

        public int Total_Filas_Leidas { get; set; }
        public List<string> Errores { get; set; }

        public bool Es_Valida
        {
            get
            {
                return Total_Filas_Leidas > 0 &&
                       Errores != null &&
                       Errores.Count == 0;
            }
        }

        public Importacion_Miembros_E()
        {
            Errores = new List<string>();
        }
    }
}
