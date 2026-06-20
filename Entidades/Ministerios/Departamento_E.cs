using System;

namespace Entidades.Ministerios
{
    public class Departamento_E
    {
        public int Id_Departamento { get; set; }
        public string Nombre_Departamento { get; set; }
        public bool Estado { get; set; }
        public int Id_Lider_Departamento { get; set; }
        public int Id_Diacono_Departamento { get; set; }
    }
}
