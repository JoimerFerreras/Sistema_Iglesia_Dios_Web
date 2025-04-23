// Autor: Joimer Ferreras

using Negocio.Usuarios;
using Entidades.Usuarios;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Iglesia_Dios_Web.Usuarios
{
    [CodigoFuncionalidad("Usuarios")]
    public partial class frmUsuarios : System.Web.UI.Page
    {
        #region Declaraciones
        Usuario_N Usuario_N = new Usuario_N();
        public string ID_REGISTRO
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["ID_REGISTRO"]))
                {
                    return "";
                }
                return ViewState["ID_REGISTRO"].ToString();
            }
            set
            {
                ViewState["ID_REGISTRO"] = value;
            }
        }

        public bool EDITAR_REGISTRO
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["EDITAR_REGISTRO"]))
                {
                    ViewState["EDITAR_REGISTRO"] = new bool();
                }
                return (bool)ViewState["EDITAR_REGISTRO"];
            }
            set
            {
                ViewState["EDITAR_REGISTRO"] = value;
            }
        }

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
        private void Consultar()
        {
            DT_DATOS = Usuario_N.Listar(cmbRol_Filtro.SelectedValue);

            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private void LlenerCombos()
        {
            DataTable dt = new DataTable();

            // Roles
            Rol_N Rol_N = new Rol_N();
            dt = Rol_N.ListaCombo();
            cmbRol_Filtro.DataSource = dt;
            cmbRol_Filtro.DataValueField = "Id_Rol";
            cmbRol_Filtro.DataTextField = "Nombre_Rol";
            cmbRol_Filtro.DataBind();

            cmbRol.DataSource = dt;
            cmbRol.DataValueField = "Id_Rol";
            cmbRol.DataTextField = "Nombre_Rol";
            cmbRol.DataBind();
        }

        private bool ValidarCampos()
        {
            bool Validacion = false;

            if (txtNombre1.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El campo Primer nombre no puede estar vacío");
            }
            else if (txtApellido1.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El campo Primer apellido no puede estar vacío");
            }
            else if (cmbSexo.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe seleccionar un sexo");
            }
            else if (txtUsuario.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El campo Nombre de usuario no puede estar vacío");
            }
            else if (chkVerificacionDosPasos.Checked == true && txtCorreo.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Para habilitar la verificación en dos pasos es necesario proporcionar un correo electrónico válido");
            }
            else if (chkVerificacionDosPasos.Checked == true && txtCorreo.Text.Length > 0 && Utilidad_N.ValidarEmail(txtCorreo.Text) == false)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El correo electrónico no es válido");
            }
            else if (txtPassword.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La contraseña no puede estar vacía");
            }
            else if (txtPassword.Text.Length > 0 && txtPassword.Text.Length < 8)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La contraseña debe tener un mínimo de 8 caracteres");
            }
            else if (Utilidad_N.ValidarPassword(txtPassword.Text) == false)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La contraseña no es válida");
            }
            else if (cmbRol.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe seleccionar un rol");
            }
            else
            {
                Validacion = true;
            }

            return Validacion;
        }

        private void GuardarRegistro()
        {
            try
            {
                if (ValidarCampos() == true)
                {
                    Usuario_E Usuario_E = new Usuario_E();
                    Usuario_E.Id_Usuario = int.Parse(ID_REGISTRO);
                    Usuario_E.Nombre1 = txtNombre1.Text;
                    Usuario_E.Nombre2 = txtNombre2.Text;
                    Usuario_E.Apellido1 = txtApellido1.Text;
                    Usuario_E.Apellido2 = txtApellido2.Text;
                    Usuario_E.Sexo = int.Parse(cmbSexo.SelectedValue);
                    Usuario_E.Celular = txtCelular.Text;
                    Usuario_E.Telefono = txtTelefono.Text;

                    Usuario_E.Id_Rol = int.Parse(cmbRol.SelectedValue);
                    Usuario_E.Correo = txtCorreo.Text;
                    Usuario_E.Usuario = txtUsuario.Text;
                    Usuario_E.Password = Utilidad_N.Encriptar(txtPassword.Text);
                    Usuario_E.Bloqueo = chkBloqueo.Checked;
                    Usuario_E.Verificacion_Dos_Pasos = chkVerificacionDosPasos.Checked;
                    Usuario_E.RestablecerPassword = chkRestablecerPassword.Checked;

                    Usuario_E.Fecha_Creacion = DateTime.Now;
                    Usuario_E.Fecha_Ultima_Modificacion = DateTime.Now;

                    if (EDITAR_REGISTRO == true)
                    {
                        // Guardar registro existente
                        bool salida = Usuario_N.Editar(Usuario_E);

                        if (salida == true)
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Success(this, this.GetType());
                            LimpiarCampos();
                            Consultar();
                        }
                        else
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Error(this, this.GetType());
                        }
                    }
                    else
                    {
                        // Agregar registro
                        bool salida = Usuario_N.Agregar(Usuario_E);

                        if (salida == true)
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Success(this, this.GetType());
                            LimpiarCampos();
                            Consultar();
                        }
                        else
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Error(this, this.GetType());
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Fatal(this, this.GetType());
            }

        }

        private void VerRegistro()
        {
            // Llenado de datos generales
            Usuario_E Usuario_E = new Usuario_E();
            Usuario_E = Usuario_N.ObtenerRegistro(ID_REGISTRO);

            txtIdUsuario.Text = Usuario_E.Id_Usuario.ToString();
            txtNombre1.Text = Usuario_E.Nombre1.ToString();
            txtNombre2.Text = Usuario_E.Nombre2.ToString();
            txtApellido1.Text = Usuario_E.Apellido1.ToString();
            txtApellido2.Text = Usuario_E.Apellido2.ToString();
            txtCelular.Text = Usuario_E.Celular.ToString();
            txtTelefono.Text = Usuario_E.Telefono.ToString();
            cmbSexo.SelectedValue = Usuario_E.Sexo.ToString();

            cmbRol.SelectedValue = Usuario_E.Id_Rol.ToString();
            txtCorreo.Text = Usuario_E.Correo.ToString();
            txtUsuario.Text = Usuario_E.Usuario.ToString();

            if (Usuario_E.Password != null)
            {
                txtPassword.Text = Utilidad_N.Desencriptar(Usuario_E.Password);
                txtPassword.Attributes["Value"] = Utilidad_N.Desencriptar(Usuario_E.Password);
            }

            chkBloqueo.Checked = Usuario_E.Bloqueo;
            chkVerificacionDosPasos.Checked = Usuario_E.Verificacion_Dos_Pasos;
            chkRestablecerPassword.Checked = Usuario_E.RestablecerPassword;

            txtFechaRegistro.Text = Usuario_E.Fecha_Creacion.ToString("dd/MM/yyyy hh:mm:ss tt");
            if (Usuario_E.Fecha_Ultima_Modificacion != null)
            {
                txtFechaUltimaModificacion.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Usuario_E.Fecha_Ultima_Modificacion);
            }

            rtsTabulador.Tabs[1].Selected = true;
            rmpTabs.SelectedIndex = 1;

            txtNombre1.Focus();
        }

        private void Eliminar(int Id_Registro)
        {
            if (Id_Registro == 0)
            {
                Utilidad_C.MostrarAlerta_Eliminar_Error(this, this.GetType(), "Primero seleccione un registro para poder eliminarlo");
            }
            else
            {
                if (Usuario_N.RegistrosExistentes(Id_Registro) == false)
                {
                    bool respuesta = Usuario_N.Eliminar(Id_Registro);

                    if (respuesta)
                    {
                        Utilidad_C.MostrarAlerta_Eliminar_Success(this, this.GetType());
                        Consultar();
                    }
                    else
                    {
                        Utilidad_C.MostrarAlerta_Eliminar_Error_Fatal(this, this.GetType());
                    }
                }
                else
                {
                    Utilidad_C.MostrarAlerta_Eliminar_Error(this, this.GetType(), $@"No se puede eliminar este registro porque está siendo utilizado por otra entidad. Sin embargo, puede establecer el Estado en ""Inactivo"" para deshabilitarlo sin eliminarlo definitivamente.");
                }

                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            ID_REGISTRO = "0";
            EDITAR_REGISTRO = false;

            txtIdUsuario.Text = "(Nuevo)";
            txtNombre1.Text = "";
            txtNombre2.Text = "";
            txtApellido1.Text = "";
            txtApellido2.Text = "";
            txtCelular.Text = "";
            txtTelefono.Text = "";
            cmbSexo.SelectedValue = "0";

            cmbRol.SelectedValue = "0";
            txtCorreo.Text = "";
            txtUsuario.Text = "";
            txtPassword.Text = "";
            txtPassword.Attributes["Value"] = "";
            chkBloqueo.Checked = false;
            chkVerificacionDosPasos.Checked = false;
            chkRestablecerPassword.Checked = false;

            txtFechaRegistro.Text = "";
            txtFechaUltimaModificacion.Text = "";

            txtNombre1.Focus();
        }

        private void LimpiarFiltros()
        {
            cmbRol_Filtro.SelectedValue = "0";
        }
        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());
            txtPassword.Attributes["Value"] = txtPassword.Text;

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Usuarios");
                LlenerCombos();
                LimpiarCampos();
                Consultar();

                string a = Utilidad_C.ObtenerCodigoPantalla(this);
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
            ID_REGISTRO = btn.CommandArgument.ToString();
            EDITAR_REGISTRO = true;
            VerRegistro();
        }
        
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarRegistro();
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int Id_Registro;
            Id_Registro = System.Convert.ToInt32(btn.CommandArgument.ToString());
            Eliminar(Id_Registro);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
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

        // Estas dos funciones se utilizan para asignarle clases css de color rojo o verde a los items de la columan "Estado" en los Grid
        protected string GetStatusText(object status)
        {
            string statusText = status.ToString();

            if (statusText == "Bloqueado")
            {
                return "Bloqueado";
            }
            else if(statusText == "Sin bloqueo")
            {
                return "Sin bloqueo";
            }
            else if (statusText == "Desactivado")
            {
                return "Desactivado";
            }
            else if (statusText == "Activado")
            {
                return "Activado";
            }
            else
            {
                return "";
            }
        }

        protected string GetStatusColor(object status)
        {
            string statusText = status.ToString();

            if (statusText == "Bloqueado")
            {
                return "status-green";
            }
            else if (statusText == "Activado")
            {
                return "status-green";
            }
            else
            {
                return "status-red";
            }
        }

        #endregion
    }
}