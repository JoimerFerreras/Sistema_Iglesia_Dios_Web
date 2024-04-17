using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades.Miembros;
using Datos.ConexionBD;
using Entidades.Ministerios;

namespace Datos.Miembros
{
    public class Miembro_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion


        #region Miembros

        public DataTable Consultar(string TipoFecha, DateTime FechaDesde, DateTime FechaHasta, string TextoBusqueda, int Sexo, int EstadoCivil, int Ministerio)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                sentencia = @"SELECT 
                                m.Id_Miembro, 
                                m.Numero_Alternativo_Miembro, 
                                m.Nombres + ' ' + m.Apellidos AS Miembro, 
                                m.Fecha_Nacimiento, 
                                m.Desde_Cuando_Miembro,
                                ISNULL( STUFF(
                                    (SELECT ', ' +  CAST(m_.Nombre_Ministerio AS VARCHAR(50)) AS Ministerios
                                    FROM Miembros_Ministerios m_m 
                                    LEFT JOIN Ministerios m_ ON m_.Id_Ministerio = m_m.Id_Ministerio
                                    WHERE m_m.Id_Miembro = m.Id_Miembro
                                    FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'),
                                    1, 2, ''), '') AS Ministerios   
                            FROM 
                                Miembros m
                            LEFT JOIN 
                                Miembros_Ministerios mm ON m.Id_Miembro = mm.Id_Miembro
                            LEFT JOIN 
                                Ministerios mi ON mm.Id_Ministerio = mi.Id_Ministerio
                            
                            ";

                // Tipo de fecha
                sentencia += $@" WHERE ({TipoFecha} BETWEEN @FechaDesde AND @FechaHasta) ";
                cmd.Parameters.AddWithValue("@FechaDesde", FechaDesde);
                cmd.Parameters.AddWithValue("@FechaHasta", FechaHasta);

                // Sexo
                if (Sexo > 0)
                {
                    sentencia += $" AND (m.Sexo = @Sexo) ";
                    cmd.Parameters.AddWithValue("@Sexo", Sexo);
                }

                //Estado Civil
                if (EstadoCivil > 0)
                {
                    sentencia += $" AND (m.Estado_Civil = @Estado_Civil) ";
                    cmd.Parameters.AddWithValue("@Estado_Civil", EstadoCivil);
                }

                //Ministerio
                if (Ministerio > 0)
                {
                    sentencia += $" AND (mi.Id_Ministerio = @Id_Ministerio) ";
                    cmd.Parameters.AddWithValue("@Id_Ministerio", Ministerio);
                }

                // Texto de busqueda (Nombre o Matricula)
                if (TextoBusqueda.Length > 0)
                {
                    sentencia += $" AND ";

                    char Delimitador = ' '; // Delimitador de espacio
                    string[] Palabras; // Palabras en la que se dividirá el registro

                    Palabras = TextoBusqueda.Split(Delimitador);
                    for (int i = 0; i < Palabras.Length; i++)
                    {
                        if (i > 0)
                        {
                            sentencia += " OR ";
                        }
                        sentencia += $@"(m.Nombres LIKE '%' + @Palabra{i} + '%' OR m.Apellidos LIKE '%' + @Palabra{i} + '%' OR m.Nombre_Pila LIKE '%' + @Palabra{i} + '%' OR m.Id_Miembro LIKE '%' + @Palabra{i} + '%' )";
                        cmd.Parameters.AddWithValue($"@Palabra{i}", Palabras[i]);
                    }
                }

                sentencia += $@"GROUP BY 
                                m.Id_Miembro, 
                                m.Numero_Alternativo_Miembro, 
                                m.Nombres, 
                                m.Apellidos, 
                                m.Fecha_Nacimiento, 
                                m.Desde_Cuando_Miembro 

                                ORDER BY Miembro ASC";

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

        public Miembro_E ObtenerRegistro(string Id)
        {
            Miembro_E entidad = new Miembro_E();

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT 
                                        Id_Miembro,
		                                Nombres,
		                                Apellidos,
		                                Nombre_Pila,
		                                Sexo,
		                                Fecha_Nacimiento,
		                                Estado_Civil,
		                                Tiene_Hijos,
		                                Email,
		                                Celular,
		                                Sector,
		                                Barrio_Residencial,
		                                Calle,
		                                Numero_Casa,
		                                Es_Miembro,
		                                Desde_Cuando_Miembro,
		                                Pertenece_Ministerio,
		                                Le_Gustaria_Pertenecer_Ministerio,
		                                Numero_Alternativo_Miembro,
		                                Rol_Miembro,
		                                Otro_Rol,
		                                Nombre_Diacono,
		                                Nombre_Lider_Ministerio,
		                                Comentarios_Diacono_Lider_Ministerio,
		                                Revisado_Por,
		                                Autorizado_Por
                                        FROM Miembros 

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
                        entidad.Nombres = row["Nombres"].ToString();
                        entidad.Apellidos = row["Apellidos"].ToString();
                        entidad.Nombre_Pila = row["Nombre_Pila"].ToString();
                        entidad.Sexo = int.Parse(row["Sexo"].ToString());
                        entidad.Fecha_Nacimiento = DateTime.Parse(row["Fecha_Nacimiento"].ToString());
                        entidad.Estado_Civil = int.Parse(row["Estado_Civil"].ToString());
                        entidad.Tiene_Hijos = row["Tiene_Hijos"].ToString() == "True" ? true : false;
                        entidad.Email = row["Email"].ToString();
                        entidad.Celular = row["Celular"].ToString();
                        entidad.Sector = row["Sector"].ToString();
                        entidad.Barrio_Residencial = row["Barrio_Residencial"].ToString();
                        entidad.Calle = row["Calle"].ToString();
                        entidad.Numero_Casa = row["Numero_Casa"].ToString();
                        entidad.Es_Miembro = row["Es_Miembro"].ToString() == "True" ? true : false;
                        entidad.Desde_Cuando_Miembro = DateTime.Parse(row["Desde_Cuando_Miembro"].ToString());
                        entidad.Pertenece_Ministerio = row["Pertenece_Ministerio"].ToString() == "True" ? true : false;
                        entidad.Le_Gustaria_Pertenecer_Ministerio = row["Le_Gustaria_Pertenecer_Ministerio"].ToString() == "True" ? true : false;
                        entidad.Numero_Alternativo_Miembro = int.Parse(row["Numero_Alternativo_Miembro"].ToString());
                        entidad.Rol_Miembro = int.Parse(row["Rol_Miembro"].ToString());
                        entidad.Otro_Rol = row["Otro_Rol"].ToString();
                        entidad.Nombre_Diacono = row["Nombre_Diacono"].ToString();
                        entidad.Nombre_Lider_Ministerio = row["Nombre_Lider_Ministerio"].ToString();
                        entidad.Comentarios_Diacono_Lider_Ministerio = row["Comentarios_Diacono_Lider_Ministerio"].ToString();
                        entidad.Revisado_Por = row["Revisado_Por"].ToString();
                        entidad.Autorizado_Por = row["Autorizado_Por"].ToString();                    }
                    conexion.Close();
                    return entidad;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public int Agregar(Miembro_E entidad)
        {
            int Id = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Miembros(
                                    Nombres, 
                                    Apellidos,
                                    Nombre_Pila,
                                    Sexo,
                                    Fecha_Nacimiento,
                                    Estado_Civil,
                                    Tiene_Hijos,
                                    Email,
                                    Celular,
                                    Sector,
                                    Barrio_Residencial,
                                    Calle,
                                    Numero_Casa,
                                    Es_Miembro,
                                    Desde_Cuando_Miembro,
                                    Pertenece_Ministerio,
                                    Le_Gustaria_Pertenecer_Ministerio,
                                    Numero_Alternativo_Miembro,
                                    
                                    Rol_Miembro,
                                    Otro_Rol,
                                    Nombre_Diacono,
                                    Nombre_Lider_Ministerio,
                                    Comentarios_Diacono_Lider_Ministerio,
                                    Revisado_Por,
                                    Autorizado_Por)

                                    VALUES( 
                                    @Nombres,
                                    @Apellidos,
                                    @Nombre_Pila,
                                    @Sexo,
                                    @Fecha_Nacimiento,
                                    @Estado_Civil,
                                    @Tiene_Hijos,
                                    @Email,
                                    @Celular,
                                    @Sector,
                                    @Barrio_Residencial,
                                    @Calle,
                                    @Numero_Casa,
                                    @Es_Miembro,
                                    @Desde_Cuando_Miembro,
                                    @Pertenece_Ministerio,
                                    @Le_Gustaria_Pertenecer_Ministerio,
                                    @Numero_Alternativo_Miembro,
                                    
                                    @Rol_Miembro,
                                    @Otro_Rol,
                                    @Nombre_Diacono,
                                    @Nombre_Lider_Ministerio,
                                    @Comentarios_Diacono_Lider_Ministerio,
                                    @Revisado_Por,
                                    @Autorizado_Por);

                                    SELECT SCOPE_IDENTITY() AS UltimoRegistroAgregado;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Nombres", entidad.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", entidad.Apellidos);
                cmd.Parameters.AddWithValue("@Nombre_Pila", entidad.Nombre_Pila);
                cmd.Parameters.AddWithValue("@Sexo", entidad.Sexo);
                if (entidad.Fecha_Nacimiento == null)
                {
                    cmd.Parameters.AddWithValue("@Fecha_Nacimiento", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Fecha_Nacimiento", entidad.Fecha_Nacimiento);
                }
                cmd.Parameters.AddWithValue("@Estado_Civil", entidad.Estado_Civil);
                cmd.Parameters.AddWithValue("@Tiene_Hijos", entidad.Tiene_Hijos);
                cmd.Parameters.AddWithValue("@Email", entidad.Email);
                cmd.Parameters.AddWithValue("@Celular", entidad.Celular);
                cmd.Parameters.AddWithValue("@Sector", entidad.Sector);
                cmd.Parameters.AddWithValue("@Barrio_Residencial", entidad.Barrio_Residencial);
                cmd.Parameters.AddWithValue("@Calle", entidad.Calle);
                cmd.Parameters.AddWithValue("@Numero_Casa", entidad.Numero_Casa);
                cmd.Parameters.AddWithValue("@Es_Miembro", entidad.Es_Miembro);
                if (entidad.Desde_Cuando_Miembro == null)
                {
                    cmd.Parameters.AddWithValue("@Desde_Cuando_Miembro", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Desde_Cuando_Miembro", entidad.Desde_Cuando_Miembro);
                }
                cmd.Parameters.AddWithValue("@Pertenece_Ministerio", entidad.Pertenece_Ministerio);
                cmd.Parameters.AddWithValue("@Le_Gustaria_Pertenecer_Ministerio", entidad.Le_Gustaria_Pertenecer_Ministerio);
                cmd.Parameters.AddWithValue("@Numero_Alternativo_Miembro", entidad.Numero_Alternativo_Miembro);
                //cmd.Parameters.AddWithValue("@Ministerio_Al_Que_Pertenece", entidad.Ministerio_Al_Que_Pertenece);
                cmd.Parameters.AddWithValue("@Rol_Miembro", entidad.Rol_Miembro);
                cmd.Parameters.AddWithValue("@Otro_Rol", entidad.Otro_Rol);
                cmd.Parameters.AddWithValue("@Nombre_Diacono", entidad.Nombre_Diacono);
                cmd.Parameters.AddWithValue("@Nombre_Lider_Ministerio", entidad.Nombre_Lider_Ministerio);
                cmd.Parameters.AddWithValue("@Comentarios_Diacono_Lider_Ministerio", entidad.Comentarios_Diacono_Lider_Ministerio);
                cmd.Parameters.AddWithValue("@Revisado_Por", entidad.Revisado_Por);
                cmd.Parameters.AddWithValue("@Autorizado_Por", entidad.Autorizado_Por);
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

        public bool Guardar(Miembro_E entidad)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"UPDATE Miembros SET
                                    Nombres = @Nombres, 
                                    Apellidos = @Apellidos,
                                    Nombre_Pila = @Nombre_Pila,
                                    Sexo = @Sexo,
                                    Fecha_Nacimiento = @Fecha_Nacimiento,
                                    Estado_Civil = @Estado_Civil,
                                    Tiene_Hijos = @Tiene_Hijos,
                                    Email = @Email,
                                    Celular = @Celular,
                                    Sector = @Sector,
                                    Barrio_Residencial = @Barrio_Residencial,
                                    Calle = @Calle,
                                    Numero_Casa = @Numero_Casa,
                                    Es_Miembro = @Es_Miembro,
                                    Desde_Cuando_Miembro = @Desde_Cuando_Miembro,
                                    Pertenece_Ministerio = @Pertenece_Ministerio,
                                    Le_Gustaria_Pertenecer_Ministerio = @Le_Gustaria_Pertenecer_Ministerio,
                                    Numero_Alternativo_Miembro = @Numero_Alternativo_Miembro,
                                    
                                    Rol_Miembro = @Rol_Miembro,
                                    Otro_Rol = @Otro_Rol,
                                    Nombre_Diacono = @Nombre_Diacono,
                                    Nombre_Lider_Ministerio = @Nombre_Lider_Ministerio,
                                    Comentarios_Diacono_Lider_Ministerio = @Comentarios_Diacono_Lider_Ministerio,
                                    Revisado_Por = @Revisado_Por,
                                    Autorizado_Por = @Autorizado_Por 

                                    WHERE Id_Miembro = @Id_Miembro";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", entidad.Id_Miembro);
                cmd.Parameters.AddWithValue("@Nombres", entidad.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", entidad.Apellidos);
                cmd.Parameters.AddWithValue("@Nombre_Pila", entidad.Nombre_Pila);
                cmd.Parameters.AddWithValue("@Sexo", entidad.Sexo);
                if (entidad.Fecha_Nacimiento == null)
                {
                    cmd.Parameters.AddWithValue("@Fecha_Nacimiento", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Fecha_Nacimiento", entidad.Fecha_Nacimiento);
                }
                cmd.Parameters.AddWithValue("@Estado_Civil", entidad.Estado_Civil);
                cmd.Parameters.AddWithValue("@Tiene_Hijos", entidad.Tiene_Hijos);
                cmd.Parameters.AddWithValue("@Email", entidad.Email);
                cmd.Parameters.AddWithValue("@Celular", entidad.Celular);
                cmd.Parameters.AddWithValue("@Sector", entidad.Sector);
                cmd.Parameters.AddWithValue("@Barrio_Residencial", entidad.Barrio_Residencial);
                cmd.Parameters.AddWithValue("@Calle", entidad.Calle);
                cmd.Parameters.AddWithValue("@Numero_Casa", entidad.Numero_Casa);
                cmd.Parameters.AddWithValue("@Es_Miembro", entidad.Es_Miembro);
                if (entidad.Desde_Cuando_Miembro == null)
                {
                    cmd.Parameters.AddWithValue("@Desde_Cuando_Miembro", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Desde_Cuando_Miembro", entidad.Desde_Cuando_Miembro);
                }
                cmd.Parameters.AddWithValue("@Pertenece_Ministerio", entidad.Pertenece_Ministerio);
                cmd.Parameters.AddWithValue("@Le_Gustaria_Pertenecer_Ministerio", entidad.Le_Gustaria_Pertenecer_Ministerio);
                cmd.Parameters.AddWithValue("@Numero_Alternativo_Miembro", entidad.Numero_Alternativo_Miembro);
                //cmd.Parameters.AddWithValue("@Ministerio_Al_Que_Pertenece", entidad.Ministerio_Al_Que_Pertenece);
                cmd.Parameters.AddWithValue("@Rol_Miembro", entidad.Rol_Miembro);
                cmd.Parameters.AddWithValue("@Otro_Rol", entidad.Otro_Rol);
                cmd.Parameters.AddWithValue("@Nombre_Diacono", entidad.Nombre_Diacono);
                cmd.Parameters.AddWithValue("@Nombre_Lider_Ministerio", entidad.Nombre_Lider_Ministerio);
                cmd.Parameters.AddWithValue("@Comentarios_Diacono_Lider_Ministerio", entidad.Comentarios_Diacono_Lider_Ministerio);
                cmd.Parameters.AddWithValue("@Revisado_Por", entidad.Revisado_Por);
                cmd.Parameters.AddWithValue("@Autorizado_Por", entidad.Autorizado_Por);
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



        #region Miembros_Ministerios
        // Ministerios de los miembros
        public bool AgregarMiembroMinisterio(int Id_Miembro, int Id_Ministerio)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"INSERT INTO Miembros_Ministerios(
                                    Id_Miembro,
                                    Id_Ministerio)

                                   VALUES(
                                    @Id_Miembro,
                                    @Id_Ministerio);";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Ministerio", Id_Ministerio);
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

        public bool EliminarMiembroMinisterio(int Id_Miembro, int Id_Ministerio)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"DELETE FROM Miembros_Ministerios WHERE Id_Miembro = @Id_Miembro AND Id_Ministerio = @Id_Ministerio;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Miembro", Id_Miembro);
                cmd.Parameters.AddWithValue("@Id_Ministerio", Id_Ministerio);
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
