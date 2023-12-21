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
    }
}
