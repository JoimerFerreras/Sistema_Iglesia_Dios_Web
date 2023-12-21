﻿using Datos.Miembros;
using Entidades.Miembros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Miembros
{
    public class Miembro_Informacion_Familiar1_N
    {
        Miembro_Informacion_Familiar1_D miembro_informacion_familiar1_D = new Miembro_Informacion_Familiar1_D();
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
    }
}
