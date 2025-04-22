using Datos.Egresos;
using Entidades.Egresos;
using System;
using System.Data;

namespace Negocio.Egresos
{
    public class Egreso_N
    {
        Egreso_D Egreso_D = new Egreso_D();

        public DataTable Listar(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, string Miembro, string Descripcion_Egreso, string Miscelaneo)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "Fecha_Egreso";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "Fecha_Registro";
                }

                FechaFinal = FechaFinal.Date.AddDays(1).AddTicks(-1);

                return Egreso_D.Listar(TextoTipoFecha, FechaInicial, FechaFinal, int.Parse(Miembro), int.Parse(Descripcion_Egreso), int.Parse(Miscelaneo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarResumen(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, string Miembro, string Descripcion_Egreso, string Miscelaneo)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "Fecha_Egreso";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "Fecha_Registro";
                }
                return Egreso_D.ListarResumen(TextoTipoFecha, FechaInicial, FechaFinal, int.Parse(Miembro), int.Parse(Descripcion_Egreso), int.Parse(Miscelaneo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Egreso_E ObtenerRegistro(string Id)
        {
            try
            {
                return Egreso_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Agregar(Egreso_E entidad)
        {
            try
            {
                return Egreso_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Egreso_E entidad)
        {
            try
            {
                if (entidad.Id_Egreso == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Id_Descripcion == 0)
                {
                    throw new OperationCanceledException("La descripcion no puede estar vacía");
                }

                return Egreso_D.Editar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Eliminar(string Id)
        {
            try
            {
                if (int.Parse(Id) == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para eliminar");
                }

                return Egreso_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

