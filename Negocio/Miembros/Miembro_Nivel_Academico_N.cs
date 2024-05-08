﻿using Datos.Miembros;
using Entidades.Miembros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Miembros
{
    public class Miembro_Nivel_Academico_N
    {
        Miembro_Nivel_Academico_D miembro_nivel_academico_D = new Miembro_Nivel_Academico_D();

        public Miembro_Nivel_Academico_E ObtenerRegistro(string Id)
        {
            try
            {
                return miembro_nivel_academico_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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

        public bool Guardar(Miembro_Nivel_Academico_E entidad)
        {
            try
            {
                if (entidad != null)
                {
                    return miembro_nivel_academico_D.Guardar(entidad);
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
