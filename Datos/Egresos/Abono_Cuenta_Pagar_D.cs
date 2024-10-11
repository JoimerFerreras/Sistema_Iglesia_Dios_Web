using Datos.ConexionBD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Egresos;

namespace Datos.Egresos
{
    public class Abono_Cuenta_Pagar_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable Listar(int Id_Cuenta_Pagar)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                                    sentencia = @"SELECT ACP.Id_Abono_CP,
                                                        ACP.Monto_Abono,
	                                                    FP.Descripcion_Forma_Pago,
                                                        ACP.Fecha_Abono,
                                                        ACP.Fecha_Registro
      
                                                    FROM Abonos_Cuentas_Pagar ACP
                                                    LEFT JOIN Formas_Pago FP ON FP.Id_Forma_Pago = ACP.Id_Forma_Pago";


                // Id de cuenta por pagar
                sentencia += $" WHERE (ACP.Id_Cuenta_Pagar = @Id_Cuenta_Pagar) ";
                cmd.Parameters.AddWithValue("@Id_Cuenta_Pagar", Id_Cuenta_Pagar);

                sentencia += $@"ORDER BY ACP.Id_Abono_CP DESC";

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

        public DataTable ObtenerTotalesPagadosRestantes(int Id_Cuenta_Pagar)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {

                // Con esta consutla se obtienen los totales pagados y los totales pendientes por pagar
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SELECT SUM(ACP.Monto_Abono) AS Total_Pagado, CPP.Monto_Total_Pagar - SUM(ACP.Monto_Abono) AS Total_Restante FROM Abonos_Cuentas_Pagar ACP
                            INNER JOIN Cuentas_Por_Pagar CPP ON CPP.Id_Cuenta_Pagar = ACP.Id_Cuenta_Pagar 

                            WHERE CPP.Id_Cuenta_Pagar = @Id_Cuenta_Pagar

                            GROUP BY CPP.Monto_Total_Pagar";
                cmd.Parameters.AddWithValue("@Id_Cuenta_Pagar", Id_Cuenta_Pagar);

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


        public Abono_Cuenta_Pagar_E ObtenerRegistro(string Id)
        {
            Abono_Cuenta_Pagar_E entidad = new Abono_Cuenta_Pagar_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT ACP.Id_Abono_CP,
                                              ACP.Id_Cuenta_Pagar,
                                              ACP.Monto_Abono,
                                              ACP.Fecha_Abono,
                                              ACP.Id_Usuario_Registro,
	                                          U1.Nombre1 + ' ' + U1.Apellido1 AS Nombre_Usuario_Registro,
                                              ACP.Fecha_Registro,
                                              ACP.Id_Usuario_Ultima_Modificacion,
	                                          U2.Nombre1 + ' ' + U2.Apellido1 AS Nombre_Usuario_Ultima_Modificacion,
                                              ACP.Fecha_Ultima_Modificacion,
                                              ACP.Comentario,
                                              ACP.Id_Forma_Pago
                                          FROM Abonos_Cuentas_Pagar ACP
                                          LEFT JOIN Usuarios U1 ON U1.Id_Usuario = ACP.Id_Usuario_Registro
                                          LEFT JOIN Usuarios U2 ON U2.Id_Usuario = ACP.Id_Usuario_Ultima_Modificacion

                                          WHERE ACP.Id_Abono_CP = @Id";
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
                        entidad.Id_Abono_CP = int.Parse(row["Id_Abono_CP"].ToString());
                        entidad.Id_Cuenta_Pagar = int.Parse(row["Id_Cuenta_Pagar"].ToString());
                        entidad.Monto_Abono = float.Parse(row["Monto_Abono"].ToString());
                        entidad.Fecha_Abono = DateTime.Parse(row["Fecha_Abono"].ToString());
                        entidad.Id_Forma_Pago = int.Parse(row["Id_Forma_Pago"].ToString());
                        entidad.Comentario = row["Comentario"].ToString();

                        entidad.Id_Usuario_Registro = int.Parse(row["Id_Usuario_Registro"].ToString());
                        entidad.Nombre_Usuario_Registro = row["Nombre_Usuario_Registro"].ToString();
                        entidad.Fecha_Registro = DateTime.Parse(row["Fecha_Registro"].ToString());

                        entidad.Id_Usuario_Ultima_Modificacion = int.Parse(row["Id_Usuario_Ultima_Modificacion"].ToString());
                        entidad.Nombre_Usuario_Ultima_Modificacion = row["Nombre_Usuario_Ultima_Modificacion"].ToString();
                        if (row["Fecha_Ultima_Modificacion"] != DBNull.Value)
                        {
                            entidad.Fecha_Ultima_Modificacion = DateTime.Parse(row["Fecha_Ultima_Modificacion"].ToString());
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

        #endregion



        #region Mantenimientos

        public bool Agregar(Abono_Cuenta_Pagar_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Abonos_Cuentas_Pagar(
                                        Id_Cuenta_Pagar,
                                        Monto_Abono,
                                        Fecha_Abono,
                                        Id_Usuario_Registro,
                                        Fecha_Registro,
                                        Id_Usuario_Ultima_Modificacion,
                                        Comentario,
                                        Id_Forma_Pago)

                                    VALUES(
                                        @Id_Cuenta_Pagar,
                                        @Monto_Abono,
                                        @Fecha_Abono,
                                        @Id_Usuario_Registro,
                                        @Fecha_Registro,
                                        @Id_Usuario_Ultima_Modificacion,
                                        @Comentario,
                                        @Id_Forma_Pago);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Cuenta_Pagar", entidad.Id_Cuenta_Pagar);
                cmd.Parameters.AddWithValue("@Monto_Abono", entidad.Monto_Abono);
                cmd.Parameters.AddWithValue("@Fecha_Abono", entidad.Fecha_Abono);
                cmd.Parameters.AddWithValue("@Id_Usuario_Registro", entidad.Id_Usuario_Registro);
                cmd.Parameters.AddWithValue("@Fecha_Registro", entidad.Fecha_Registro);
                cmd.Parameters.AddWithValue("@Id_Usuario_Ultima_Modificacion", "0");
                cmd.Parameters.AddWithValue("@Comentario", entidad.Comentario);
                cmd.Parameters.AddWithValue("@Id_Forma_Pago", entidad.Id_Forma_Pago);
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

        public bool Editar(Abono_Cuenta_Pagar_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Abonos_Cuentas_Pagar SET 
                                        Id_Cuenta_Pagar = @Id_Cuenta_Pagar, 
                                        Monto_Abono = @Monto_Abono, 
                                        Fecha_Abono = @Fecha_Abono,
                                        Comentario = @Comentario, 
                                        Id_Forma_Pago = @Id_Forma_Pago, 
                                        Id_Usuario_Ultima_Modificacion = @Id_Usuario_Ultima_Modificacion, 
                                        Fecha_Ultima_Modificacion = @Fecha_Ultima_Modificacion 

                                        WHERE Id_Abono_CP = @Id_Abono_CP";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Abono_CP", entidad.Id_Abono_CP);
                cmd.Parameters.AddWithValue("@Id_Cuenta_Pagar", entidad.Id_Cuenta_Pagar);
                cmd.Parameters.AddWithValue("@Monto_Abono", entidad.Monto_Abono);
                cmd.Parameters.AddWithValue("@Fecha_Abono", entidad.Fecha_Abono);
                cmd.Parameters.AddWithValue("@Comentario", entidad.Comentario);
                cmd.Parameters.AddWithValue("@Id_Forma_Pago", entidad.Id_Forma_Pago);
                cmd.Parameters.AddWithValue("@Id_Usuario_Ultima_Modificacion", entidad.Id_Usuario_Ultima_Modificacion);
                cmd.Parameters.AddWithValue("@Fecha_Ultima_Modificacion", entidad.Fecha_Ultima_Modificacion);
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
                string sentencia = "DELETE FROM Abonos_Cuentas_Pagar WHERE Id_Abono_CP = @id;";
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
        #endregion
    }
}
