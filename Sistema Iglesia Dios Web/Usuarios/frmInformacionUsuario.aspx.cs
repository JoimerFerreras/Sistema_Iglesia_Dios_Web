// Autor: Joimer Ferreras

using Entidades.Usuarios;
using Negocio.Usuarios;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Data;

namespace Sistema_Iglesia_Dios_Web.Usuarios
{
    [CodigoFuncionalidad("Informacion_Usuario")]
    public partial class frmInformacionUsuario : System.Web.UI.Page
    {
        #region Declaraciones

        private readonly Usuario_N Usuario_N = new Usuario_N();

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

        #endregion

        #region Permisos

        public bool[] PERMISOS
        {
            get
            {
                if (ViewState["PERMISOS"] == null)
                {
                    ViewState["PERMISOS"] = new bool[3];
                }

                return (bool[])ViewState["PERMISOS"];
            }
            set
            {
                ViewState["PERMISOS"] = value;
            }
        }

        private void ObtenerPermisos()
        {
            DataTable dt = Utilidad_C.ObtenerPermisos_RolFuncionalidad(
                Utilidad_C.ObtenerRolUsuarioSession(this),
                Utilidad_C.ObtenerCodigoPantalla(this)
            );

            if (dt.Rows.Count > 0)
            {
                PERMISOS[0] = dt.Rows[0].Field<bool>("Permiso_Visualizar");
                PERMISOS[1] = dt.Rows[0].Field<bool>("Permiso_Editar");
                PERMISOS[2] = dt.Rows[0].Field<bool>("Permiso_Eliminar");
            }
            else
            {
                PERMISOS[0] = false;
                PERMISOS[1] = false;
                PERMISOS[2] = false;
            }
        }

        private bool EvaluarAccionPermiso(int idAccion)
        {
            if (idAccion < 0 || idAccion > 2)
            {
                return false;
            }

            return PERMISOS[idAccion];
        }

        #endregion

        #region Métodos

        private void ConfigurarCamposSoloLectura()
        {
            txtIdUsuario.ReadOnly = true;

            txtNombre1.ReadOnly = true;
            txtNombre2.ReadOnly = true;
            txtApellido1.ReadOnly = true;
            txtApellido2.ReadOnly = true;

            txtCelular.ReadOnly = true;
            txtTelefono.ReadOnly = true;

            txtSexo.Enabled = false;

            txtUsuario.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            txtRolUsuario.ReadOnly = true;
        }

        private void ObtenerInformacionUsuario()
        {
            if (!EvaluarAccionPermiso(0))
            {
                Utilidad_C.MostrarAlerta_AccionDenegada(
                    this,
                    this.GetType()
                );

                return;
            }

            try
            {
                DataTable dt =
                    Usuario_N.ObtenerUsuario_VerInformacion(ID_REGISTRO);

                if (dt == null || dt.Rows.Count == 0)
                {
                    Utilidad_C.MostrarAlerta_Personalizada(
                        this,
                        this.GetType(),
                        "Error al consultar sus datos",
                        "No fue posible obtener la información del usuario.",
                        "error"
                    );

                    return;
                }

                DataRow row = dt.Rows[0];

                txtIdUsuario.Text = row["Id_Usuario"].ToString();
                txtNombre1.Text = row["Nombre1"].ToString();
                txtNombre2.Text = row["Nombre2"].ToString();
                txtApellido1.Text = row["Apellido1"].ToString();
                txtApellido2.Text = row["Apellido2"].ToString();
                txtCelular.Text = row["Celular"].ToString();
                txtTelefono.Text = row["Telefono"].ToString();

                txtSexo.Text = row["Sexo"].ToString();

                txtRolUsuario.Text = row["Rol"].ToString();
                txtCorreo.Text = row["Correo"].ToString();
                txtUsuario.Text = row["Usuario"].ToString();
            }
            catch (Exception ex)
            {
                Utilidad_C.MostrarAlerta_Personalizada(
                    this,
                    this.GetType(),
                    "Error al consultar sus datos",
                    "Ocurrió un error al obtener la información: " +
                    ex.Message,
                    "error"
                );
            }
        }

        private void MostrarFormularioPassword()
        {
            pnlCambiarPassword.Visible = true;
            btnCambiarPassword.Visible = false;
        }

        private void OcultarFormularioPassword()
        {
            txtNuevaPassword.Text = "";
            txtRepetirPassword.Text = "";

            pnlCambiarPassword.Visible = false;
            btnCambiarPassword.Visible = true;
        }

        private bool ValidarCambioPassword()
        {
            string nuevaPassword = txtNuevaPassword.Text;
            string repetirPassword = txtRepetirPassword.Text;

            if (string.IsNullOrWhiteSpace(nuevaPassword))
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(
                    this,
                    this.GetType(),
                    "El campo Nueva contraseña no puede estar vacío."
                );

                return false;
            }

            if (string.IsNullOrWhiteSpace(repetirPassword))
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(
                    this,
                    this.GetType(),
                    "El campo Repetir contraseña no puede estar vacío."
                );

                return false;
            }

            if (nuevaPassword != repetirPassword)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(
                    this,
                    this.GetType(),
                    "Las contraseñas no coinciden."
                );

                return false;
            }

            if (nuevaPassword.Length < 8)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(
                    this,
                    this.GetType(),
                    "La contraseña debe tener un mínimo de 8 caracteres."
                );

                return false;
            }

            if (!Utilidad_N.ValidarPassword(nuevaPassword))
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(
                    this,
                    this.GetType(),
                    "La contraseña debe incluir letras mayúsculas, " +
                    "letras minúsculas, números y un carácter especial."
                );

                return false;
            }

            return true;
        }

        private void CambiarPassword()
        {
            if (!EvaluarAccionPermiso(1))
            {
                Utilidad_C.MostrarAlerta_AccionDenegada(
                    this,
                    this.GetType()
                );

                return;
            }

            MostrarFormularioPassword();

            if (!ValidarCambioPassword())
            {
                return;
            }

            try
            {
                /*
                 * Obtiene el usuario completo para evitar que el método
                 * Editar coloque en blanco los demás campos.
                 */
                Usuario_E usuario =
                    Usuario_N.ObtenerRegistro(ID_REGISTRO);

                if (usuario == null)
                {
                    Utilidad_C.MostrarAlerta_Personalizada(
                        this,
                        this.GetType(),
                        "Error",
                        "No se encontró la información del usuario.",
                        "error"
                    );

                    return;
                }

                usuario.Password =
                    Utilidad_N.Encriptar(txtNuevaPassword.Text);

                usuario.Fecha_Ultima_Modificacion =
                    DateTime.Now;

                bool salida = Usuario_N.Editar(usuario);

                if (salida)
                {
                    OcultarFormularioPassword();

                    Utilidad_C.MostrarAlerta_Personalizada(
                        this,
                        this.GetType(),
                        "Contraseña actualizada",
                        "La contraseña se actualizó correctamente.",
                        "success"
                    );
                }
                else
                {
                    Utilidad_C.MostrarAlerta_Personalizada(
                        this,
                        this.GetType(),
                        "Error",
                        "No fue posible actualizar la contraseña.",
                        "error"
                    );
                }
            }
            catch (Exception ex)
            {
                MostrarFormularioPassword();

                Utilidad_C.MostrarAlerta_Personalizada(
                    this,
                    this.GetType(),
                    "Error al cambiar la contraseña",
                    "Ocurrió un error al actualizar la contraseña: " +
                    ex.Message,
                    "error"
                );
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(
                this,
                this.GetType()
            );

            ConfigurarCamposSoloLectura();

            if (!Page.IsPostBack)
            {
                ObtenerPermisos();

                if (!EvaluarAccionPermiso(0))
                {
                    ((SiteMaster)Master).IrPantallaPrincipal();
                    return;
                }

                /*
                 * Obtiene el ID del usuario que inició sesión.
                 * Ya no se establece ID_REGISTRO en "0".
                 */
                ID_REGISTRO =
                    Utilidad_C.ObtenerUsuarioSession(this.Page);

                ((SiteMaster)Master).EstablecerNombrePantalla(
                    "Mi información"
                );

                OcultarFormularioPassword();
                ObtenerInformacionUsuario();
            }
        }

        protected void btnCambiarPassword_Click(
            object sender,
            EventArgs e)
        {
            txtNuevaPassword.Text = "";
            txtRepetirPassword.Text = "";

            MostrarFormularioPassword();
            txtNuevaPassword.Focus();
        }

        protected void btnGuardarPassword_Click(
            object sender,
            EventArgs e)
        {
            CambiarPassword();
        }

        protected void btnCancelarCambioPassword_Click(
            object sender,
            EventArgs e)
        {
            OcultarFormularioPassword();
        }

        #endregion
    }
}