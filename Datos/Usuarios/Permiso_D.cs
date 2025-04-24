using Datos.ConexionBD;
using System;
using System.Data.SqlClient;
using System.Data;
using Entidades.Usuarios;
using System.Text;

namespace Datos.Usuarios
{
    public class Permiso_D
    {
        #region Declaraciones
        SqlDataReader leer;
        SqlCommand comando = new SqlCommand();
        #endregion

        #region Consultas

        public DataTable Listar(int Id_Rol)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT 
                                        R.Id_Rol,
                                        F.Id_Funcionalidad,
	                                    F.Nombre_Funcionalidad,
	                                    F.Nombre_Archivo,
                                        ISNULL(P.Permiso_Visualizar, 0) AS Permiso_Visualizar,
                                        ISNULL(P.Permiso_Editar, 0) AS Permiso_Editar,
                                        ISNULL(P.Permiso_Eliminar, 0) AS Permiso_Eliminar,
                                        CASE 
                                            WHEN P.Id_Rol IS NOT NULL AND P.Id_Funcionalidad IS NOT NULL THEN 1
                                            ELSE 0
                                        END AS RegistroExistente
                                    FROM 
                                        Roles R
                                    CROSS JOIN 
                                        Funcionalidades F
                                    LEFT JOIN 
                                        Permisos P ON R.Id_Rol = P.Id_Rol AND F.Id_Funcionalidad = P.Id_Funcionalidad
                                    WHERE 
                                        R.Estado = 1 AND F.Estado = 1 AND R.Id_Rol = @Id_Rol
                                    ORDER BY 
                                        R.Id_Rol, F.Id_Funcionalidad;";

                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Rol", Id_Rol);
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

        public DataTable ListarPlantillaPermisos()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = $@"SELECT 
                                        @Id_Rol AS Id_Rol,
                                        F.Id_Funcionalidad,
                                        F.Nombre_Funcionalidad,
                                        F.Nombre_Archivo,
                                        ISNULL(P.Permiso_Visualizar, 0) AS Permiso_Visualizar,
                                        ISNULL(P.Permiso_Editar, 0) AS Permiso_Editar,
                                        ISNULL(P.Permiso_Eliminar, 0) AS Permiso_Eliminar,
                                        CASE 
                                            WHEN P.Id_Rol IS NOT NULL AND P.Id_Funcionalidad IS NOT NULL THEN 1
                                            ELSE 0
                                        END AS RegistroExistente
                                    FROM 
                                        Funcionalidades F
                                    LEFT JOIN 
                                        Permisos P ON P.Id_Funcionalidad = F.Id_Funcionalidad AND P.Id_Rol = @Id_Rol
                                    WHERE 
                                        F.Estado = 1
                                    ORDER BY 
                                        F.Id_Funcionalidad;";

                    SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Rol", 0);
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

        //public Permiso_E ObtenerRegistro(int Id_Rol, int Id_Funcionalidad)
        //{
        //    Permiso_E entidad = new Permiso_E();

        //    using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
        //    {
        //        string sentencia = $@"SELECT Id_Rol, Id_Funcionalidad, Permiso_Visualizar, Permiso_Editar, Permiso_Eliminar FROM Permisos WHERE Id_Rol = @Id AND Id_Funcionalidad = @Id_Funcionalidad";
        //        SqlCommand cmd = new SqlCommand(sentencia, conexion);
        //        cmd.Parameters.AddWithValue("@Id_Rol", Id_Rol);
        //        cmd.Parameters.AddWithValue("@Id_Funcionalidad", Id_Funcionalidad);
        //        cmd.CommandType = CommandType.Text;
        //        try
        //        {
        //            conexion.Open();
        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                DataTable dt = new DataTable();
        //                dt.Load(dr);
        //                DataRow row = dt.Rows[0];
        //                entidad.Id_Rol = int.Parse(row["Id_Rol"].ToString());
        //                entidad.Id_Funcionalidad = int.Parse(row["Id_Funcionalidad"].ToString());
        //                entidad.Permiso_Visualizar = bool.Parse(row["Permiso_Visualizar"].ToString());
        //                entidad.Permiso_Editar = bool.Parse( row["Permiso_Editar"].ToString());
        //                entidad.Permiso_Eliminar = bool.Parse(row["Permiso_Eliminar"].ToString());
        //            }
        //            conexion.Close();
        //            return entidad;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}

        #endregion



        #region Mantenimientos

        public bool Agregar(DataTable dtInsert)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                try
                {
                    conexion.Open();
                    if (dtInsert.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulk = new SqlBulkCopy(conexion))
                        {
                            bulk.DestinationTableName = "Permisos";
                            bulk.ColumnMappings.Add("Id_Rol", "Id_Rol");
                            bulk.ColumnMappings.Add("Id_Funcionalidad", "Id_Funcionalidad");
                            bulk.ColumnMappings.Add("Permiso_Visualizar", "Permiso_Visualizar");
                            bulk.ColumnMappings.Add("Permiso_Editar", "Permiso_Editar");
                            bulk.ColumnMappings.Add("Permiso_Eliminar", "Permiso_Eliminar");

                            bulk.WriteToServer(dtInsert);
                        }
                        conexion.Close();
                        return true;
                    }
                    else
                    {
                        conexion.Close();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool Editar(DataTable dtUpdate)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                StringBuilder sb = new StringBuilder();

                foreach (DataRow row in dtUpdate.Rows)
                {
                    sb.AppendFormat(@"
                UPDATE Permisos
                SET Permiso_Visualizar = {0},
                    Permiso_Editar = {1},
                    Permiso_Eliminar = {2}
                WHERE Id_Rol = {3} AND Id_Funcionalidad = {4};",

                    Convert.ToBoolean(row["Permiso_Visualizar"]) ? 1 : 0,
                    Convert.ToBoolean(row["Permiso_Editar"]) ? 1 : 0,
                    Convert.ToBoolean(row["Permiso_Eliminar"]) ? 1 : 0,
                    row["Id_Rol"],
                    row["Id_Funcionalidad"]);
                }

                if (sb.Length == 0)
                    return false; // No hay nada que ejecutar

                SqlCommand cmd = new SqlCommand(sb.ToString(), conexion);
                cmd.CommandType = CommandType.Text;

                try
                {
                    conexion.Open();
                    int FilasAfectadas = cmd.ExecuteNonQuery();
                    conexion.Close();

                    if (FilasAfectadas > 0)
                        Respuesta = true;

                    return Respuesta;
                }
                catch (Exception ex)
                {
                    throw; // Puedes hacer log aquí si lo deseas
                }
            }
        }


        public bool Eliminar(int Id_Rol)
        {
            bool Respuesta = false;

            using (SqlConnection conexion = new SqlConnection(Conexion_D.CadenaSQL))
            {
                string sentencia = "DELETE FROM Permisos WHERE Id_Rol = @Id_Rol";
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                cmd.Parameters.AddWithValue("@Id_Rol", Id_Rol);
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
