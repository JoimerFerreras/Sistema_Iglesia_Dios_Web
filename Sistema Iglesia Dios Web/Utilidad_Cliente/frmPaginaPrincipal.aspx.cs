using System;
using System.Web.UI;

namespace Sistema_Iglesia_Dios_Web.Utilidad_Cliente
{
    public partial class frmPaginaPrincipal : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("");
            }
        }
        #endregion
    }
}