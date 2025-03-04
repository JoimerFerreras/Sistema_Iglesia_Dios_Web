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
    public class Miscelaneo_N
    {
        Miscelaneo_D Miscelaneo_D = new Miscelaneo_D();

        public DataTable Listar()
        {
            try
            {
                return Miscelaneo_D.Listar();
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
                return Miscelaneo_D.ListaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Miscelaneo_E ObtenerRegistro(string Id)
        {
            try
            {
                return Miscelaneo_D.ObtenerRegistro(Id);
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

                return utilidad.RegistrosExistentesEnTablas(Id_Registro.ToString(), "Id_Miscelaneo", "Miscelaneos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Miscelaneo_E entidad)
        {
            try
            {
                return Miscelaneo_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Miscelaneo_E entidad)
        {
            try
            {
                if (entidad.Id_Miscelaneo == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Descripcion_Miscelaneo.Length == 0)
                {
                    throw new OperationCanceledException("El nombre del misceláneo no puede estar vacío");
                }

                return Miscelaneo_D.Editar(entidad);
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

                return Miscelaneo_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

