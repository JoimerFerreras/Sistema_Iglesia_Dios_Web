using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Egresos
{
    public class Abono_Cuenta_Pagar_E
    {
        public int Id_Abono_CP { get; set; }
        public int Id_Cuenta_Pagar { get; set; }
        public float Monto_Abono { get; set; }
        public int Id_Moneda { get; set; }
        public float Valor_Moneda { get; set; }
        public DateTime Fecha_Abono { get; set; }
        public int Id_Usuario_Registro { get; set; }
        public string Nombre_Usuario_Registro { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public int Id_Usuario_Ultima_Modificacion { get; set; }
        public string Nombre_Usuario_Ultima_Modificacion { get; set; }
        public DateTime? Fecha_Ultima_Modificacion { get; set; }
        public string Comentario { get; set; }
        public int Id_Forma_Pago { get; set; }
    }
}
