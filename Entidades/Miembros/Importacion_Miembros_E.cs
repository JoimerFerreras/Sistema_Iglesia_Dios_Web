using System;
using System.Collections.Generic;
using System.Data;

namespace Entidades.Miembros
{
    [Serializable]
    public class Error_Importacion_Excel_E
    {
        public int Fila_Excel { get; set; }
        public int Columna_Excel { get; set; }
        public string Nombre_Columna { get; set; }
        public string Nombre_Columna_Grid { get; set; }
        public string Valor { get; set; }
        public string Mensaje { get; set; }
        public bool Es_Encabezado { get; set; }

        public string Descripcion
        {
            get
            {
                if (Fila_Excel > 0 &&
                    !string.IsNullOrWhiteSpace(Nombre_Columna))
                {
                    return "Fila " + Fila_Excel +
                           ", columna '" + Nombre_Columna +
                           "': " + Mensaje;
                }

                if (Fila_Excel > 0)
                {
                    return "Fila " + Fila_Excel +
                           ": " + Mensaje;
                }

                return Mensaje;
            }
        }
    }

    /// <summary>
    /// Contiene los seis DataTable enviados a SQL Server, la vista
    /// original del Excel y los errores encontrados durante la lectura.
    /// </summary>
    [Serializable]
    public class Importacion_Miembros_E
    {
        public DataTable Miembros { get; set; }
        public DataTable Informacion_Familiar_1 { get; set; }
        public DataTable Informacion_Familiar_2 { get; set; }
        public DataTable Informacion_Laboral { get; set; }
        public DataTable Nivel_Academico { get; set; }
        public DataTable Pasatiempos { get; set; }

        /*
            Conserva los datos tal como se muestran en el Excel. Esta tabla
            se usa únicamente para presentar las filas con errores en la UI.
        */
        public DataTable Datos_Excel { get; set; }

        public int Total_Filas_Leidas { get; set; }
        public int Total_Filas_Validas { get; set; }

        /*
            Errores se conserva para no romper el código que ya consulta
            mensajes como texto. Errores_Detallados permite localizar la
            fila y la columna exactas.
        */
        public List<string> Errores { get; set; }
        public List<Error_Importacion_Excel_E> Errores_Detallados
        {
            get;
            set;
        }

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
            Errores_Detallados =
                new List<Error_Importacion_Excel_E>();
            Datos_Excel = new DataTable("Datos_Excel");
        }

        public void AgregarError(
            Error_Importacion_Excel_E error)
        {
            if (error == null)
                return;

            if (string.IsNullOrWhiteSpace(error.Mensaje))
                error.Mensaje = "Error de validación.";

            Errores_Detallados.Add(error);
            Errores.Add(error.Descripcion);
        }
    }
}
