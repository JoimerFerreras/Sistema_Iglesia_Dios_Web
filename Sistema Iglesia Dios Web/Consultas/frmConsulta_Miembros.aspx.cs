// Autor: Joimer Ferreras

using Entidades.Miembros;
using Negocio.Miembros;
using Negocio.Ministerios;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Iglesia_Dios_Web.Consultas
{
    public partial class frmConsulta_Miembros : System.Web.UI.Page
    {
        #region Declaraciones
        Miembro_N miembro_N = new Miembro_N();

        public DataTable DT_DATOS
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["DT_DATOS"]))
                {
                    ViewState["DT_DATOS"] = new DataTable();
                }
                return (DataTable)ViewState["DT_DATOS"];
            }
            set
            {
                ViewState["DT_DATOS"] = value;
            }
        }
        #endregion


        #region Metodos/ Procedimientos
     
        private void LimpiarFiltros()
        {
            rbtnTipoFecha.SelectedValue = "2";

            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime PrimerDiaMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime UltimoDiaMes = PrimerDiaMes.Date.AddMonths(1).AddDays(-1);

            string PrimerDiaAnio = "1/1/" + DateTime.Now.Year.ToString();
            //string UltimoDiaAnio = "31/12/" + DateTime.Now.Year.ToString();

            dtpFechaDesde.SelectedDate = DateTime.Parse(PrimerDiaAnio);
            dtpFechaHasta.SelectedDate = DateTime.Now;

            txtTextoBusqueda.Text = "";
            cmbSexo.SelectedValue = "0";
            cmbEstadoCivil.SelectedValue = "0";
            cmbMinisterio.SelectedValue = "0";
        }

        private void Consultar()
        {
            DateTime fecha;

            if (dtpFechaDesde.SelectedDate != null && dtpFechaHasta.SelectedDate != null)
            {
                if (DateTime.TryParse(dtpFechaDesde.SelectedDate.Value.ToString(), out fecha) == true)
                {
                    if (DateTime.TryParse(dtpFechaHasta.SelectedDate.Value.ToString(), out fecha) == true)
                    {
                        DT_DATOS = miembro_N.Consultar(
                        rbtnTipoFecha.SelectedValue,
                        dtpFechaDesde.SelectedDate.Value,
                        dtpFechaHasta.SelectedDate.Value,
                        txtTextoBusqueda.Text,
                        cmbSexo.SelectedValue,
                        cmbEstadoCivil.SelectedValue,
                        cmbMinisterio.SelectedValue);

                        gvDatos.DataSource = DT_DATOS;
                        gvDatos.DataBind();
                    }
                    else
                    {
                        Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Fecha Desde y Fechas Hasta deben tener valores válidos", "warning");
                    }
                }
                else
                {
                    Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Fecha Desde y Fechas Hasta deben tener valores válidos", "warning");
                }
            }
            else
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Fecha Desde y Fechas Hasta deben tener valores válidos", "warning");
            }
        }

        private void LlenerCombos()
        {
            DataTable dt = new DataTable();

            // Ministerio
            Ministerio_N Ministerio_N = new Ministerio_N();
            dt = Ministerio_N.ListaCombo("0", false);
            cmbMinisterio.DataSource = dt;
            cmbMinisterio.DataValueField = "Id_Ministerio";
            cmbMinisterio.DataTextField = "Nombre_Ministerio";
            cmbMinisterio.DataBind();
        }

        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Consulta de miembros");
                LlenerCombos();
                LimpiarFiltros();
            }
        }

        protected void gvDatos_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            ActualizarGrid();
        }

        protected void gvDatos_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            ActualizarGrid();
        }

        protected void gvDatos_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            ActualizarGrid();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string Id_Miembro;
            Id_Miembro = btn.CommandArgument.ToString();

            Response.Redirect(Utilidad_N.ObtenerRutaServer() + "Miembros/frmMiembros.aspx?Id_Miembro=" + Id_Miembro);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Consultar();
        }
       
        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {

        }

        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}