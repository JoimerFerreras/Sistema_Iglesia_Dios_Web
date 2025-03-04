using Datos.Egresos;
using Entidades.Egresos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Egresos
{
    public class Cuenta_Pagar_N
    {
        Cuenta_Pagar_D cuenta_pagar_D = new Cuenta_Pagar_D();

        public DataTable Listar(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, string Beneficiario, string Descripcion_Egreso, string Moneda, string Estado, string Miscelaneo)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "CPP.Fecha";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "CPP.Fecha_Vencimiento";
                }
                else if (TipoFecha == "3")
                {
                    TextoTipoFecha = "CPP.Fecha_Registro";
                }
                return cuenta_pagar_D.Listar(TextoTipoFecha, FechaInicial, FechaFinal, int.Parse(Beneficiario), int.Parse(Descripcion_Egreso), int.Parse(Moneda), int.Parse(Estado), int.Parse(Miscelaneo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cuenta_Pagar_E ObtenerRegistro(string Id)
        {
            try
            {
                return cuenta_pagar_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Agregar(Cuenta_Pagar_E entidad)
        {
            try
            {
                return cuenta_pagar_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Cuenta_Pagar_E entidad)
        {
            try
            {
                if (entidad.Id_Cuenta_Pagar == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Id_Descripcion_Egreso == 0)
                {
                    throw new OperationCanceledException("La descripcion de la cuenta por cobrar no puede estar vacía");
                }

                return cuenta_pagar_D.Editar(entidad);
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

                return cuenta_pagar_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

