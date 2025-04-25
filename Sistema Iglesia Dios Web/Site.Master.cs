using Negocio.Util_N;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Iglesia_Dios_Web
{
    public partial class SiteMaster : MasterPage
    {

        #region Metodos/Funciones
        private void EvaluarSesion()
        {
            if (Session["ID_USUARIO_SESSION"] != null && Session["ID_USUARIO_SESSION"].ToString() != "0" && Utilidad_N.ValidarNull(Session["ID_USUARIO_SESSION"].ToString()) == false)
            {
                lblNombreUsuario.Text = Session["USERNAME_SESSION"].ToString();
                lblRolUsuario.Text = Session["NOMBRE_ROL_SESSION"].ToString();
            }
            else
            {
                CerrarSesion();
            }
        }

        public void EstablecerNombrePantalla(string NombrePantalla)
        {
            lblNombrePantalla.Text = NombrePantalla;
        }

        private void CerrarSesion()
        {
            Response.Redirect(Utilidad_N.ObtenerRutaServer() + "frmLogin.aspx");
        }

        // Método para encontrar el padre de un control de tipo Update panel para Notificaciones
        private T FindParent<T>(Control control) where T : Control
        {
            while (control != null && !(control is T))
            {
                control = control.Parent;
            }
            return control as T;
        }

        private void CargarNotificaciones()
        {
            
        }

       



        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            EvaluarSesion();

            if (!Page.IsPostBack)
            {
                
            }
        }
        protected void EliminarNotificacion_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int idNoti = int.Parse(btn.CommandArgument);

            // 🧩 Lógica de eliminación en base de datos
            // NotificacionDAO.Eliminar(idNoti);

            // 🔍 Buscar el Panel que representa la notificación
            Panel notiPanel = FindParent<Panel>(btn);
            if (notiPanel != null)
            {
                notiPanel.Visible = false;  // Ocultar solo el contenido visual
            }
        }
        #endregion
    }
}