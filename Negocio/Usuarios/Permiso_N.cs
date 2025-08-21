using Datos.Usuarios;
using Entidades.Usuarios;
using Negocio.Util_N;
using System;
using System.Data;

namespace Negocio.Usuarios
{
    public class Permiso_N
    {
        Permiso_D Permiso_D = new Permiso_D();

        public DataTable Listar(string Id_Rol)
        {
            try
            {
                return Permiso_D.Listar(int.Parse(Id_Rol));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerPermisos_RolFuncionalidad(string Id_Rol, string Nombre_Funcionalidad)
        {
            try
            {
                return Permiso_D.ObtenerPermisos_RolFuncionalidad(int.Parse(Id_Rol), Nombre_Funcionalidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerPermisosOpciones_MasterPage(string Id_Rol)
        {
            try
            {
                return Permiso_D.ObtenerPermisosOpciones_MasterPage(int.Parse(Id_Rol));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListadoFuncionalidadesRol_MasterPage(string Id_Rol)
        {
            try
            {
                return Permiso_D.ListadoFuncionalidadesRol_MasterPage(int.Parse(Id_Rol));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarPlantillaPermisos()
        {
            try
            {
                return Permiso_D.ListarPlantillaPermisos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(DataTable dtInsert)
        {
            try
            {
                return Permiso_D.Agregar(dtInsert);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(DataTable dtUpdate)
        {
            try
            {
                return Permiso_D.Editar(dtUpdate);
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

                return Permiso_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

