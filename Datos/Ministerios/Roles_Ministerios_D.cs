using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Ministerios;

namespace Datos.Ministerios
{
    public class Roles_Ministerios_D
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
                string sentencia = $@"SELECT Id_Rol_Ministerio_Ministerio,
                                        Nombre_Rol_Ministerio,
                                        CASE Estado 
			                                WHEN '0' THEN 'Inactivo' 
			                                WHEN '1' THEN 'Activo' 
	                                    END AS Estado
                                      FROM Roles_Ministerios";
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

        public Roles_Ministerios_E ObtenerRegistro(string Id)
        {
            Roles_Ministerios_E entidad = new Roles_Ministerios_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "SELECT Id_Rol_Ministerio, Nombre_Rol_Ministerio, Estado FROM Roles_Ministerios WHERE Id_Rol_Ministerio = @id";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        DataRow row = dt.Rows[0];
                        entidad.Id_Rol_Ministerio = int.Parse(row["Id_Rol_Ministerio"].ToString());
                        entidad.Nombre_Rol_Ministerio = row["Nombre_Rol_Ministerio"].ToString();

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

        public DataTable ListaCombo(int Id_Registro, bool TipoConsulta)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia;

                if (TipoConsulta == true) // Sentencia para Insercion y edicion de registro
                {
                    // Sentencia que obtiene todos los registros con status activo y tambien trae el registro inactivo de la referencia correspondiente.
                    sentencia = @"SELECT DISTINCT RM.Id_Rol_Ministerio, RM.Nombre_Rol_Ministerio FROM Roles_Ministerios RM
                                LEFT JOIN Miembros MM ON RM.Id_Rol_Ministerio = MM.Id_Rol_Ministerio_Miembro
                                WHERE RM.Estado = 1 OR (RM.Estado = 0 AND EXISTS (SELECT 1 FROM Miembros WHERE Id_Miembro = MM.Id_Miembro))
                                AND Id_Miembro = @Id";
                }
                else //Sentencia para consulta de registros en pantallas de Consultas
                {
                    // Sentencia que obtiene todos los registros
                    sentencia = "SELECT Id_Rol_Ministerio, Nombre_Rol_Ministerio FROM Roles_Ministerios";
                }
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@id", Id_Registro);
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

        public bool Agregar(Roles_Ministerios_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Roles_Ministerios(
                                    Nombre_Rol_Ministerio, Estado)

                                   VALUES(
                                    @Nombre_Rol_Ministerio, @Estado);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Nombre_Rol_Ministerio", entidad.Nombre_Rol_Ministerio);
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

        public bool Editar(Roles_Ministerios_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Roles_Ministerios SET 
                                        Nombre_Rol_Ministerio = @Nombre_Rol_Ministerio, 
                                        Estado = @Estado 
                                        WHERE Id_Rol_Ministerio = @Id_Rol_Ministerio";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Rol_Ministerio", entidad.Id_Rol_Ministerio);
                cmd.Parameters.AddWithValue("@Nombre_Rol_Ministerio", entidad.Nombre_Rol_Ministerio);
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
                string sentencia = "DELETE FROM Nombre_Rol_Ministerio WHERE Id_Rol_Ministerio = @id;";
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
