using System;

namespace Entidades.Egresos
{
    public class Egreso_E
    {
        public int Id_Egreso { get; set; }
        public int Id_Miembro { get; set; }
        public int Id_Descripcion { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha_Egreso { get; set; }
        public int Id_Usuario_Registro { get; set; }
        public string Nombre_Usuario_Registro { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public int Id_Usuario_Ultima_Modificacion { get; set; }
        public string Nombre_Usuario_Ultima_Modificacion { get; set; }
        public DateTime? Fecha_Ultima_Modificacion { get; set; }
        public int Id_Forma_Pago { get; set; }
        public string Comentario { get; set; }
        public int Id_Miscelaneo { get; set; }
    }
}
