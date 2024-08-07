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
    public class Abono_Cuenta_Pagar_N
    {
        Abono_Cuenta_Pagar_D abono_cuenta_pagar_D = new Abono_Cuenta_Pagar_D();

        public DataTable Listar(string Id_Cuenta_Pagar)
        {
            try
            {
                return abono_cuenta_pagar_D.Listar(int.Parse(Id_Cuenta_Pagar));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Abono_Cuenta_Pagar_E ObtenerRegistro(string Id)
        {
            try
            {
                return abono_cuenta_pagar_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Abono_Cuenta_Pagar_E entidad)
        {
            try
            {
                return abono_cuenta_pagar_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Abono_Cuenta_Pagar_E entidad)
        {
            try
            {
                if (entidad.Id_Abono_CP == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                return abono_cuenta_pagar_D.Editar(entidad);
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

                return abono_cuenta_pagar_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

