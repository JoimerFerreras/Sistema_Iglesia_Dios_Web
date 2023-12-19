using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Usuarios;
using Datos.ConexionBD;

namespace Datos.Usuarios
{
    public class Usuario_D
    {
        public Usuario_E Login(string Username, string Password)
        {
            Usuario_E entidad = new Usuario_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "SELECT Id_Usuario, Nombre, Apellido, Usuario, Tipo_Usuario FROM Usuarios WHERE Usuario = @usuario AND Password = @password";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@usuario", Username);
                cmd.Parameters.AddWithValue("@password", Password);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) // Si el DataReader tiene filas
                        {
                            entidad.Id_Usuario = Convert.ToInt32(dr["Id_Usuario"].ToString());
                            entidad.Nombre = Username = dr["Nombre"].ToString();
                            entidad.Apellido = Username = dr["Apellido"].ToString();
                            entidad.Usuario = Username = dr["Usuario"].ToString();
                            entidad.Tipo_Usuario = Convert.ToInt32(dr["Tipo_Usuario"].ToString());
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
    }
}
