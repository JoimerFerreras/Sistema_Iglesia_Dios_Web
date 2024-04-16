using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Miembros
{
    public class Miembro_E
    {
        public int Id_Miembro { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Nombre_Pila { get; set; }
        public int Sexo { get; set; }
        public DateTime? Fecha_Nacimiento { get; set; }
        public int Estado_Civil { get; set; }
        public bool Tiene_Hijos { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Sector { get; set; }
        public string Barrio_Residencial { get; set; }
        public string Calle { get; set; }
        public string Numero_Casa { get; set; }
        public bool Es_Miembro { get; set; }
        public DateTime? Desde_Cuando_Miembro { get; set; }
        public bool Pertenece_Ministerio { get; set; }
        public bool Le_Gustaria_Pertenecer_Ministerio { get; set; }
        public int Numero_Alternativo_Miembro { get; set; }
        public string Ministerio_Al_Que_Pertenece { get; set; }
        public int Rol_Miembro { get; set; }
        public string Otro_Rol { get; set; }
        public string Nombre_Diacono { get; set; }
        public string Nombre_Lider_Ministerio { get; set; }
        public string Comentarios_Diacono_Lider_Ministerio { get; set; }
        public string Revisado_Por { get; set; }
        public string Autorizado_Por { get; set; }
    }
}
