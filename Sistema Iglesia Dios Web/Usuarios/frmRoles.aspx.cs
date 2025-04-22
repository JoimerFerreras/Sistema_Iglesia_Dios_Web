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
    public partial class frmRoles : System.Web.UI.Page
    {
        #region Declaraciones
        Rol_N Rol_N = new Rol_N();
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
            DT_DATOS = Rol_N.Listar();

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

            if (txtNombreRol.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El nombre del rol no puede estar vacío");
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
                    Rol_E Rol_E = new Rol_E();
                    Rol_E.Id_Rol = int.Parse(ID_REGISTRO);
                    Rol_E.Nombre_Rol = txtNombreRol.Text;
                    Rol_E.Fecha_Registro = DateTime.Now;
                    Rol_E.Fecha_Ultima_Modificacion = DateTime.Now;
                    Rol_E.Estado = Convert.ToBoolean(cmbEstado.SelectedValue);

                    if (EDITAR_REGISTRO == true)
                    {
                        
                        // Guardar registro existente
                        bool salida = Rol_N.Editar(Rol_E);

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
                        bool salida = Rol_N.Agregar(Rol_E);

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
            Rol_E Rol_E = new Rol_E();
            Rol_E = Rol_N.ObtenerRegistro(ID_REGISTRO);

            txtIdRol.Text = Rol_E.Id_Rol.ToString();
            txtNombreRol.Text = Rol_E.Nombre_Rol.ToString();
            txtFechaRegistro.Text = Rol_E.Fecha_Registro.ToString("dd/MM/yyyy hh:mm:ss tt");

            if (Rol_E.Fecha_Ultima_Modificacion != null)
            {
                txtFechaUltimaModificacion.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Rol_E.Fecha_Ultima_Modificacion);
            }
            cmbEstado.SelectedValue = Rol_E.Estado.ToString();
        }

        private void Eliminar(int Id_Registro)
        {
            if (Id_Registro == 0)
            {
                Utilidad_C.MostrarAlerta_Eliminar_Error(this, this.GetType(), "Primero seleccione un registro para poder eliminarlo");
            }
            else
            {
                if (Rol_N.RegistrosExistentes(Id_Registro) == false)
                {
                    bool respuesta = Rol_N.Eliminar(Id_Registro);

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

            txtIdRol.Text = "(Nuevo)";
            txtNombreRol.Text = "";
            txtFechaRegistro.Text = "";
            txtFechaUltimaModificacion.Text = "";
            cmbEstado.SelectedValue = "True";

            txtNombreRol.Focus();
        }

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Roles de usuario");
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