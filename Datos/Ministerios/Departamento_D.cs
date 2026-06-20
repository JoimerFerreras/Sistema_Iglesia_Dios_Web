using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Ministerios;

namespace Datos.Ministerios
{
    public class Departamento_D
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
                string sentencia = $@"SELECT D.Id_Departamento, 
                                        D.Nombre_Departamento, 
                                        CASE D.Estado 
                                            WHEN '0' THEN 'Inactivo' 
                                            WHEN '1' THEN 'Activo' 
                                        END AS Estado,
										Lider.Nombres + ' ' + Lider.Apellidos AS Lider_Departamento,
										Diacono.Nombres + ' '  + Diacono.Apellidos AS Diacono_Departamento

                                        FROM Departamentos D
										LEFT JOIN Miembros Lider ON Lider.Id_Miembro = D.Id_Lider_Departamento
										LEFT JOIN Miembros Diacono ON Diacono.Id_Miembro = D.Id_Diacono_Departamento";
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

        public Departamento_E ObtenerRegistro(string Id)
        {
            Departamento_E entidad = new Departamento_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "SELECT Id_Departamento, Nombre_Departamento, Estado, Id_Lider_Departamento, Id_Diacono_Departamento  FROM Departamentos WHERE Id_Departamento = @id";
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
                        entidad.Id_Departamento = int.Parse(row["Id_Departamento"].ToString());
                        entidad.Nombre_Departamento = row["Nombre_Departamento"].ToString();
                        entidad.Id_Lider_Departamento = int.Parse(row["Id_Lider_Departamento"].ToString());
                        entidad.Id_Diacono_Departamento = int.Parse(row["Id_Diacono_Departamento"].ToString());

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
                    sentencia = @"SELECT DISTINCT D.Id_Departamento, D.Nombre_Departamento FROM Departamentos D
                                LEFT JOIN Miembros MM ON D.Id_Departamento = MM.Id_Departamento
                                WHERE D.Estado = 1 OR (D.Estado = 0 AND EXISTS (SELECT 1 FROM Miembros WHERE Id_Miembro = MM.Id_Miembro))
                                AND Id_Miembro = @Id";
                }
                else //Sentencia para consulta de registros en pantallas de Consultas
                {
                    // Sentencia que obtiene todos los registros
                    sentencia = "SELECT Id_Departamento, Nombre_Departamento FROM Departamentos";
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

        public bool Agregar(Departamento_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Departamentos(
                                    Nombre_Departamento, Estado, Id_Lider_Departamento, Id_Diacono_Departamento)

                                   VALUES(
                                    @Nombre_Departamento, @Estado, @Id_Lider_Departamento, @Id_Diacono_Departamento);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Nombre_Departamento", entidad.Nombre_Departamento);
                cmd.Parameters.AddWithValue("@Estado", entidad.Estado);
                cmd.Parameters.AddWithValue("@Id_Lider_Departamento", entidad.Id_Lider_Departamento);
                cmd.Parameters.AddWithValue("@Id_Diacono_Departamento", entidad.Id_Diacono_Departamento);
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

        public bool Editar(Departamento_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Departamentos SET 
                                        Nombre_Departamento = @Nombre_Departamento, 
                                        Estado = @Estado, 
                                        Id_Lider_Departamento = @Id_Lider_Departamento, 
                                        Id_Diacono_Departamento = @Id_Diacono_Departamento 
                                        WHERE Id_Departamento = @Id_Departamento";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Departamento", entidad.Id_Departamento);
                cmd.Parameters.AddWithValue("@Nombre_Departamento", entidad.Nombre_Departamento);
                cmd.Parameters.AddWithValue("@Estado", entidad.Estado);
                cmd.Parameters.AddWithValue("@Id_Lider_Departamento", entidad.Id_Lider_Departamento);
                cmd.Parameters.AddWithValue("@Id_Diacono_Departamento", entidad.Id_Diacono_Departamento);
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
                string sentencia = "DELETE FROM Departamentos WHERE Id_Departamento = @id;";
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
