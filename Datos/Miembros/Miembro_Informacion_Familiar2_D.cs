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

                cmd.Parameters.AddWithValue("@Hermano1_Nombre_Completo", entidad.Hermano1_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano1_Escolaridad", entidad.Hermano1_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano1_Correo_Electronico", entidad.Hermano1_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano1_Celular", entidad.Hermano1_Celular);

                cmd.Parameters.AddWithValue("@Hermano2_Nombre_Completo", entidad.Hermano2_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano2_Escolaridad", entidad.Hermano2_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano2_Correo_Electronico", entidad.Hermano2_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano2_Celular", entidad.Hermano2_Celular);

                cmd.Parameters.AddWithValue("@Hermano3_Nombre_Completo", entidad.Hermano3_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano3_Escolaridad", entidad.Hermano3_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano3_Correo_Electronico", entidad.Hermano3_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano3_Celular", entidad.Hermano3_Celular);

                cmd.Parameters.AddWithValue("@Hermano4_Nombre_Completo", entidad.Hermano4_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano4_Escolaridad", entidad.Hermano4_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano4_Correo_Electronico", entidad.Hermano4_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano4_Celular", entidad.Hermano4_Celular);

                cmd.Parameters.AddWithValue("@Hermano5_Nombre_Completo", entidad.Hermano5_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano5_Escolaridad", entidad.Hermano5_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano5_Correo_Electronico", entidad.Hermano5_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano5_Celular", entidad.Hermano5_Celular);

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

        public bool Guardar(Miembro_Informacion_Familiar2_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Miembros_Informacion_Familiar_2 SET
                                    Padre_Nombre_Completo = @Padre_Nombre_Completo,
                                    Padre_Edad = @Padre_Edad,
                                    Padre_Empleado = @Padre_Empleado,
                                    Padre_Negocio_Propio = @Padre_Negocio_Propio,
                                    Padre_Celular = @Padre_Celular,
                                    Padre_Miembro_Iglesia = @Padre_Miembro_Iglesia,
                                    Madre_Nombre_Completo = @Madre_Nombre_Completo,
                                    Madre_Edad = @Madre_Edad,
                                    Madre_Empleada = @Madre_Empleada,
                                    Madre_Negocio_Propio = @Madre_Negocio_Propio,
                                    Madre_Celular = @Madre_Celular,
                                    Madre_Miembro_Iglesia = @Madre_Miembro_Iglesia,
                                    Hermano1_Nombre_Completo = @Hermano1_Nombre_Completo,
                                    Hermano1_Escolaridad = @Hermano1_Escolaridad,
                                    Hermano1_Correo_Electronico = @Hermano1_Correo_Electronico,
                                    Hermano1_Celular = @Hermano1_Celular,
                                    Hermano2_Nombre_Completo = @Hermano2_Nombre_Completo,
                                    Hermano2_Escolaridad = @Hermano2_Escolaridad,
                                    Hermano2_Correo_Electronico = @Hermano2_Correo_Electronico,
                                    Hermano2_Celular = @Hermano2_Celular,
                                    Hermano3_Nombre_Completo = @Hermano3_Nombre_Completo,
                                    Hermano3_Escolaridad = @Hermano3_Escolaridad,
                                    Hermano3_Correo_Electronico = @Hermano3_Correo_Electronico,
                                    Hermano3_Celular = @Hermano3_Celular,
                                    Hermano4_Nombre_Completo = @Hermano4_Nombre_Completo,
                                    Hermano4_Escolaridad = @Hermano4_Escolaridad,
                                    Hermano4_Correo_Electronico = @Hermano4_Correo_Electronico,
                                    Hermano4_Celular = @Hermano4_Celular,
                                    Hermano5_Nombre_Completo = @Hermano5_Nombre_Completo,
                                    Hermano5_Escolaridad = @Hermano5_Escolaridad,
                                    Hermano5_Correo_Electronico = @Hermano5_Correo_Electronico,
                                    Hermano5_Celular = @Hermano5_Celular

                                    WHERE Id_Miembro = @Id_Miembro;";

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

                cmd.Parameters.AddWithValue("@Hermano1_Nombre_Completo", entidad.Hermano1_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano1_Escolaridad", entidad.Hermano1_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano1_Correo_Electronico", entidad.Hermano1_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano1_Celular", entidad.Hermano1_Celular);

                cmd.Parameters.AddWithValue("@Hermano2_Nombre_Completo", entidad.Hermano2_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano2_Escolaridad", entidad.Hermano2_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano2_Correo_Electronico", entidad.Hermano2_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano2_Celular", entidad.Hermano2_Celular);

                cmd.Parameters.AddWithValue("@Hermano3_Nombre_Completo", entidad.Hermano3_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano3_Escolaridad", entidad.Hermano3_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano3_Correo_Electronico", entidad.Hermano3_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano3_Celular", entidad.Hermano3_Celular);

                cmd.Parameters.AddWithValue("@Hermano4_Nombre_Completo", entidad.Hermano4_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano4_Escolaridad", entidad.Hermano4_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano4_Correo_Electronico", entidad.Hermano4_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano4_Celular", entidad.Hermano4_Celular);

                cmd.Parameters.AddWithValue("@Hermano5_Nombre_Completo", entidad.Hermano5_Nombre_Completo);
                cmd.Parameters.AddWithValue("@Hermano5_Escolaridad", entidad.Hermano5_Escolaridad);
                cmd.Parameters.AddWithValue("@Hermano5_Correo_Electronico", entidad.Hermano5_Correo_Electronico);
                cmd.Parameters.AddWithValue("@Hermano5_Celular", entidad.Hermano5_Celular);

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
