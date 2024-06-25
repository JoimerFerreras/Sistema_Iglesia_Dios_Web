using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Ingresos
{
    public class Ingreso_E
    {
        public int Id_Ingreso { get; set; }
        public int Id_Miembro { get; set; }
        public int Id_Descripcion_Ingreso { get; set; }
        public int Id_Moneda { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public double Valor_Moneda { get; set; }
        public int Id_Usuario_Registro { get; set; }
        public string Nombre_Usuario_Registro { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public int Id_Usuario_Ultima_Modificacion { get; set; }
        public string Nombre_Usuario_Ultima_Modificacion { get; set; }
        public DateTime? Fecha_Ultima_Modificacion { get; set; }
        public int Id_Forma_Pago { get; set; }
        public string Comentario { get; set; }
    }
}
