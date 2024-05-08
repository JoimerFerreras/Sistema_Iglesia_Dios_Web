using Datos.Miembros;
using Entidades.Miembros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Miembros
{
    public class Miembro_Informacion_Laboral_N
    {
        Miembro_Informacion_Laboral_D miembro_Informacion_Laboral_D = new Miembro_Informacion_Laboral_D();

        public Miembro_Informacion_Laboral_E ObtenerRegistro(string Id)
        {
            try
            {
                return miembro_Informacion_Laboral_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Miembro_Informacion_Laboral_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_Informacion_Laboral_D.Agregar(entidad);
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

        public bool Guardar(Miembro_Informacion_Laboral_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_Informacion_Laboral_D.Guardar(entidad);
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
