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
        public Miembro_Informacion_Familiar2_E ObtenerRegistro(string Id)
        {
            Miembro_Informacion_Familiar2_E entidad = new Miembro_Informacion_Familiar2_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT Id_Miembro,
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
                                        Hermano5_Celular
                                        FROM Miembros_Informacion_Familiar_2

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

                            entidad.Padre_Nombre_Completo = row["Padre_Nombre_Completo"].ToString();
                            entidad.Padre_Edad = int.Parse(row["Padre_Edad"].ToString());
                            entidad.Padre_Empleado = row["Padre_Empleado"].ToString() == "True" ? true : false;
                            entidad.Padre_Negocio_Propio = row["Padre_Negocio_Propio"].ToString() == "True" ? true : false;
                            entidad.Padre_Celular = row["Padre_Celular"].ToString();
                            entidad.Padre_Miembro_Iglesia = row["Padre_Miembro_Iglesia"].ToString() == "True" ? true : false;

                            entidad.Madre_Nombre_Completo = row["Madre_Nombre_Completo"].ToString();
                            entidad.Madre_Edad = int.Parse(row["Madre_Edad"].ToString());
                            entidad.Madre_Empleada = row["Madre_Empleada"].ToString() == "True" ? true : false;
                            entidad.Madre_Negocio_Propio = row["Madre_Negocio_Propio"].ToString() == "True" ? true : false;
                            entidad.Madre_Celular = row["Madre_Celular"].ToString();
                            entidad.Madre_Miembro_Iglesia = row["Madre_Miembro_Iglesia"].ToString() == "True" ? true : false;

                            entidad.Hermano1_Nombre_Completo = row["Hermano1_Nombre_Completo"].ToString();
                            entidad.Hermano1_Escolaridad = row["Hermano1_Escolaridad"].ToString();
                            entidad.Hermano1_Correo_Electronico = row["Hermano1_Correo_Electronico"].ToString();
                            entidad.Hermano1_Celular = row["Hermano1_Celular"].ToString();

                            entidad.Hermano2_Nombre_Completo = row["Hermano2_Nombre_Completo"].ToString();
                            entidad.Hermano2_Escolaridad = row["Hermano2_Escolaridad"].ToString();
                            entidad.Hermano2_Correo_Electronico = row["Hermano2_Correo_Electronico"].ToString();
                            entidad.Hermano2_Celular = row["Hermano2_Celular"].ToString();

                            entidad.Hermano3_Nombre_Completo = row["Hermano3_Nombre_Completo"].ToString();
                            entidad.Hermano3_Escolaridad = row["Hermano3_Escolaridad"].ToString();
                            entidad.Hermano3_Correo_Electronico = row["Hermano3_Correo_Electronico"].ToString();
                            entidad.Hermano3_Celular = row["Hermano3_Celular"].ToString();

                            entidad.Hermano4_Nombre_Completo = row["Hermano4_Nombre_Completo"].ToString();
                            entidad.Hermano4_Escolaridad = row["Hermano4_Escolaridad"].ToString();
                            entidad.Hermano4_Correo_Electronico = row["Hermano4_Correo_Electronico"].ToString();
                            entidad.Hermano4_Celular = row["Hermano4_Celular"].ToString();

                            entidad.Hermano5_Nombre_Completo = row["Hermano5_Nombre_Completo"].ToString();
                            entidad.Hermano5_Escolaridad = row["Hermano5_Escolaridad"].ToString();
                            entidad.Hermano5_Correo_Electronico = row["Hermano5_Correo_Electronico"].ToString();
                            entidad.Hermano5_Celular = row["Hermano5_Celular"].ToString();
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
