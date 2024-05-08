using Datos.Miembros;
using Entidades.Miembros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Miembros
{
    public class Miembro_Informacion_Familiar2_N
    {
        Miembro_Informacion_Familiar2_D miembro_informacion_familiar2_D = new Miembro_Informacion_Familiar2_D();

        public Miembro_Informacion_Familiar2_E ObtenerRegistro(string Id)
        {
            try
            {
                return miembro_informacion_familiar2_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Miembro_Informacion_Familiar2_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_informacion_familiar2_D.Agregar(entidad);
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

        public bool Guardar(Miembro_Informacion_Familiar2_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_informacion_familiar2_D.Guardar(entidad);
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
