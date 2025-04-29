using Datos.Usuarios;
using Entidades.Usuarios;
using Negocio.Util_N;
using System;
using System.Data;

namespace Negocio.Usuarios
{
    public class Notificacion_N
    {
        Notificacion_D Notificacion_D = new Notificacion_D();

        public DataTable Listar(int Id_Usuario)
        {
            try
            {
                return Notificacion_D.Listar(Id_Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Agregar(Notificacion_E entidad)
        {
            try
            {
                return Notificacion_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool MarcarComoVista(int Id_Notificacion)
        {
            try
            {
                return Notificacion_D.MarcarComoVista(Id_Notificacion);
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
                return Notificacion_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarTodo(int Id)
        {
            try
            {
                return Notificacion_D.EliminarTodo(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

