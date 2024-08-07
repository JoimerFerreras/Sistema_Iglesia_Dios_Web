using Datos.ConexionBD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Egresos;

namespace Datos.Egresos
{
    public class Archivo_Egreso_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable Listar(int Id_Egreso)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT 
                                        Id_Archivo, 
                                        NombreArchivo,
                                        NombreArchivoCarpeta + Extencion AS NombreArchivoCarpeta,
                                        Extencion,
                                        Descripcion,
                                        CAST(ROUND(Tamano, 4) AS DECIMAL(18, 4)) AS Tamano,
                                        Fecha_Registro

                                        FROM Archivos_Egresos
                                        WHERE Id_Egreso = @Id_Egreso ORDER BY Id_Archivo";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Egreso", Id_Egreso);
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

        public Archivo_Egreso_E ObtenerArchivo(int Id_Archivo)
        {
            Archivo_Egreso_E entidad = new Archivo_Egreso_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT 
                                        Id_Archivo, 
                                        NombreArchivoCarpeta,
                                        Extencion,
                                        Archivo

                                        FROM Archivos_Egresos
                                        WHERE Id_Archivo = @Id_Archivo";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Archivo", Id_Archivo);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        DataRow row = dt.Rows[0];
                        entidad.Id_Archivo = int.Parse(row["Id_Archivo"].ToString());
                        entidad.NombreArchivoCarpeta = row["NombreArchivoCarpeta"].ToString();
                        entidad.Extencion = row["Extencion"].ToString();

                        // Convertir la cadena base64 a byte[]
                        if (row["Archivo"] != DBNull.Value)
                        {
                            entidad.Archivo = (byte[])row["Archivo"];
                        }
                        else
                        {
                            entidad.Archivo = null; // Manejar el caso donde el campo sea NULL en la base de datos
                        }
                    }
                    conexion.Close();
                    return entidad;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion



        #region Mantenimientos

        public int Agregar(Archivo_Egreso_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Archivos_Egresos(
                                        Id_Egreso,
                                        NombreArchivo,
                                        NombreArchivoCarpeta,
                                        TipoArchivo,
                                        Extencion,
                                        Descripcion,
                                        Archivo,
                                        Fecha_Registro,
                                        Tamano)

                                    VALUES(
                                        @Id_Egreso,
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
                cmd.Parameters.AddWithValue("@Id_Egreso", entidad.Id_Egreso);
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

        public bool Eliminar(int Id)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "DELETE FROM Archivos_Egresos WHERE Id_Archivo = @id;";
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
    }
}
