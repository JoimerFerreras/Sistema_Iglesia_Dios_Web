using Entidades.Usuarios;
using Negocio.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Negocio.Util_N;

namespace Sistema_Iglesia_Dios_Web
{
    public partial class frmLogin : System.Web.UI.Page
    {

        #region Declaraciones
        Usuario_N usuario_N = new Usuario_N();

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion


        private void Login()
        {
            var usuario = usuario_N.Login(txtUsuario.Text, txtPassword.Text);

            if (usuario.Id_Usuario != 0)
            {
                Response.Redirect(Utilidad_N.ObtenerRutaServer() + "/Utilidad_Cliente/frmPaginaPrincipal.aspx");
            }

        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Login();
        }

        protected void btnMostrarPassword_Click(object sender, EventArgs e)
        {

        }

        protected void btOlvidarPassword_Click(object sender, EventArgs e)
        {

        }
    }
}