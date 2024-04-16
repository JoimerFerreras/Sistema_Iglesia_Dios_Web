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
    public class Miembro_Informacion_Familiar1_D
    {
        public bool Agregar(Miembro_Informacion_Familiar1_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Miembros_Informacion_Familiar_1(
                                    Id_Miembro, 
                                    Conyuge_Nombre,
                                    Conyuge_Cristiano,
                                    Conyuge_FechaNacimiento,
                                    Hijo1_Nombre,
                                    Hijo1_FechaNacimiento,
                                    Hijo1_Cristiano,
                                    Hijo2_Nombre,
                                    Hijo2_FechaNacimiento,
                                    Hijo2_Cristiano,
                                    Hijo3_Nombre,
                                    Hijo3_FechaNacimiento,
                                    Hijo3_Cristiano,
                                    Hijo4_Nombre,
                                    Hijo4_FechaNacimiento,
                                    Hijo4_Cristiano,
                                    Hijo5_Nombre,
                                    Hijo5_FechaNacimiento,
                                    Hijo5_Cristiano,
                                    Hijo6_Nombre,
                                    Hijo6_FechaNacimiento,
                                    Hijo6_Cristiano)

                                   VALUES(
                                    @Id_Miembro,
                                    @Conyuge_Nombre,
                                    @Conyuge_Cristiano,
                                    @Conyuge_FechaNacimiento,
                                    @Hijo1_Nombre,
                                    @Hijo1_FechaNacimiento,
                                    @Hijo1_Cristiano,
                                    @Hijo2_Nombre,
                                    @Hijo2_FechaNacimiento,
                                    @Hijo2_Cristiano,
                                    @Hijo3_Nombre,
                                    @Hijo3_FechaNacimiento,
                                    @Hijo3_Cristiano,
                                    @Hijo4_Nombre,
                                    @Hijo4_FechaNacimiento,
                                    @Hijo4_Cristiano,
                                    @Hijo5_Nombre,
                                    @Hijo5_FechaNacimiento,
                                    @Hijo5_Cristiano,
                                    @Hijo6_Nombre,
                                    @Hijo6_FechaNacimiento,
                                    @Hijo6_Cristiano);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);

                // Conyuge
                cmd.Parameters.AddWithValue("@Conyuge_Nombre", entidad.Conyuge_Nombre);
                cmd.Parameters.AddWithValue("@Conyuge_Cristiano", entidad.Conyuge_Cristiano);
                if (entidad.Conyuge_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Conyuge_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Conyuge_FechaNacimiento", entidad.Conyuge_FechaNacimiento);

                // Hijo1
                cmd.Parameters.AddWithValue("@Hijo1_Nombre", entidad.Hijo1_Nombre);
                if (entidad.Hijo1_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo1_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo1_FechaNacimiento", entidad.Hijo1_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo1_Cristiano", entidad.Hijo1_Cristiano);

                // Hijo2
                cmd.Parameters.AddWithValue("@Hijo2_Nombre", entidad.Hijo2_Nombre);
                if (entidad.Hijo2_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo2_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo2_FechaNacimiento", entidad.Hijo2_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo2_Cristiano", entidad.Hijo2_Cristiano);

                // Hijo3
                cmd.Parameters.AddWithValue("@Hijo3_Nombre", entidad.Hijo3_Nombre);
                if (entidad.Hijo3_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo3_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo3_FechaNacimiento", entidad.Hijo3_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo3_Cristiano", entidad.Hijo3_Cristiano);

                // Hijo4
                cmd.Parameters.AddWithValue("@Hijo4_Nombre", entidad.Hijo4_Nombre);
                if (entidad.Hijo4_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo4_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo4_FechaNacimiento", entidad.Hijo4_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo4_Cristiano", entidad.Hijo4_Cristiano);

                // Hijo5
                cmd.Parameters.AddWithValue("@Hijo5_Nombre", entidad.Hijo5_Nombre);
                if (entidad.Hijo5_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo5_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo5_FechaNacimiento", entidad.Hijo5_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo5_Cristiano", entidad.Hijo5_Cristiano);

                // Hijo6
                cmd.Parameters.AddWithValue("@Hijo6_Nombre", entidad.Hijo6_Nombre);
                if (entidad.Hijo6_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo6_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo6_FechaNacimiento", entidad.Hijo6_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo6_Cristiano", entidad.Hijo6_Cristiano);

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
