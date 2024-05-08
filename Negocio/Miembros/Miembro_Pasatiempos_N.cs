using Datos.Miembros;
using Entidades.Miembros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Miembros
{
    public class Miembro_Pasatiempos_N
    {
        Miembro_Pasatiempos_D miembro_pasatiempos_D = new Miembro_Pasatiempos_D();
        public Miembro_Pasatiempos_E ObtenerRegistro(string Id)
        {
            try
            {
                return miembro_pasatiempos_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Miembro_Pasatiempos_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_pasatiempos_D.Agregar(entidad);
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

        public bool Guardar(Miembro_Pasatiempos_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_pasatiempos_D.Guardar(entidad);
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
