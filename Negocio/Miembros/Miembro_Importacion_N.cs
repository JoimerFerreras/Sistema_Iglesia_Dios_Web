using Datos.Miembros;
using Entidades.Miembros;
using System;

namespace Negocio.Miembros
{
    public class Miembro_Importacion_N
    {
        private readonly Miembro_Importacion_D miembro_Importacion_D =
            new Miembro_Importacion_D();

        public int Importar(Importacion_Miembros_E importacion)
        {
            if (importacion == null)
                throw new ArgumentNullException("importacion");

            if (importacion.Errores == null)
                throw new Exception(
                    "No fue posible validar los errores de la importación.");

            if (importacion.Errores.Count > 0)
                throw new Exception(
                    "La importación contiene errores de validación.");

            if (importacion.Miembros == null ||
                importacion.Miembros.Rows.Count == 0)
            {
                throw new Exception(
                    "No existen miembros para importar.");
            }

            int cantidad = importacion.Miembros.Rows.Count;

            ValidarCantidad(
                importacion.Informacion_Familiar_1,
                cantidad,
                "Información Familiar 1");

            ValidarCantidad(
                importacion.Informacion_Familiar_2,
                cantidad,
                "Información Familiar 2");

            ValidarCantidad(
                importacion.Informacion_Laboral,
                cantidad,
                "Información Laboral");

            ValidarCantidad(
                importacion.Nivel_Academico,
                cantidad,
                "Nivel Académico");

            ValidarCantidad(
                importacion.Pasatiempos,
                cantidad,
                "Pasatiempos");

            return miembro_Importacion_D.Importar(importacion);
        }

        private void ValidarCantidad(
            System.Data.DataTable tabla,
            int cantidadEsperada,
            string nombreTabla)
        {
            if (tabla == null)
            {
                throw new Exception(
                    "El DataTable de " + nombreTabla +
                    " no fue creado.");
            }

            if (tabla.Rows.Count != cantidadEsperada)
            {
                throw new Exception(
                    "La cantidad de filas de " + nombreTabla +
                    " no coincide con la cantidad de miembros.");
            }
        }
    }
}
