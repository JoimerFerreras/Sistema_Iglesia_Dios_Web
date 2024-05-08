using Datos.Ingresos;
using Datos.Ministerios;
using Entidades.Ingresos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Ingresos
{
    public class Descripcion_Ingreso_N
    {
        Descripcion_Ingreso_D Descripcion_Ingreso_D = new Descripcion_Ingreso_D();

        public DataTable Listar()
        {
            try
            {
                return Descripcion_Ingreso_D.Listar();
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
                return Descripcion_Ingreso_D.ListaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Descripcion_Ingreso_E ObtenerRegistro(string Id)
        {
            try
            {
                return Descripcion_Ingreso_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Descripcion_Ingreso_E entidad)
        {
            try
            {
                return Descripcion_Ingreso_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Descripcion_Ingreso_E entidad)
        {
            try
            {
                if (entidad.Id_Descripcion_Ingreso == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Descripcion_Ingreso.Length == 0)
                {
                    throw new OperationCanceledException("La descripción del ingreso no puede estar vacía");
                }

                return Descripcion_Ingreso_D.Editar(entidad);
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

                return Descripcion_Ingreso_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

