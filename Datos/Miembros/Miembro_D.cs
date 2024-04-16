using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades.Miembros;
using Datos.ConexionBD;

namespace Datos.Miembros
{
    public class Miembro_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

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
