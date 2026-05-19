// Autor: Joimer Ferreras

using Entidades.Ministerios;
using Negocio.Ministerios;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Miembros;

namespace Sistema_Iglesia_Dios_Web.Ministerios
{
    [CodigoFuncionalidad("Ministerios")]
    public partial class frmMinisterios : System.Web.UI.Page
    {
        #region Declaraciones
        Ministerio_N Ministerio_N = new Ministerio_N();
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

        private void LlenerCombos()
        {
            DataTable dt = new DataTable();

            // Miembro
            Miembro_N Miembro_N = new Miembro_N();
            dt = Miembro_N.ListaCombo();
            cmbLiderMinisterio.DataSource = dt;
            cmbLiderMinisterio.DataValueField = "Id_Miembro";
            cmbLiderMinisterio.DataTextField = "Nombre_Miembro";
            cmbLiderMinisterio.DataBind();

            cmbDiaconoMinisterio.DataSource = dt;
            cmbDiaconoMinisterio.DataValueField = "Id_Miembro";
            cmbDiaconoMinisterio.DataTextField = "Nombre_Miembro";
            cmbDiaconoMinisterio.DataBind();
        }
        #endregion


        #region Metodos/ Procedimientos
        private void Consultar()
        {
            if (EvaluarAccionPermiso(0) == false)
            {
                Utilidad_C.MostrarAlerta_AccionDenegada(this, this.GetType());
                return;
            }

            DT_DATOS = Ministerio_N.Listar();

            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private bool ValidarCampos()
        {
            bool Validacion = false;

            if (txtNombreMinisterio.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El campo descripcion no puede estar vacío");
            }
            else
            {
                Validacion = true;
            }

            return Validacion;
        }

        private void GuardarRegistro()
        {
            if (EvaluarAccionPermiso(1) == false)
            {
                Utilidad_C.MostrarAlerta_AccionDenegada(this, this.GetType());
                return;
            }

            try
            {
                if (ValidarCampos() == true)
                {
                    // Agregacion de la informacion basica del miembro
                    Ministerio_E Ministerio_E = new Ministerio_E();
                    Ministerio_E.Id_Ministerio = int.Parse(ID_REGISTRO);
                    Ministerio_E.Nombre_Ministerio = txtNombreMinisterio.Text;
                    Ministerio_E.Estado = Convert.ToBoolean(cmbEstado.SelectedValue);
                    Ministerio_E.Id_Lider_Ministerio = int.Parse(cmbLiderMinisterio.SelectedValue);
                    Ministerio_E.Id_Diacono_Ministerio = int.Parse(cmbDiaconoMinisterio.SelectedValue);

                    if (EDITAR_REGISTRO == true)
                    {
                        
                        // Guardar registro existente
                        bool salida = Ministerio_N.Editar(Ministerio_E);

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
                        bool salida = Ministerio_N.Agregar(Ministerio_E);

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
            Ministerio_E Ministerio_E = new Ministerio_E();
            Ministerio_E = Ministerio_N.ObtenerRegistro(ID_REGISTRO);

            txtId_Descripcion.Text = Ministerio_E.Id_Ministerio.ToString();
            txtNombreMinisterio.Text = Ministerio_E.Nombre_Ministerio.ToString();
            cmbEstado.SelectedValue = Ministerio_E.Estado.ToString();
            cmbLiderMinisterio.SelectedValue = Ministerio_E.Id_Lider_Ministerio.ToString();
            cmbDiaconoMinisterio.SelectedValue = Ministerio_E.Id_Diacono_Ministerio.ToString();
        }

        private void Eliminar(int Id_Registro)
        {
            if (EvaluarAccionPermiso(2) == false)
            {
                Utilidad_C.MostrarAlerta_AccionDenegada(this, this.GetType());
                return;
            }

            if (Id_Registro == 0)
            {
                Utilidad_C.MostrarAlerta_Eliminar_Error(this, this.GetType(), "Primero seleccione un registro para poder eliminarlo");
            }
            else
            {
                if (Ministerio_N.RegistrosExistentes(Id_Registro) == false)
                {
                    bool respuesta = Ministerio_N.Eliminar(Id_Registro);

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

            txtId_Descripcion.Text = "(Nuevo)";
            txtNombreMinisterio.Text = "";
            cmbEstado.SelectedValue = "True";

            txtNombreMinisterio.Focus();
        }

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                // Permisos *************************
                ObtenerPermisos();
                if (EvaluarAccionPermiso(0) == false)
                {
                    ((SiteMaster)Master).IrPantallaPrincipal();
                }
                // **********************************

                ((SiteMaster)Master).EstablecerNombrePantalla("Ministerios");
                LimpiarCampos();

                LlenerCombos();
                Consultar();
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

        // Estas dos funciones se utilizan para asignarle clases css de color rojo o verde a los items de la columan "Estado" en los Grid
        protected string GetStatusText(object status)
        {
            string statusText = status.ToString();
            return statusText == "Activo" ? "Activo" : "Inactivo";
        }

        protected string GetStatusColor(object status)
        {
            string statusText = status.ToString();
            return statusText == "Activo" ? "status-green" : "status-red";
        }
        #endregion

        protected void cmbTipoDescripcion_Consulta_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Consultar();
        }
    }
}