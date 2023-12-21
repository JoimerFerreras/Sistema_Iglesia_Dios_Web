using Datos.Miembros;
using Entidades.Miembros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Miembros
{
    public class Miembro_Nivel_Academico
    {
        Miembro_Nivel_Academico_D miembro_nivel_academico_D = new Miembro_Nivel_Academico_D();
        public bool Agregar(Miembro_Nivel_Academico_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_nivel_academico_D.Agregar(entidad);
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
