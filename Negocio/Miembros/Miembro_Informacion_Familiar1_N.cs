using Datos.Miembros;
using Entidades.Miembros;
using System;

namespace Negocio.Miembros
{
    public class Miembro_Informacion_Familiar1_N
    {
        Miembro_Informacion_Familiar1_D miembro_informacion_familiar1_D = new Miembro_Informacion_Familiar1_D();


        public Miembro_Informacion_Familiar1_E ObtenerRegistro(string Id)
        {
            try
            {
                return miembro_informacion_familiar1_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool Agregar(Miembro_Informacion_Familiar1_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_informacion_familiar1_D.Agregar(entidad);
                }else 
                { 
                    return false; 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Guardar(Miembro_Informacion_Familiar1_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_informacion_familiar1_D.Guardar(entidad);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
