using System;

namespace Entidades.Util_E
{
    public class TooltipPersonalizado
    {
        public string NombreIdentificador { get; set; }
        public string Texto { get; set; }
        public string Posicion { get; set; }
        public string Arrow { get; set; }

        public TooltipPersonalizado(string nombreIdentificador, string texto, string posicion, string arrow)
        {
            NombreIdentificador = nombreIdentificador;
            Texto = texto;
            Posicion = posicion;
            Arrow = arrow;
        }
    }
}
