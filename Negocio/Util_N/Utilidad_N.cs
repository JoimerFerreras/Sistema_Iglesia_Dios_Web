using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

    }
}
