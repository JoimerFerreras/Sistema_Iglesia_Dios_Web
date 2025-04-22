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
    public partial class frmDescripciones : System.Web.UI.Page
    {
        #region Declaraciones
        Descripciones_N Descripciones_N = new Descripciones_N();
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
            DT_DATOS = Descripciones_N.Listar(cmbTipoDescripcion_Consulta.SelectedValue);

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

            if (txtNombreDescripcion.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El nombre descripción no puede estar vacío");
            }
            else if (cmbTipoDescripcion.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe espesificar el módulo al que pertenece la descripción");
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
                    // Agregacion de la informacion basica del miembro
                    Descripciones_E Descripciones_E = new Descripciones_E();
                    Descripciones_E.Id_Descripcion = int.Parse(ID_REGISTRO);
                    Descripciones_E.Nombre = txtNombreDescripcion.Text;
                    Descripciones_E.Tipo_Descripcion = int.Parse(cmbTipoDescripcion.SelectedValue);
                    Descripciones_E.Estado = Convert.ToBoolean(cmbEstado.SelectedValue);

                    if (EDITAR_REGISTRO == true)
                    {
                        
                        // Guardar registro existente
                        bool salida = Descripciones_N.Editar(Descripciones_E);

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
                        bool salida = Descripciones_N.Agregar(Descripciones_E);

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
            Descripciones_E Descripciones_E = new Descripciones_E();
            Descripciones_E = Descripciones_N.ObtenerRegistro(ID_REGISTRO);

            txtId_Descripcion.Text = Descripciones_E.Id_Descripcion.ToString();
            txtNombreDescripcion.Text = Descripciones_E.Nombre.ToString();
            cmbTipoDescripcion.SelectedValue = Descripciones_E.Tipo_Descripcion.ToString();
            cmbEstado.SelectedValue = Descripciones_E.Estado.ToString();
        }

        private void Eliminar(int Id_Registro)
        {
            if (Id_Registro == 0)
            {
                Utilidad_C.MostrarAlerta_Eliminar_Error(this, this.GetType(), "Primero seleccione un registro para poder eliminarlo");
            }
            else
            {
                if (Descripciones_N.RegistrosExistentes(Id_Registro) == false)
                {
                    bool respuesta = Descripciones_N.Eliminar(Id_Registro);

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
            txtNombreDescripcion.Text = "";
            cmbTipoDescripcion.SelectedValue = "0";
            cmbEstado.SelectedValue = "True";

            txtNombreDescripcion.Focus();
        }

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Descripciones");
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

        protected void cmbTipoDescripcion_Consulta_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Consultar();
        }
    }
}