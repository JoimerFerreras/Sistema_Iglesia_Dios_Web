using Datos.Ingresos;
using Datos.Util_D;
using Entidades.Ingresos;
using Negocio.Util_N;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Negocio.Ingresos
{
    public class Archivo_Ingreso_N
    {
        Archivo_Ingreso_D Archivo_Ingreso_D = new Archivo_Ingreso_D();

        public DataTable Listar(string Id_Ingreso)
        {
            try
            {
                return Archivo_Ingreso_D.Listar(int.Parse(Id_Ingreso));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Archivo_Ingreso_E ObtenerArchivo(int Id_Archivo)
        {
            try
            {
                return Archivo_Ingreso_D.ObtenerArchivo(Id_Archivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Archivo_Ingreso_E EstructurarArchivo(HttpPostedFile postedFile, string NombreArchivo, string DescripcionArchivo, int Numero_Lista, string Id_Ingreso)
        {
            try
            {
                Utilidad_N utilidad_N = new Utilidad_N();
                Archivo_Ingreso_E archivo_E = new Archivo_Ingreso_E();

                // Obtener el nombre del archivo
                archivo_E.NombreArchivoCarpeta = Path.GetFileNameWithoutExtension(postedFile.FileName);

                // Obtener la extensión del archivo
                archivo_E.Extencion = Path.GetExtension(postedFile.FileName);

                // Obtener el tipo de contenido (MIME type)
                archivo_E.TipoArchivo = postedFile.ContentType;

                // Convertir el tamaño del archivo a MB
                archivo_E.Tamano = utilidad_N.CalcularBytesToMB(postedFile.ContentLength);

                // Leer el contenido del archivo en un byte[]
                archivo_E.Archivo = utilidad_N.ReadFileToByteArray(postedFile.InputStream);

                archivo_E.NombreArchivo = NombreArchivo;
                archivo_E.Descripcion = DescripcionArchivo;
                archivo_E.Fecha_Registro = DateTime.Now;
                archivo_E.Id_Ingreso = int.Parse(Id_Ingreso);

                archivo_E.Id_Archivo = Numero_Lista;

                return archivo_E;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int AgregarArchivo(Archivo_Ingreso_E archivo_E, int Id_Ingreso)
        {
            try
            {
                if (Id_Ingreso > 0)
                {
                    archivo_E.Id_Ingreso = Id_Ingreso;
                }
                int Id_Archivo = Archivo_Ingreso_D.Agregar(archivo_E);

                return Id_Archivo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Eliminar(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    throw new OperationCanceledException("Debe seleccionar un registro para eliminar");
                }

                return Archivo_Ingreso_D.Eliminar(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

