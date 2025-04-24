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
                string sentencia = $@"
                                        DECLARE @TableName NVARCHAR(255);
                                        DECLARE @SQL NVARCHAR(MAX);

                                        CREATE TABLE #TableCounts (
                                            NombreTabla NVARCHAR(255),
                                            CantidadRegistros INT
                                        );

                                        DECLARE TableCursor CURSOR FOR
                                        SELECT TABLE_SCHEMA + '.' + TABLE_NAME
                                        FROM INFORMATION_SCHEMA.COLUMNS
                                        WHERE COLUMN_NAME = @NombreCampo
                                          AND TABLE_SCHEMA + '.' + TABLE_NAME NOT IN (
                                              SELECT TRIM(value) FROM STRING_SPLIT(@TablasExcluir, ',')
                                          );

                                        OPEN TableCursor;

                                        FETCH NEXT FROM TableCursor INTO @TableName;

                                        WHILE @@FETCH_STATUS = 0
                                        BEGIN
                                            SET @SQL = 
                                                'INSERT INTO #TableCounts (NombreTabla, CantidadRegistros)
                                                 SELECT ''' + @TableName + ''', COUNT(*) 
                                                 FROM ' + @TableName + ' 
                                                 WHERE ' + QUOTENAME(@NombreCampo) + ' = @IdValor';

                                            EXEC sp_executesql @SQL, N'@IdValor NVARCHAR(255)', @IdValor;

                                            FETCH NEXT FROM TableCursor INTO @TableName;
                                        END

                                        CLOSE TableCursor;
                                        DEALLOCATE TableCursor;

                                        SELECT * FROM #TableCounts;
                                        DROP TABLE #TableCounts;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@IdValor", Id_Valor);
                cmd.Parameters.AddWithValue("@NombreCampo", NombreCampo);
                cmd.Parameters.AddWithValue("@TablasExcluir", NombreTablaExcluir);
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
