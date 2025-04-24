using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Egresos;

namespace Datos.Egresos
{
    public class Egreso_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable Listar(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, int Miembro, int Descripcion, int Miscelaneo)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SELECT Id_Egreso,
                                    FORMAT(Fecha_Egreso, 'dd/MM/yyyy') AS Fecha_Egreso,
                                    D.Nombre AS Descripcion,
                                    M.Nombres + ' ' + M.Apellidos AS Miembro,
                                    MC.Descripcion_Miscelaneo AS Miscelaneo,
                                    Monto,
                                    FORMAT(Fecha_Registro, 'dd/MM/yyyy') AS Fecha_Registro

                                    FROM Egresos E
                                    LEFT JOIN Descripciones D ON D.Id_Descripcion = E.Id_Descripcion
                                    LEFT JOIN Miembros M ON M.Id_Miembro = E.Id_Miembro
                                    LEFT JOIN Miscelaneos MC ON MC.Id_Miscelaneo = E.Id_Miscelaneo";

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
                    sentencia += $" AND (E.Id_Miscelaneo = @Id_Miscelaneo) ";
                    cmd.Parameters.AddWithValue("@Id_Miscelaneo", Miscelaneo);
                }

                //Descripcion
                if (Descripcion > 0)
                {
                    sentencia += $" AND (D.Id_Descripcion = @Id_Descripcion) ";
                    cmd.Parameters.AddWithValue("@Id_Descripcion", Descripcion);
                }

                sentencia += $@"ORDER BY Id_Egreso DESC";

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


        public DataTable ListarResumen(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, int Miembro, int Descripcion, int Miscelaneo)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = @"
                    SELECT 
                        D.Nombre AS Descripcion,
                        SUM(Monto) AS Total_Monto
                    FROM Egresos E
                    LEFT JOIN Descripciones D ON D.Id_Descripcion = E.Id_Descripcion
                    WHERE (" + TipoFecha + " BETWEEN @FechaInicial AND @FechaFinal) ";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                cmd.Parameters.AddWithValue("@FechaFinal", FechaFinal);

                // Filtrar por Miembro
                if (Miembro > 0)
                {
                    sentencia += " AND (E.Id_Miembro = @Miembro) ";
                    cmd.Parameters.AddWithValue("@Miembro", Miembro);
                }

                //Miscelaneo
                if (Miscelaneo > 0)
                {
                    sentencia += $" AND (E.Id_Miscelaneo = @Id_Miscelaneo) ";
                    cmd.Parameters.AddWithValue("@Id_Miscelaneo", Miscelaneo);
                }

                // Filtrar por Descripción de Egreso
                if (Descripcion > 0)
                {
                    sentencia += " AND (D.Id_Descripcion = @Id_Descripcion) ";
                    cmd.Parameters.AddWithValue("@Id_Descripcion", Descripcion);
                }

                sentencia += "GROUP BY D.Nombre ORDER BY D.Nombre";

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


        public Egreso_E ObtenerRegistro(string Id)
        {
            Egreso_E entidad = new Egreso_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT E.Id_Egreso,
                                        E.Id_Miembro,
                                        E.Id_Descripcion,
                                        E.Monto,
                                        E.Fecha_Egreso,
                                        E.Id_Forma_Pago,
                                        E.Comentario,
                                        E.Id_Miscelaneo,
                                        E.Id_Usuario,
                                        U1.Nombre1 + ' ' + U1.Apellido1 AS Nombre_Usuario_Registro,
                                        E.Fecha_Registro,
                                        E.Id_Usuario_Ultima_Modificacion,
                                        U2.Nombre1 + ' ' + U2.Apellido1 AS Nombre_Usuario_Ultima_Modificacion,
                                        E.Fecha_Ultima_Modificacion
                                        FROM Egresos E
                                        LEFT JOIN Usuarios U1 ON U1.Id_Usuario = E.Id_Usuario
                                        LEFT JOIN Usuarios U2 ON U2.Id_Usuario = E.Id_Usuario_Ultima_Modificacion

                                        WHERE E.Id_Egreso = @Id";
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
                        entidad.Id_Egreso = int.Parse(row["Id_Egreso"].ToString());
                        entidad.Id_Miembro = int.Parse(row["Id_Miembro"].ToString());
                        entidad.Id_Descripcion = int.Parse(row["Id_Descripcion"].ToString());
                        entidad.Monto = double.Parse(row["Monto"].ToString());
                        entidad.Fecha_Egreso = DateTime.Parse(row["Fecha_Egreso"].ToString());
                        entidad.Id_Forma_Pago = int.Parse(row["Id_Forma_Pago"].ToString());
                        entidad.Comentario = row["Comentario"].ToString();
                        entidad.Id_Miscelaneo = int.Parse(row["Id_Miscelaneo"].ToString());
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

        public int Agregar(Egreso_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Egresos(
                                        Id_Miembro,
                                        Id_Descripcion,
                                        Monto,
                                        Fecha_Egreso,
                                        Id_Usuario,
                                        Fecha_Registro,
                                        Id_Usuario_Ultima_Modificacion,
                                        Id_Forma_Pago,
                                        Comentario,
                                        Id_Miscelaneo)

                                    VALUES(
                                        @Id_Miembro,
                                        @Id_Descripcion,
                                        @Monto,
                                        @Fecha_Egreso,
                                        @Id_Usuario,
                                        @Fecha_Registro,
                                        @Id_Usuario_Ultima_Modificacion,
                                        @Id_Forma_Pago,
                                        @Comentario,
                                        @Id_Miscelaneo);

                                        SELECT SCOPE_IDENTITY() AS UltimoRegistroAgregado;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Descripcion", entidad.Id_Descripcion);
                cmd.Parameters.AddWithValue("@Monto", entidad.Monto);
                cmd.Parameters.AddWithValue("@Fecha_Egreso", entidad.Fecha_Egreso);
                cmd.Parameters.AddWithValue("@Id_Usuario", entidad.Id_Usuario);
                cmd.Parameters.AddWithValue("@Fecha_Registro", entidad.Fecha_Registro);
                cmd.Parameters.AddWithValue("@Id_Usuario_Ultima_Modificacion", "0");
                cmd.Parameters.AddWithValue("@Id_Forma_Pago", entidad.Id_Forma_Pago);
                cmd.Parameters.AddWithValue("@Comentario", entidad.Comentario);
                cmd.Parameters.AddWithValue("@Id_Miscelaneo", entidad.Id_Miscelaneo);
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

        public bool Editar(Egreso_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Egresos SET 
                                        Id_Miembro = @Id_Miembro, 
                                        Id_Descripcion = @Id_Descripcion, 
                                        Monto = @Monto, 
                                        Fecha_Egreso = @Fecha_Egreso, 
                                        Id_Usuario_Ultima_Modificacion = @Id_Usuario_Ultima_Modificacion, 
                                        Fecha_Ultima_Modificacion = @Fecha_Ultima_Modificacion, 
                                        Id_Forma_Pago = @Id_Forma_Pago, 
                                        Comentario = @Comentario, 
                                        Id_Miscelaneo = @Id_Miscelaneo 

                                        WHERE Id_Egreso = @Id_Egreso";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Egreso", entidad.Id_Egreso);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Descripcion", entidad.Id_Descripcion);
                cmd.Parameters.AddWithValue("@Monto", entidad.Monto);
                cmd.Parameters.AddWithValue("@Fecha_Egreso", entidad.Fecha_Egreso);
                cmd.Parameters.AddWithValue("@Id_Usuario_Ultima_Modificacion", entidad.Id_Usuario_Ultima_Modificacion);
                cmd.Parameters.AddWithValue("@Fecha_Ultima_Modificacion", entidad.Fecha_Ultima_Modificacion);
                cmd.Parameters.AddWithValue("@Id_Forma_Pago", entidad.Id_Forma_Pago);
                cmd.Parameters.AddWithValue("@Comentario", entidad.Comentario);
                cmd.Parameters.AddWithValue("@Id_Miscelaneo", entidad.Id_Miscelaneo);
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
                string sentencia = "DELETE FROM Egresos WHERE Id_Egreso = @id;";
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
