using Datos.ConexionBD;
using Entidades.Miembros;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Miembros
{
    public class Miembro_Nivel_Academico_D
    {
        public Miembro_Nivel_Academico_E ObtenerRegistro(string Id)
        {
            Miembro_Nivel_Academico_E entidad = new Miembro_Nivel_Academico_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT 
                                        Id_Miembro,
                                        Primario,
                                        Secundario,
                                        Grado_Universitario,
                                        Post_Grado_Maestria
                                        FROM Miembros_Nivel_Academico

                                        WHERE Id_Miembro = @Id";
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
                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            entidad.Id_Miembro = int.Parse(row["Id_Miembro"].ToString());
                            entidad.Primario = row["Primario"].ToString() == "True" ? true : false;
                            entidad.Secundario = row["Secundario"].ToString() == "True" ? true : false;
                            entidad.Grado_Universitario = row["Grado_Universitario"].ToString() == "True" ? true : false;
                            entidad.Post_Grado_Maestria = row["Post_Grado_Maestria"].ToString() == "True" ? true : false;
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


        public bool Agregar(Miembro_Nivel_Academico_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Miembros_Nivel_Academico(
                                    Id_Miembro, 
                                    Primario,
                                    Secundario,
                                    Grado_Universitario,
                                    Post_Grado_Maestria)

                                   VALUES(
                                    @Id_Miembro,
                                    @Primario,
                                    @Secundario,
                                    @Grado_Universitario,
                                    @Post_Grado_Maestria);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Primario", entidad.Primario);
                cmd.Parameters.AddWithValue("@Secundario", entidad.Secundario);
                cmd.Parameters.AddWithValue("@Grado_Universitario", entidad.Grado_Universitario);
                cmd.Parameters.AddWithValue("@Post_Grado_Maestria", entidad.Post_Grado_Maestria);
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

        public bool Guardar(Miembro_Nivel_Academico_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Miembros_Nivel_Academico SET
                                    Primario = @Primario,
                                    Secundario = @Secundario,
                                    Grado_Universitario = @Grado_Universitario,
                                    Post_Grado_Maestria = @Post_Grado_Maestria 
                                    WHERE Id_Miembro = @Id_Miembro;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Primario", entidad.Primario);
                cmd.Parameters.AddWithValue("@Secundario", entidad.Secundario);
                cmd.Parameters.AddWithValue("@Grado_Universitario", entidad.Grado_Universitario);
                cmd.Parameters.AddWithValue("@Post_Grado_Maestria", entidad.Post_Grado_Maestria);
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
    }
}
