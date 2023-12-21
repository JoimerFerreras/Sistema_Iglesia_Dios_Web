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
                                    Conyuge_Edad,
                                    Hijo1_Nombre,
                                    Hijo1_Edad,
                                    Hijo1_Cristiano,
                                    Hijo2_Nombre,
                                    Hijo2_Edad,
                                    Hijo2_Cristiano,
                                    Hijo3_Nombre,
                                    Hijo3_Edad,
                                    Hijo3_Cristiano,
                                    Hijo4_Nombre,
                                    Hijo4_Edad,
                                    Hijo4_Cristiano,
                                    Hijo5_Nombre,
                                    Hijo5_Edad,
                                    Hijo5_Cristiano,
                                    Hijo6_Nombre,
                                    Hijo6_Edad,
                                    Hijo6_Cristiano)

                                   VALUES(
                                    @Id_Miembro,
                                    @Conyuge_Nombre,
                                    @Conyuge_Cristiano,
                                    @Conyuge_Edad,
                                    @Hijo1_Nombre,
                                    @Hijo1_Edad,
                                    @Hijo1_Cristiano,
                                    @Hijo2_Nombre,
                                    @Hijo2_Edad,
                                    @Hijo2_Cristiano,
                                    @Hijo3_Nombre,
                                    @Hijo3_Edad,
                                    @Hijo3_Cristiano,
                                    @Hijo4_Nombre,
                                    @Hijo4_Edad,
                                    @Hijo4_Cristiano,
                                    @Hijo5_Nombre,
                                    @Hijo5_Edad,
                                    @Hijo5_Cristiano,
                                    @Hijo6_Nombre,
                                    @Hijo6_Edad,
                                    @Hijo6_Cristiano);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Conyuge_Nombre", entidad.Conyuge_Nombre);
                cmd.Parameters.AddWithValue("@Conyuge_Cristiano", entidad.Conyuge_Cristiano);
                cmd.Parameters.AddWithValue("@Conyuge_Edad", entidad.Conyuge_Edad);
                cmd.Parameters.AddWithValue("@Hijo1_Nombre", entidad.Hijo1_Nombre);
                cmd.Parameters.AddWithValue("@Hijo1_Edad", entidad.Hijo1_Edad);
                cmd.Parameters.AddWithValue("@Hijo1_Cristiano", entidad.Hijo1_Cristiano);
                cmd.Parameters.AddWithValue("@Hijo2_Nombre", entidad.Hijo2_Nombre);
                cmd.Parameters.AddWithValue("@Hijo2_Edad", entidad.Hijo2_Edad);
                cmd.Parameters.AddWithValue("@Hijo2_Cristiano", entidad.Hijo2_Cristiano);
                cmd.Parameters.AddWithValue("@Hijo3_Nombre", entidad.Hijo3_Nombre);
                cmd.Parameters.AddWithValue("@Hijo3_Edad", entidad.Hijo3_Edad);
                cmd.Parameters.AddWithValue("@Hijo3_Cristiano", entidad.Hijo3_Cristiano);
                cmd.Parameters.AddWithValue("@Hijo4_Nombre", entidad.Hijo4_Nombre);
                cmd.Parameters.AddWithValue("@Hijo4_Edad", entidad.Hijo4_Edad);
                cmd.Parameters.AddWithValue("@Hijo4_Cristiano", entidad.Hijo4_Cristiano);
                cmd.Parameters.AddWithValue("@Hijo5_Nombre", entidad.Hijo5_Nombre);
                cmd.Parameters.AddWithValue("@Hijo5_Edad", entidad.Hijo5_Edad);
                cmd.Parameters.AddWithValue("@Hijo5_Cristiano", entidad.Hijo5_Cristiano);
                cmd.Parameters.AddWithValue("@Hijo6_Nombre", entidad.Hijo6_Nombre);
                cmd.Parameters.AddWithValue("@Hijo6_Edad", entidad.Hijo6_Edad);
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
