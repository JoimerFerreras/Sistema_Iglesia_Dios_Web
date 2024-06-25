// Autor: Joimer Ferreras

using Entidades.Ingresos;
using Negocio.Ingresos;
using Negocio.Otros_Parametros;
using Negocio.Miembros;
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

namespace Sistema_Iglesia_Dios_Web.Ingresos
{
    public partial class frmIngresos : System.Web.UI.Page
    {
        #region Declaraciones
        Ingreso_N ingreso_N = new Ingreso_N();
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
     
        private void LimpiarFiltros()
        {
            rbtnTipoFecha.SelectedValue = "2";

            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime PrimerDiaMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime UltimoDiaMes = PrimerDiaMes.Date.AddMonths(1).AddDays(-1);

            string PrimerDiaAnio = "1/1/" + DateTime.Now.Year.ToString();
            //string UltimoDiaAnio = "31/12/" + DateTime.Now.Year.ToString();

            dtpFechaDesde.SelectedDate = PrimerDiaMes;
            dtpFechaHasta.SelectedDate = DateTime.Now;

            cmbDescripcionIngreso_Consulta.SelectedValue = "0";
            cmbMiembro_Consulta.SelectedValue = "0";
            cmbMoneda_Consulta.SelectedValue = "0";
        }

        private void Consultar()
        {
            DateTime fecha;

            if (dtpFechaDesde.SelectedDate != null && dtpFechaHasta.SelectedDate != null)
            {
                if (DateTime.TryParse(dtpFechaDesde.SelectedDate.Value.ToString(), out fecha) == true)
                {
                    if (DateTime.TryParse(dtpFechaHasta.SelectedDate.Value.ToString(), out fecha) == true)
                    {
                        DT_DATOS = ingreso_N.Listar(
                        rbtnTipoFecha.SelectedValue,
                        dtpFechaDesde.SelectedDate.Value,
                        dtpFechaHasta.SelectedDate.Value,
                        cmbMiembro_Consulta.SelectedValue,
                        cmbDescripcion_Ingreso.SelectedValue,
                        cmbMoneda_Consulta.SelectedValue);

                        gvDatos.DataSource = DT_DATOS;
                        gvDatos.DataBind();
                    }
                    else
                    {
                        Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Fecha Desde y Fechas Hasta deben tener valores válidos", "warning");
                    }
                }
                else
                {
                    Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Fecha Desde y Fechas Hasta deben tener valores válidos", "warning");
                }
            }
            else
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Fecha Desde y Fechas Hasta deben tener valores válidos", "warning");
            }
        }

        private void LlenerCombos()
        {
            DataTable dt = new DataTable();

            // Miembro
            Miembro_N Miembro_N = new Miembro_N();
            dt = Miembro_N.ListaCombo();
            cmbMiembro.DataSource = dt;
            cmbMiembro.DataValueField = "Id_Miembro";
            cmbMiembro.DataTextField = "Nombre_Miembro";
            cmbMiembro.DataBind();

            cmbMiembro_Consulta.DataSource = dt;
            cmbMiembro_Consulta.DataValueField = "Id_Miembro";
            cmbMiembro_Consulta.DataTextField = "Nombre_Miembro";
            cmbMiembro_Consulta.DataBind();

            // Moneda
            Moneda_N Moneda_N = new Moneda_N();
            dt = Moneda_N.ListaCombo();
            cmbMoneda.DataSource = dt;
            cmbMoneda.DataValueField = "Id_Moneda";
            cmbMoneda.DataTextField = "Nombre_Moneda";
            cmbMoneda.DataBind();

            cmbMoneda_Consulta.DataSource = dt;
            cmbMoneda_Consulta.DataValueField = "Id_Moneda";
            cmbMoneda_Consulta.DataTextField = "Nombre_Moneda";
            cmbMoneda_Consulta.DataBind();

            // Forma de pago
            Forma_Pago_N Forma_Pago_N = new Forma_Pago_N();
            dt = Forma_Pago_N.ListaCombo();
            cmbFormaPago.DataSource = dt;
            cmbFormaPago.DataValueField = "Id_Forma_Pago";
            cmbFormaPago.DataTextField = "Descripcion_Forma_Pago";
            cmbFormaPago.DataBind();


            LlenarComboDescripcion();
        }

        private void LlenarComboDescripcion()
        {
            DataTable dt = new DataTable();
            // Descripcion de ingreso
            Descripcion_Ingreso_N Descripcion_Ingreso_N = new Descripcion_Ingreso_N();
            dt = Descripcion_Ingreso_N.ListaCombo();

            // Crear un nuevo DataRow para el ítem "Seleccionar..."
            DataRow dr = dt.NewRow();
            dr["Id_Descripcion_Ingreso"] = 0; // Asegúrate de que este campo coincida con el nombre del campo Id_Miembro en tu DataTable
            dr["Descripcion_Ingreso"] = "Seleccionar...";

            // Insertar el nuevo DataRow al principio del DataTable
            dt.Rows.InsertAt(dr, 0);

            cmbDescripcion_Ingreso.Items.Clear();
            cmbDescripcion_Ingreso.DataSource = dt;
            cmbDescripcion_Ingreso.DataValueField = "Id_Descripcion_Ingreso";
            cmbDescripcion_Ingreso.DataTextField = "Descripcion_Ingreso";
            cmbDescripcion_Ingreso.DataBind();

            cmbDescripcionIngreso_Consulta.DataSource = dt;
            cmbDescripcionIngreso_Consulta.DataValueField = "Id_Descripcion_Ingreso";
            cmbDescripcionIngreso_Consulta.DataTextField = "Descripcion_Ingreso";
            cmbDescripcionIngreso_Consulta.DataBind();
        }


        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private bool ValidarCampos()
        {
            bool Validacion = false;

            if (cmbDescripcion_Ingreso.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La descripción del ingreso no puede estar vacía");
            }
            else if (cmbMoneda.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe seleccionar una moneda");
            }
            else if (cmbMoneda.SelectedValue != "1" && txtValorMoneda.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe espesificar el tipo de cambio de la moneda");
            }
            else if (cmbFormaPago.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe espesificar el la forma de pago");
            }
            else if (!double.TryParse(txtValorMoneda.Text, out double resultado_moneda))
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El tipo de cambio no es válido");
            }
            else if(!double.TryParse(txtMonto.Text, out double resultado_monto))
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El monto no es válido");
            }
            else if (dtpFechaIngreso.SelectedDate.Value == null)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La fecha de ingreso no es válida");
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
                    Ingreso_E ingreso_E = new Ingreso_E();
                    ingreso_E.Id_Ingreso = int.Parse(ID_REGISTRO);
                    ingreso_E.Id_Miembro = int.Parse(cmbMiembro.SelectedValue);
                    ingreso_E.Id_Descripcion_Ingreso = int.Parse(cmbDescripcion_Ingreso.SelectedValue);
                    ingreso_E.Id_Moneda = int.Parse(cmbMoneda.SelectedValue);
                    ingreso_E.Monto = double.Parse(txtMonto.Text);
                    ingreso_E.Fecha_Ingreso = dtpFechaIngreso.SelectedDate.Value;
                    ingreso_E.Valor_Moneda = double.Parse(txtValorMoneda.Text);
                    ingreso_E.Id_Usuario_Registro = int.Parse(Utilidad_C.ObtenerUsuarioSession(this.Page));
                    ingreso_E.Fecha_Registro = DateTime.Now;
                    ingreso_E.Id_Usuario_Ultima_Modificacion = int.Parse(Utilidad_C.ObtenerUsuarioSession(this.Page));
                    ingreso_E.Fecha_Ultima_Modificacion = DateTime.Now;
                    ingreso_E.Id_Forma_Pago = int.Parse(cmbFormaPago.SelectedValue);
                    ingreso_E.Comentario = txtComentario.Text;

                    if (EDITAR_REGISTRO == true)
                    {
                        // Agregar registro
                        bool salida = ingreso_N.Editar(ingreso_E);

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
                        // Guardar registro existente
                        bool salida = ingreso_N.Agregar(ingreso_E);

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
            catch 
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Fatal(this, this.GetType());
            }

        }

        private void VerRegistro()
        {
            // Llenado de datos generales
            Ingreso_E Ingreso_E = new Ingreso_E();
            Ingreso_E = ingreso_N.ObtenerRegistro(ID_REGISTRO);

            txtId_Ingreso.Text = Ingreso_E.Id_Ingreso.ToString();
            cmbMiembro.SelectedValue = Ingreso_E.Id_Miembro.ToString();
            cmbDescripcion_Ingreso.SelectedValue = Ingreso_E.Id_Descripcion_Ingreso.ToString();
            cmbMoneda.SelectedValue = Ingreso_E.Id_Moneda.ToString();
            txtValorMoneda.Text = Utilidad_N.FormatearNumero(Ingreso_E.Valor_Moneda.ToString(), 2, 2);
            txtMonto.Text = Utilidad_N.FormatearNumero(Ingreso_E.Monto.ToString(), 2, 2);
            dtpFechaIngreso.SelectedDate = Ingreso_E.Fecha_Ingreso;
            txtUsuarioRegistro.Text = Ingreso_E.Nombre_Usuario_Registro;
            txtFechaRegistro.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss tt}", Ingreso_E.Fecha_Registro);
            txtUsuarioUltimaModificacion.Text = Ingreso_E.Nombre_Usuario_Ultima_Modificacion;
            cmbFormaPago.SelectedValue = Ingreso_E.Id_Forma_Pago.ToString();
            txtComentario.Text = Ingreso_E.Comentario;

            if (Ingreso_E.Fecha_Ultima_Modificacion != null)
            {
                txtFechaUltimaModificacion.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss tt}", Ingreso_E.Fecha_Ultima_Modificacion);
            }

            if (cmbMoneda.SelectedValue != "1" || cmbMoneda.SelectedValue != "0")
            {
                divValorMoneda.Visible = true;
            }
            else
            {
                divValorMoneda.Visible = false;
            }

            txtId_Ingreso.Focus();
        }

        private void LimpiarCampos()
        {
            ID_REGISTRO = "0";
            EDITAR_REGISTRO = false;

            txtId_Ingreso.Text = "(Nuevo)";
            cmbMiembro.SelectedValue = "0";
            cmbDescripcion_Ingreso.SelectedValue = "0";
            cmbMoneda.SelectedValue = "0";
            txtValorMoneda.Text = Utilidad_N.FormatearNumero("0", 2, 2);
            txtMonto.Text = Utilidad_N.FormatearNumero("0", 2, 2);
            dtpFechaIngreso.SelectedDate = DateTime.Now;
            txtUsuarioRegistro.Text = "";
            txtFechaRegistro.Text = "";
            txtUsuarioUltimaModificacion.Text = "";
            txtFechaUltimaModificacion.Text = "";
            cmbFormaPago.SelectedValue = "0";
            txtComentario.Text = "";

            if (cmbMoneda.SelectedValue == "1" || cmbMoneda.SelectedValue == "0")
            {
                divValorMoneda.Visible = false;
            }
            else
            {
                divValorMoneda.Visible = true;
            }

            cmbMiembro.Focus();
        }

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            string scriptModal = @"
                    <script>
                        $(document).ready(function () {
                            $('#btnAbrirPanelDescripcion').on('click', function () {
                                $('#exampleModal').modal('show');
                            });
                        });

                         tippy('#btnAbrirPanelDescripcion', {
                                        content: 'Agregaar descripción',
                                        placement: 'bottom',
                                        arrow: true,
                                    });
                    </script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Modal", scriptModal, false);

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Ingresos");
                LlenerCombos();
                LimpiarFiltros();
                LimpiarCampos();
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Consultar();
        }
       
        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {

        }

        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {

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

        protected void cmbMoneda_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbMoneda.SelectedValue == "1" || cmbMoneda.SelectedValue == "0")
            {
                divValorMoneda.Visible = false;
            }
            else
            {
                divValorMoneda.Visible = true;
            }
        }

        private void AgregarDescripcion()
        {
            if (txtDescripcionIngresoAgregar.Text.Length > 0)
            {
                Descripcion_Ingreso_E entidad = new Descripcion_Ingreso_E();
                Descripcion_Ingreso_N Descripcion_Ingreso_N = new Descripcion_Ingreso_N();
                entidad.Descripcion_Ingreso = txtDescripcionIngresoAgregar.Text;
                entidad.Estado = true;
                Descripcion_Ingreso_N.Agregar(entidad);
                txtDescripcionIngresoAgregar.Text = "";

                LlenarComboDescripcion();
            }
            else
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe proporcionar una descripción válida");
            }
        }

        protected void btnAgregarDescripcion_Click(object sender, EventArgs e)
        {
            AgregarDescripcion();
        }
    }
}