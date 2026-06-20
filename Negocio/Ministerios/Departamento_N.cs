using Datos.Ministerios;
using Entidades.Ministerios;
using Negocio.Util_N;
using System;
using System.Data;

namespace Negocio.Departamentos
{
    public class Departamento_N
    {
        Departamento_D Departamento_D = new Departamento_D();

        public DataTable Listar()
        {
            try
            {
                return Departamento_D.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Departamento_E ObtenerRegistro(string Id)
        {
            try
            {
                return Departamento_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RegistrosExistentes(int Id_Registro)
        {
            try
            {
                Utilidad_N utilidad = new Utilidad_N();

                return utilidad.RegistrosExistentesEnTablas(Id_Registro.ToString(), "Id_Departamento", "dbo.Departamentos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListaCombo(string Id_Registro, bool TipoConsulta)
        {
            try
            {
                return Departamento_D.ListaCombo(int.Parse(Id_Registro), TipoConsulta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Agregar(Departamento_E entidad)
        {
            try
            {
                return Departamento_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Departamento_E entidad)
        {
            try
            {
                if (entidad.Id_Departamento == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Nombre_Departamento.Length == 0)
                {
                    throw new OperationCanceledException("El nombre del honor no puede estar vacío");
                }

                return Departamento_D.Editar(entidad);
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

                return Departamento_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
