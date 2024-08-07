using Datos.ConexionBD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Ingresos;

namespace Datos.Ingresos
{
    public class Ingreso_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable Listar(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, int Miembro, int Descripcion_Ingreso, int Moneda)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SELECT Id_Ingreso,
                                          M.Nombres + ' ' + M.Apellidos AS Miembro,
                                          DI.Descripcion_Ingreso,
                                          Mon.Nombre_Moneda AS Moneda,
                                          Monto,
                                          Fecha_Ingreso,
                                          Valor_Moneda,
                                          Fecha_Registro
                                      FROM Ingresos I
                                      LEFT JOIN Descripciones_Ingreso DI ON DI.Id_Descripcion_Ingreso = I.Id_Descripcion_Ingreso
                                      LEFT JOIN Miembros M ON M.Id_Miembro = I.Id_Miembro
                                      LEFT JOIN Monedas Mon ON Mon.Id_Moneda = I.Id_Moneda ";

                // Tipo de fecha
                sentencia += $@" WHERE ({TipoFecha} BETWEEN @FechaInicial AND @FechaFinal) ";
                cmd.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", FechaFinal);

                // Miembro
                if (Miembro > 0)
                {
                    sentencia += $" AND (M.Id_Miembro = @Miembro) ";
                    cmd.Parameters.AddWithValue("@Miembro", Miembro);
                }

                //Descripcion de ingreso
                if (Descripcion_Ingreso > 0)
                {
                    sentencia += $" AND (DI.Id_Descripcion_Ingreso = @Descripcion_Ingreso) ";
                    cmd.Parameters.AddWithValue("@Descripcion_Ingreso", Descripcion_Ingreso);
                }

                //Moneda
                if (Moneda > 0)
                {
                    sentencia += $" AND (Mon.Id_Moneda = @Moneda) ";
                    cmd.Parameters.AddWithValue("@Moneda", Moneda);
                }

                sentencia += $@"ORDER BY Id_Ingreso DESC";

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


        public Ingreso_E ObtenerRegistro(string Id)
        {
            Ingreso_E entidad = new Ingreso_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT I.Id_Ingreso,
                                        I.Id_Miembro,
                                        I.Id_Descripcion_Ingreso,
                                        I.Id_Moneda,
                                        I.Monto,
                                        I.Fecha_Ingreso,
                                        I.Valor_Moneda,
                                        I.Id_Usuario_Registro,
                                        U1.Nombre1 + ' ' + U1.Apellido1 AS Nombre_Usuario_Registro,
                                        I.Fecha_Registro,
                                        I.Id_Usuario_Ultima_Modificacion,
                                        U2.Nombre1 + ' ' + U2.Apellido1 AS Nombre_Usuario_Ultima_Modificacion,
                                        I.Fecha_Ultima_Modificacion
                                        FROM Ingresos I
                                        LEFT JOIN Usuarios U1 ON U1.Id_Usuario = I.Id_Usuario_Registro
                                        LEFT JOIN Usuarios U2 ON U2.Id_Usuario = I.Id_Usuario_Ultima_Modificacion

                                        WHERE I.Id_Ingreso = @Id";
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
                        entidad.Id_Ingreso = int.Parse(row["Id_Ingreso"].ToString());
                        entidad.Id_Miembro = int.Parse(row["Id_Miembro"].ToString());
                        entidad.Id_Descripcion_Ingreso = int.Parse(row["Id_Descripcion_Ingreso"].ToString());
                        entidad.Id_Moneda = int.Parse(row["Id_Moneda"].ToString());
                        entidad.Monto = double.Parse(row["Monto"].ToString());
                        entidad.Fecha_Ingreso = DateTime.Parse(row["Fecha_Ingreso"].ToString());
                        entidad.Valor_Moneda = double.Parse(row["Valor_Moneda"].ToString());
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

        public int Agregar(Ingreso_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Ingresos(
                                        Id_Miembro,
                                        Id_Descripcion_Ingreso,
                                        Id_Moneda,
                                        Monto,
                                        Fecha_Ingreso,
                                        Valor_Moneda,
                                        Id_Usuario_Registro,
                                        Fecha_Registro,
                                        Id_Usuario_Ultima_Modificacion,
                                        Id_Forma_Pago,
                                        Comentario)

                                    VALUES(
                                        @Id_Miembro,
                                        @Id_Descripcion_Ingreso,
                                        @Id_Moneda,
                                        @Monto,
                                        @Fecha_Ingreso,
                                        @Valor_Moneda,
                                        @Id_Usuario_Registro,
                                        @Fecha_Registro,
                                        @Id_Usuario_Ultima_Modificacion,
                                        @Id_Forma_Pago,
                                        @Comentario);

                                        SELECT SCOPE_IDENTITY() AS UltimoRegistroAgregado;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Descripcion_Ingreso", entidad.Id_Descripcion_Ingreso);
                cmd.Parameters.AddWithValue("@Id_Moneda", entidad.Id_Moneda);
                cmd.Parameters.AddWithValue("@Monto", entidad.Monto);
                cmd.Parameters.AddWithValue("@Fecha_Ingreso", entidad.Fecha_Ingreso);
                cmd.Parameters.AddWithValue("@Valor_Moneda", entidad.Valor_Moneda);
                cmd.Parameters.AddWithValue("@Id_Usuario_Registro", entidad.Id_Usuario_Registro);
                cmd.Parameters.AddWithValue("@Fecha_Registro", entidad.Fecha_Registro);
                cmd.Parameters.AddWithValue("@Id_Usuario_Ultima_Modificacion", "0");
                cmd.Parameters.AddWithValue("@Id_Forma_Pago", entidad.Id_Forma_Pago);
                cmd.Parameters.AddWithValue("@Comentario", entidad.Comentario);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) // Si el DataReader tiene filas entonces hacer lo siguiente
                        {
                            Id = Convert.ToInt32(dr["UltimoRegistroAgregado"].ToString());
                        }
                    }
                    conexion.Close();

                    return Id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Editar(Ingreso_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Ingresos SET 
                                        Id_Miembro = @Id_Miembro, 
                                        Id_Descripcion_Ingreso = @Id_Descripcion_Ingreso, 
                                        Id_Moneda = @Id_Moneda, 
                                        Monto = @Monto, 
                                        Valor_Moneda = @Valor_Moneda, 
                                        Id_Usuario_Ultima_Modificacion = @Id_Usuario_Ultima_Modificacion, 
                                        Fecha_Ultima_Modificacion = @Fecha_Ultima_Modificacion, 
                                        Id_Forma_Pago = @Id_Forma_Pago, 
                                        Comentario = @Comentario 

                                        WHERE Id_Ingreso = @Id_Ingreso";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Ingreso", entidad.Id_Ingreso);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Descripcion_Ingreso", entidad.Id_Descripcion_Ingreso);
                cmd.Parameters.AddWithValue("@Id_Moneda", entidad.Id_Moneda);
                cmd.Parameters.AddWithValue("@Monto", entidad.Monto);
                cmd.Parameters.AddWithValue("@Valor_Moneda", entidad.Valor_Moneda);
                cmd.Parameters.AddWithValue("@Id_Usuario_Ultima_Modificacion", entidad.Id_Usuario_Ultima_Modificacion);
                cmd.Parameters.AddWithValue("@Fecha_Ultima_Modificacion", entidad.Fecha_Ultima_Modificacion);
                cmd.Parameters.AddWithValue("@Id_Forma_Pago", entidad.Id_Forma_Pago);
                cmd.Parameters.AddWithValue("@Comentario", entidad.Comentario);
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
                string sentencia = "DELETE FROM Ingresos WHERE Id_Ingreso = @id;";
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
