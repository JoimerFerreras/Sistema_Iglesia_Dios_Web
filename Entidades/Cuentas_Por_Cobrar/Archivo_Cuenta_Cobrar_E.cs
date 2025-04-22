using System;

namespace Entidades.Cuentas_Por_Cobrar
{
    [Serializable]
    public class Archivo_Cuenta_Cobrar_E
    {
        public int Id_Archivo { get; set; }
        public int Id_Cuenta_Cobrar { get; set; }
        public string NombreArchivo { get; set; }
        public string NombreArchivoCarpeta { get; set; }
        public string TipoArchivo { get; set; }
        public string Extencion { get; set; }
        public string Descripcion { get; set; }
        public byte[] Archivo { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public float Tamano { get; set; }
    }
}
