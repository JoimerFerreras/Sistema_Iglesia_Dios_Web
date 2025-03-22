using Datos.Cuenta_Por_Cobrar;
using Entidades.Cuentas_Por_Cobrar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Cuentas_Por_Cobrar
{
    public class Cuenta_Cobrar_N
    {
        Cuenta_Cobrar_D Cuenta_Cobrar_D = new Cuenta_Cobrar_D();

        public DataTable ListarDetalle(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, string Miembro, string Miscelaneo, string Descripcion, string Tipo_Documento)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "CPC.Fecha_CC";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "CPC.Fecha_Registro";
                }

                FechaFinal = FechaFinal.Date.AddDays(1).AddTicks(-1);

                return Cuenta_Cobrar_D.ListarDetalle(TextoTipoFecha, FechaInicial, FechaFinal, int.Parse(Miembro), int.Parse(Miscelaneo), int.Parse(Descripcion), int.Parse(Tipo_Documento));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarResumen(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, string Miembro, string Miscelaneo, string Descripcion, string Tipo_Documento)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "CPC.Fecha_CC";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "CPC.Fecha_Registro";
                }
                return Cuenta_Cobrar_D.ListarResumen(TextoTipoFecha, FechaInicial, FechaFinal, int.Parse(Miembro), int.Parse(Miscelaneo), int.Parse(Descripcion), int.Parse(Tipo_Documento));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Cuenta_Cobrar_E ObtenerRegistro(string Id)
        {
            try
            {
                return Cuenta_Cobrar_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Agregar(Cuenta_Cobrar_E entidad)
        {
            try
            {
                return Cuenta_Cobrar_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Cuenta_Cobrar_E entidad)
        {
            try
            {
                if (entidad.Id_Cuenta_Cobrar == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Id_Descripcion == 0)
                {
                    throw new OperationCanceledException("La descripcion de la cuenta por cobrar no puede estar vacía");
                }

                return Cuenta_Cobrar_D.Editar(entidad);
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

                return Cuenta_Cobrar_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

