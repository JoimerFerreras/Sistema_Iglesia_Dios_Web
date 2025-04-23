using Datos.Usuarios;
using Entidades.Usuarios;
using Negocio.Util_N;
using System;
using System.Data;

namespace Negocio.Usuarios
{
    public class Usuario_N
    {
        Usuario_D Usuario_D = new Usuario_D();

        public Usuario_E Login(string Usuario)
        {
            try
            {
                return Usuario_D.Login(Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Listar(string Id_Rol)
        {
            try
            {
                return Usuario_D.Listar(int.Parse(Id_Rol));
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
                return Usuario_D.ListaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario_E ObtenerRegistro(string Id)
        {
            try
            {
                return Usuario_D.ObtenerRegistro(Id);
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

                return utilidad.RegistrosExistentesEnTablas(Id_Registro.ToString(), "Id_Rol", "Id_Rol");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Usuario_E entidad)
        {
            try
            {
                return Usuario_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Usuario_E entidad)
        {
            try
            {
                return Usuario_D.Editar(entidad);
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

                return Usuario_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
