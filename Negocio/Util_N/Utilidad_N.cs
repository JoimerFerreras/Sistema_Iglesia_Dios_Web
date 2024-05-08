using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace Negocio.Util_N
{
    public class Utilidad_N
    {
        #region Aplicacion
        public static string ObtenerRutaServer()
        {
            string url = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/";
            return url;
        }
        #endregion


        #region Informacion del usuario
        public static string Id_Usuario
        {
            get { return _Id_Usuario; }
            set { _Id_Usuario = value; }
        }
        public static string Tipo_Usuario
        {
            get { return _Tipo_Usuario; }
            set { _Tipo_Usuario = value; }
        }

        private static string _Id_Usuario;
        private static string _Tipo_Usuario;

        #endregion


        #region Validaciones
        public static bool ValidarNull(object objeto)
        {
            if (objeto == DBNull.Value)
                return true;

            if (objeto == null)
                return true;

            if (objeto is string)
            {
                if (string.IsNullOrWhiteSpace((string)objeto))
                    return true;
            }

            return false;
        }

        public static bool ValidarEmail(string Email)
        {
            // Expresión regular para validar el correo electrónico
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(Email, pattern);
        }

        public static bool ValidarPassword(string Password)
        {
            // Expresión regular para validar la password
            string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";

            return Regex.IsMatch(Password, pattern);
        }
        #endregion


        #region Converciones
        public static string FormatearNumero(string Numero, int Tipo, int CantidadDecimales)
        {
            string NumeroFormateado = "0";
            string CantidadDecimales_String = ".";
            if (CantidadDecimales > 0)
            {
                for (int i = 0; i < CantidadDecimales; i++)
                {
                    CantidadDecimales_String += "0";
                }
            }
            switch (Tipo)
            {
                case 0: // Entero
                    NumeroFormateado = (string.Format(CultureInfo.InvariantCulture, "{0:0,0" + CantidadDecimales_String + "}", int.Parse(Numero)));
                    break;

                case 1: // Float
                    NumeroFormateado = (string.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", float.Parse(Numero)));
                    break;

                case 2: // Double
                    NumeroFormateado = (string.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", double.Parse(Numero)));
                    break;
                default:
                    break;
            }
            return NumeroFormateado;
        }
        #endregion


        #region Email
        public static void EnviarCorreo(string Correo_Destinatario)
        {

        }

        public static bool EnviarCodigoVerificacion(string Correo_Destinatario, int CodigoVerificacion)
        {
            bool ConfirmacionCorreo = false;
            try
            {
                string CorreoAplicacion = ConfigurationManager.AppSettings["CorreoAplicacion"].ToString();
                string Password_CorreoAplicacion = ConfigurationManager.AppSettings["Password_CorreoAplicacion"].ToString();
                string Smtp = ConfigurationManager.AppSettings["SmtpHost_CorreoAplicacion"].ToString();
                string Port = ConfigurationManager.AppSettings["Port_CorreoAplicacion"].ToString();

                string SSL = ConfigurationManager.AppSettings["SSL_CorreoAplicacion"].ToString(); // El valor debe ser "true" o "false"

                MailMessage ms = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                ms.From = new MailAddress(CorreoAplicacion);
                ms.To.Add(new MailAddress(Correo_Destinatario));

                ms.Subject = "(no-reply) Egresados PUCMM: Código de Verificación";

                string html = "<!DOCTYPE html> <html style=\"height: 100%;\"> <head> <style> body { display: flex; justify-content: center; /* Centrado horizontal */ align-items: center; /* Centrado vertical */ } .fondo { /*  background-image: url('/Recursos/Imagenes/Fondos/fondo_login.png'); */ background: rgb(0, 51, 160); background: linear-gradient(90deg, rgba(206, 221, 255, 1) 21%, rgba(255, 243, 204, 1) 73%); } </style> </head> <body style=\"height: 100%; margin: 0 auto;\" class=\"fondo\"> <div style=\"width: 100%; margin-top: 100px; margin-bottom: 100px;\"> <div style=\"background-color: #f1f1f1; box-shadow: 0px 0px 20px 2px rgba(0, 0, 0, 0.3); background-color: white; border-radius: 10px; margin: auto; width: 50%; background-color: #EFF4FF; border-radius: 10px; font-family: 'Roboto', sans-serif\"> <div style=\"background-color: #ffffff; border-top-left-radius: 10px; border-top-right-radius: 10px;\"> <span style=\"display: inline-block; color: #0F53CD; padding-left: 50px; padding-top: 10px; margin-bottom: -10px; font-size: 25px; font-weight: bold;\">Egresados&nbsp;</span> <span style=\"display: inline-block; color: #F9CB33; padding-top: 10px; margin-bottom: -10px; font-size: 25px; font-weight: bold;\">PUCMM</span> <div style=\"background-color: #E6E7E8; width: 100%; height: 1px; margin-top: 10px;\"></div> <h1 style=\"color: black; padding-left: 50px; padding-bottom: 30px; font-weight: unset;\"> Recuperaci&oacute;n de cuenta</h1> </div> <label style=\"color: rgb(53, 53, 53); padding-left: 50px; font-size: 18px;\">Su codigo de verificaci&oacute;n es: </label> <label style=\"color: rgb(53, 53, 53); font-size: 18px; font-weight: bold; background-color: transparent; border: transparent; border-color: transparent; font-family: 'Roboto', sans-serif;\">" + CodigoVerificacion + "</label><br><br> <label style=\"color: rgb(53, 53, 53); padding-left: 50px; font-size: 18px;\">Si desconoce esta acci&oacute;n por favor contacte a su administrador.</label> <br><br> </div> </div> </body> </html>";

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);

                ms.AlternateViews.Add(htmlView);

                smtp.Host = Smtp;
                smtp.Port = Convert.ToInt32(Port);

                smtp.Credentials = new NetworkCredential(CorreoAplicacion, Password_CorreoAplicacion);
                smtp.EnableSsl = Convert.ToBoolean(SSL);

                smtp.Send(ms);
                ConfirmacionCorreo = true;
            }
            catch (Exception)
            {
                ConfirmacionCorreo = false;
                return ConfirmacionCorreo;
            }

            return ConfirmacionCorreo;
        }


        #endregion


        #region Seguridad
        public static int GenerarCodigoVerificacion()
        {
            int NumeroAleatorio = 0;

            Random rnd = new Random();
            NumeroAleatorio = rnd.Next(100150, 998735);

            return NumeroAleatorio;
        }

        #endregion


        #region Cifrado/Descrifado

        public static byte[] Encriptar(string TextoOriginal)
        {
            byte[] key;
            byte[] iv;
            // Encriptar con claves aleatorias
            //key = GenerateRandomKey();
            //iv = GenerateRandomIV();

            // Encriptar con claves fijas (Ideal para passwords)
            key = Encoding.UTF8.GetBytes("86s7kbE/zERKfJq6"); // Clave fija
            iv = Encoding.UTF8.GetBytes("1+rrHZyjeqyP0tR0"); // IV fijo

            // Devuelve un string que se guarda en una variable varbinary
            return EncryptStringToBytes_Aes(TextoOriginal);
        }

        public static string Desencriptar(byte[] TextoEncriptado)
        {
            return DecryptStringFromBytes_Aes(TextoEncriptado);
        }

        static byte[] GenerateRandomKey()
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.GenerateKey();
                return aes.Key;
            }
        }
        private static byte[] GenerateRandomIV()
        {
            /* IV significa "Initial Vector" o "Vector de Inicialización" en español.
               Es un valor aleatorio que se utiliza en el cifrado simétrico para asegurar que el mismo mensaje cifrado con la misma clave no se vea igual cada vez que se cifra. */
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }
        private static byte[] EncryptStringToBytes_Aes(string plainText)
        {
            try
            {
                byte[] encrypted, key, iv;
                using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
                {
                    key = Encoding.UTF8.GetBytes("86s7kbE/zERKfJq6"); // Clave fija
                    aes.Key = key;
                    aes.IV = iv = Encoding.UTF8.GetBytes("1+rrHZyjeqyP0tR0"); // IV fijo
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(plainText);
                            }
                            encrypted = ms.ToArray();
                        }
                    }
                }
                byte[] result = encrypted;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText)
        {
            string plaintext = null;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                byte[] key = Encoding.UTF8.GetBytes("86s7kbE/zERKfJq6"); // Clave fija
                aes.Key = key;

                byte[] iv = Encoding.UTF8.GetBytes("1+rrHZyjeqyP0tR0"); // IV fijo
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            plaintext = sr.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
        #endregion



        #region Archivos TXT

        public void GenerarLog(string Ex_Message, string Ex_TargetSite, string Ex_Source, string Ex_HResult, string NombreMetodo)
        {
            // Verificacion de la existencia de la carpeta de logs
            CrearCarpetaLogs();

            // Ruta del archivo
            DateTime Fecha = DateTime.Now;
            string NombreArchivo = "/Log_" + Fecha.Day.ToString() + Fecha.Month.ToString() + Fecha.Year.ToString() + ".txt";
            string rutaArchivo = ConfigurationManager.AppSettings["RutaCarpetaLogs"].ToString() + NombreArchivo;

            // Verificar si el archivo existe
            if (File.Exists(rutaArchivo))
            {
                EditarLogTXT(Ex_Message, Ex_TargetSite, Ex_Source, Ex_HResult, NombreMetodo);
            }
            else
            {
                CrearLogTXT(Ex_Message, Ex_TargetSite, Ex_Source, Ex_HResult, NombreMetodo);
            }
        }

        private void CrearLogTXT(string Ex_Message, string Ex_TargetSite, string Ex_Source, string Ex_HResult, string NombreMetodo)
        {
            // Ruta del archivo donde se creará el archivo de texto
            DateTime Fecha = DateTime.Now;
            string NombreArchivo = "/Log_" + Fecha.Day.ToString() + Fecha.Month.ToString() + Fecha.Year.ToString() + ".txt";
            string rutaArchivo = ConfigurationManager.AppSettings["RutaCarpetaLogs"].ToString() + NombreArchivo;

            // Contenido que se escribirá en el archivo de texto
            string contenido = "\n-------------  " + Fecha.Hour + ":" + Fecha.Minute + ":" + Fecha.Second + "  ------------- \n";
            contenido += "Source: " + Ex_Source + "\n";
            contenido += "Target Site: " + Ex_TargetSite + "\n";
            contenido += "Message: " + Ex_Message + "\n";
            contenido += "HResult: " + Ex_HResult + "\n";
            contenido += "Método: " + NombreMetodo + "\n";

            try
            {
                // Crear un nuevo archivo de texto y obtener un objeto StreamWriter para escribir en él
                using (StreamWriter sw = new StreamWriter(rutaArchivo))
                {
                    // Escribir el contenido en el archivo
                    sw.WriteLine(contenido);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el archivo de texto: " + ex.Message);
            }
        }

        private void EditarLogTXT(string Ex_Message, string Ex_TargetSite, string Ex_Source, string Ex_HResult, string NombreMetodo)
        {
            // Ruta del archivo de texto
            DateTime Fecha = DateTime.Now;
            string NombreArchivo = "/Log_" + Fecha.Day.ToString() + Fecha.Month.ToString() + Fecha.Year.ToString() + ".txt";
            string rutaArchivo = ConfigurationManager.AppSettings["RutaCarpetaLogs"].ToString() + NombreArchivo;

            try
            {
                // Leer el contenido del archivo de texto
                string contenido;
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    contenido = sr.ReadToEnd();
                }

                contenido += "\n-------------  " + Fecha.Hour + ":" + Fecha.Minute + ":" + Fecha.Second + "  ------------- \n";
                contenido += "Source: " + Ex_Source + "\n";
                contenido += "Target Site: " + Ex_TargetSite + "\n";
                contenido += "Message: " + Ex_Message + "\n";
                contenido += "HResult: " + Ex_HResult + "\n";
                contenido += "Método: " + NombreMetodo + "\n";

                // Sobreescribir el contenido del archivo
                using (StreamWriter sw = new StreamWriter(rutaArchivo))
                {
                    sw.Write(contenido);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer o sobrescribir el archivo de texto: " + ex.Message);
            }
        }

        private void CrearCarpetaLogs()
        {
            // Ruta de la carpeta que deseas verificar y crear si no existe
            string RutaCarpeta = ConfigurationManager.AppSettings["RutaCarpetaLogs"].ToString();

            // Verificar si la carpeta existe
            if (!Directory.Exists(RutaCarpeta))
            {
                try
                {
                    // Crear la carpeta si no existe
                    Directory.CreateDirectory(RutaCarpeta);
                    Console.WriteLine("La carpeta se ha creado exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al crear la carpeta: " + ex.Message);
                }
            }
        }

        public string ObtenerNombreMetodo()
        {
            // Utiliza reflection para obtener información sobre el método actual.
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            return stackFrame.GetMethod().Name;
        }

        #endregion
    }
}
