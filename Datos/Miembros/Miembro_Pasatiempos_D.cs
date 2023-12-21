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
    public class Miembro_Pasatiempos_D
    {
        public bool Agregar(Miembro_Pasatiempos_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Miembros_Informacion_Laboral(
                                    Id_Miembro, 
                                    Cine,
                                    Leer,
                                    Ver_TV,
                                    Socializar,
                                    Viajar,
                                    Otros)

                                   VALUES(
                                    @Id_Miembro,
                                    @Cine,
                                    @Leer,
                                    @Ver_TV,
                                    @Socializar,
                                    @Viajar,
                                    @Otros);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Cine", entidad.Cine);
                cmd.Parameters.AddWithValue("@Leer", entidad.Leer);
                cmd.Parameters.AddWithValue("@Ver_TV", entidad.Ver_TV);
                cmd.Parameters.AddWithValue("@Socializar", entidad.Socializar);
                cmd.Parameters.AddWithValue("@Viajar", entidad.Viajar);
                cmd.Parameters.AddWithValue("@Otros", entidad.Otros);
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
