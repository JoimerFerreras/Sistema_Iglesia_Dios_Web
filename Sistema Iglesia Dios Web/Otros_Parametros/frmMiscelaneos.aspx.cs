// Autor: Joimer Ferreras

using Entidades.Otros_Parametros;
using Negocio.Otros_Parametros;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Iglesia_Dios_Web.Otros_Parametros
{
    [CodigoFuncionalidad("Miscelaneos")]
    public partial class frmMiscelaneos : System.Web.UI.Page
    {
        #region Declaraciones
        Miscelaneo_N Miscelaneo_N = new Miscelaneo_N();
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
        #endregion


        #region Metodos/ Procedimientos
        private void Consultar()
        {
            if (EvaluarAccionPermiso(0) == false)
            {
                Utilidad_C.MostrarAlerta_AccionDenegada(this, this.GetType());
                return;
            }

            DT_DATOS = Miscelaneo_N.Listar();

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

            if (txtDescripcion.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La descripción no puede estar vacía");
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
                    Miscelaneo_E Miscelaneo_E = new Miscelaneo_E();
                    Miscelaneo_E.Id_Miscelaneo = int.Parse(ID_REGISTRO);
                    Miscelaneo_E.Descripcion_Miscelaneo = txtDescripcion.Text;
                    Miscelaneo_E.Estado = Convert.ToBoolean(cmbEstado.SelectedValue);

                    if (EDITAR_REGISTRO == true)
                    {
                        
                        // Guardar registro existente
                        bool salida = Miscelaneo_N.Editar(Miscelaneo_E);

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
                        bool salida = Miscelaneo_N.Agregar(Miscelaneo_E);

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
            Miscelaneo_E Miscelaneo_E = new Miscelaneo_E();
            Miscelaneo_E = Miscelaneo_N.ObtenerRegistro(ID_REGISTRO);

            txtId_Miscelaneo.Text = Miscelaneo_E.Id_Miscelaneo.ToString();
            txtDescripcion.Text = Miscelaneo_E.Descripcion_Miscelaneo.ToString();
            cmbEstado.SelectedValue = Miscelaneo_E.Estado.ToString();
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
                if (Miscelaneo_N.RegistrosExistentes(Id_Registro) == false)
                {
                    bool respuesta = Miscelaneo_N.Eliminar(Id_Registro);

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

            txtId_Miscelaneo.Text = "(Nuevo)";
            txtDescripcion.Text = "";
            cmbEstado.SelectedValue = "True";

            txtDescripcion.Focus();
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

                ((SiteMaster)Master).EstablecerNombrePantalla("Misceláneos");
                LimpiarCampos();
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
    }
}