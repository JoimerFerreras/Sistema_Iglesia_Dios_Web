using Datos.ConexionBD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Cuentas_Por_Pagar;

namespace Datos.Cuenta_Por_Pagar
{
    public class Cuenta_Pagar_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable ListarDetalle(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, int Miembro, int Miscelaneo, int Descripcion, int Tipo_Documento)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                // Nota: Es necesario el orden en que está desarrollada esta consulta para que funcione correctamente
                string sentencia = "";
                
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                                    sentencia = @"SELECT 
                                                    CPP.Id_Cuenta_Pagar,
                                                    FORMAT(CPP.Fecha_CP, 'dd/MM/yyyy') AS Fecha_CP,
                                                    D.Nombre AS Descripcion,
                                                    M.Nombres + ' ' + M.Apellidos AS Miembro,
                                                    MC.Descripcion_Miscelaneo AS Miscelaneo,
    
                                                    -- Aplica signo según el tipo de documento
                                                    CASE 
                                                        WHEN CPP.Tipo_Documento IN ('FT', 'ND') THEN CPP.Valor
                                                        WHEN CPP.Tipo_Documento IN ('NC', 'RI') THEN -CPP.Valor
                                                        ELSE 0
                                                    END AS Valor,
    
                                                    CPP.Tipo_Documento,
                                                    FP.Descripcion_Forma_Pago,
                                                    FORMAT(CPP.Fecha_Registro, 'dd/MM/yyyy') AS Fecha_Registro

                                                FROM Cuentas_Por_Pagar CPP
                                                LEFT JOIN Descripciones D ON D.Id_Descripcion = CPP.Id_Descripcion
                                                LEFT JOIN Miembros M ON M.Id_Miembro = CPP.Id_Miembro
                                                LEFT JOIN Formas_Pago FP ON FP.Id_Forma_Pago = CPP.Id_Forma_Pago
                                                LEFT JOIN Miscelaneos MC ON MC.Id_Miscelaneo = CPP.Id_Miscelaneo";
                              
                
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

                //Miscelaneo
                if (Miscelaneo > 0)
                {
                    sentencia += $" AND (CPP.Id_Miscelaneo = @Id_Miscelaneo) ";
                    cmd.Parameters.AddWithValue("@Id_Miscelaneo", Miscelaneo);
                }

                //Descripcion de egreso
                if (Descripcion > 0)
                {
                    sentencia += $" AND (D.Id_Descripcion = @Descripcion) ";
                    cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                }

                //Tipo de documento
                if (Tipo_Documento > 0)
                {
                    sentencia += $" AND (CPP.Tipo_Documento = @Tipo_Documento) ";
                    cmd.Parameters.AddWithValue("@Tipo_Documento", Tipo_Documento);
                }

                sentencia += $@"ORDER BY Id_Cuenta_Pagar ASC";

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

        public DataTable ListarResumen(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, int Miembro, int Miscelaneo, int Descripcion, int Tipo_Documento)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);

                sentencia = @"
                        SELECT 
                        FORMAT(CPP.Fecha_CP, 'dd/MM/yyyy') AS Fecha,
	                    CPP.Id_Cuenta_Pagar,
                        CPP.No_Documento,
	                    CPP.Tipo_Documento,
                        D.Nombre AS Descripcion_Cuenta,

                        -- Debe: valor positivo si aplica, de lo contrario cadena vacía
                        ISNULL(
                            CASE 
                                WHEN CPP.Tipo_Documento IN ('FT', 'ND') THEN CPP.Valor
                            END, ''
                        ) AS Debito,

                        -- Haber: valor negativo si aplica, de lo contrario cadena vacía
                        ISNULL(
                            CASE 
                                WHEN CPP.Tipo_Documento IN ('NC', 'RI') THEN -CPP.Valor
                            END, ''
                        ) AS Credito,

                        -- Balance acumulado con signo
                        SUM(
                            CASE 
                                WHEN CPP.Tipo_Documento IN ('FT', 'ND') THEN CPP.Valor
                                WHEN CPP.Tipo_Documento IN ('NC', 'RI') THEN -CPP.Valor
                                ELSE 0
                            END
                        ) OVER (
                            ORDER BY CPP.Fecha_CP, CPP.Id_Cuenta_Pagar 
                            ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
                        ) AS Balance

                    FROM Cuentas_Por_Pagar CPP
                    LEFT JOIN Descripciones D ON D.Id_Descripcion = CPP.Id_Descripcion ";

                // Tipo de fecha
                sentencia += $@" WHERE ({TipoFecha} BETWEEN @FechaInicial AND @FechaFinal) ";
                cmd.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", FechaFinal);

                // Miembro
                if (Miembro > 0)
                {
                    sentencia += $" AND (CPP.Id_Miembro = @Miembro) ";
                    cmd.Parameters.AddWithValue("@Miembro", Miembro);
                }

                //Miscelaneo
                if (Miscelaneo > 0)
                {
                    sentencia += $" AND (CPP.Id_Miscelaneo = @Id_Miscelaneo) ";
                    cmd.Parameters.AddWithValue("@Id_Miscelaneo", Miscelaneo);
                }

                //Descripcion de egreso
                if (Descripcion > 0)
                {
                    sentencia += $" AND (D.Id_Descripcion = @Descripcion) ";
                    cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                }

                //Tipo de documento
                if (Tipo_Documento > 0)
                {
                    sentencia += $" AND (CPP.Tipo_Documento = @Tipo_Documento) ";
                    cmd.Parameters.AddWithValue("@Tipo_Documento", Tipo_Documento);
                }

                sentencia += $@"ORDER BY CPP.Fecha_CP ASC;";

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
                string sentencia = $@"SELECT 
                                            CPP.Id_Cuenta_Pagar,
                                            CPP.Id_Descripcion,
                                            CPP.Id_Miembro,
                                            CPP.Id_Miscelaneo,
                                            CPP.Fecha_CP,
                                            CPP.Valor,
                                            CPP.Id_Forma_Pago,
                                            CPP.Tipo_Documento,
                                            CPP.No_Documento,
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
                        entidad.Id_Descripcion = int.Parse(row["Id_Descripcion"].ToString());
                        entidad.Id_Miembro = int.Parse(row["Id_Miembro"].ToString());
                        entidad.Id_Miscelaneo = int.Parse(row["Id_Miscelaneo"].ToString());
                        entidad.Fecha_CP = DateTime.Parse(row["Fecha_CP"].ToString());
                        entidad.Valor = float.Parse(row["Valor"].ToString());
                        entidad.Id_Forma_Pago = int.Parse(row["Id_Forma_Pago"].ToString());
                        entidad.Tipo_Documento = row["Tipo_Documento"].ToString();
                        entidad.No_Documento = row["No_Documento"].ToString();
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

        public int Agregar(Cuenta_Pagar_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Cuentas_Por_Pagar(
                                        Id_Descripcion,
                                        Id_Miembro,
                                        Id_Miscelaneo,
                                        Fecha_CP,
                                        Valor,
                                        Id_Forma_Pago,
                                        Tipo_Documento,
                                        No_Documento,
                                        Comentario,
                                        Id_Usuario_Registro,
                                        Fecha_Registro,
                                        Id_Usuario_Ultima_Modificacion)

                                    VALUES(
                                        @Id_Descripcion,
                                        @Id_Miembro,
                                        @Id_Miscelaneo,
                                        @Fecha_CP,
                                        @Valor,
                                        @Id_Forma_Pago,
                                        @Tipo_Documento,
                                        @No_Documento,
                                        @Comentario,
                                        @Id_Usuario_Registro,
                                        @Fecha_Registro,
                                        @Id_Usuario_Ultima_Modificacion);

                                        SELECT SCOPE_IDENTITY() AS UltimoRegistroAgregado;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Descripcion", entidad.Id_Descripcion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Miscelaneo", entidad.Id_Miscelaneo);
                cmd.Parameters.AddWithValue("@Fecha_CP", entidad.Fecha_CP);
                cmd.Parameters.AddWithValue("@Valor", entidad.Valor);
                cmd.Parameters.AddWithValue("@Id_Forma_Pago", entidad.Id_Forma_Pago);
                cmd.Parameters.AddWithValue("@Tipo_Documento", entidad.Tipo_Documento);
                cmd.Parameters.AddWithValue("@No_Documento", entidad.No_Documento);
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
                                        Id_Descripcion = @Id_Descripcion, 
                                        Id_Miembro = @Id_Miembro, 
                                        Id_Miscelaneo = @Id_Miscelaneo, 
                                        Fecha_CP = @Fecha_CP, 
                                        Valor = @Valor, 
                                        Id_Forma_Pago = @Id_Forma_Pago, 
                                        Tipo_Documento = @Tipo_Documento, 
                                        No_Documento = @No_Documento, 
                                        Comentario = @Comentario, 
                                        Id_Usuario_Ultima_Modificacion = @Id_Usuario_Ultima_Modificacion, 
                                        Fecha_Ultima_Modificacion = @Fecha_Ultima_Modificacion 

                                        WHERE Id_Cuenta_Pagar = @Id_Cuenta_Pagar";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Cuenta_Pagar", entidad.Id_Cuenta_Pagar);
                cmd.Parameters.AddWithValue("@Id_Descripcion", entidad.Id_Descripcion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Miscelaneo", entidad.Id_Miscelaneo);
                cmd.Parameters.AddWithValue("@Fecha_CP", entidad.Fecha_CP);
                cmd.Parameters.AddWithValue("@Valor", entidad.Valor);
                cmd.Parameters.AddWithValue("@Id_Forma_Pago", entidad.Id_Forma_Pago);
                cmd.Parameters.AddWithValue("@Tipo_Documento", entidad.Tipo_Documento);
                cmd.Parameters.AddWithValue("@No_Documento", entidad.No_Documento);
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
