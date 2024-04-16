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
        public Usuario_E Login(string Username, byte[] Password)
        {
            Usuario_E entidad = new Usuario_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "SELECT Id_Usuario, Nombre1, Apellido1, Usuario, Correo, Id_Rol, Bloqueo, Verificacion_Dos_Pasos, RestablecerPassword FROM Usuarios WHERE Usuario = @usuario AND Password = @password";

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
                            entidad.Nombre1 = Username = dr["Nombre1"].ToString();
                            entidad.Apellido1 = Username = dr["Apellido1"].ToString();
                            entidad.Usuario = Username = dr["Usuario"].ToString();
                            entidad.Correo = dr["Correo"].ToString();
                            entidad.Id_Rol = Convert.ToInt32(dr["Id_Rol"].ToString());
                            entidad.Bloqueo = bool.Parse(dr["Bloqueo"].ToString());
                            entidad.Verificacion_Dos_Pasos = bool.Parse(dr["Verificacion_Dos_Pasos"].ToString());
                            entidad.RestablecerPassword = bool.Parse(dr["RestablecerPassword"].ToString());
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
