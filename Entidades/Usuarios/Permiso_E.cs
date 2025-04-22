using System;

namespace Entidades.Usuarios
{
    public class Permiso_E
    {
        public int Id_Rol { get; set; }
        public int Id_Funcionalidad { get; set; }
        public bool Permiso_Visualizar { get; set; }
        public bool Permiso_Editar { get; set; }
        public bool Permiso_Eliminar { get; set; }
    }
}
