using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Miembros
{
    public class Miembro_Informacion_Familiar1_E
    {
        public int Id_Miembro { get; set; }
       
        // Informacion del conyuge del miembro
        public string Conyuge_Nombre { get; set; }
        public bool Conyuge_Cristiano { get; set; }
        public int Conyuge_Edad { get; set; }


        // Informacion del primer hijo del miembro
        public string Hijo1_Nombre { get; set; }
        public int Hijo1_Edad { get; set; }
        public bool Hijo1_Cristiano { get; set; }

        // Informacion del segundo hijo del miembro
        public string Hijo2_Nombre { get; set; }
        public int Hijo2_Edad { get; set; }
        public bool Hijo2_Cristiano { get; set; }


        // Informacion tercer hijo del miembro
        public string Hijo3_Nombre { get; set; }
        public int Hijo3_Edad { get; set; }
        public bool Hijo3_Cristiano { get; set; }


        // Informacion cuarto hijo del miembro
        public string Hijo4_Nombre { get; set; }
        public int Hijo4_Edad { get; set; }
        public bool Hijo4_Cristiano { get; set; }


        // Informacion quinto hijo del miembro
        public string Hijo5_Nombre { get; set; }
        public int Hijo5_Edad { get; set; }
        public bool Hijo5_Cristiano { get; set; }


        // Informacion sexto hijo del miembro
        public string Hijo6_Nombre { get; set; }
        public int Hijo6_Edad { get; set; }
        public bool Hijo6_Cristiano { get; set; }
    }
}