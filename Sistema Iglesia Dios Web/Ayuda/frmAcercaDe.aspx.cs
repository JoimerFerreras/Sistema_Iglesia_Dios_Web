// Autor: Joimer Ferreras

using Entidades.Usuarios;
using Entidades.Util_E;
using Negocio.Ingresos;
using Negocio.Usuarios;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Skins;

namespace Sistema_Iglesia_Dios_Web.Ayuda
{
    [CodigoFuncionalidad("Informacion_Sistema")]
    public partial class frmAcercaDe : System.Web.UI.Page
    {
        #region Declaraciones
        Log_Usuario_Acceso_N Log_Usuario_Acceso_N = new Log_Usuario_Acceso_N();
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
            DataTable dt = Utilidad_C.ObtenerPermisos_RolFuncionalidad(Utilidad_C.ObtenerRolUsuarioSession(this), Utilidad_C.ObtenerCodigoPantalla(this));
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

        private bool EvaluarAccionPermiso(int Id_Accion)
        {
            bool Validacion = false;

            if (Id_Accion >= 0 && Id_Accion <= 2)
            {
                Validacion = PERMISOS[Id_Accion];
            }

            return Validacion;
        }
        #endregion


        #region Metodos/ Procedimientos
        public static void RecargarTooltips(Page pagina, Type type)
        {
            string script = @" <script>
            tippy('.btn_copiar_correo', {
                content: 'Copiar correo',
                placement: 'right',
                arrow: true,
            });
        </script>";

            ScriptManager.RegisterStartupScript(pagina, type.GetType(), "RecargarTooltips", script, false);
        }

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                // Permisos *************************
                ObtenerPermisos();
                if (EvaluarAccionPermiso(0) == false)
                {
                    ((SiteMaster)Master).IrPantallaPrincipal();
                }
                // **********************************

                ((SiteMaster)Master).EstablecerNombrePantalla("Información del sistema");

            }
        }
        #endregion

        protected void btnDescargarManualUsuairo_Click(object sender, EventArgs e)
        {

        }

        protected void btnDescargarManualUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}