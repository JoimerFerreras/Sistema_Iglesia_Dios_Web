using System;

namespace Entidades.Usuarios
{
    public class Rol_E
    {
        public int Id_Rol { get; set; }
        public string Nombre_Rol { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public DateTime? Fecha_Ultima_Modificacion { get; set; }
        public bool Estado { get; set; }
    }
}
