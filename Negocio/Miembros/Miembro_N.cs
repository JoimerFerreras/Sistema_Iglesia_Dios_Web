using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos.Miembros;
using Entidades.Miembros;

namespace Negocio.Miembros
{
    public class Miembro_N
    {
        Miembro_D miembro_D = new Miembro_D();
        public bool Agregar(Miembro_E entidad)
        {
            try
            {
                if (entidad.Nombres.Length == 0)
                {
                    throw new OperationCanceledException("El nombre del miembro no puede estar vacío");
                }
                else if (entidad.Apellidos.Length == 0)
                {
                    throw new OperationCanceledException("El apellido del miembro no puede estar vacío");
                }

                return miembro_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
