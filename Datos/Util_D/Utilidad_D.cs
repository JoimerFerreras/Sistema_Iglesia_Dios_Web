using System;
using System.Data.SqlClient;
using System.Data;
using Datos.ConexionBD;

namespace Datos.Util_D
{
    public class Utilidad_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion


        #region Consultas
        public DataTable ObtenerCantidadRegistros(string Id_Valor, string NombreCampo, string NombreTablaExcluir)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"DECLARE @ExcludedTable NVARCHAR(255) = 'dbo.{NombreTablaExcluir}' -- Reemplaza 'NombreDeLaTablaAExcluir' con el nombre de la tabla que deseas excluir
                                        DECLARE @TableName NVARCHAR(255)
                                        DECLARE @SQL NVARCHAR(MAX)

                                        -- Crear una tabla temporal para almacenar los resultados
                                        CREATE TABLE #TableCounts (
                                            NombreTabla NVARCHAR(255),
                                            CantidadRegistros INT
                                        )

                                        -- Declarar un cursor para iterar a través de las tablas que contienen el campo ""Id_Descripcion_Ingreso""
                                        DECLARE TableCursor CURSOR FOR
                                        SELECT TABLE_SCHEMA + '.' + TABLE_NAME
                                        FROM INFORMATION_SCHEMA.COLUMNS
                                        WHERE COLUMN_NAME = '{NombreCampo}'
                                          AND TABLE_SCHEMA + '.' + TABLE_NAME != @ExcludedTable

                                        OPEN TableCursor

                                        FETCH NEXT FROM TableCursor INTO @TableName

                                        WHILE @@FETCH_STATUS = 0
                                        BEGIN
                                            -- Crear el SQL dinámico para contar los registros donde Id_Descripcion_Ingreso = 1
                                            SET @SQL = 'INSERT INTO #TableCounts (NombreTabla, CantidadRegistros) 
                                                        SELECT ''' + @TableName + ''', COUNT(*) 
                                                        FROM ' + @TableName + ' 
                                                        WHERE {NombreCampo} = {Id_Valor}'

                                            -- Ejecutar el SQL dinámico
                                            EXEC sp_executesql @SQL

                                            FETCH NEXT FROM TableCursor INTO @TableName
                                        END

                                        CLOSE TableCursor
                                        DEALLOCATE TableCursor

                                        -- Seleccionar los resultados
                                        SELECT * FROM #TableCounts

                                        -- Limpiar la tabla temporal
                                        DROP TABLE #TableCounts";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
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
        #endregion



        #region Mantenimientos


        #endregion
    }
}
