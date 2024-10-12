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
    public class Cuenta_Pagar_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable Listar(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, int Beneficiario, int Descripcion_Egreso, int Moneda, int Estado)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                                    sentencia = @"SELECT * 
                                                    FROM (
                                                        SELECT 
                                                            CPP.Id_Cuenta_Pagar,
                                                            CPP.Fecha,
                                                            CPP.Fecha_Vencimiento,
                                                            CPP.Fecha_Registro,
                                                            M.Nombres + ' ' + M.Apellidos AS Beneficiario,
                                                            CPP.Otro_Beneficiario,
                                                            DE.Descripcion_Egreso,
                                                            Mon.Nombre_Moneda AS Moneda,
                                                            CPP.Valor_Moneda,
                                                            CPP.Monto_Total_Pagar,
                                                            CASE 
                                                                WHEN ISNULL(SUM(ACP.Monto_Abono), 0) <= 0 THEN 3
                                                                WHEN ISNULL(SUM(ACP.Monto_Abono), 0) < CPP.Monto_Total_Pagar THEN 2
                                                                WHEN ISNULL(SUM(ACP.Monto_Abono), 0) >= CPP.Monto_Total_Pagar THEN 1
                                                            END AS Estado
                                                        FROM Cuentas_Por_Pagar CPP
                                                        LEFT JOIN Descripciones_Egreso DE ON DE.Id_Descripcion_Egreso = CPP.Id_Descripcion_Egreso
                                                        LEFT JOIN Miembros M ON M.Id_Miembro = CPP.Id_Beneficiario
                                                        LEFT JOIN Monedas Mon ON Mon.Id_Moneda = CPP.Id_Moneda 
                                                        LEFT JOIN Abonos_Cuentas_Pagar ACP ON ACP.Id_Cuenta_Pagar = CPP.Id_Cuenta_Pagar ";
                              
                
                // Tipo de fecha
                sentencia += $@" WHERE ({TipoFecha} BETWEEN @FechaInicial AND @FechaFinal) ";
                cmd.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", FechaFinal);

                // Miembro
                if (Beneficiario > 0)
                {
                    sentencia += $" AND (M.Id_Miembro = @Beneficiario) ";
                    cmd.Parameters.AddWithValue("@Beneficiario", Beneficiario);
                }

                //Descripcion de egreso
                if (Descripcion_Egreso > 0)
                {
                    sentencia += $" AND (DE.Id_Descripcion_Egreso = @Descripcion_Egreso) ";
                    cmd.Parameters.AddWithValue("@Descripcion_Egreso", Descripcion_Egreso);
                }

                //Moneda
                if (Moneda > 0)
                {
                    sentencia += $" AND (Mon.Id_Moneda = @Moneda) ";
                    cmd.Parameters.AddWithValue("@Moneda", Moneda);
                }

                sentencia += @"GROUP BY CPP.Id_Cuenta_Pagar, CPP.Fecha, CPP.Fecha_Vencimiento, CPP.Fecha_Registro, 
                                                                 M.Nombres, M.Apellidos, CPP.Otro_Beneficiario, DE.Descripcion_Egreso, 
                                                                 Mon.Nombre_Moneda, CPP.Valor_Moneda, CPP.Monto_Total_Pagar
                                                    ) AS CuentasConEstado ";

                // Estado
                if (Estado > 0)
                {
                    sentencia += $" WHERE (Estado = @Estado) ";
                    cmd.Parameters.AddWithValue("@Estado", Estado);
                }

                sentencia += $@"ORDER BY Id_Cuenta_Pagar DESC";

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


        public Cuenta_Pagar_E ObtenerRegistro(string Id)
        {
            Cuenta_Pagar_E entidad = new Cuenta_Pagar_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT  CPP.Id_Cuenta_Pagar,
		                                    CPP.Id_Descripcion_Egreso,
		                                    CPP.Monto_Total_Pagar,
		                                    CPP.Id_Moneda,
		                                    CPP.Valor_Moneda,
		                                    CPP.Fecha,
		                                    CPP.No_Factura,
		                                    CPP.Id_Beneficiario,
		                                    CPP.Otro_Beneficiario,
		                                    CPP.Fecha_Vencimiento,
		                                    CPP.Comentario,
		                                    Id_Usuario_Registro,
		                                    U1.Nombre1 + ' ' + U1.Apellido1 AS Nombre_Usuario_Registro,
		                                    CPP.Fecha_Registro,
		                                    Id_Usuario_Ultima_Modificacion,
		                                    U2.Nombre1 + ' ' + U2.Apellido1 AS Nombre_Usuario_Ultima_Modificacion,
		                                    CPP.Fecha_Ultima_Modificacion

                                      FROM Cuentas_Por_Pagar CPP
                                      LEFT JOIN Usuarios U1 ON U1.Id_Usuario = CPP.Id_Usuario_Registro
                                      LEFT JOIN Usuarios U2 ON U2.Id_Usuario = CPP.Id_Usuario_Ultima_Modificacion

                                        WHERE CPP.Id_Cuenta_Pagar = @Id";
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
                        entidad.Id_Cuenta_Pagar = int.Parse(row["Id_Cuenta_Pagar"].ToString());
                        entidad.Id_Descripcion_Egreso = int.Parse(row["Id_Descripcion_Egreso"].ToString());
                        entidad.Monto_Total_Pagar = float.Parse(row["Monto_Total_Pagar"].ToString());
                        entidad.Id_Moneda = int.Parse(row["Id_Moneda"].ToString());
                        entidad.Valor_Moneda = float.Parse(row["Valor_Moneda"].ToString());
                        entidad.Fecha = DateTime.Parse(row["Fecha"].ToString());
                        entidad.No_Factura = row["No_Factura"].ToString();
                        entidad.Id_Beneficiario = int.Parse(row["Id_Beneficiario"].ToString());
                        entidad.Otro_Beneficiario = row["Otro_Beneficiario"].ToString();
                        entidad.Comentario = row["Comentario"].ToString();
                        if (row["Fecha_Vencimiento"] != DBNull.Value)
                        {
                            entidad.Fecha_Vencimiento = DateTime.Parse(row["Fecha_Vencimiento"].ToString());
                        }

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

        public int Agregar(Cuenta_Pagar_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Cuentas_Por_Pagar(
                                        Id_Descripcion_Egreso,
                                        Monto_Total_Pagar,
                                        Id_Moneda,
                                        Valor_Moneda,
                                        Fecha,
                                        Fecha_Vencimiento,
                                        No_Factura,
                                        Id_Beneficiario,
                                        Otro_Beneficiario,
                                        Comentario,
                                        Id_Usuario_Registro,
                                        Fecha_Registro,
                                        Id_Usuario_Ultima_Modificacion)

                                    VALUES(
                                        @Id_Descripcion_Egreso,
                                        @Monto_Total_Pagar,
                                        @Id_Moneda,
                                        @Valor_Moneda,
                                        @Fecha,
                                        @Fecha_Vencimiento,
                                        @No_Factura,
                                        @Id_Beneficiario,
                                        @Otro_Beneficiario,
                                        @Comentario,
                                        @Id_Usuario_Registro,
                                        @Fecha_Registro,
                                        @Id_Usuario_Ultima_Modificacion);

                                        SELECT SCOPE_IDENTITY() AS UltimoRegistroAgregado;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Descripcion_Egreso", entidad.Id_Descripcion_Egreso);
                cmd.Parameters.AddWithValue("@Monto_Total_Pagar", entidad.Monto_Total_Pagar);
                cmd.Parameters.AddWithValue("@Id_Moneda", entidad.Id_Moneda);
                cmd.Parameters.AddWithValue("@Valor_Moneda", entidad.Valor_Moneda);
                cmd.Parameters.AddWithValue("@Fecha", entidad.Fecha);
                cmd.Parameters.AddWithValue("@Fecha_Vencimiento", entidad.Fecha_Vencimiento);
                cmd.Parameters.AddWithValue("@No_Factura", entidad.No_Factura);
                cmd.Parameters.AddWithValue("@Id_Beneficiario", entidad.Id_Beneficiario);
                cmd.Parameters.AddWithValue("@Otro_Beneficiario", entidad.Otro_Beneficiario);
                cmd.Parameters.AddWithValue("@Comentario", entidad.Comentario);
                cmd.Parameters.AddWithValue("@Id_Usuario_Registro", entidad.Id_Usuario_Registro);
                cmd.Parameters.AddWithValue("@Fecha_Registro", entidad.Fecha_Registro);
                cmd.Parameters.AddWithValue("@Id_Usuario_Ultima_Modificacion", "0");
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

        public bool Editar(Cuenta_Pagar_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Cuentas_Por_Pagar SET 
                                        Id_Descripcion_Egreso = @Id_Descripcion_Egreso, 
                                        Monto_Total_Pagar = @Monto_Total_Pagar, 
                                        Id_Moneda = @Id_Moneda, 
                                        Valor_Moneda = @Valor_Moneda, 
                                        Fecha = @Fecha, 
                                        Fecha_Vencimiento = @Fecha_Vencimiento, 
                                        No_Factura = @No_Factura, 
                                        Id_Beneficiario = @Id_Beneficiario, 
                                        Otro_Beneficiario = @Otro_Beneficiario, 
                                        Comentario = @Comentario, 
                                        Id_Usuario_Ultima_Modificacion = @Id_Usuario_Ultima_Modificacion, 
                                        Fecha_Ultima_Modificacion = @Fecha_Ultima_Modificacion 

                                        WHERE Id_Cuenta_Pagar = @Id_Cuenta_Pagar";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Cuenta_Pagar", entidad.Id_Cuenta_Pagar);
                cmd.Parameters.AddWithValue("@Id_Descripcion_Egreso", entidad.Id_Descripcion_Egreso);
                cmd.Parameters.AddWithValue("@Monto_Total_Pagar", entidad.Monto_Total_Pagar);
                cmd.Parameters.AddWithValue("@Id_Moneda", entidad.Id_Moneda);
                cmd.Parameters.AddWithValue("@Valor_Moneda", entidad.Valor_Moneda);
                cmd.Parameters.AddWithValue("@Fecha", entidad.Fecha);
                cmd.Parameters.AddWithValue("@Fecha_Vencimiento", entidad.Fecha_Vencimiento);
                cmd.Parameters.AddWithValue("@No_Factura", entidad.No_Factura);
                cmd.Parameters.AddWithValue("@Id_Beneficiario", entidad.Id_Beneficiario);
                cmd.Parameters.AddWithValue("@Otro_Beneficiario", entidad.Otro_Beneficiario);
                cmd.Parameters.AddWithValue("@Comentario", entidad.Comentario);
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
                string sentencia = "DELETE FROM Cuentas_Por_Pagar WHERE Id_Cuenta_Pagar = @id;";
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
