using Negocio.Util_N;
using System;
using System.Collections.Generic;
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
                //if (Session["TIPO_USUARIO_SESSION"] != null && Session["TIPO_USUARIO_SESSION"].ToString() == "1")
                //{
                //    lblRolUsuario.Text = "Administrador";
                //}
                //else if (Session["TIPO_USUARIO_SESSION"] != null && Session["TIPO_USUARIO_SESSION"].ToString() == "2")
                //{
                //    lblRolUsuario.Text = "Operativo";
                //    btnConfiguracion_Usuarios.Visible = false;
                //    btnCambiarPassword.HRef = Utilidad_N.ObtenerRutaServer() + "Usuarios/frmCambiarPassword.aspx";
                //}
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
        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            EvaluarSesion();
        }

        #endregion
    }
}