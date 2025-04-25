using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Usuarios;

namespace Datos.Usuarios
{
    public class Notificacion_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable Listar(int Id_Usuario)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT TOP 50 
                                        Id_Notificacion, 
                                        Id_Usuario, 
                                        Titulo, 
                                        Texto, 
                                        Tipo_Notificacion, 
                                        Fecha, 
                                        Visto, 
                                        Link, 
                                        Link_Destino_En_Sistema

                                    FROM Notificaciones 
                                    WHERE Id_Usuario = @Id_Usuario 
                                    ORDER BY Fecha DESC; ";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);
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

        public int Agregar(Notificacion_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Notificaciones(
                                        Id_Usuario, 
                                        Titulo, 
                                        Texto, 
                                        Tipo_Notificacion, 
                                        Fecha, 
                                        Visto, 
                                        Link, 
                                        Link_Destino_En_Sistema) 

                                        VALUES(
                                        @Id_Usuario,
                                        @Titulo,
                                        @Texto,
                                        @Tipo_Notificacion,
                                        @Fecha,
                                        @Visto,
                                        @Link,
                                        @Link_Destino_En_Sistema);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Usuario", entidad.Id_Usuario);
                cmd.Parameters.AddWithValue("@Titulo", entidad.Titulo);
                cmd.Parameters.AddWithValue("@Texto", entidad.Texto);
                cmd.Parameters.AddWithValue("@Tipo_Notificacion", entidad.Tipo_Notificacion);
                cmd.Parameters.AddWithValue("@Fecha", entidad.Fecha);
                cmd.Parameters.AddWithValue("@Visto", entidad.Visto);
                cmd.Parameters.AddWithValue("@Link", entidad.Link);
                cmd.Parameters.AddWithValue("@Link_Destino_En_Sistema", entidad.Link_Destino_En_Sistema);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    cmd.ExecuteReader();
                    conexion.Close();

                    return Id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Editar(Notificacion_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Notificaciones SET Visto = 1 WHERE Id_Notificacion = @Id_Notificacion AND Id_Usuario = @Id_Usuario";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Notificacion", entidad.Id_Notificacion);
                cmd.Parameters.AddWithValue("@Id_Usuario", entidad.Id_Usuario);
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

        public bool Eliminar(int Id)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "DELETE FROM Notificaciones WHERE Id_Notificacion = @id;";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Notificacion", Id);
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
