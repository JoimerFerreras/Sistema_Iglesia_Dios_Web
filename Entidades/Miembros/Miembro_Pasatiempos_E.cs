using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Miembros
{
    public class Miembro_Pasatiempos_E
    {
        public int Id_Miembro { get; set; }
        public bool Cine { get; set; }
        public bool Leer { get; set; }
        public bool Ver_TV { get; set; }
        public bool Socializar { get; set; }
        public bool Viajar { get; set; }
        public string Otros { get; set; }
    }
}
