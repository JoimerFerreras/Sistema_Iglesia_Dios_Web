using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Iglesia_Dios_Web.Utilidad_Cliente
{
    public partial class frmPaginaPrincipal : System.Web.UI.Page
    {
        #region Metodos/Funciones
        private void EstablecerNombrePantalla()
        {
            if (Master != null)
            {
                try
                {
                    Label lblMensaje = (Label)Master.FindControl("lblNombrePantalla");
                    if (lblMensaje != null)
                    {
                        lblMensaje.Text = "";
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                EstablecerNombrePantalla();
            }
        }
        #endregion
    }
}