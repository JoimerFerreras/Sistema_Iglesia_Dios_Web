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
        private void GraficoIngresosVsEgresos()
        {
            try
            {
                DataTable dt = resumen_N.GraficoIngresosVsEgresos();
                if (dt.Rows.Count > 0)
                {
                    // Asegurar orden cronológico en el eje X (YYYY-MM ascendente)
                    DataView dv = dt.DefaultView;
                    dv.Sort = "Periodo ASC";

                    chtFinanzas.DataSource = dv.ToTable();
                    chtFinanzas.DataBind();
                }

             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GraficoCobrarPorMes()
        {
            try
            {
                DataTable dt = resumen_N.GraficoCobrarPorMes();
                if (dt.Rows.Count > 0)
                {
                    chCobrarMes.DataSource = dt;
                    chCobrarMes.DataBind();
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

                GraficoIngresosVsEgresos();
                GraficoCobrarPorMes();
            }
        }
        #endregion
    }
}