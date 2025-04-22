using System;

namespace Entidades.Miembros
{
    public class Miembro_Informacion_Laboral_E
    {
        public int Id_Miembro { get; set; }
        public bool Empleado_Privado { get; set; }
        public bool Empleado_Publico { get; set; }
        public bool Dueno_Negocio { get; set; }
        public bool Independiente { get; set; }
        public bool Otros { get; set; }
        public string Nombre_Empresa_Negocio { get; set; }
    }
}
