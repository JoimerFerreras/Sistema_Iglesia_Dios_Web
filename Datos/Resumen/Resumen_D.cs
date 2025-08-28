using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;

namespace Datos.Resumen
{
    public class Resumen_D
    {

        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable GraficoIngresosVsEgresos(int IncluirMesActual)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SET LANGUAGE Spanish;  -- fuerza español para DATENAME

                                DECLARE @IncluirMesActual BIT = 1;  -- 1 = incluye el mes en curso, 0 = hasta el mes pasado
                                DECLARE @UltimoMes date = IIF(@IncluirMesActual=1,
                                    DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1),
                                    DATEADD(MONTH,-1,DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)));

                                ;WITH Meses AS (  -- genera exactamente 3 meses: -2, -1, 0
                                    SELECT 0 AS n, @UltimoMes AS Mes
                                    UNION ALL SELECT n+1, DATEADD(MONTH,-1,Mes) FROM Meses WHERE n+1 < 3
                                ),
                                Ing AS (
                                    SELECT DATEFROMPARTS(YEAR(Fecha_Ingreso),MONTH(Fecha_Ingreso),1) Mes, SUM(Monto) Ingresos
                                    FROM dbo.Ingresos
                                    WHERE Fecha_Ingreso >= DATEADD(MONTH,-2,@UltimoMes)
                                      AND Fecha_Ingreso <  DATEADD(MONTH, 1,@UltimoMes)
                                    GROUP BY DATEFROMPARTS(YEAR(Fecha_Ingreso),MONTH(Fecha_Ingreso),1)
                                ),
                                Egr AS (
                                    SELECT DATEFROMPARTS(YEAR(Fecha_Egreso),MONTH(Fecha_Egreso),1) Mes, SUM(Monto) Egresos
                                    FROM dbo.Egresos
                                    WHERE Fecha_Egreso >= DATEADD(MONTH,-2,@UltimoMes)
                                      AND Fecha_Egreso <  DATEADD(MONTH, 1,@UltimoMes)
                                    GROUP BY DATEFROMPARTS(YEAR(Fecha_Egreso),MONTH(Fecha_Egreso),1)
                                )
                                SELECT
                                  CONVERT(char(7), M.Mes, 120) AS Periodo,              -- 'YYYY-MM' (útil para ordenar)
                                  -- Nombre del mes en español con la primera letra en mayúscula
                                  UPPER(LEFT(DATENAME(MONTH, M.Mes),1)) + SUBSTRING(DATENAME(MONTH, M.Mes),2,50) AS MesNombre,
                                  ISNULL(I.Ingresos,0)          AS Ingresos,
                                  ISNULL(E.Egresos,0)           AS Egresos,
                                  ISNULL(I.Ingresos,0)-ISNULL(E.Egresos,0) AS Neto
                                FROM Meses M
                                LEFT JOIN Ing I ON I.Mes = M.Mes
                                LEFT JOIN Egr E ON E.Mes = M.Mes
                                ORDER BY M.Mes
                                OPTION (MAXRECURSION 3);
                                ";

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


        public DataTable GraficoCobrarPorMes(int meses, bool incluirMesActual)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SET LANGUAGE Spanish;
                            DECLARE @Meses INT = @pMeses;
                            DECLARE @IncluirMesActual BIT = @pIncluye;
                            DECLARE @UltimoMes date = IIF(@IncluirMesActual=1,
                                DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1),
                                DATEADD(MONTH,-1,DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)));
                            DECLARE @Inicio date = DATEADD(MONTH, -(@Meses-1), @UltimoMes);

                            ;WITH Meses AS (
                                SELECT @Inicio AS Mes
                                UNION ALL SELECT DATEADD(MONTH,1,Mes) FROM Meses
                                WHERE DATEADD(MONTH,1,Mes) <= @UltimoMes
                            ),
                            Agg AS (
                                SELECT DATEFROMPARTS(YEAR(Fecha_CC),MONTH(Fecha_CC),1) Mes, SUM(Valor) Total
                                FROM dbo.Cuentas_Por_Cobrar
                                WHERE Fecha_CC >= @Inicio AND Fecha_CC < DATEADD(MONTH,1,@UltimoMes)
                                GROUP BY DATEFROMPARTS(YEAR(Fecha_CC),MONTH(Fecha_CC),1)
                            )
                            SELECT
                                CONVERT(char(7), M.Mes, 120) AS Periodo,
                                UPPER(LEFT(DATENAME(MONTH,M.Mes),1)) + SUBSTRING(DATENAME(MONTH,M.Mes),2,50) AS MesNombre,
                                ISNULL(A.Total,0) AS TotalCobrar
                            FROM Meses M
                            LEFT JOIN Agg A ON A.Mes = M.Mes
                            ORDER BY M.Mes
                            OPTION (MAXRECURSION 0);";
                cmd.Parameters.AddWithValue("@pMeses", meses);
                cmd.Parameters.AddWithValue("@pIncluye", incluirMesActual);
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