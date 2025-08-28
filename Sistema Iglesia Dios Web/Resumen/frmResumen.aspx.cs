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

        private void TotalesMesActual()
        {
            try
            {
                DataTable dt = resumen_N.TotalesMesActual();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblTotalIngresos_MesActual.Text = Convert.ToDecimal(row["IngresosMes"]).ToString("C2");
                    lblTotalEgresos_MesActual.Text = Convert.ToDecimal(row["EgresosMes"]).ToString("C2");
                    lblTotalNeto_MesActual.Text = Convert.ToDecimal(row["NetoMes"]).ToString("C2");

                    decimal PctIngresos = Convert.ToDecimal(row["VarPctIngresos"]);
                    decimal PctEgresos = Convert.ToDecimal(row["VarPctEgresos"]);
                    decimal PctNeto = Convert.ToDecimal(row["VarPctNeto"]);

                    //string SimboloIngresos = PctIngresos >= 0 ? "▲" : "▼";

                    lblPorcentajeTotalIngresos.InnerHtml = PctIngresos.ToString("+0;-0;0") + " %";
                    lblPorcentajeTotalEgresos.InnerHtml =  PctEgresos.ToString("+0;-0;0") + " %";
                    lblPorcentajeTotalNeto.InnerHtml = PctNeto.ToString("+0;-0;0") + " %";

                    lblTituloCard_TotalIngresos.InnerHtml = "Ingresos del mes actual (" + DateTime.Now.ToString("MMMM") +")";
                    lblTituloCard_TotalEgresos.InnerHtml = "Egresos del mes actual (" + DateTime.Now.ToString("MMMM") +")";
                    lblTituloCard_TotalNeto.InnerHtml = "Neto del mes actual (" + DateTime.Now.ToString("MMMM") +")";

                    if (PctIngresos > 0)
                    {
                        divIconTotalesIngresos.InnerHtml = "<i class=\"fa-solid fa-square-caret-up fa-lg\"></i>";
                    }
                    else if (PctIngresos < 0)
                    {
                        divIconTotalesIngresos.InnerHtml = "<i class=\"fa-solid fa-square-caret-down fa-lg\"></i>";
                    }
                    else
                    {
                        divIconTotalesIngresos.InnerHtml = "<i class=\"fa-solid fa-square fa-lg\"></i>";
                    }

                    if (PctEgresos > 0)
                    {
                        divIconTotalesEgresos.InnerHtml = "<i class=\"fa-solid fa-square-caret-up fa-lg\"></i>";
                    }
                    else if (PctEgresos < 0)
                    {
                        divIconTotalesEgresos.InnerHtml = "<i class=\"fa-solid fa-square-caret-down fa-lg\"></i>";
                    }
                    else
                    {
                        divIconTotalesEgresos.InnerHtml = "<i class=\"fa-solid fa-square fa-lg\"></i>";
                    }

                    if (PctNeto > 0)
                    {
                        divIconTotalesNeto.InnerHtml = "<i class=\"fa-solid fa-square-caret-up fa-lg\"></i>";
                    }
                    else if (PctNeto < 0)
                    {
                        divIconTotalesNeto.InnerHtml = "<i class=\"fa-solid fa-square-caret-down fa-lg\"></i>";
                    }
                    else
                    {
                        divIconTotalesNeto.InnerHtml = "<i class=\"fa-solid fa-square fa-lg\"></i>";
                    }
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
                TotalesMesActual();
            }
        }
        #endregion
    }
}