using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos.Miembros;
using Entidades.Miembros;
using Datos.Ministerios;
using Entidades.Ministerios;
using Negocio.Ministerios;

namespace Negocio.Miembros
{
    public class Miembro_N
    {
        Miembro_D miembro_D = new Miembro_D();

        public DataTable Consultar(string TipoFecha, DateTime FechaDesde, DateTime FechaHasta, string TextoBusqueda, string Sexo, string EstadoCivil, string Ministerio)
        {
            try
            {
                // Tipo de fecha
                string TextoTipoFecha = "";
                if (TipoFecha == "1")
                {
                    TextoTipoFecha = "Fecha_Nacimiento";
                }
                else if (TipoFecha == "2")
                {
                    TextoTipoFecha = "Desde_Cuando_Miembro";
                }

                return miembro_D.Consultar(TextoTipoFecha, FechaDesde, FechaHasta, TextoBusqueda, int.Parse(Sexo), int.Parse(EstadoCivil), int.Parse(Ministerio));
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
                return miembro_D.ListaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Miembro_E ObtenerRegistro(string Id)
        {
            try
            {
                return miembro_D.ObtenerRegistro(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Agregar(
            Miembro_E miembro_E,
            Miembro_Informacion_Familiar1_E informacion_familiar1_E,
            Miembro_Informacion_Familiar2_E informacion_familiar2_E,
            Miembro_Informacion_Laboral_E informacion_laboral_E,
            Miembro_Nivel_Academico_E nivel_academico_E,
            Miembro_Pasatiempos_E pasatiempos_E)
        {
            string MensajeSalida = "";
            try
            {
                // Validaciones
                if (miembro_E.Nombres.Length == 0)
                {
                    MensajeSalida = "El nombre del miembro no puede estar vacío";
                    return MensajeSalida;
                }
                else if (miembro_E.Apellidos.Length == 0)
                {
                    MensajeSalida = "El apellido del miembro no puede estar vacío";
                    return MensajeSalida;
                }

                // Agregacion del miembro
                int Id = miembro_D.Agregar(miembro_E);

                // Agregación de los demas datos
                Miembro_Informacion_Familiar1_N informacion_familia1_N = new Miembro_Informacion_Familiar1_N();
                informacion_familiar1_E.Id_Miembro = Id;
                informacion_familia1_N.Agregar(informacion_familiar1_E);

                Miembro_Informacion_Familiar2_N informacion_familiar2_N = new Miembro_Informacion_Familiar2_N();
                informacion_familiar2_E.Id_Miembro = Id;
                informacion_familiar2_N.Agregar(informacion_familiar2_E);

                Miembro_Informacion_Laboral_N informacion_laboral_N = new Miembro_Informacion_Laboral_N();
                informacion_laboral_E.Id_Miembro = Id;
                informacion_laboral_N.Agregar(informacion_laboral_E);

                Miembro_Nivel_Academico_N nivel_academico_N = new Miembro_Nivel_Academico_N();
                nivel_academico_E.Id_Miembro = Id;
                nivel_academico_N.Agregar(nivel_academico_E);

                Miembro_Pasatiempos_N pasatiempos_N = new Miembro_Pasatiempos_N();
                pasatiempos_E.Id_Miembro = Id;
                pasatiempos_N.Agregar(pasatiempos_E);

                MensajeSalida = "1";
                return MensajeSalida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Guardar(
            Miembro_E miembro_E,
            Miembro_Informacion_Familiar1_E informacion_familiar1_E,
            Miembro_Informacion_Familiar2_E informacion_familiar2_E,
            Miembro_Informacion_Laboral_E informacion_laboral_E,
            Miembro_Nivel_Academico_E nivel_academico_E,
            Miembro_Pasatiempos_E pasatiempos_E)
        {
            string MensajeSalida = "";
            try
            {
                // Validaciones
                if (miembro_E.Nombres.Length == 0)
                {
                    MensajeSalida = "El nombre del miembro no puede estar vacío";
                    return MensajeSalida;
                }
                else if (miembro_E.Apellidos.Length == 0)
                {
                    MensajeSalida = "El apellido del miembro no puede estar vacío";
                    return MensajeSalida;
                }

                // Agregacion del miembro
                bool Respuesta = miembro_D.Guardar(miembro_E);

                // Agregación de los demas datos
                Miembro_Informacion_Familiar1_N informacion_familia1_N = new Miembro_Informacion_Familiar1_N();
                informacion_familiar1_E.Id_Miembro = miembro_E.Id_Miembro;
                informacion_familia1_N.Guardar(informacion_familiar1_E);

                Miembro_Informacion_Familiar2_N informacion_familiar2_N = new Miembro_Informacion_Familiar2_N();
                informacion_familiar2_E.Id_Miembro = miembro_E.Id_Miembro;
                informacion_familiar2_N.Guardar(informacion_familiar2_E);

                Miembro_Informacion_Laboral_N informacion_laboral_N = new Miembro_Informacion_Laboral_N();
                informacion_laboral_E.Id_Miembro = miembro_E.Id_Miembro;
                informacion_laboral_N.Guardar(informacion_laboral_E);

                Miembro_Nivel_Academico_N nivel_academico_N = new Miembro_Nivel_Academico_N();
                nivel_academico_E.Id_Miembro = miembro_E.Id_Miembro;
                nivel_academico_N.Guardar(nivel_academico_E);

                Miembro_Pasatiempos_N pasatiempos_N = new Miembro_Pasatiempos_N();
                pasatiempos_E.Id_Miembro = miembro_E.Id_Miembro;
                pasatiempos_N.Guardar(pasatiempos_E);

                MensajeSalida = "1";
                return MensajeSalida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
