using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Usuarios;

namespace Datos.Usuarios
{
    public class Rol_D
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
                                        Nombre_Rol, 
                                        Fecha_Registro, 
                                        CASE Estado 
                                            WHEN '0' THEN 'Inactivo' 
                                            WHEN '1' THEN 'Activo' 
                                        END AS Estado 

                                        FROM Roles ORDER BY Nombre_Rol ASC";
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

        public DataTable ListaCombo()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT Id_Rol, Nombre_Rol FROM Roles WHERE Estado = 1 ORDER BY Nombre_Rol ASC";
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

        public Rol_E ObtenerRegistro(string Id)
        {
            Rol_E entidad = new Rol_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT Id_Rol, Nombre_Rol, Fecha_Registro, Fecha_Ultima_Modificacion, Estado FROM Roles WHERE Id_Rol = @Id";
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
                        entidad.Id_Rol = int.Parse(row["Id_Rol"].ToString());
                        entidad.Nombre_Rol = row["Nombre_Rol"].ToString();
                        entidad.Fecha_Registro = DateTime.Parse(row["Fecha_Registro"].ToString());
                        if (row["Fecha_Ultima_Modificacion"] != DBNull.Value)
                        {
                            entidad.Fecha_Ultima_Modificacion = DateTime.Parse(row["Fecha_Ultima_Modificacion"].ToString());
                        }

                        if (row["Estado"].ToString() == "True")
                            entidad.Estado = true;
                        else
                            entidad.Estado = false;
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

        public int Agregar(Rol_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Roles(Nombre_Rol, Fecha_Registro, Estado) VALUES(@Nombre_Rol, @Fecha_Registro, @Estado);

                                        SELECT SCOPE_IDENTITY() AS UltimoRegistroAgregado;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Nombre_Rol", entidad.Nombre_Rol);
                cmd.Parameters.AddWithValue("@Fecha_Registro", entidad.Fecha_Registro);
                cmd.Parameters.AddWithValue("@Estado", entidad.Estado);
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

        public bool Editar(Rol_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Roles SET Nombre_Rol = @Nombre_Rol, Fecha_Ultima_Modificacion = @Fecha_Ultima_Modificacion, Estado = @Estado WHERE Id_Rol = @Id_Rol";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Rol", entidad.Id_Rol);
                cmd.Parameters.AddWithValue("@Nombre_Rol", entidad.Nombre_Rol);
                cmd.Parameters.AddWithValue("@Fecha_Ultima_Modificacion", entidad.Fecha_Ultima_Modificacion);
                cmd.Parameters.AddWithValue("@Estado", entidad.Estado);
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
                string sentencia = "DELETE FROM Roles WHERE Id_Rol = @id;";
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
