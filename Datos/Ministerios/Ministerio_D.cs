using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Ministerios;

namespace Datos.Ministerios
{
    public class Ministerio_D
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
                string sentencia = $@"SELECT MN.Id_Ministerio, 
                                        MN.Nombre_Ministerio, 
                                        CASE MN.Estado 
                                            WHEN '0' THEN 'Inactivo' 
                                            WHEN '1' THEN 'Activo' 
                                        END AS Estado,
										Lider.Nombres + ' ' + Lider.Apellidos AS Lider_Ministerio,
										Diacono.Nombres + ' '  + Diacono.Apellidos AS Diacono_Ministerio

                                        FROM Ministerios MN
										LEFT JOIN Miembros Lider ON Lider.Id_Miembro = MN.Id_Lider_Ministerio
										LEFT JOIN Miembros Diacono ON Diacono.Id_Miembro = MN.Id_Diacono_Ministerio";
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

        public Ministerio_E ObtenerRegistro(string Id)
        {
            Ministerio_E entidad = new Ministerio_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "SELECT Id_Ministerio, Nombre_Ministerio, Estado, Id_Lider_Ministerio, Id_Diacono_Ministerio  FROM Ministerios WHERE Id_Ministerio = @id";
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
                        entidad.Id_Ministerio = int.Parse(row["Id_Ministerio"].ToString());
                        entidad.Nombre_Ministerio = row["Nombre_Ministerio"].ToString();
                        entidad.Id_Lider_Ministerio = int.Parse(row["Id_Lider_Ministerio"].ToString());
                        entidad.Id_Diacono_Ministerio = int.Parse(row["Id_Diacono_Ministerio"].ToString());

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
                    sentencia = @"SELECT DISTINCT M.Id_Ministerio, M.Nombre_Ministerio FROM Ministerios M
                                LEFT JOIN Miembros_Ministerios MM ON M.Id_Ministerio = MM.Id_Ministerio
                                WHERE M.Estado = 1 OR (M.Estado = 0 AND EXISTS (SELECT 1 FROM Miembros WHERE Id_Miembro = MM.Id_Miembro))
                                AND Id_Miembro = @Id";
                }
                else //Sentencia para consulta de registros en pantallas de Consultas
                {
                    // Sentencia que obtiene todos los registros
                    sentencia = "SELECT Id_Ministerio, Nombre_Ministerio FROM Ministerios";
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

        public DataTable RegistrosExistentes(int Id_Registro)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = @"SELECT COUNT(Id_Ministerio) AS CantidadRegistros FROM Ministerios WHERE Id_Ministerio = @id";
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

        public bool Agregar(Ministerio_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Ministerios(
                                    Nombre_Ministerio, Estado, Id_Lider_Ministerio, Id_Diacono_Ministerio)

                                   VALUES(
                                    @Nombre_Ministerio, @Estado, @Id_Lider_Ministerio, @Id_Diacono_Ministerio);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Nombre_Ministerio", entidad.Nombre_Ministerio);
                cmd.Parameters.AddWithValue("@Estado", entidad.Estado);
                cmd.Parameters.AddWithValue("@Id_Lider_Ministerio", entidad.Id_Lider_Ministerio);
                cmd.Parameters.AddWithValue("@Id_Diacono_Ministerio", entidad.Id_Diacono_Ministerio);
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

        public bool Editar(Ministerio_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Ministerios SET 
                                        Nombre_Ministerio = @Nombre_Ministerio, 
                                        Estado = @Estado, 
                                        Id_Lider_Ministerio = @Id_Lider_Ministerio, 
                                        Id_Diacono_Ministerio = @Id_Diacono_Ministerio 
                                        WHERE Id_Ministerio = @Id_Ministerio";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Ministerio", entidad.Id_Ministerio);
                cmd.Parameters.AddWithValue("@Nombre_Ministerio", entidad.Nombre_Ministerio);
                cmd.Parameters.AddWithValue("@Estado", entidad.Estado);
                cmd.Parameters.AddWithValue("@Id_Lider_Ministerio", entidad.Id_Lider_Ministerio);
                cmd.Parameters.AddWithValue("@Id_Diacono_Ministerio", entidad.Id_Diacono_Ministerio);
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
                string sentencia = "DELETE FROM Ministerios WHERE Id_Ministerio = @id;";
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
