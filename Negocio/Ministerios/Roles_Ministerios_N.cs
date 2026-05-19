using Datos.Ministerios;
using Entidades.Ministerios;
using Negocio.Util_N;
using System;
using System.Data;

namespace Negocio.Ministerios
{
    public class Roles_Ministerios_N
    {
        Roles_Ministerios_D roles_ministerio_D = new Roles_Ministerios_D();

        public DataTable Listar()
        {
            try
            {
                return roles_ministerio_D.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Roles_Ministerios_E ObtenerRegistro(string Id)
        {
            try
            {
                return roles_ministerio_D.ObtenerRegistro(Id);
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

                return utilidad.RegistrosExistentesEnTablas(Id_Registro.ToString(), "Id_Rol_Ministerio", "dbo.Roles_Ministerios");
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
                return roles_ministerio_D.ListaCombo(int.Parse(Id_Registro), TipoConsulta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Roles_Ministerios_E entidad)
        {
            try
            {
                return roles_ministerio_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Roles_Ministerios_E entidad)
        {
            try
            {
                if (entidad.Id_Rol_Ministerio == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Nombre_Rol_Ministerio.Length == 0)
                {
                    throw new OperationCanceledException("El nombre del honor no puede estar vacío");
                }

                return roles_ministerio_D.Editar(entidad);
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

                return roles_ministerio_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
