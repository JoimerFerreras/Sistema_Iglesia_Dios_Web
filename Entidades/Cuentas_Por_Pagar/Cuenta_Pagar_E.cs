using System;

namespace Entidades.Cuentas_Por_Pagar
{
    public class Cuenta_Pagar_E
    {
        public int Id_Cuenta_Pagar { get; set; }
        public int Id_Descripcion { get; set; }
        public int Id_Miembro { get; set; }
        public int Id_Miscelaneo { get; set; }
        public DateTime Fecha_CP { get; set; }
        public float Valor { get; set; }
        public int Id_Forma_Pago { get; set; }
        public string Tipo_Documento { get; set; } // Pueden ser FT, NC, RS, o ND
        public string No_Documento { get; set; } // Puede ser no. factura o demas...
        public string Comentario { get; set; }
        public int Id_Usuario { get; set; }
        public string Nombre_Usuario_Registro { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public int Id_Usuario_Ultima_Modificacion { get; set; }
        public string Nombre_Usuario_Ultima_Modificacion { get; set; }
        public DateTime? Fecha_Ultima_Modificacion { get; set; }
    }
}
