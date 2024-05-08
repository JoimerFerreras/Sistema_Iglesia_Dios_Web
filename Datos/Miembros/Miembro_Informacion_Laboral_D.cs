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
    public class Miembro_Informacion_Laboral_D
    {
        public Miembro_Informacion_Laboral_E ObtenerRegistro(string Id)
        {
            Miembro_Informacion_Laboral_E entidad = new Miembro_Informacion_Laboral_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT 
                                        Empleado_Privado,
                                        Empleado_Publico,
                                        Independiente,
                                        Otros,
                                        Nombre_Empresa_Negocio
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
                        DataRow row = dt.Rows[0];
                        entidad.Id_Miembro = int.Parse(row["Id_Miembro"].ToString());
                        entidad.Empleado_Privado = row["Empleado_Privado"].ToString() == "True" ? true : false;
                        entidad.Empleado_Publico = row["Empleado_Publico"].ToString() == "True" ? true : false;
                        entidad.Independiente = row["Independiente"].ToString() == "True" ? true : false;
                        entidad.Otros = row["Otros"].ToString() == "True" ? true : false;
                        entidad.Nombre_Empresa_Negocio = row["Nombre_Empresa_Negocio"].ToString();
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

        public bool Agregar(Miembro_Informacion_Laboral_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Miembros_Informacion_Laboral(
                                    Id_Miembro, 
                                    Empleado_Privado,
                                    Empleado_Publico,
                                    Independiente,
                                    Otros,
                                    Nombre_Empresa_Negocio)

                                   VALUES(
                                    @Id_Miembro,
                                    @Empleado_Privado,
                                    @Empleado_Publico,
                                    @Independiente,
                                    @Otros,
                                    @Nombre_Empresa_Negocio);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Empleado_Privado", entidad.Empleado_Privado);
                cmd.Parameters.AddWithValue("@Empleado_Publico", entidad.Empleado_Publico);
                cmd.Parameters.AddWithValue("@Independiente", entidad.Independiente);
                cmd.Parameters.AddWithValue("@Otros", entidad.Otros);
                cmd.Parameters.AddWithValue("@Nombre_Empresa_Negocio", entidad.Nombre_Empresa_Negocio);
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

        public bool Guardar(Miembro_Informacion_Laboral_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Miembros_Informacion_Laboral SET
                                    Empleado_Privado = @Empleado_Privado,
                                    Empleado_Publico = @Empleado_Publico,
                                    Independiente = @Independiente,
                                    Otros = @Otros,
                                    Nombre_Empresa_Negocio = @Nombre_Empresa_Negocio

                                    WHERE Id_Miembro = @Id_Miembro";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Empleado_Privado", entidad.Empleado_Privado);
                cmd.Parameters.AddWithValue("@Empleado_Publico", entidad.Empleado_Publico);
                cmd.Parameters.AddWithValue("@Independiente", entidad.Independiente);
                cmd.Parameters.AddWithValue("@Otros", entidad.Otros);
                cmd.Parameters.AddWithValue("@Nombre_Empresa_Negocio", entidad.Nombre_Empresa_Negocio);
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
