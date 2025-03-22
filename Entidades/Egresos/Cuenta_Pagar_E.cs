using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Egresos
{
    public class Cuenta_Pagar_E
    {
        public int Id_Cuenta_Pagar { get; set; }
        public int Id_Descripcion_Egreso { get; set; }
        public float Monto_Total_Pagar { get; set; }
        public int Id_Moneda { get; set; }
        public float Valor_Moneda { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? Fecha_Vencimiento { get; set; }
        public string No_Factura { get; set; }
        public int Id_Miembro { get; set; }
        public int Id_Miscelaneo { get; set; }
        public int Id_Usuario_Registro { get; set; }
        public string Nombre_Usuario_Registro { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public int Id_Usuario_Ultima_Modificacion { get; set; }
        public string Nombre_Usuario_Ultima_Modificacion { get; set; }
        public DateTime? Fecha_Ultima_Modificacion { get; set; }
        public string Comentario { get; set; }
    }
}
