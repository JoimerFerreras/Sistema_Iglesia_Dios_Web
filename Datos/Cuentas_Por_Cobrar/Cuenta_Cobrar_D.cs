using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Cuentas_Por_Cobrar;

namespace Datos.Cuenta_Por_Cobrar
{
    public class Cuenta_Cobrar_D
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
                                                    CPC.Id_Cuenta_Cobrar,
                                                    FORMAT(CPC.Fecha_CC, 'dd/MM/yyyy') AS Fecha_CC,
                                                    D.Nombre AS Descripcion,
                                                    M.Nombres + ' ' + M.Apellidos AS Miembro,
                                                    MC.Descripcion_Miscelaneo AS Miscelaneo,
    
                                                    -- Aplica signo según el tipo de documento
                                                    CASE 
                                                        WHEN CPC.Tipo_Documento IN ('FT', 'ND') THEN CPC.Valor
                                                        WHEN CPC.Tipo_Documento IN ('NC', 'RI') THEN -CPC.Valor
                                                        ELSE 0
                                                    END AS Valor,
    
                                                    CPC.Tipo_Documento,
                                                    FP.Descripcion_Forma_Pago,
                                                    FORMAT(CPC.Fecha_Registro, 'dd/MM/yyyy') AS Fecha_Registro

                                                FROM Cuentas_Por_Cobrar CPC
                                                LEFT JOIN Descripciones D ON D.Id_Descripcion = CPC.Id_Descripcion
                                                LEFT JOIN Miembros M ON M.Id_Miembro = CPC.Id_Miembro
                                                LEFT JOIN Formas_Pago FP ON FP.Id_Forma_Pago = CPC.Id_Forma_Pago
                                                LEFT JOIN Miscelaneos MC ON MC.Id_Miscelaneo = CPC.Id_Miscelaneo";


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
                    sentencia += $" AND (CPC.Id_Miscelaneo = @Id_Miscelaneo) ";
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
                    sentencia += $" AND (CPC.Tipo_Documento = @Tipo_Documento) ";
                    cmd.Parameters.AddWithValue("@Tipo_Documento", Tipo_Documento);
                }

                sentencia += $@"ORDER BY Id_Cuenta_Cobrar ASC";

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
                        FORMAT(CPC.Fecha_CC, 'dd/MM/yyyy') AS Fecha,
	                    CPC.Id_Cuenta_Cobrar,
                        CPC.No_Documento,
	                    CPC.Tipo_Documento,
                        D.Nombre AS Descripcion_Cuenta,

                        -- Debe: valor positivo si aplica, de lo contrario cadena vacía
                        ISNULL(
                            CASE 
                                WHEN CPC.Tipo_Documento IN ('FT', 'ND') THEN CPC.Valor
                            END, ''
                        ) AS Debito,

                        -- Haber: valor negativo si aplica, de lo contrario cadena vacía
                        ISNULL(
                            CASE 
                                WHEN CPC.Tipo_Documento IN ('NC', 'RI') THEN -CPC.Valor
                            END, ''
                        ) AS Credito,

                        -- Balance acumulado con signo
                        SUM(
                            CASE 
                                WHEN CPC.Tipo_Documento IN ('FT', 'ND') THEN CPC.Valor
                                WHEN CPC.Tipo_Documento IN ('NC', 'RI') THEN -CPC.Valor
                                ELSE 0
                            END
                        ) OVER (
                            ORDER BY CPC.Fecha_CC, CPC.Id_Cuenta_Cobrar 
                            ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
                        ) AS Balance

                    FROM Cuentas_Por_Cobrar CPC
                    LEFT JOIN Descripciones D ON D.Id_Descripcion = CPC.Id_Descripcion ";

                // Tipo de fecha
                sentencia += $@" WHERE ({TipoFecha} BETWEEN @FechaInicial AND @FechaFinal) ";
                cmd.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", FechaFinal);

                // Miembro
                if (Miembro > 0)
                {
                    sentencia += $" AND (CPC.Id_Miembro = @Miembro) ";
                    cmd.Parameters.AddWithValue("@Miembro", Miembro);
                }

                //Miscelaneo
                if (Miscelaneo > 0)
                {
                    sentencia += $" AND (CPC.Id_Miscelaneo = @Id_Miscelaneo) ";
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
                    sentencia += $" AND (CPC.Tipo_Documento = @Tipo_Documento) ";
                    cmd.Parameters.AddWithValue("@Tipo_Documento", Tipo_Documento);
                }

                sentencia += $@"ORDER BY CPC.Fecha_CC ASC;";

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


        public Cuenta_Cobrar_E ObtenerRegistro(string Id)
        {
            Cuenta_Cobrar_E entidad = new Cuenta_Cobrar_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT 
                                            CPC.Id_Cuenta_Cobrar,
                                            CPC.Id_Descripcion,
                                            CPC.Id_Miembro,
                                            CPC.Id_Miscelaneo,
                                            CPC.Fecha_CC,
                                            CPC.Valor,
                                            CPC.Id_Forma_Pago,
                                            CPC.Tipo_Documento,
                                            CPC.No_Documento,
                                            CPC.Comentario,
                                            CPC.Id_Usuario,
	                                        U1.Nombre1 + ' ' + U1.Apellido1 AS Nombre_Usuario_Registro,
	                                        CPC.Fecha_Registro,
                                            Id_Usuario_Ultima_Modificacion,
	                                        U2.Nombre1 + ' ' + U2.Apellido1 AS Nombre_Usuario_Ultima_Modificacion,
	                                        CPC.Fecha_Ultima_Modificacion
                                        FROM Cuentas_Por_Cobrar CPC
                                        LEFT JOIN Usuarios U1 ON U1.Id_Usuario = CPC.Id_Usuario
                                        LEFT JOIN Usuarios U2 ON U2.Id_Usuario = CPC.Id_Usuario_Ultima_Modificacion

                                        WHERE CPC.Id_Cuenta_Cobrar = @Id";
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
                        entidad.Id_Cuenta_Cobrar = int.Parse(row["Id_Cuenta_Cobrar"].ToString());
                        entidad.Id_Descripcion = int.Parse(row["Id_Descripcion"].ToString());
                        entidad.Id_Miembro = int.Parse(row["Id_Miembro"].ToString());
                        entidad.Id_Miscelaneo = int.Parse(row["Id_Miscelaneo"].ToString());
                        entidad.Fecha_CC = DateTime.Parse(row["Fecha_CC"].ToString());
                        entidad.Valor = float.Parse(row["Valor"].ToString());
                        entidad.Id_Forma_Pago = int.Parse(row["Id_Forma_Pago"].ToString());
                        entidad.Tipo_Documento = row["Tipo_Documento"].ToString();
                        entidad.No_Documento = row["No_Documento"].ToString();
                        entidad.Comentario = row["Comentario"].ToString();

                        entidad.Id_Usuario = int.Parse(row["Id_Usuario"].ToString());
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

        public int Agregar(Cuenta_Cobrar_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Cuentas_Por_Cobrar(
                                        Id_Descripcion,
                                        Id_Miembro,
                                        Id_Miscelaneo,
                                        Fecha_CC,
                                        Valor,
                                        Id_Forma_Pago,
                                        Tipo_Documento,
                                        No_Documento,
                                        Comentario,
                                        Id_Usuario,
                                        Fecha_Registro,
                                        Id_Usuario_Ultima_Modificacion)

                                    VALUES(
                                        @Id_Descripcion,
                                        @Id_Miembro,
                                        @Id_Miscelaneo,
                                        @Fecha_CC,
                                        @Valor,
                                        @Id_Forma_Pago,
                                        @Tipo_Documento,
                                        @No_Documento,
                                        @Comentario,
                                        @Id_Usuario,
                                        @Fecha_Registro,
                                        @Id_Usuario_Ultima_Modificacion);

                                        SELECT SCOPE_IDENTITY() AS UltimoRegistroAgregado;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Descripcion", entidad.Id_Descripcion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Miscelaneo", entidad.Id_Miscelaneo);
                cmd.Parameters.AddWithValue("@Fecha_CC", entidad.Fecha_CC);
                cmd.Parameters.AddWithValue("@Valor", entidad.Valor);
                cmd.Parameters.AddWithValue("@Id_Forma_Pago", entidad.Id_Forma_Pago);
                cmd.Parameters.AddWithValue("@Tipo_Documento", entidad.Tipo_Documento);
                cmd.Parameters.AddWithValue("@No_Documento", entidad.No_Documento);
                cmd.Parameters.AddWithValue("@Comentario", entidad.Comentario);
                cmd.Parameters.AddWithValue("@Id_Usuario", entidad.Id_Usuario);
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

        public bool Editar(Cuenta_Cobrar_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Cuentas_Por_Cobrar SET 
                                        Id_Descripcion = @Id_Descripcion, 
                                        Id_Miembro = @Id_Miembro, 
                                        Id_Miscelaneo = @Id_Miscelaneo, 
                                        Fecha_CC = @Fecha_CC, 
                                        Valor = @Valor, 
                                        Id_Forma_Pago = @Id_Forma_Pago, 
                                        Tipo_Documento = @Tipo_Documento, 
                                        No_Documento = @No_Documento, 
                                        Comentario = @Comentario, 
                                        Id_Usuario_Ultima_Modificacion = @Id_Usuario_Ultima_Modificacion, 
                                        Fecha_Ultima_Modificacion = @Fecha_Ultima_Modificacion 

                                        WHERE Id_Cuenta_Cobrar = @Id_Cuenta_Cobrar";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Cuenta_Cobrar", entidad.Id_Cuenta_Cobrar);
                cmd.Parameters.AddWithValue("@Id_Descripcion", entidad.Id_Descripcion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Miscelaneo", entidad.Id_Miscelaneo);
                cmd.Parameters.AddWithValue("@Fecha_CC", entidad.Fecha_CC);
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
                string sentencia = "DELETE FROM Cuentas_Por_Cobrar WHERE Id_Cuenta_Cobrar = @id;";
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
