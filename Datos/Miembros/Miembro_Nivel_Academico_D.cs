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
    }
}
