﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Util_E
{
    public class Archivo_E
    {
        public int Id_Archivo { get; set; }
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
