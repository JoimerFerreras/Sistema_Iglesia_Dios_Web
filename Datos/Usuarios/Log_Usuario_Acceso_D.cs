using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Usuarios;

namespace Datos.Usuarios
{
    public class Log_Usuario_Acceso_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable Listar(DateTime FechaInicial, DateTime FechaFinal, int Id_Usuario)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);

                sentencia = $@"SELECT 
                                LUA.Id_Log,
                                LUA.Id_Usuario,
                                U.Nombre1 + ' ' + U.Nombre2 + ' ' + U.Apellido1 + ' ' + U.Apellido2 AS NombreCompleto,
                                U.Usuario,
                                LUA.IPv4,
                                LUA.FechaHora_Login,
                                LUA.Latitud_Coord,
                                LUA.Longitud_Coord

                                FROM Log_Usuarios_Accesos LUA
                                LEFT JOIN Usuarios U ON U.Id_Usuario = LUA.Id_Usuario ";


                sentencia += $@" WHERE (LUA.FechaHora_Login BETWEEN @FechaInicial AND @FechaFinal) ";
                cmd.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", FechaFinal);

                // Usuario
                if (Id_Usuario > 0)
                {
                    sentencia += $" AND (LUA.Id_Usuario = @Id_Usuario) ";
                    cmd.Parameters.AddWithValue("@Id_Usuario", Id_Usuario);
                }

                sentencia += $" ORDER BY LUA.FechaHora_Login DESC";

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

        public Log_Usuario_Acceso_E ObtenerUbicacionLog(string Id)
        {
            Log_Usuario_Acceso_E entidad = new Log_Usuario_Acceso_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT Latitud_Coord, Longitud_Coord FROM Log_Usuarios_Accesos WHERE Id_Log = @Id";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        DataRow row = dt.Rows[0];
                        entidad.Latitud_Coord = decimal.Parse(row["Latitud_Coord"].ToString());
                        entidad.Longitud_Coord = decimal.Parse(row["Longitud_Coord"].ToString());
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

        public bool Agregar(Log_Usuario_Acceso_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Log_Usuarios_Accesos(
                                            Id_Usuario, 
                                            IPv4, 
                                            FechaHora_Login, 
                                            Latitud_Coord, 
                                            Longitud_Coord) 

                                            VALUES(
                                            @Id_Usuario, 
                                            @IPv4, 
                                            @FechaHora_Login, 
                                            @Latitud_Coord, 
                                            @Longitud_Coord);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Usuario", entidad.Id_Usuario);
                cmd.Parameters.AddWithValue("@IPv4", entidad.IPv4);
                cmd.Parameters.AddWithValue("@FechaHora_Login", entidad.FechaHora_Login);
                cmd.Parameters.AddWithValue("@Latitud_Coord", entidad.Latitud_Coord);
                cmd.Parameters.AddWithValue("@Longitud_Coord", entidad.Longitud_Coord);
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
                string sentencia = "DELETE FROM Log_Usuarios_Accesos WHERE Id_Log = @id;";
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
