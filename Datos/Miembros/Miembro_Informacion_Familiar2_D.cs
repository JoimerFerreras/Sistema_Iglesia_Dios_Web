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
    public class Miembro_Informacion_Familiar2_D
    {
        public bool Agregar(Miembro_Informacion_Familiar2_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Miembros_Informacion_Familiar_2(
                                    Id_Miembro, 
                                    Padre_Nombre_Completo,
                                    Padre_Edad,
                                    Padre_Empleado,
                                    Padre_Negocio_Propio,
                                    Padre_Celular,
                                    Padre_Miembro_Iglesia,
                                    Madre_Nombre_Completo,
                                    Madre_Edad,
                                    Madre_Empleada,
                                    Madre_Negocio_Propio,
                                    Madre_Celular,
                                    Madre_Miembro_Iglesia,
                                    Hermano1_Nombre_Completo,
                                    Hermano1_Escolaridad,
                                    Hermano1_Correo_Electronico,
                                    Hermano1_Celular,
                                    Hermano2_Nombre_Completo,
                                    Hermano2_Escolaridad,
                                    Hermano2_Correo_Electronico,
                                    Hermano2_Celular,
                                    Hermano3_Nombre_Completo,
                                    Hermano3_Escolaridad,
                                    Hermano3_Correo_Electronico,
                                    Hermano3_Celular,
                                    Hermano4_Nombre_Completo,
                                    Hermano4_Escolaridad,
                                    Hermano4_Correo_Electronico,
                                    Hermano4_Celular,
                                    Hermano5_Nombre_Completo,
                                    Hermano5_Escolaridad,
                                    Hermano5_Correo_Electronico,
                                    Hermano5_Celular)

                                    VALUES(
                                    @Id_Miembro,
                                    @Padre_Nombre_Completo,
                                    @Padre_Edad,
                                    @Padre_Empleado,
                                    @Padre_Negocio_Propio,
                                    @Padre_Celular,
                                    @Padre_Miembro_Iglesia,
                                    @Madre_Nombre_Completo,
                                    @Madre_Edad,
                                    @Madre_Empleada,
                                    @Madre_Negocio_Propio,
                                    @Madre_Celular,
                                    @Madre_Miembro_Iglesia,
                                    @Hermano1_Nombre_Completo,
                                    @Hermano1_Escolaridad,
                                    @Hermano1_Correo_Electronico,
                                    @Hermano1_Celular,
                                    @Hermano2_Nombre_Completo,
                                    @Hermano2_Escolaridad,
                                    @Hermano2_Correo_Electronico,
                                    @Hermano2_Celular,
                                    @Hermano3_Nombre_Completo,
                                    @Hermano3_Escolaridad,
                                    @Hermano3_Correo_Electronico,
                                    @Hermano3_Celular,
                                    @Hermano4_Nombre_Completo,
                                    @Hermano4_Escolaridad,
                                    @Hermano4_Correo_Electronico,
                                    @Hermano4_Celular,
                                    @Hermano5_Nombre_Completo,
                                    @Hermano5_Escolaridad,
                                    @Hermano5_Correo_Electronico,
                                    @Hermano5_Celular);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Padre_Nombre_Completo", entidad.Padre_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Padre_Edad", entidad.Padre_Edad);
                cmd.Parameters.AddWithValue("@Padre_Empleado", entidad.Padre_Empleado);
                cmd.Parameters.AddWithValue("@Padre_Negocio_Propio", entidad.Padre_Negocio_Propio);
                cmd.Parameters.AddWithValue("@Padre_Celular", entidad.Padre_Celular);
                cmd.Parameters.AddWithValue("@Padre_Miembro_Iglesia", entidad.Padre_Miembro_Iglesia);
                cmd.Parameters.AddWithValue("@Madre_Nombre_Completo", entidad.Madre_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Madre_Edad", entidad.Madre_Edad);
                cmd.Parameters.AddWithValue("@Madre_Empleada", entidad.Madre_Empleada);
                cmd.Parameters.AddWithValue("@Madre_Negocio_Propio", entidad.Madre_Negocio_Propio);
                cmd.Parameters.AddWithValue("@Madre_Celular", entidad.Madre_Celular);
                cmd.Parameters.AddWithValue("@Madre_Miembro_Iglesia", entidad.Madre_Miembro_Iglesia);
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
