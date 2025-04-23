using Datos.Usuarios;
using Entidades.Usuarios;
using Negocio.Util_N;
using System;
using System.Data;

namespace Negocio.Usuarios
{
    public class Log_Usuario_Acceso_N
    {
        Log_Usuario_Acceso_D Log_Usuario_Acceso_D = new Log_Usuario_Acceso_D();

        public DataTable Listar()
        {
            try
            {
                return Log_Usuario_Acceso_D.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Log_Usuario_Acceso_E ObtenerRegistro(string Id)
        {
            try
            {
                return Log_Usuario_Acceso_D.ObtenerUbicacionLog(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Log_Usuario_Acceso_E entidad)
        {
            try
            {
                return Log_Usuario_Acceso_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Eliminar(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para eliminar");
                }

                return Log_Usuario_Acceso_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

