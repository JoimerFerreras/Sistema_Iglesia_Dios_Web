using Datos.Usuarios;
using Entidades.Usuarios;
using Negocio.Util_N;
using System;
using System.Data;

namespace Negocio.Usuarios
{
    public class Rol_N
    {
        Rol_D Rol_D = new Rol_D();

        public DataTable Listar()
        {
            try
            {
                return Rol_D.Listar();
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
                return Rol_D.ListaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Rol_E ObtenerRegistro(string Id)
        {
            try
            {
                return Rol_D.ObtenerRegistro(Id);
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

                return utilidad.RegistrosExistentesEnTablas(Id_Registro.ToString(), "Id_Rol", "dbo.Roles, dbo.Permisos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Agregar(Rol_E entidad)
        {
            try
            {
                return Rol_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Rol_E entidad)
        {
            try
            {
                if (entidad.Id_Rol == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Nombre_Rol.Length == 0)
                {
                    throw new OperationCanceledException("El nombre del rol no puede estar vacío");
                }

                return Rol_D.Editar(entidad);
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

                return Rol_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

