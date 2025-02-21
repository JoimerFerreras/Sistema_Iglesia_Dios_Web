using Datos.ConexionBD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Resumen
{
    public class Resumen_D
    {

        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable GraficoIngresosMes(DateTime FechaBase)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"WITH Totales AS (
                                SELECT 
                                    DI.Descripcion_Ingreso,
                                    SUM(CASE 
                                            WHEN I.Id_Moneda = 1 THEN I.Monto 
                                            ELSE I.Monto * I.Valor_Moneda 
                                        END) AS Monto_Total
                                FROM Ingresos I
                                LEFT JOIN Descripciones_Ingreso DI ON I.Id_Descripcion_Ingreso = DI.Id_Descripcion_Ingreso
                                WHERE YEAR(I.Fecha_Ingreso) = YEAR(@FechaBase) 
                                  AND MONTH(I.Fecha_Ingreso) = MONTH(@FechaBase) 
                                GROUP BY DI.Descripcion_Ingreso
                            ),
                            TotalGeneral AS (
                                SELECT SUM(Monto_Total) AS Suma_Total FROM Totales
                            )
                            SELECT 
                                T.Descripcion_Ingreso,
                                FORMAT(T.Monto_Total, 'N2') + ' DOP' AS Monto_Total,
                                FORMAT(T.Monto_Total * 100.0 / TG.Suma_Total, 'N2') AS Porcentaje
                            FROM Totales T
                            CROSS JOIN TotalGeneral TG
                            ORDER BY T.Monto_Total * 100.0 / TG.Suma_Total DESC;";

                cmd.Parameters.AddWithValue("@FechaBase", FechaBase);

                cmd.CommandText = sentencia;
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    conexion.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
#endregion