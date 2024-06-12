using Datos.Otros_Parametros;
using Entidades.Otros_Parametros;
using Negocio.Util_N;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Otros_Parametros
{
    public class Forma_Pago_N
    {
        Forma_Pago_D Forma_Pago_D = new Forma_Pago_D();

        public DataTable Listar()
        {
            try
            {
                return Forma_Pago_D.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListaCombo()
        {
            try
            {
                return Forma_Pago_D.ListaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Forma_Pago_E ObtenerRegistro(string Id)
        {
            try
            {
                return Forma_Pago_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RegistrosExistentes(int Id_Registro)
        {
            try
            {
                Utilidad_N utilidad = new Utilidad_N();

                return utilidad.RegistrosExistentesEnTablas(Id_Registro.ToString(), "Id_Forma_Pago", "Formas_Pago");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Forma_Pago_E entidad)
        {
            try
            {
                return Forma_Pago_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Forma_Pago_E entidad)
        {
            try
            {
                if (entidad.Id_Forma_Pago == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Descripcion_Forma_Pago.Length == 0)
                {
                    throw new OperationCanceledException("El nombre de la moneda no puede estar vacío");
                }

                return Forma_Pago_D.Editar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Eliminar(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para eliminar");
                }

                return Forma_Pago_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

