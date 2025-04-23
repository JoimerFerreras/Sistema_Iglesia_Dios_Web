using System;

namespace Entidades.Usuarios
{
    public class Log_Usuario_Acceso_E
    {
        public int Id_Log { get; set; }
        public int Id_Usuario { get; set; }
        public DateTime FechaHora_Login { get; set; }
        public string IPv4 { get; set; }
        public decimal Latitud_Coord { get; set; }
        public decimal Longitud_Coord { get; set; }
    }
}
