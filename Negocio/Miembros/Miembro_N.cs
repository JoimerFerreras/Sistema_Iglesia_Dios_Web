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

                // Agregacion del mimebro
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

                return "Registro agregado correctamente";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
