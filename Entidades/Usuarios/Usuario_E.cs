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
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int Tipo_Usuario { get; set; }
    }
}
