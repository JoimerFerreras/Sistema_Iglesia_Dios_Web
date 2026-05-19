using System;

namespace Entidades.Ministerios
{
    public class Ministerio_E
    {
        public int Id_Ministerio { get; set; }
        public string Nombre_Ministerio { get; set; }
        public bool Estado { get; set; }
        public int Id_Lider_Ministerio { get; set; }
        public int Id_Diacono_Ministerio { get; set; }
    }
}
