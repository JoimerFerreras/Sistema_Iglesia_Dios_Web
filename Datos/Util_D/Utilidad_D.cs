using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Datos.ConexionBD;
using Entidades.Ingresos;
using Entidades.Otros_Parametros;
using Entidades.Util_E;

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

        #region Archivos
        public DataTable ObtenerArchivo(int Id_Archivo)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT Id_Ingreso,
                                          M.Nombres + ' ' + M.Apellidos AS Miembro,
                                          DI.Descripcion_Ingreso,
                                          Mon.Nombre_Moneda AS Moneda,
                                          Monto,
                                          Fecha_Ingreso,
                                          Valor_Moneda,
                                          Fecha_Registro
                                      FROM Ingresos I
                                      LEFT JOIN Descripciones_Ingreso DI ON DI.Id_Descripcion_Ingreso = I.Id_Descripcion_Ingreso
                                      LEFT JOIN Miembros M ON M.Id_Miembro = I.Id_Miembro
                                      LEFT JOIN Monedas Mon ON Mon.Id_Moneda = I.Id_Moneda

                                      ORDER BY Id_Ingreso DESC";
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


        #endregion



        #region Mantenimientos

        #region Archivos

        public int AgregarArchivo(Archivo_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Archivos_Generales(
                                        NombreArchivo,
                                        NombreArchivoCarpeta,
                                        TipoArchivo,
                                        Extencion,
                                        Descripcion,
                                        Archivo,
                                        Fecha_Registro,
                                        Tamano)

                                    VALUES(
                                        @NombreArchivo,
                                        @NombreArchivoCarpeta,
                                        @TipoArchivo,
                                        @Extencion,
                                        @Descripcion,
                                        @Archivo,
                                        @Fecha_Registro,
                                        @Tamano);

                                        SELECT SCOPE_IDENTITY() AS UltimoRegistroAgregado;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@NombreArchivo", entidad.NombreArchivo);
                cmd.Parameters.AddWithValue("@NombreArchivoCarpeta", entidad.NombreArchivoCarpeta);
                cmd.Parameters.AddWithValue("@TipoArchivo", entidad.TipoArchivo);
                cmd.Parameters.AddWithValue("@Extencion", entidad.Extencion);
                cmd.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
                cmd.Parameters.AddWithValue("@Archivo", entidad.Archivo);
                cmd.Parameters.AddWithValue("@Fecha_Registro", entidad.Fecha_Registro);
                cmd.Parameters.AddWithValue("@Tamano", entidad.Tamano);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) // Si el DataReader tiene filas entonces hacer lo siguiente
                        {
                            Id = Convert.ToInt32(dr["UltimoRegistroAgregado"].ToString());
                        }
                    }
                    conexion.Close();

                    return Id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool EliminarArchivo(int Id)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "DELETE FROM Archivos_Generales WHERE Id_Archivo = @id;";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    int FilasAfectadas = cmd.ExecuteNonQuery();
                    conexion.Close();
                    if (FilasAfectadas > 0) Respuesta = true;

                    return Respuesta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #endregion
    }
}
