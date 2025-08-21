// Autor: Joimer Ferreras

using Negocio.Resumen;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Data;
using System.Web.UI;

namespace Sistema_Iglesia_Dios_Web.Resumen
{
    [CodigoFuncionalidad("Resumen")]
    public partial class frmResumen : System.Web.UI.Page
    {
        #region Declaraciones
        Resumen_N resumen_N = new Resumen_N();

        #endregion


        #region Metodos/ Procedimientos
     

        private void GraficoIngresosMes()
        {
            try
            {
                DataTable dt = resumen_N.GraficoIngresosMes();
                if (dt.Rows.Count > 0)
                {
                    graficoIngresosMes.DataSource = dt;
                    graficoIngresosMes.DataBind();
                }
                else
                {
                    graficoIngresosMes.Visible = false;
                    divMensaje_graficoIngresosMes.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Resumen");

                //GraficoIngresosMes();
            }
        }
        #endregion
    }
}