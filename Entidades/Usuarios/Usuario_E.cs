using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Usuarios
{
    public class Usuario_E
    {
        public int Id_Usuario { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public bool Genero { get; set; }
        public bool Bloqueo { get; set; }
        public int Id_Rol { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Ultima_Modificacion { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool Verificacion_Dos_Pasos { get; set; }
        public bool RestablecerPassword { get; set; }
    }
}
