using System;

namespace Entidades.Miembros
{
    public class Miembro_Informacion_Familiar2_E
    {
        public int Id_Miembro { get; set; }

        // Informacion del padre del miembro
        public string Padre_Nombre_Completo { get; set; }
        public int Padre_Edad { get; set; }
        public bool Padre_Empleado { get; set; }
        public bool Padre_Negocio_Propio { get; set; }
        public string Padre_Celular { get; set; }
        public bool Padre_Miembro_Iglesia { get; set; }


        // Informacion de la madre del miembro
        public string Madre_Nombre_Completo { get; set; }
        public int Madre_Edad { get; set; }
        public bool Madre_Empleada { get; set; }
        public bool Madre_Negocio_Propio { get; set; }
        public string Madre_Celular { get; set; }
        public bool Madre_Miembro_Iglesia { get; set; }


        // Informacion del primer hermano del miembro
        public string Hermano1_Nombre_Completo { get; set; }
        public string Hermano1_Escolaridad { get; set; }
        public string Hermano1_Correo_Electronico { get; set; }
        public string Hermano1_Celular { get; set; }


        // Informacion del segundo hermano del miembro
        public string Hermano2_Nombre_Completo { get; set; }
        public string Hermano2_Escolaridad { get; set; }
        public string Hermano2_Correo_Electronico { get; set; }
        public string Hermano2_Celular { get; set; }


        // Informacion del tercero hermano del miembro
        public string Hermano3_Nombre_Completo { get; set; }
        public string Hermano3_Escolaridad { get; set; }
        public string Hermano3_Correo_Electronico { get; set; }
        public string Hermano3_Celular { get; set; }


        // Informacion del cuarto hermano del miembro
        public string Hermano4_Nombre_Completo { get; set; }
        public string Hermano4_Escolaridad { get; set; }
        public string Hermano4_Correo_Electronico { get; set; }
        public string Hermano4_Celular { get; set; }


        // Informacion del quinto hermano del miembro
        public string Hermano5_Nombre_Completo { get; set; }
        public string Hermano5_Escolaridad { get; set; }
        public string Hermano5_Correo_Electronico { get; set; }
        public string Hermano5_Celular { get; set; }
    }
}
