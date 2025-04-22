using Datos.ConexionBD;
using Entidades.Miembros;
using System;
using System.Data.SqlClient;
using System.Data;

namespace Datos.Miembros
{
    public class Miembro_Informacion_Familiar1_D
    {
        public Miembro_Informacion_Familiar1_E ObtenerRegistro(string Id)
        {
            Miembro_Informacion_Familiar1_E entidad = new Miembro_Informacion_Familiar1_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT 
                                        Id_Miembro,
                                        Conyuge_Nombre,
                                        Conyuge_Cristiano,
                                        Conyuge_FechaNacimiento,
                                        Hijo1_Nombre,
                                        Hijo1_FechaNacimiento,
                                        Hijo1_Cristiano,
                                        Hijo2_Nombre,
                                        Hijo2_FechaNacimiento,
                                        Hijo2_Cristiano,
                                        Hijo3_Nombre,
                                        Hijo3_FechaNacimiento,
                                        Hijo3_Cristiano,
                                        Hijo4_Nombre,
                                        Hijo4_FechaNacimiento,
                                        Hijo4_Cristiano,
                                        Hijo5_Nombre,
                                        Hijo5_FechaNacimiento,
                                        Hijo5_Cristiano,
                                        Hijo6_Nombre,
                                        Hijo6_FechaNacimiento,
                                        Hijo6_Cristiano
                                        FROM Miembros_Informacion_Familiar_1

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
                            entidad.Conyuge_Nombre = row["Conyuge_Nombre"].ToString();
                            entidad.Conyuge_Cristiano = row["Conyuge_Cristiano"].ToString() == "True" ? true : false;
                            if (row["Conyuge_FechaNacimiento"] != DBNull.Value)
                            {
                                entidad.Conyuge_FechaNacimiento = DateTime.Parse(row["Conyuge_FechaNacimiento"].ToString());
                            }

                            entidad.Hijo1_Nombre = row["Hijo1_Nombre"].ToString();
                            entidad.Hijo1_Cristiano = row["Hijo1_Cristiano"].ToString() == "True" ? true : false;
                            if (row["Hijo1_FechaNacimiento"] != DBNull.Value)
                            {
                                entidad.Hijo1_FechaNacimiento = DateTime.Parse(row["Hijo1_FechaNacimiento"].ToString());
                            }

                            entidad.Hijo2_Nombre = row["Hijo2_Nombre"].ToString();
                            entidad.Hijo2_Cristiano = row["Hijo2_Cristiano"].ToString() == "True" ? true : false;
                            if (row["Hijo2_FechaNacimiento"] != DBNull.Value)
                            {
                                entidad.Hijo2_FechaNacimiento = DateTime.Parse(row["Hijo2_FechaNacimiento"].ToString());
                            }

                            entidad.Hijo3_Nombre = row["Hijo3_Nombre"].ToString();
                            entidad.Hijo3_Cristiano = row["Hijo3_Cristiano"].ToString() == "True" ? true : false;
                            if (row["Hijo3_FechaNacimiento"] != DBNull.Value)
                            {
                                entidad.Hijo3_FechaNacimiento = DateTime.Parse(row["Hijo3_FechaNacimiento"].ToString());
                            }

                            entidad.Hijo4_Nombre = row["Hijo4_Nombre"].ToString();
                            entidad.Hijo4_Cristiano = row["Hijo4_Cristiano"].ToString() == "True" ? true : false;
                            if (row["Hijo4_FechaNacimiento"] != DBNull.Value)
                            {
                                entidad.Hijo4_FechaNacimiento = DateTime.Parse(row["Hijo4_FechaNacimiento"].ToString());
                            }

                            entidad.Hijo5_Nombre = row["Hijo5_Nombre"].ToString();
                            entidad.Hijo5_Cristiano = row["Hijo5_Cristiano"].ToString() == "True" ? true : false;
                            if (row["Hijo5_FechaNacimiento"] != DBNull.Value)
                            {
                                entidad.Hijo5_FechaNacimiento = DateTime.Parse(row["Hijo5_FechaNacimiento"].ToString());
                            }

                            entidad.Hijo6_Nombre = row["Hijo6_Nombre"].ToString();
                            entidad.Hijo6_Cristiano = row["Hijo6_Cristiano"].ToString() == "True" ? true : false;
                            if (row["Hijo6_FechaNacimiento"] != DBNull.Value)
                            {
                                entidad.Hijo6_FechaNacimiento = DateTime.Parse(row["Hijo6_FechaNacimiento"].ToString());
                            }
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

        public bool Agregar(Miembro_Informacion_Familiar1_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Miembros_Informacion_Familiar_1(
                                    Id_Miembro, 
                                    Conyuge_Nombre,
                                    Conyuge_Cristiano,
                                    Conyuge_FechaNacimiento,
                                    Hijo1_Nombre,
                                    Hijo1_FechaNacimiento,
                                    Hijo1_Cristiano,
                                    Hijo2_Nombre,
                                    Hijo2_FechaNacimiento,
                                    Hijo2_Cristiano,
                                    Hijo3_Nombre,
                                    Hijo3_FechaNacimiento,
                                    Hijo3_Cristiano,
                                    Hijo4_Nombre,
                                    Hijo4_FechaNacimiento,
                                    Hijo4_Cristiano,
                                    Hijo5_Nombre,
                                    Hijo5_FechaNacimiento,
                                    Hijo5_Cristiano,
                                    Hijo6_Nombre,
                                    Hijo6_FechaNacimiento,
                                    Hijo6_Cristiano)

                                   VALUES(
                                    @Id_Miembro,
                                    @Conyuge_Nombre,
                                    @Conyuge_Cristiano,
                                    @Conyuge_FechaNacimiento,
                                    @Hijo1_Nombre,
                                    @Hijo1_FechaNacimiento,
                                    @Hijo1_Cristiano,
                                    @Hijo2_Nombre,
                                    @Hijo2_FechaNacimiento,
                                    @Hijo2_Cristiano,
                                    @Hijo3_Nombre,
                                    @Hijo3_FechaNacimiento,
                                    @Hijo3_Cristiano,
                                    @Hijo4_Nombre,
                                    @Hijo4_FechaNacimiento,
                                    @Hijo4_Cristiano,
                                    @Hijo5_Nombre,
                                    @Hijo5_FechaNacimiento,
                                    @Hijo5_Cristiano,
                                    @Hijo6_Nombre,
                                    @Hijo6_FechaNacimiento,
                                    @Hijo6_Cristiano);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);

                // Conyuge
                cmd.Parameters.AddWithValue("@Conyuge_Nombre", entidad.Conyuge_Nombre);
                cmd.Parameters.AddWithValue("@Conyuge_Cristiano", entidad.Conyuge_Cristiano);
                if (entidad.Conyuge_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Conyuge_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Conyuge_FechaNacimiento", entidad.Conyuge_FechaNacimiento);

                // Hijo1
                cmd.Parameters.AddWithValue("@Hijo1_Nombre", entidad.Hijo1_Nombre);
                if (entidad.Hijo1_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo1_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo1_FechaNacimiento", entidad.Hijo1_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo1_Cristiano", entidad.Hijo1_Cristiano);

                // Hijo2
                cmd.Parameters.AddWithValue("@Hijo2_Nombre", entidad.Hijo2_Nombre);
                if (entidad.Hijo2_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo2_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo2_FechaNacimiento", entidad.Hijo2_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo2_Cristiano", entidad.Hijo2_Cristiano);

                // Hijo3
                cmd.Parameters.AddWithValue("@Hijo3_Nombre", entidad.Hijo3_Nombre);
                if (entidad.Hijo3_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo3_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo3_FechaNacimiento", entidad.Hijo3_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo3_Cristiano", entidad.Hijo3_Cristiano);

                // Hijo4
                cmd.Parameters.AddWithValue("@Hijo4_Nombre", entidad.Hijo4_Nombre);
                if (entidad.Hijo4_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo4_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo4_FechaNacimiento", entidad.Hijo4_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo4_Cristiano", entidad.Hijo4_Cristiano);

                // Hijo5
                cmd.Parameters.AddWithValue("@Hijo5_Nombre", entidad.Hijo5_Nombre);
                if (entidad.Hijo5_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo5_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo5_FechaNacimiento", entidad.Hijo5_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo5_Cristiano", entidad.Hijo5_Cristiano);

                // Hijo6
                cmd.Parameters.AddWithValue("@Hijo6_Nombre", entidad.Hijo6_Nombre);
                if (entidad.Hijo6_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo6_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo6_FechaNacimiento", entidad.Hijo6_FechaNacimiento);
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

        public bool Guardar(Miembro_Informacion_Familiar1_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Miembros_Informacion_Familiar_1 SET
                                    Conyuge_Nombre = @Conyuge_Nombre,
                                    Conyuge_Cristiano = @Conyuge_Cristiano,
                                    Conyuge_FechaNacimiento = @Conyuge_FechaNacimiento,
                                    Hijo1_Nombre = @Hijo1_Nombre,
                                    Hijo1_FechaNacimiento = @Hijo1_FechaNacimiento,
                                    Hijo1_Cristiano = @Hijo1_Cristiano,
                                    Hijo2_Nombre = @Hijo2_Nombre,
                                    Hijo2_FechaNacimiento = @Hijo2_FechaNacimiento,
                                    Hijo2_Cristiano = @Hijo2_Cristiano,
                                    Hijo3_Nombre = @Hijo3_Nombre,
                                    Hijo3_FechaNacimiento = @Hijo3_FechaNacimiento,
                                    Hijo3_Cristiano = @Hijo3_Cristiano,
                                    Hijo4_Nombre = @Hijo4_Nombre,
                                    Hijo4_FechaNacimiento = @Hijo4_FechaNacimiento,
                                    Hijo4_Cristiano = @Hijo4_Cristiano,
                                    Hijo5_Nombre = @Hijo5_Nombre,
                                    Hijo5_FechaNacimiento = @Hijo5_FechaNacimiento,
                                    Hijo5_Cristiano = @Hijo5_Cristiano,
                                    Hijo6_Nombre = @Hijo6_Nombre,
                                    Hijo6_FechaNacimiento = @Hijo6_FechaNacimiento,
                                    Hijo6_Cristiano = @Hijo6_Cristiano

                                    WHERE Id_Miembro = @Id_Miembro;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);

                // Conyuge
                cmd.Parameters.AddWithValue("@Conyuge_Nombre", entidad.Conyuge_Nombre);
                cmd.Parameters.AddWithValue("@Conyuge_Cristiano", entidad.Conyuge_Cristiano);
                if (entidad.Conyuge_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Conyuge_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Conyuge_FechaNacimiento", entidad.Conyuge_FechaNacimiento);

                // Hijo1
                cmd.Parameters.AddWithValue("@Hijo1_Nombre", entidad.Hijo1_Nombre);
                if (entidad.Hijo1_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo1_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo1_FechaNacimiento", entidad.Hijo1_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo1_Cristiano", entidad.Hijo1_Cristiano);

                // Hijo2
                cmd.Parameters.AddWithValue("@Hijo2_Nombre", entidad.Hijo2_Nombre);
                if (entidad.Hijo2_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo2_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo2_FechaNacimiento", entidad.Hijo2_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo2_Cristiano", entidad.Hijo2_Cristiano);

                // Hijo3
                cmd.Parameters.AddWithValue("@Hijo3_Nombre", entidad.Hijo3_Nombre);
                if (entidad.Hijo3_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo3_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo3_FechaNacimiento", entidad.Hijo3_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo3_Cristiano", entidad.Hijo3_Cristiano);

                // Hijo4
                cmd.Parameters.AddWithValue("@Hijo4_Nombre", entidad.Hijo4_Nombre);
                if (entidad.Hijo4_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo4_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo4_FechaNacimiento", entidad.Hijo4_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo4_Cristiano", entidad.Hijo4_Cristiano);

                // Hijo5
                cmd.Parameters.AddWithValue("@Hijo5_Nombre", entidad.Hijo5_Nombre);
                if (entidad.Hijo5_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo5_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo5_FechaNacimiento", entidad.Hijo5_FechaNacimiento);
                cmd.Parameters.AddWithValue("@Hijo5_Cristiano", entidad.Hijo5_Cristiano);

                // Hijo6
                cmd.Parameters.AddWithValue("@Hijo6_Nombre", entidad.Hijo6_Nombre);
                if (entidad.Hijo6_FechaNacimiento == null)
                    cmd.Parameters.AddWithValue("@Hijo6_FechaNacimiento", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Hijo6_FechaNacimiento", entidad.Hijo6_FechaNacimiento);
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
