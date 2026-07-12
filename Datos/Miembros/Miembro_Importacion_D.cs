using Datos.ConexionBD;
using Entidades.Miembros;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Miembros
{
    public class Miembro_Importacion_D
    {
        public int Importar(Importacion_Miembros_E importacion)
        {
            if (importacion == null)
                throw new ArgumentNullException("importacion");

            using (SqlConnection conexion =
                new SqlConnection(Conexion_D.CadenaSQL))
            using (SqlCommand cmd =
                new SqlCommand(
                    "dbo.SP_Importar_Miembros_Masivo",
                    conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                AgregarParametroTabla(
                    cmd,
                    "@Miembros",
                    "dbo.TVP_Importacion_Miembros",
                    importacion.Miembros);

                AgregarParametroTabla(
                    cmd,
                    "@InformacionFamiliar1",
                    "dbo.TVP_Importacion_Miembros_Informacion_Familiar_1",
                    importacion.Informacion_Familiar_1);

                AgregarParametroTabla(
                    cmd,
                    "@InformacionFamiliar2",
                    "dbo.TVP_Importacion_Miembros_Informacion_Familiar_2",
                    importacion.Informacion_Familiar_2);

                AgregarParametroTabla(
                    cmd,
                    "@InformacionLaboral",
                    "dbo.TVP_Importacion_Miembros_Informacion_Laboral",
                    importacion.Informacion_Laboral);

                AgregarParametroTabla(
                    cmd,
                    "@NivelAcademico",
                    "dbo.TVP_Importacion_Miembros_Nivel_Academico",
                    importacion.Nivel_Academico);

                AgregarParametroTabla(
                    cmd,
                    "@Pasatiempos",
                    "dbo.TVP_Importacion_Miembros_Pasatiempos",
                    importacion.Pasatiempos);

                conexion.Open();

                object respuesta = cmd.ExecuteScalar();

                if (respuesta == null || respuesta == DBNull.Value)
                    return 0;

                return Convert.ToInt32(respuesta);
            }
        }

        private void AgregarParametroTabla(
            SqlCommand cmd,
            string nombreParametro,
            string nombreTipoSQL,
            DataTable datos)
        {
            if (datos == null)
            {
                throw new ArgumentNullException(
                    nombreParametro,
                    "El DataTable no puede ser nulo.");
            }

            SqlParameter parametro = cmd.Parameters.Add(
                nombreParametro,
                SqlDbType.Structured);

            parametro.TypeName = nombreTipoSQL;
            parametro.Value = datos;
        }
    }
}
