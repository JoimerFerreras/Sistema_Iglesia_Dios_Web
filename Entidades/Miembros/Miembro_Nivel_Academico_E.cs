using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Miembros
{
    public class Miembro_Nivel_Academico_E
    {
        public int Id_Miembro { get; set; }
        public bool Primario { get; set; }
        public bool Secundario { get; set; }
        public bool Grado_Universitario { get; set; }
        public bool Post_Grado_Maestria { get; set; }
    }
}
