// Autor: Joimer Ferreras

using Negocio.Resumen;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Data;
using System.Globalization;
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

        private void GraficoCuentasCobrarPorMes()
        {
            try
            {
                DataTable dt = resumen_N.GraficoCuentasCobrarPorMes();
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

        private void GraficoAntiguedadCxC()
        {
            try
            {
                DataTable dt = resumen_N.GraficoAntiguedadCxC();
                if (dt.Rows.Count > 0)
                {
                    chCxC_Antiguedad.DataSource = dt;
                    chCxC_Antiguedad.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GraficoCuentasPagarPorMes()
        {
            try
            {
                DataTable dt = resumen_N.GraficoCuentasPagarPorMes();
                if (dt.Rows.Count > 0)
                {
                    chPagarMes.DataSource = dt;
                    chPagarMes.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GraficoAntiguedadCxP()
        {
            try
            {
                DataTable dt = resumen_N.GraficoAntiguedadCxP();
                if (dt.Rows.Count > 0)
                {
                    chCxP_Antiguedad.DataSource = dt;
                    chCxP_Antiguedad.DataBind();
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
                if (dt == null || dt.Rows.Count == 0) return;

                DataRow row = dt.Rows[0];

                decimal ingresos = GetDecimal(row, "IngresosMes");
                decimal egresos = GetDecimal(row, "EgresosMes");
                decimal neto = GetDecimal(row, "NetoMes");

                lblTotalIngresos_MesActual.Text = ingresos.ToString("$#,##0.00;-$#,##0.00;$0.00");
                lblTotalEgresos_MesActual.Text = egresos.ToString("$#,##0.00;-$#,##0.00;$0.00");
                lblTotalNeto_MesActual.Text = neto.ToString("$#,##0.00;-$#,##0.00;$0.00");

                decimal pctIngresos = GetDecimal(row, "VarPctIngresos");
                decimal pctEgresos = GetDecimal(row, "VarPctEgresos");
                decimal pctNeto = GetDecimal(row, "VarPctNeto");

                lblPorcentajeTotalIngresos.InnerHtml = pctIngresos.ToString("+0;-0;0") + " %";
                lblPorcentajeTotalEgresos.InnerHtml = pctEgresos.ToString("+0;-0;0") + " %";
                lblPorcentajeTotalNeto.InnerHtml = pctNeto.ToString("+0;-0;0") + " %";

                string mes = GetMonthNameEs();
                lblTituloCard_TotalIngresos.InnerHtml = "Ingresos del mes actual (" + mes + ")";
                lblTituloCard_TotalEgresos.InnerHtml = "Egresos del mes actual (" + mes + ")";
                lblTituloCard_TotalNeto.InnerHtml = "Neto del mes actual (" + mes + ")";

                divIconTotalesIngresos.InnerHtml = pctIngresos > 0 ? "<i class=\"fa-solid fa-square-caret-up fa-lg\"></i>"
                                             : pctIngresos < 0 ? "<i class=\"fa-solid fa-square-caret-down fa-lg\"></i>"
                                             : "<i class=\"fa-solid fa-square fa-lg\"></i>";

                divIconTotalesEgresos.InnerHtml = pctEgresos > 0 ? "<i class=\"fa-solid fa-square-caret-up fa-lg\"></i>"
                                            : pctEgresos < 0 ? "<i class=\"fa-solid fa-square-caret-down fa-lg\"></i>"
                                            : "<i class=\"fa-solid fa-square fa-lg\"></i>";

                divIconTotalesNeto.InnerHtml = pctNeto > 0 ? "<i class=\"fa-solid fa-square-caret-up fa-lg\"></i>"
                                         : pctNeto < 0 ? "<i class=\"fa-solid fa-square-caret-down fa-lg\"></i>"
                                         : "<i class=\"fa-solid fa-square fa-lg\"></i>";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static decimal GetDecimal(DataRow row, string colName)
        {
            if (row == null) return 0m;
            if (!row.Table.Columns.Contains(colName)) return 0m;

            object v = row[colName];
            if (v == null || v == DBNull.Value) return 0m;

            return Convert.ToDecimal(v);
        }

        private static string GetMonthNameEs()
        {
            // si quieres el mes en español siempre, aunque el server tenga otra cultura
            return DateTime.Now.ToString("MMMM", new CultureInfo("es-DO"));
        }


        private void TotalMiscelaneos()
        {
            try
            {
                DataTable dt = resumen_N.TotalMiscelaneos();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblTotalMiscelaneos.Text = Convert.ToDecimal(row["CantidadRegistros"]).ToString("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TotalDescripciones()
        {
            try
            {
                DataTable dt = resumen_N.TotalDescripciones();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblTotalDescripciones.Text = Convert.ToDecimal(row["CantidadRegistros"]).ToString("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TotalFormas_Pago()
        {
            try
            {
                DataTable dt = resumen_N.TotalFormas_Pago();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblTotalFormasPago.Text = Convert.ToDecimal(row["CantidadRegistros"]).ToString("");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TotalMiembros()
        {
            try
            {
                DataTable dt = resumen_N.TotalMiembros();
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblTotalMiembros.Text = Convert.ToDecimal(row["CantidadRegistros"]).ToString("");
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

                GraficoCuentasCobrarPorMes();
                GraficoAntiguedadCxC();

                GraficoCuentasPagarPorMes();
                GraficoAntiguedadCxP();

                TotalesMesActual();
                TotalMiscelaneos();
                TotalDescripciones();
                TotalFormas_Pago();
                TotalMiembros();
            }
        }
        #endregion
    }
}