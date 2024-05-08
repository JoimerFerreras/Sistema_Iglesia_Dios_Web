﻿using Datos.Ingresos;
using Datos.Ministerios;
using Entidades.Ingresos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Ingresos
{
    public class Moneda_N
    {
        Moneda_D Moneda_D = new Moneda_D();

        public DataTable Listar()
        {
            try
            {
                return Moneda_D.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListaCombo()
        {
            try
            {
                return Moneda_D.ListaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Moneda_E ObtenerRegistro(string Id)
        {
            try
            {
                return Moneda_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Moneda_E entidad)
        {
            try
            {
                return Moneda_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Moneda_E entidad)
        {
            try
            {
                if (entidad.Id_Moneda == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Nombre_Moneda.Length == 0)
                {
                    throw new OperationCanceledException("El nombre de la moneda no puede estar vacío");
                }

                return Moneda_D.Editar(entidad);
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
                if (Id == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para eliminar");
                }

                return Moneda_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

