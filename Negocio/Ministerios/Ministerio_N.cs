using Datos.Ministerios;
using Entidades.Ministerios;
using System;
using System.Data;

namespace Negocio.Ministerios
{
    public class Ministerio_N
    {
        Ministerio_D ministerio_D = new Ministerio_D();

        public DataTable Listar()
        {
            try
            {
                return ministerio_D.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Ministerio_E ObtenerRegistro(string Id)
        {
            try
            {
                return ministerio_D.ObtenerRegistro(Id);
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
                bool CantidadRegistros = false;
                DataTable dt = ministerio_D.RegistrosExistentes(Id_Registro);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (Convert.ToInt32(row["CantidadRegistros"]) > 0)
                    {
                        CantidadRegistros = true;
                    }
                }

                return CantidadRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListaCombo(string Id_Registro, bool TipoConsulta)
        {
            try
            {
                return ministerio_D.ListaCombo(int.Parse(Id_Registro), TipoConsulta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Ministerio_E entidad)
        {
            try
            {
                return ministerio_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Ministerio_E entidad)
        {
            try
            {
                if (entidad.Id_Ministerio == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Nombre_Ministerio.Length == 0)
                {
                    throw new OperationCanceledException("El nombre del honor no puede estar vacío");
                }

                return ministerio_D.Editar(entidad);
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

                return ministerio_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
