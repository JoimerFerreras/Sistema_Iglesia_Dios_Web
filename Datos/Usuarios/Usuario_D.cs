using System;
using System.Data.SqlClient;
using System.Data;
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


        public DataTable Listar(int Rol)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = @"
                    SELECT 
                      U.Id_Usuario,
                      U.Nombre1 + ' ' + U.Nombre2 + ' ' + U.Apellido1 + ' ' + U.Apellido2 AS NombreCompleto,
                      CASE U.Sexo 
                          WHEN '1' THEN 'Masculino' 
                          WHEN '2' THEN 'Femenino' 
                      END AS Sexo,
                      R.Nombre_Rol,
                      U.Usuario,
                       CASE U.Bloqueo 
                          WHEN '1' THEN 'Bloqueado' 
                          WHEN '0' THEN 'Sin bloqueo' 
                      END AS Bloqueo,
                      CASE U.Verificacion_Dos_Pasos 
                          WHEN '1' THEN 'Desactivado' 
                          WHEN '0' THEN 'Activado' 
                      END AS Verificacion_Dos_Pasos,
                      CASE U.RestablecerPassword 
                          WHEN '1' THEN 'Desactivado' 
                          WHEN '0' THEN 'Activado' 
                      END AS RestablecerPassword

                    FROM Usuarios U
                    LEFT JOIN Roles R ON R.Id_Rol = U.Id_Rol ";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);

                // Rol
                if (Rol > 0)
                {
                    sentencia += $" WHERE (U.Id_Rol = @Id_Rol) ";
                    cmd.Parameters.AddWithValue("@Id_Rol", Rol);
                }

                sentencia += " ORDER BY NombreCompleto ASC";

                cmd.CommandText = sentencia;
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;

                try
                {
                    conexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    conexion.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public Usuario_E ObtenerRegistro(string Id)
        {
            Usuario_E entidad = new Usuario_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@" SELECT 
	                                        Id_Usuario,
                                            Nombre1,
                                            Nombre2,
                                            Apellido1,
                                            Apellido2,
                                            Sexo,
                                            Bloqueo,
                                            Id_Rol,
                                            Fecha_Creacion,
                                            Fecha_Ultima_Modificacion,
                                            Celular,
                                            Telefono,
                                            Correo,
                                            Usuario,
                                            Password,
                                            Verificacion_Dos_Pasos,
                                            RestablecerPassword
                                        FROM Usuarios

                                        WHERE I.Id_Usuario = @Id";
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
                        entidad.Id_Usuario = int.Parse(row["Id_Usuario"].ToString());
                        entidad.Nombre1 = row["Nombre1"].ToString();
                        entidad.Nombre2 = row["Nombre2"].ToString();
                        entidad.Apellido1 = row["Apellido1"].ToString();
                        entidad.Apellido2 = row["Apellido2"].ToString();
                        entidad.Sexo = int.Parse(row["Sexo"].ToString());
                        entidad.Bloqueo = bool.Parse(row["Bloqueo"].ToString());
                        entidad.Id_Rol = int.Parse(row["Id_Rol"].ToString());
                        entidad.Fecha_Creacion = DateTime.Parse(row["Fecha_Creacion"].ToString());
                        if (row["Fecha_Ultima_Modificacion"] != DBNull.Value)
                        {
                            entidad.Fecha_Ultima_Modificacion = DateTime.Parse(row["Fecha_Ultima_Modificacion"].ToString());
                        }

                        entidad.Celular = row["Celular"].ToString();
                        entidad.Telefono = row["Telefono"].ToString();
                        entidad.Correo = row["Correo"].ToString();
                        entidad.Usuario = row["Usuario"].ToString();

                        if (!row.IsNull("Password"))
                        {
                            entidad.Password = (byte[])row["Password"];
                        }
                        entidad.Verificacion_Dos_Pasos = bool.Parse(row["Verificacion_Dos_Pasos"].ToString());
                        entidad.RestablecerPassword = bool.Parse(row["RestablecerPassword"].ToString());
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

        public bool Agregar(Usuario_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Usuarios(
                                        Nombre1,
                                        Nombre2,
                                        Apellido1,
                                        Apellido2,
                                        Sexo,
                                        Bloqueo,
                                        Id_Rol,
                                        Fecha_Creacion,
                                        Celular,
                                        Telefono,
                                        Correo,
                                        Usuario,
                                        Password,
                                        Verificacion_Dos_Pasos,
                                        RestablecerPassword)

                                    VALUES(
                                        @Nombre1,
                                        @Nombre2,
                                        @Apellido1,
                                        @Apellido2,
                                        @Sexo,
                                        @Bloqueo,
                                        @Id_Rol,
                                        @Fecha_Creacion,
                                        @Celular,
                                        @Telefono,
                                        @Correo,
                                        @Usuario,
                                        @Password,
                                        @Verificacion_Dos_Pasos,
                                        @RestablecerPassword);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Nombre1", entidad.Nombre1);
                cmd.Parameters.AddWithValue("@Nombre2", entidad.Nombre2);
                cmd.Parameters.AddWithValue("@Apellido1", entidad.Apellido1);
                cmd.Parameters.AddWithValue("@Apellido2", entidad.Apellido2);
                cmd.Parameters.AddWithValue("@Sexo", entidad.Sexo);
                cmd.Parameters.AddWithValue("@Bloqueo", entidad.Bloqueo);
                cmd.Parameters.AddWithValue("@Id_Rol", entidad.Id_Rol);
                cmd.Parameters.AddWithValue("@Fecha_Creacion", entidad.Fecha_Creacion);
                cmd.Parameters.AddWithValue("@Celular", entidad.Celular);
                cmd.Parameters.AddWithValue("@Telefono", entidad.Telefono);
                cmd.Parameters.AddWithValue("@Correo", entidad.Correo);
                cmd.Parameters.AddWithValue("@Usuario", entidad.Usuario);
                cmd.Parameters.AddWithValue("@Password", entidad.Password);
                cmd.Parameters.AddWithValue("@Verificacion_Dos_Pasos", entidad.Verificacion_Dos_Pasos);
                cmd.Parameters.AddWithValue("@RestablecerPassword", entidad.RestablecerPassword);
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

        public bool Editar(Usuario_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Usuarios SET 
                                        Nombre1 = @Nombre1, 
                                        Nombre2 = @Nombre2, 
                                        Apellido1 = @Apellido1, 
                                        Apellido2 = @Apellido2, 
                                        Sexo = @Sexo, 
                                        Bloqueo = @Bloqueo, 
                                        Id_Rol = @Id_Rol, 
                                        Fecha_Creacion = @Fecha_Creacion, 
                                        Fecha_Ultima_Modificacion = @Fecha_Ultima_Modificacion, 
                                        Celular = @Celular, 
                                        Telefono = @Telefono, 
                                        Correo = @Correo, 
                                        Usuario = @Usuario, 
                                        Password = @Password, 
                                        Verificacion_Dos_Pasos = @Verificacion_Dos_Pasos, 
                                        RestablecerPassword = @RestablecerPassword

                                        WHERE Id_Usuario = @Id_Usuario";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Usuario", entidad.Id_Usuario);
                cmd.Parameters.AddWithValue("@Nombre1", entidad.Nombre1);
                cmd.Parameters.AddWithValue("@Nombre2", entidad.Nombre2);
                cmd.Parameters.AddWithValue("@Apellido1", entidad.Apellido1);
                cmd.Parameters.AddWithValue("@Apellido2", entidad.Apellido2);
                cmd.Parameters.AddWithValue("@Sexo", entidad.Sexo);
                cmd.Parameters.AddWithValue("@Bloqueo", entidad.Bloqueo);
                cmd.Parameters.AddWithValue("@Id_Rol", entidad.Id_Rol);
                cmd.Parameters.AddWithValue("@Fecha_Creacion", entidad.Fecha_Creacion);
                cmd.Parameters.AddWithValue("@Fecha_Ultima_Modificacion", entidad.Fecha_Ultima_Modificacion);
                cmd.Parameters.AddWithValue("@Celular", entidad.Celular);
                cmd.Parameters.AddWithValue("@Telefono", entidad.Telefono);
                cmd.Parameters.AddWithValue("@Correo", entidad.Correo);
                cmd.Parameters.AddWithValue("@Usuario", entidad.Usuario);
                cmd.Parameters.AddWithValue("@Password", entidad.Password);
                cmd.Parameters.AddWithValue("@Verificacion_Dos_Pasos", entidad.Verificacion_Dos_Pasos);
                cmd.Parameters.AddWithValue("@RestablecerPassword", entidad.RestablecerPassword);
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

        public bool Eliminar(int Id)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "DELETE FROM Usuarios WHERE Id_Usuario = @id;";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@id", Id);
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
