using Entidades.Usuarios;
using Negocio.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using Microsoft.Ajax.Utilities;

namespace Sistema_Iglesia_Dios_Web
{
    public partial class frmLogin : System.Web.UI.Page
    {
        #region Declaraciones
        Usuario_N usuario_N = new Usuario_N();

        public Usuario_E Entidad_Usuario
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["Entidad_Usuario"]))
                {
                    ViewState["Entidad_Usuario"] = new Usuario_E();
                }
                return (Usuario_E)ViewState["Entidad_Usuario"];
            }
            set
            {
                ViewState["Entidad_Usuario"] = value;
            }
        }
        #endregion

        #region Metodos/ Funciones 
        private void GuardarPasswordCookie()
        {
            if (chkRecordarPassword.Checked == true)
            {
                // Verificar si la cookie no existe
                if (Request.Cookies["CheckRecordarPassword"] == null)
                {
                    // Crear una nueva cookie
                    HttpCookie cookie1 = new HttpCookie("Username");
                    HttpCookie cookie2 = new HttpCookie("Password");
                    HttpCookie cookie3 = new HttpCookie("CheckRecordarPassword");
                    cookie1.Value = Convert.ToBase64String(Utilidad_N.Encriptar(txtUsuario.Text));
                    cookie2.Value = Convert.ToBase64String(Utilidad_N.Encriptar(txtPassword.Text));
                    cookie3.Value = chkRecordarPassword.Checked.ToString();

                    // Establecer la duración de la cookie en días (opcional)
                    cookie1.Expires = DateTime.Now.AddMonths(1);
                    cookie2.Expires = DateTime.Now.AddMonths(1);
                    cookie3.Expires = DateTime.Now.AddMonths(1);

                    // Agregar la cookie a la respuesta del servidor
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);
                    Response.Cookies.Add(cookie3);
                }
                // Verificar si la cookie existe
                else if (Request.Cookies["CheckRecordarPassword"] != null)
                {
                    HttpCookie cookie1 = new HttpCookie("Username");
                    HttpCookie cookie2 = new HttpCookie("Password");
                    HttpCookie cookie3 = new HttpCookie("CheckRecordarPassword");
                    cookie1.Value = Convert.ToBase64String(Utilidad_N.Encriptar(txtUsuario.Text));
                    cookie2.Value = Convert.ToBase64String(Utilidad_N.Encriptar(txtPassword.Text));
                    cookie3.Value = chkRecordarPassword.Checked.ToString();

                    // Establecer la duración de la cookie en días (opcional)
                    cookie1.Expires = DateTime.Now.AddMonths(1);
                    cookie2.Expires = DateTime.Now.AddMonths(1);
                    cookie3.Expires = DateTime.Now.AddMonths(1);

                    // Agregar la cookie a la respuesta del servidor
                    Response.Cookies.Set(cookie1);
                    Response.Cookies.Set(cookie2);
                    Response.Cookies.Set(cookie3);
                }
            }
            else
            {
                if (Request.Cookies["CheckRecordarPassword"] != null)
                {
                    HttpCookie cookie1 = new HttpCookie("Username");
                    HttpCookie cookie2 = new HttpCookie("Password");
                    HttpCookie cookie3 = new HttpCookie("CheckRecordarPassword");
                    cookie1.Value = null;
                    cookie2.Value = null;
                    cookie3.Value = null;

                    // Establecer la duración de la cookie en días (opcional)
                    cookie1.Expires = DateTime.Now.AddMonths(1);
                    cookie2.Expires = DateTime.Now.AddMonths(1);
                    cookie3.Expires = DateTime.Now.AddMonths(1);

                    // Agregar la cookie a la respuesta del servidor
                    Response.Cookies.Set(cookie1);
                    Response.Cookies.Set(cookie2);
                    Response.Cookies.Set(cookie3);
                }
            }
        }

        private void LeerCookies()
        {
            if (Request.Cookies["CheckRecordarPassword"] == null)
            {
                txtUsuario.Text = "";
                txtPassword.Text = "";
                chkRecordarPassword.Checked = false;
            }
            else
            {
                HttpCookie cookie1 = Request.Cookies["Username"];
                HttpCookie cookie2 = Request.Cookies["Password"];
                HttpCookie cookie3 = Request.Cookies["CheckRecordarPassword"];
                if (cookie1 != null)
                {
                    txtUsuario.Text = Utilidad_N.Desencriptar(Convert.FromBase64String(cookie1.Value.ToString())).ToString();
                }
                if (cookie2 != null)
                {
                    string PasswordDesencriptada = Utilidad_N.Desencriptar(Convert.FromBase64String(cookie2.Value.ToString()));
                    txtPassword.Text = PasswordDesencriptada;
                    txtPassword.Attributes["Value"] = PasswordDesencriptada;
                }


                if (cookie3 != null && cookie3.Value == "True")
                {
                    chkRecordarPassword.Checked = true;
                }
                else
                {
                    chkRecordarPassword.Checked = false;
                }
            }
        }

        private void Login()
        {
            // Aqui se evalua los textboxs
            if (txtUsuario.Text.Length == 0)
            {
                Utilidad_C.EjecutarScript(this, "CambiarBorderColor", "var input = document.getElementById('" + txtUsuario.ClientID + @"'); input.style.borderColor = 'red';", false);
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo iniciar sesión", "El usuario no puede estar vacío", "warning");
            }
            else if (txtPassword.Text.Length == 0)
            {
                Utilidad_C.EjecutarScript(this, "CambiarBorderColor", "var input = document.getElementById('" + txtPassword.ClientID + @"'); input.style.borderColor = 'red';", false);
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo iniciar sesión", "La contraseña no puede estar vacía", "warning");
            }
            else
            {
                // Se realiza el login buscando coincidencias con las credenciales proporciandas en los textboxs
                var usuario = usuario_N.Login(txtUsuario.Text);

                // Si el usuario es diferente a 0, entonces se encontro una conincidencia
                if (usuario.Id_Usuario != 0)
                {
                    // Validar contraseña digitada con la contraseña del usuario encontrado
                    if (txtPassword.Text == Utilidad_N.Desencriptar(usuario.Password))
                    {
                        // Si el estatus de bloqueo del usuario es falso significa que el usuario fue deshabilitado y no podrá ingresar a la aplicacion
                        if (usuario.Bloqueo == true)
                        {
                            Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo iniciar sesión", "Su usuario ha sido deshabilitado", "warning");
                        }
                        // De lo contrario, se guardaran la informacion necesaria del usuario en variables de sesion y se redireciona a la pantalla principal
                        else
                        {
                            // Variables de sesión de usuario
                            Session["ID_USUARIO_SESSION"] = usuario.Id_Usuario.ToString();
                            Session["USERNAME_SESSION"] = usuario.Usuario.ToString();
                            Session["ID_ROL_SESSION"] = usuario.Id_Rol.ToString();
                            Session["EMAIL_USUARIO_SESSION"] = usuario.Correo.ToString();
                            Session["BLOQUEO_USUARIO_SESSION"] = usuario.Bloqueo.ToString();

                            GuardarPasswordCookie();
                            Response.Redirect(Utilidad_N.ObtenerRutaServer() + "/Utilidad_Cliente/frmPaginaPrincipal.aspx");
                        }
                    }
                    else
                    {
                        // De lo contrario, devolverá una alerta de credenciales incorrectras
                        string script = @"var input1 = document.getElementById('" + txtUsuario.ClientID + @"');
                                    input1.style.borderColor = 'red'; 
                                    var input2 = document.getElementById('" + txtPassword.ClientID + @"');
                                    input2.style.borderColor = 'red';";

                        Utilidad_C.EjecutarScript(this, "CambiarBorderColor", script, false);
                        Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo iniciar sesión", "El usuario o la contraseña son incorrectos", "warning");
                    }
                }
                else
                {
                    // De lo contrario, devolverá una alerta de credenciales incorrectras
                    string script = @"var input1 = document.getElementById('" + txtUsuario.ClientID + @"');
                                    input1.style.borderColor = 'red'; 
                                    var input2 = document.getElementById('" + txtPassword.ClientID + @"');
                                    input2.style.borderColor = 'red';";

                    Utilidad_C.EjecutarScript(this, "CambiarBorderColor", script, false);
                    Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo iniciar sesión", "El usuario o la contraseña son incorrectos", "warning");
                }
            }
        }
        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPassword.Attributes["Value"] = txtPassword.Text;

            // Aqui se reinician las variables de sesion
            Session["ID_USUARIO_SESSION"] = "0";
            Session["USERNAME_SESSION"] = "";
            Session["ID_ROL_SESSION"] = "";
            Session["EMAIL_USUARIO_SESSION"] = "";
            Session["BLOQUEO_USUARIO_SESSION"] = "";

            // Aqui se le asigna el valor del texto a su atributo Value del txtPassword
            if (!Page.IsPostBack)
            {
                LeerCookies();
            }

            // Aqui se registra el script para controlar los colores de los textbox
            string script = @"
                function cambiarColorBorde(textBox) {
                    // Agregar un evento al TextBox para que se active cuando cambie su contenido
                    textBox.addEventListener('input', function () {
                        // Verificar si la longitud del texto es mayor a 0
                        if (textBox.value.length > 0) {
                            // Cambiar el color del borde cuando la longitud sea mayor a 0
                            textBox.style.borderColor = '#DEE2E6'; // Puedes cambiar 'red' por el color que desees
                        } else {
                            // Restablecer el color del borde cuando la longitud sea 0
                            textBox.style.borderColor = ''; // Esto eliminará el estilo de borde personalizado
                        }
                    });
                }

                // Obtener los TextBox mediante sus IDs
                var textBox1 = document.getElementById('" + txtUsuario.ClientID + @"');
                var textBox2 = document.getElementById('" + txtPassword.ClientID + @"');

                // Verificar si los TextBox se encontraron en el documento
                if (textBox1) {
                    cambiarColorBorde(textBox1);
                }

                if (textBox2) {
                    cambiarColorBorde(textBox2);
                }";
            Utilidad_C.EjecutarScript(this, "CambiarColorBordeScript", script, false);
        }
      
        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Login();
        }

        protected void btnMostrarPassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.TextMode == TextBoxMode.Password)
            {
                txtPassword.TextMode = TextBoxMode.SingleLine;
                btnMostrarPassword.CssClass = btnMostrarPassword.CssClass.Replace("fa-eye", "fa-eye-slash");
            }
            else
            {
                txtPassword.TextMode = TextBoxMode.Password;
                btnMostrarPassword.CssClass = btnMostrarPassword.CssClass.Replace("fa-eye-slash", "fa-eye");
            }
        }

        protected void btOlvidarPassword_Click(object sender, EventArgs e)
        {
            //if (txtUsuario.Text.Length > 0)
            //{
            //    if (usuario_N.ObtenerExistenciaUsuario(txtUsuario.Text) == true)
            //    {
            //        Session["USUARIO_VERIFICACION"] = txtUsuario.Text;
            //        Response.Redirect(Utilidad_N.ObtenerRutaServer() + "Usuarios/frmRecuperarPassword.aspx");
            //    }
            //    else
            //    {
            //        Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo verificar el usuario", "No se encontró el usuario proporcionado", "warning");
            //    }
            //}
            //else
            //{
            //    Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Debe digitar el nombre de usuario", "", "warning");
            //}
        }
        #endregion
    }
}