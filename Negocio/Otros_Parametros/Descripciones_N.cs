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
    public class Descripciones_N
    {
        Descripciones_D Descripciones_D = new Descripciones_D();

        public DataTable Listar(string Tipo_Descripcion)
        {
            try
            {
                return Descripciones_D.Listar(int.Parse(Tipo_Descripcion));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListaCombo(int Tipo_Descripcion)
        {
            try
            {
                // 1 = Ingresos
                // 2 = Egresos
                // 3 = Cuentas por cobrar 
                // 4 = Cuentas por pagar

                return Descripciones_D.ListaCombo(Tipo_Descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Descripciones_E ObtenerRegistro(string Id)
        {
            try
            {
                return Descripciones_D.ObtenerRegistro(Id);
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

                return utilidad.RegistrosExistentesEnTablas(Id_Registro.ToString(), "Id_Descripcion", "Descripciones");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Descripciones_E entidad)
        {
            try
            {
                return Descripciones_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Descripciones_E entidad)
        {
            try
            {
                if (entidad.Id_Descripcion == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Nombre.Length == 0)
                {
                    throw new OperationCanceledException("El nombre de descripción no puede estar vacío");
                }
                if (entidad.Tipo_Descripcion == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar una descripcion");
                }

                return Descripciones_D.Editar(entidad);
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

                return Descripciones_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

