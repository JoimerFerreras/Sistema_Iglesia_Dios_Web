using System;

namespace Entidades.Usuarios
{
    public class Notificacion_E
    {
        public int Id_Notificacion { get; set; }
        public int Id_Usuario { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public int Tipo_Notificacion { get; set; }
        public DateTime? Fecha { get; set; }
        public bool Visto { get; set; }
        public string Link { get; set; }
        public bool Link_Destino_En_Sistema { get; set; }
    }
}
