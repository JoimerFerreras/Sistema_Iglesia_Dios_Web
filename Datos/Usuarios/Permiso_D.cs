using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Usuarios;

namespace Datos.Usuarios
{
    public class Permiso_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable Listar()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT Id_Rol, 
                                        Id_Funcionalidad, 
                                        Permiso_Visualizar, 
                                        Permiso_Editar, 
                                        Permiso_Eliminar

                                        FROM Permisos";
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

        public Permiso_E ObtenerRegistro(int Id_Rol, int Id_Funcionalidad)
        {
            Permiso_E entidad = new Permiso_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT Id_Rol, Id_Funcionalidad, Permiso_Visualizar, Permiso_Editar, Permiso_Eliminar FROM Permisos WHERE Id_Rol = @Id AND Id_Funcionalidad = @Id_Funcionalidad";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Rol", Id_Rol);
                cmd.Parameters.AddWithValue("@Id_Funcionalidad", Id_Funcionalidad);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        DataRow row = dt.Rows[0];
                        entidad.Id_Rol = int.Parse(row["Id_Rol"].ToString());
                        entidad.Id_Funcionalidad = int.Parse(row["Id_Funcionalidad"].ToString());
                        entidad.Permiso_Visualizar = bool.Parse(row["Permiso_Visualizar"].ToString());
                        entidad.Permiso_Editar = bool.Parse( row["Permiso_Editar"].ToString());
                        entidad.Permiso_Eliminar = bool.Parse(row["Permiso_Eliminar"].ToString());
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

        public bool Agregar(Permiso_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Permisos(
                                                Id_Rol, 
                                                Id_Funcionalidad, 
                                                Permiso_Visualizar, 
                                                Permiso_Editar,
                                                Permiso_Eliminar) 

                                                VALUES(
                                                @Id_Rol, 
                                                @Id_Funcionalidad, 
                                                @Permiso_Visualizar,
                                                @Permiso_Editar, 
                                                @Permiso_Eliminar);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Rol", entidad.Id_Rol);
                cmd.Parameters.AddWithValue("@Id_Funcionalidad", entidad.Id_Funcionalidad);
                cmd.Parameters.AddWithValue("@Permiso_Visualizar", entidad.Permiso_Visualizar);
                cmd.Parameters.AddWithValue("@Permiso_Editar", entidad.Permiso_Editar);
                cmd.Parameters.AddWithValue("@Permiso_Eliminar", entidad.Permiso_Eliminar);
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

        public bool Editar(Permiso_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Permisos SET 
                                            Permiso_Visualizar = @Permiso_Visualizar,
                                            Permiso_Editar = @Permiso_Editar,
                                            Permiso_Eliminar = @Permiso_Eliminar 

                                            WHERE Id_Rol = @Id_Rol AND Id_Funcionalidad = @Id_Funcionalidad, ";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Rol", entidad.Id_Rol);
                cmd.Parameters.AddWithValue("@Id_Funcionalidad", entidad.Id_Funcionalidad);
                cmd.Parameters.AddWithValue("@Permiso_Visualizar", entidad.Permiso_Visualizar);
                cmd.Parameters.AddWithValue("@Permiso_Editar", entidad.Permiso_Editar);
                cmd.Parameters.AddWithValue("@Permiso_Eliminar", entidad.Permiso_Eliminar);
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

        public bool Eliminar(int Id_Rol, int Id_Funcionalidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "DELETE FROM Permisos WHERE Id_Rol = @Id_Rol AND Id_Funcionalidad = @Id_Funcionalidad";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Rol", Id_Rol);
                cmd.Parameters.AddWithValue("@Id_Funcionalidad", Id_Funcionalidad);
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
