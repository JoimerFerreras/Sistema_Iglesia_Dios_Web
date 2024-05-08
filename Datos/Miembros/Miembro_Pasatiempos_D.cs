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
        public Miembro_Pasatiempos_E ObtenerRegistro(string Id)
        {
            Miembro_Pasatiempos_E entidad = new Miembro_Pasatiempos_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT Id_Miembro,
                                        Cine,
                                        Leer,
                                        Ver_TV,
                                        Socializar,
                                        Viajar,
                                        Otros
                                        FROM Miembros_Pasatiempos

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
                        entidad.Cine = row["Cine"].ToString() == "True" ? true : false;
                        entidad.Leer = row["Leer"].ToString() == "True" ? true : false;
                        entidad.Ver_TV = row["Ver_TV"].ToString() == "True" ? true : false;
                        entidad.Socializar = row["Socializar"].ToString() == "True" ? true : false;
                        entidad.Viajar = row["Viajar"].ToString() == "True" ? true : false;
                        entidad.Otros = row["Otros"].ToString();
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
        public bool Agregar(Miembro_Pasatiempos_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Miembros_Pasatiempos(
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

        public bool Guardar(Miembro_Pasatiempos_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Miembros_Pasatiempos SET
                                    Cine = @Cine,
                                    Leer = @Leer,
                                    Ver_TV = @Ver_TV,
                                    Socializar = @Socializar,
                                    Viajar = @Viajar,
                                    Otros = @Otros
                                    WHERE Id_Miembro = @Id_Miembro";

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
