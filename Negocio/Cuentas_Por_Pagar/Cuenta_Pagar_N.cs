using Datos.Cuenta_Por_Pagar;
using Entidades.Cuentas_Por_Pagar;
using System;
using System.Data;

namespace Negocio.Cuentas_Por_Pagar
{
    public class Cuenta_Pagar_N
    {
        Cuenta_Pagar_D Cuenta_Pagar_D = new Cuenta_Pagar_D();

        public DataTable ListarDetalle(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, string Miembro, string Miscelaneo, string Descripcion, string Tipo_Documento)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "CPP.Fecha_CP";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "CPP.Fecha_Registro";
                }

                FechaFinal = FechaFinal.Date.AddDays(1).AddTicks(-1);

                return Cuenta_Pagar_D.ListarDetalle(TextoTipoFecha, FechaInicial, FechaFinal, int.Parse(Miembro), int.Parse(Miscelaneo), int.Parse(Descripcion), int.Parse(Tipo_Documento));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarResumen(string TipoFecha, DateTime FechaInicial, DateTime FechaFinal, string Miembro, string Miscelaneo, string Descripcion, string Tipo_Documento)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "CPP.Fecha_CP";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "CPP.Fecha_Registro";
                }
                return Cuenta_Pagar_D.ListarResumen(TextoTipoFecha, FechaInicial, FechaFinal, int.Parse(Miembro), int.Parse(Miscelaneo), int.Parse(Descripcion), int.Parse(Tipo_Documento));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Cuenta_Pagar_E ObtenerRegistro(string Id)
        {
            try
            {
                return Cuenta_Pagar_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Agregar(Cuenta_Pagar_E entidad)
        {
            try
            {
                return Cuenta_Pagar_D.Agregar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Editar(Cuenta_Pagar_E entidad)
        {
            try
            {
                if (entidad.Id_Cuenta_Pagar == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para editar");
                }
                if (entidad.Id_Descripcion == 0)
                {
                    throw new OperationCanceledException("La descripcion de la cuenta por pagar no puede estar vacía");
                }

                return Cuenta_Pagar_D.Editar(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Eliminar(string Id)
        {
            try
            {
                if (int.Parse(Id) == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para eliminar");
                }

                return Cuenta_Pagar_D.Eliminar(Convert.ToInt32(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

