using Datos.Cuenta_Por_Cobrar;
using Entidades.Cuentas_Por_Cobrar;
using Negocio.Util_N;
using System;
using System.Data;
using System.IO;
using System.Web;

namespace Negocio.Cuentas_Por_Cobrar
{
    public class Archivo_Cuenta_Cobrar_N
    {
        Archivo_Cuenta_Cobrar_D Archivo_Cuenta_Cobrar_D = new Archivo_Cuenta_Cobrar_D();

        public DataTable Listar(string Id_Cuenta_Cobrar)
        {
            try
            {
                return Archivo_Cuenta_Cobrar_D.Listar(int.Parse(Id_Cuenta_Cobrar));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Archivo_Cuenta_Cobrar_E ObtenerArchivo(int Id_Archivo)
        {
            try
            {
                return Archivo_Cuenta_Cobrar_D.ObtenerArchivo(Id_Archivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Archivo_Cuenta_Cobrar_E EstructurarArchivo(HttpPostedFile postedFile, string NombreArchivo, string DescripcionArchivo, int Numero_Lista, string Id_Cuenta_Cobrar)
        {
            try
            {
                Utilidad_N utilidad_N = new Utilidad_N();
                Archivo_Cuenta_Cobrar_E archivo_E = new Archivo_Cuenta_Cobrar_E();

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
                archivo_E.Id_Cuenta_Cobrar = int.Parse(Id_Cuenta_Cobrar);

                archivo_E.Id_Archivo = Numero_Lista;

                return archivo_E;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int AgregarArchivo(Archivo_Cuenta_Cobrar_E archivo_E, int Id_Cuenta_Cobrar)
        {
            try
            {
                if (Id_Cuenta_Cobrar > 0)
                {
                    archivo_E.Id_Cuenta_Cobrar = Id_Cuenta_Cobrar;
                }
                int Id_Archivo = Archivo_Cuenta_Cobrar_D.Agregar(archivo_E);

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

                return Archivo_Cuenta_Cobrar_D.Eliminar(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

