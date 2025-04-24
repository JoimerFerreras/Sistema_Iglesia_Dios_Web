using System;

namespace Entidades.Usuarios
{
    public class Usuario_E
    {
        public int Id_Usuario { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int Sexo { get; set; }
        public bool Bloqueo { get; set; }
        public int Id_Rol { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime? Fecha_Ultima_Modificacion { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public byte[] Password { get; set; }
        public bool Verificacion_Dos_Pasos { get; set; }
        public bool RestablecerPassword { get; set; }

        public string Nombre_Rol { get; set; }
    }
}
