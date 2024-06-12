// Autor: Joimer Ferreras

using Entidades.Egresos;
using Negocio.Egresos;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Iglesia_Dios_Web.Egresos
{
    public partial class frmDescripcion_Egresos : System.Web.UI.Page
    {
        #region Declaraciones
        Descripcion_Egreso_N Descripcion_Egreso_N = new Descripcion_Egreso_N();
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
            DT_DATOS = Descripcion_Egreso_N.Listar();

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
            try
            {
                if (ValidarCampos() == true)
                {
                    // Agregacion de la informacion basica del miembro
                    Descripcion_Egreso_E Descripcion_Egreso_E = new Descripcion_Egreso_E();
                    Descripcion_Egreso_E.Id_Descripcion_Egreso = int.Parse(ID_REGISTRO);
                    Descripcion_Egreso_E.Descripcion_Egreso = txtDescripcion.Text;

                    if (EDITAR_REGISTRO == true)
                    {
                        
                        // Guardar registro existente
                        bool salida = Descripcion_Egreso_N.Editar(Descripcion_Egreso_E);

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
                        bool salida = Descripcion_Egreso_N.Agregar(Descripcion_Egreso_E);

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
            Descripcion_Egreso_E Descripcion_Egreso_E = new Descripcion_Egreso_E();
            Descripcion_Egreso_E = Descripcion_Egreso_N.ObtenerRegistro(ID_REGISTRO);

            txtId_Egreso.Text = Descripcion_Egreso_E.Id_Descripcion_Egreso.ToString();
            txtDescripcion.Text = Descripcion_Egreso_E.Descripcion_Egreso.ToString();
        }

        private void LimpiarCampos()
        {
            ID_REGISTRO = "0";
            EDITAR_REGISTRO = false;

            txtId_Egreso.Text = "(Nuevo)";
            txtDescripcion.Text = "";

            txtDescripcion.Focus();
        }

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Descripción de egresos");
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
        #endregion
    }
}