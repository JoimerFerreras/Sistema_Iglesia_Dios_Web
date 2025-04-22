using Datos.Egresos;
using Entidades.Egresos;
using Negocio.Util_N;
using System;
using System.Data;
using System.IO;
using System.Web;

namespace Negocio.Egresos
{
    public class Archivo_Egreso_N
    {
        Archivo_Egreso_D Archivo_Egreso_D = new Archivo_Egreso_D();

        public DataTable Listar(string Id_Egreso)
        {
            try
            {
                return Archivo_Egreso_D.Listar(int.Parse(Id_Egreso));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Archivo_Egreso_E ObtenerArchivo(int Id_Archivo)
        {
            try
            {
                return Archivo_Egreso_D.ObtenerArchivo(Id_Archivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Archivo_Egreso_E EstructurarArchivo(HttpPostedFile postedFile, string NombreArchivo, string DescripcionArchivo, int Numero_Lista, string Id_Egreso)
        {
            try
            {
                Utilidad_N utilidad_N = new Utilidad_N();
                Archivo_Egreso_E archivo_E = new Archivo_Egreso_E();

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
                archivo_E.Id_Egreso = int.Parse(Id_Egreso);

                archivo_E.Id_Archivo = Numero_Lista;

                return archivo_E;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int AgregarArchivo(Archivo_Egreso_E archivo_E, int Id_Egreso)
        {
            try
            {
                if (Id_Egreso > 0)
                {
                    archivo_E.Id_Egreso = Id_Egreso;
                }
                int Id_Archivo = Archivo_Egreso_D.Agregar(archivo_E);

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

                return Archivo_Egreso_D.Eliminar(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

