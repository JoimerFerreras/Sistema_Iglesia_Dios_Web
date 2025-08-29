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

        public DataTable GraficoIngresosVsEgresos()
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


        public DataTable GraficoCuentasCobrarPorMes(int meses, bool incluirMesActual)
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

        public DataTable GraficoAntiguedadCxC()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @";WITH Datos AS (
                              SELECT DATEDIFF(DAY, CAST(Fecha_CC AS date), CAST(GETDATE() AS date)) AS Dias, Valor
                              FROM dbo.Cuentas_Por_Cobrar
                              WHERE Fecha_CC IS NOT NULL
                            ),
                            Buckets AS (
                              SELECT
                                CASE
                                  WHEN Dias <= 30 THEN '0-30 días'
                                  WHEN Dias BETWEEN 31 AND 60 THEN '31-60 días'
                                  WHEN Dias BETWEEN 61 AND 90 THEN '61-90 días'
                                  ELSE '91+ días'
                                END AS Rango, Valor
                              FROM Datos
                            )
                            SELECT Rango, SUM(Valor) AS Total
                            FROM Buckets
                            GROUP BY Rango
                            ORDER BY CASE Rango
                              WHEN '0-30 días' THEN 1 WHEN '31-60 días' THEN 2
                              WHEN '61-90 días' THEN 3 ELSE 4 END;
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

        public DataTable GraficoCuentasPagarPorMes(int meses, bool incluirMesActual)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SET LANGUAGE Spanish;

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
                                  SELECT DATEFROMPARTS(YEAR(Fecha_CP),MONTH(Fecha_CP),1) Mes, SUM(Valor) Total
                                  FROM dbo.Cuentas_Por_Pagar
                                  WHERE Fecha_CP >= @Inicio AND Fecha_CP < DATEADD(MONTH,1,@UltimoMes)
                                  GROUP BY DATEFROMPARTS(YEAR(Fecha_CP),MONTH(Fecha_CP),1)
                                )
                                SELECT
                                  CONVERT(char(7), M.Mes, 120) AS Periodo,
                                  UPPER(LEFT(DATENAME(MONTH,M.Mes),1)) + SUBSTRING(DATENAME(MONTH,M.Mes),2,50) AS MesNombre,
                                  ISNULL(A.Total,0) AS TotalPagar
                                FROM Meses M
                                LEFT JOIN Agg A ON A.Mes = M.Mes
                                ORDER BY M.Mes
                                OPTION (MAXRECURSION 0);";

                cmd.Parameters.AddWithValue("@Meses", meses);
                cmd.Parameters.AddWithValue("@IncluirMesActual", incluirMesActual);
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

        public DataTable GraficoAntiguedadCxP()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @";WITH Datos AS (
                              SELECT DATEDIFF(DAY, CAST(Fecha_CP AS date), CAST(GETDATE() AS date)) AS Dias, Valor
                              FROM dbo.Cuentas_Por_Pagar
                              WHERE Fecha_CP IS NOT NULL
                            ),
                            Buckets AS (
                              SELECT
                                CASE
                                  WHEN Dias <= 30 THEN '0-30 días'
                                  WHEN Dias BETWEEN 31 AND 60 THEN '31-60 días'
                                  WHEN Dias BETWEEN 61 AND 90 THEN '61-90 días'
                                  ELSE '91+ días'
                                END AS Rango, Valor
                              FROM Datos
                            )
                            SELECT Rango, SUM(Valor) AS Total
                            FROM Buckets
                            GROUP BY Rango
                            ORDER BY CASE Rango
                              WHEN '0-30 días' THEN 1 WHEN '31-60 días' THEN 2
                              WHEN '61-90 días' THEN 3 ELSE 4 END;";
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

        public DataTable TotalesMesActual()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"DECLARE @iniMes date = DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1);
                                DECLARE @finMes date = DATEADD(MONTH,1,@iniMes);

                                -- Rango mes anterior
                                DECLARE @iniAnt date = DATEADD(MONTH,-1,@iniMes);
                                DECLARE @finAnt date = @iniMes;

                                WITH CurI AS (
                                    SELECT SUM(Monto) AS Ingresos
                                    FROM Ingresos
                                    WHERE Fecha_Ingreso >= @iniMes AND Fecha_Ingreso < @finMes
                                ),
                                CurE AS (
                                    SELECT SUM(Monto) AS Egresos
                                    FROM Egresos
                                    WHERE Fecha_Egreso >= @iniMes AND Fecha_Egreso < @finMes
                                ),
                                PrevI AS (
                                    SELECT SUM(Monto) AS IngresosPrev
                                    FROM Ingresos
                                    WHERE Fecha_Ingreso >= @iniAnt AND Fecha_Ingreso < @finAnt
                                ),
                                PrevE AS (
                                    SELECT SUM(Monto) AS EgresosPrev
                                    FROM Egresos
                                    WHERE Fecha_Egreso >= @iniAnt AND Fecha_Egreso < @finAnt
                                )
                                SELECT
                                    ISNULL(CurI.Ingresos,0)                                   AS IngresosMes,
                                    ISNULL(CurE.Egresos,0)                                    AS EgresosMes,
                                    ISNULL(CurI.Ingresos,0) - ISNULL(CurE.Egresos,0)          AS NetoMes,

                                    -- % variación Ingresos vs mes anterior
                                    CAST( CASE WHEN ISNULL(PrevI.IngresosPrev,0)=0 THEN NULL
                                               ELSE 100.0 * (ISNULL(CurI.Ingresos,0) - PrevI.IngresosPrev)
                                                          / NULLIF(PrevI.IngresosPrev,0) END AS decimal(10,2) )
                                    AS VarPctIngresos,

                                    -- % variación Egresos vs mes anterior
                                    CAST( CASE WHEN ISNULL(PrevE.EgresosPrev,0)=0 THEN NULL
                                               ELSE 100.0 * (ISNULL(CurE.Egresos,0) - PrevE.EgresosPrev)
                                                          / NULLIF(PrevE.EgresosPrev,0) END AS decimal(10,2) )
                                    AS VarPctEgresos,

                                    -- % variación Neto vs mes anterior
                                    CAST( CASE WHEN (ISNULL(PrevI.IngresosPrev,0) - ISNULL(PrevE.EgresosPrev,0)) = 0 THEN NULL
                                               ELSE 100.0 * ( (ISNULL(CurI.Ingresos,0) - ISNULL(CurE.Egresos,0))
                                                             - (ISNULL(PrevI.IngresosPrev,0) - ISNULL(PrevE.EgresosPrev,0)) )
                                                          / NULLIF( (ISNULL(PrevI.IngresosPrev,0) - ISNULL(PrevE.EgresosPrev,0)), 0) END
                                          AS decimal(10,2) )
                                    AS VarPctNeto
                                FROM CurI CROSS JOIN CurE CROSS JOIN PrevI CROSS JOIN PrevE;";

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

        public DataTable TotalMiscelaneos()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SELECT COUNT(*) AS CantidadRegistros FROM Miscelaneos";

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

        public DataTable TotalDescripciones()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SELECT COUNT(*) AS CantidadRegistros FROM Descripciones";

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

        public DataTable TotalFormas_Pago()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SELECT COUNT(*) AS CantidadRegistros FROM Formas_Pago";

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

        public DataTable TotalMiembros()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SELECT COUNT(*) AS CantidadRegistros FROM Miembros";

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


        public DataTable IngresosPorDia_MesActual()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SET LANGUAGE Spanish;  -- para nombre de día en español (opcional)

                                DECLARE @iniMes date = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
                                DECLARE @finMes date = EOMONTH(@iniMes);  -- último día del mes actual

                                ;WITH Dias AS (
                                    SELECT @iniMes AS Dia
                                    UNION ALL
                                    SELECT DATEADD(DAY, 1, Dia)
                                    FROM Dias
                                    WHERE DATEADD(DAY, 1, Dia) <= @finMes
                                ),
                                Agg AS (
                                    SELECT
                                        CAST(Fecha_Ingreso AS date) AS Dia,
                                        SUM(Monto) AS TotalIngresos
                                    FROM dbo.Ingresos
                                    WHERE Fecha_Ingreso >= @iniMes
                                      AND Fecha_Ingreso < DATEADD(DAY, 1, @finMes)  -- hasta 23:59:59 del último día
                                    GROUP BY CAST(Fecha_Ingreso AS date)
                                )
                                SELECT
                                    D.Dia,                                          -- fecha (date)
                                    CONVERT(char(10), D.Dia, 23)   AS DiaISO,       -- 'YYYY-MM-DD'
                                    DATENAME(weekday, D.Dia)        AS DiaNombre,   -- Lunes, Martes, ...
                                    ISNULL(A.TotalIngresos, 0)      AS IngresosDia
                                FROM Dias D
                                LEFT JOIN Agg A ON A.Dia = D.Dia
                                ORDER BY D.Dia
                                OPTION (MAXRECURSION 100);
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
    }
}
#endregion