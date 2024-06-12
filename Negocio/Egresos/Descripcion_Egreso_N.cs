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
    public class Descripcion_Egreso_N
    {
        Descripcion_Egreso_D Descripcion_Egreso_D = new Descripcion_Egreso_D();

        public DataTable Listar()
        {
            try
            {
                return Descripcion_Egreso_D.Listar();
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
                return Descripcion_Egreso_D.ListaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Descripcion_Egreso_E ObtenerRegistro(string Id)
        {
            try
            {
                return Descripcion_Egreso_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Descripcion_Egreso_E entidad)
        {
            try
            {
                return Descripcion_Egreso_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Descripcion_Egreso_E entidad)
        {
            try
            {
                if (entidad.Id_Descripcion_Egreso == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Descripcion_Egreso.Length == 0)
                {
                    throw new OperationCanceledException("La descripción del egreso no puede estar vacía");
                }

                return Descripcion_Egreso_D.Editar(entidad);
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

                return Descripcion_Egreso_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

