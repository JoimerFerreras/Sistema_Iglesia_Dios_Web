using Datos.Ingresos;
using Entidades.Ingresos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Ingresos
{
    public class Ingreso_N
    {
        Ingreso_D Ingreso_D = new Ingreso_D();

        public DataTable Listar(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, string Miembro, string Descripcion_Ingreso, string Miscelaneo)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "Fecha_Ingreso";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "Fecha_Registro";
                }

                FechaFinal = FechaFinal.Date.AddDays(1).AddTicks(-1);

                return Ingreso_D.Listar(TextoTipoFecha, FechaInicial, FechaFinal, int.Parse(Miembro), int.Parse(Descripcion_Ingreso), int.Parse(Miscelaneo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarResumen(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, string Miembro, string Descripcion_Ingreso, string Miscelaneo)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "Fecha_Ingreso";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "Fecha_Registro";
                }
                return Ingreso_D.ListarResumen(TextoTipoFecha, FechaInicial, FechaFinal, int.Parse(Miembro), int.Parse(Descripcion_Ingreso), int.Parse(Miscelaneo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ingreso_E ObtenerRegistro(string Id)
        {
            try
            {
                return Ingreso_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Agregar(Ingreso_E entidad)
        {
            try
            {
                return Ingreso_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Ingreso_E entidad)
        {
            try
            {
                if (entidad.Id_Ingreso == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Id_Descripcion == 0)
                {
                    throw new OperationCanceledException("La descripcion no puede estar vacía");
                }

                return Ingreso_D.Editar(entidad);
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

                return Ingreso_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

