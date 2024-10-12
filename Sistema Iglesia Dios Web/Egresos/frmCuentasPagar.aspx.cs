// Autor: Joimer Ferreras

using Entidades.Egresos;
using Negocio.Egresos;
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
using System.IO;
using System.Net;
using Telerik.Web.UI;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;


namespace Sistema_Iglesia_Dios_Web.Egresos
{
    public partial class frmCuentasPagar : System.Web.UI.Page
    {
        #region Declaraciones
        Cuenta_Pagar_N cuenta_pagar_N = new Cuenta_Pagar_N();
        Abono_Cuenta_Pagar_N abonoCP_N = new Abono_Cuenta_Pagar_N();

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

        public string ID_REGISTRO_ARCHIVO
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["ID_REGISTRO_ARCHIVO"]))
                {
                    return "";
                }
                return ViewState["ID_REGISTRO_ARCHIVO"].ToString();
            }
            set
            {
                ViewState["ID_REGISTRO_ARCHIVO"] = value;
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

        // Archivos
        public List<Archivo_Egreso_E> ListaArchivoE
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["ListaArchivoE"]))
                {
                    ViewState["ListaArchivoE"] = new List<Archivo_Egreso_E>();
                }
                return (List<Archivo_Egreso_E>)ViewState["ListaArchivoE"];
            }
            set
            {
                ViewState["ListaArchivoE"] = value;
            }
        }

        public DataTable DT_DATOS_ARCHIVOS
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["DT_DATOS_ARCHIVOS"]))
                {
                    ViewState["DT_DATOS_ARCHIVOS"] = new DataTable();
                }
                return (DataTable)ViewState["DT_DATOS_ARCHIVOS"];
            }
            set
            {
                ViewState["DT_DATOS_ARCHIVOS"] = value;
            }
        }


        // ABONOS
        public string ID_REGISTRO_ABONO
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["ID_REGISTRO_ABONO"]))
                {
                    return "";
                }
                return ViewState["ID_REGISTRO_ABONO"].ToString();
            }
            set
            {
                ViewState["ID_REGISTRO_ABONO"] = value;
            }
        }
        public bool EDITAR_REGISTRO_ABONO
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["EDITAR_REGISTRO_ABONO"]))
                {
                    ViewState["EDITAR_REGISTRO_ABONO"] = new bool();
                }
                return (bool)ViewState["EDITAR_REGISTRO_ABONO"];
            }
            set
            {
                ViewState["EDITAR_REGISTRO_ABONO"] = value;
            }
        }
        public DataTable DT_DATOS_ABONOS
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["DT_DATOS_ABONOS"]))
                {
                    ViewState["DT_DATOS_ABONOS"] = new DataTable();
                }
                return (DataTable)ViewState["DT_DATOS_ABONOS"];
            }
            set
            {
                ViewState["DT_DATOS_ABONOS"] = value;
            }
        }

        #endregion


        #region Metodos/ Procedimientos

        #region Cuentas por  pagar

        private void LimpiarFiltros()
        {
            rbtnTipoFecha.SelectedValue = "1";

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

            cmbDescripcionEgreso_Consulta.SelectedValue = "0";
            cmbBeneficiarios_Consulta.SelectedValue = "0";
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
                        DT_DATOS = cuenta_pagar_N.Listar(
                        rbtnTipoFecha.SelectedValue,
                        dtpFechaDesde.SelectedDate.Value,
                        dtpFechaHasta.SelectedDate.Value,
                        cmbBeneficiarios_Consulta.SelectedValue,
                        cmbDescripcionEgreso_Consulta.SelectedValue,
                        cmbMoneda_Consulta.SelectedValue,
                        cmbEstado_Consulta.SelectedValue);

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
            cmbBeneficiario.DataSource = dt;
            cmbBeneficiario.DataValueField = "Id_Miembro";
            cmbBeneficiario.DataTextField = "Nombre_Miembro";
            cmbBeneficiario.DataBind();

            cmbBeneficiarios_Consulta.DataSource = dt;
            cmbBeneficiarios_Consulta.DataValueField = "Id_Miembro";
            cmbBeneficiarios_Consulta.DataTextField = "Nombre_Miembro";
            cmbBeneficiarios_Consulta.DataBind();

            // Moneda
            Moneda_N Moneda_N = new Moneda_N();
            dt = Moneda_N.ListaCombo();
            cmbMoneda_CuentaPagar.DataSource = dt;
            cmbMoneda_CuentaPagar.DataValueField = "Id_Moneda";
            cmbMoneda_CuentaPagar.DataTextField = "Nombre_Moneda";
            cmbMoneda_CuentaPagar.DataBind();

            cmbMoneda_Consulta.DataSource = dt;
            cmbMoneda_Consulta.DataValueField = "Id_Moneda";
            cmbMoneda_Consulta.DataTextField = "Nombre_Moneda";
            cmbMoneda_Consulta.DataBind();

            // Forma de pago
            Forma_Pago_N Forma_Pago_N = new Forma_Pago_N();
            dt = Forma_Pago_N.ListaCombo();
            cmbFormaPagoAbono.DataSource = dt;
            cmbFormaPagoAbono.DataValueField = "Id_Forma_Pago";
            cmbFormaPagoAbono.DataTextField = "Descripcion_Forma_Pago";
            cmbFormaPagoAbono.DataBind();

            LlenarComboDescripcion();
        }

        private void LlenarComboDescripcion()
        {
            DataTable dt = new DataTable();
            // Descripcion de ingreso
            Descripcion_Egreso_N Descripcion_Egreso_N = new Descripcion_Egreso_N();
            dt = Descripcion_Egreso_N.ListaCombo();

            // Crear un nuevo DataRow para el ítem "Seleccionar..."
            DataRow dr = dt.NewRow();
            dr["Id_Descripcion_Egreso"] = 0; // Asegúrate de que este campo coincida con el nombre del campo Id_Miembro en tu DataTable
            dr["Descripcion_Egreso"] = "Seleccionar...";

            // Insertar el nuevo DataRow al principio del DataTable
            dt.Rows.InsertAt(dr, 0);

            cmbDescripcion_Egreso.Items.Clear();
            cmbDescripcion_Egreso.DataSource = dt;
            cmbDescripcion_Egreso.DataValueField = "Id_Descripcion_Egreso";
            cmbDescripcion_Egreso.DataTextField = "Descripcion_Egreso";
            cmbDescripcion_Egreso.DataBind();


            // Consulta
            DataTable dtConsulta = dt.Copy();

            DataRow drConsulta = dtConsulta.NewRow();
            drConsulta["Id_Descripcion_Egreso"] = 0; // Asegúrate de que este campo coincida con el nombre del campo Id_Miembro en tu DataTable
            drConsulta["Descripcion_Egreso"] = "Todos";

            // Insertar el nuevo DataRow al principio del DataTable
            dtConsulta.Rows.RemoveAt(0);
            dtConsulta.Rows.InsertAt(drConsulta, 0);

            cmbDescripcionEgreso_Consulta.Items.Clear();
            cmbDescripcionEgreso_Consulta.DataSource = dtConsulta;
            cmbDescripcionEgreso_Consulta.DataValueField = "Id_Descripcion_Egreso";
            cmbDescripcionEgreso_Consulta.DataTextField = "Descripcion_Egreso";
            cmbDescripcionEgreso_Consulta.DataBind();
        }


        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private bool ValidarCamposCuentasPagar()
        {
            bool Validacion = false;

            if (cmbDescripcion_Egreso.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La descripción del egreso no puede estar vacía");
            }
            else if (txtNo_Factura.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe escribir el número o código de la factura");
            }
            else if (cmbBeneficiario.SelectedValue == "0" && txtOtroBeneficiario.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Si no va a seleccionar un beneficiario, entonces el campo Otro beneficiario no puede estar vacío");
            }
            else if (cmbMoneda_CuentaPagar.SelectedValue != "1" && txtValorMoneda_CuentaPagar.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe espesificar el tipo de cambio de la moneda");
            }
            else if (!double.TryParse(txtValorMoneda_CuentaPagar.Text, out double resultado_moneda))
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El tipo de cambio no es válido");
            }
            else if(!double.TryParse(txtMontoTotalPagar.Text, out double resultado_monto))
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El monto total a pagar no es válido");
            }
            else if (dtpFecha_CuentaPagar.SelectedDate.Value == null)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La fecha no es válida");
            }
            else if (dtpFechaVencimiento.SelectedDate.Value == null)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La fecha de vencimiento no es válida");
            }
            else
            {
                Validacion = true;
            }

            return Validacion;
        }

        private void GuardarRegistroCuentasPagar()
        {
            try
            {
                if (ValidarCamposCuentasPagar() == true)
                {
                    // Agregacion de la informacion basica del miembro
                    Cuenta_Pagar_E cuenta_pagar_E = new Cuenta_Pagar_E();
                    cuenta_pagar_E.Id_Cuenta_Pagar = int.Parse(ID_REGISTRO);
                    cuenta_pagar_E.Id_Descripcion_Egreso = int.Parse(cmbDescripcion_Egreso.SelectedValue);
                    cuenta_pagar_E.Monto_Total_Pagar = float.Parse(txtMontoTotalPagar.Text);
                    cuenta_pagar_E.Id_Moneda = int.Parse(cmbMoneda_CuentaPagar.SelectedValue);
                    cuenta_pagar_E.Valor_Moneda = float.Parse(txtValorMoneda_CuentaPagar.Text);
                    cuenta_pagar_E.Fecha = dtpFecha_CuentaPagar.SelectedDate.Value;
                    cuenta_pagar_E.Fecha_Vencimiento = dtpFechaVencimiento.SelectedDate.Value;
                    cuenta_pagar_E.No_Factura = txtNo_Factura.Text;
                    cuenta_pagar_E.Id_Beneficiario = int.Parse(cmbBeneficiario.SelectedValue);
                    cuenta_pagar_E.Otro_Beneficiario = txtOtroBeneficiario.Text;
                    cuenta_pagar_E.Comentario = txtComentarioCuentaPagar.Text;
                    cuenta_pagar_E.Id_Usuario_Registro = int.Parse(Utilidad_C.ObtenerUsuarioSession(this.Page));
                    cuenta_pagar_E.Fecha_Registro = DateTime.Now;
                    cuenta_pagar_E.Id_Usuario_Ultima_Modificacion = int.Parse(Utilidad_C.ObtenerUsuarioSession(this.Page));
                    cuenta_pagar_E.Fecha_Ultima_Modificacion = DateTime.Now;

                    if (EDITAR_REGISTRO == true)
                    {
                        // Guardar registro existente
                        bool salida = cuenta_pagar_N.Editar(cuenta_pagar_E);

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
                        int Id_Egreso = cuenta_pagar_N.Agregar(cuenta_pagar_E);

                        if (Id_Egreso > 0 )
                        {
                            // Agregar los archivos que estan en la tabla temporal
                            if (ListaArchivoE.Count > 0)
                            {
                                Archivo_Egreso_N archivo_N = new Archivo_Egreso_N();
                                for (int i = 0; i < ListaArchivoE.Count; i++)
                                {
                                    
                                    archivo_N.AgregarArchivo(ListaArchivoE[i], Id_Egreso);
                                }
                            }

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
            catch (Exception ex)
            {
                string a = ex.Message;
                Utilidad_C.MostrarAlerta_Guardar_Error_Fatal(this, this.GetType());
            }

        }

        private void VerRegistro()
        {
            // Llenado de datos generales
            Cuenta_Pagar_E Cuenta_Pagar_E = new Cuenta_Pagar_E();
            Cuenta_Pagar_E = cuenta_pagar_N.ObtenerRegistro(ID_REGISTRO);

            txtId_CuentaPagar.Text = Cuenta_Pagar_E.Id_Cuenta_Pagar.ToString();
            cmbDescripcion_Egreso.SelectedValue = Cuenta_Pagar_E.Id_Descripcion_Egreso.ToString();
            txtMontoTotalPagar.Text = Utilidad_N.FormatearNumero(Cuenta_Pagar_E.Monto_Total_Pagar.ToString(), 2, 2);
            cmbMoneda_CuentaPagar.SelectedValue = Cuenta_Pagar_E.Id_Moneda.ToString();
            txtValorMoneda_CuentaPagar.Text = Utilidad_N.FormatearNumero(Cuenta_Pagar_E.Valor_Moneda.ToString(), 2, 2);
            dtpFecha_CuentaPagar.SelectedDate = Cuenta_Pagar_E.Fecha;
            dtpFechaVencimiento.SelectedDate = Cuenta_Pagar_E.Fecha_Vencimiento;
            txtNo_Factura.Text = Cuenta_Pagar_E.No_Factura;
            cmbBeneficiario.SelectedValue = Cuenta_Pagar_E.Id_Beneficiario.ToString();
            txtOtroBeneficiario.Text = Cuenta_Pagar_E.Otro_Beneficiario;
            txtComentarioCuentaPagar.Text = Cuenta_Pagar_E.Comentario;

            txtUsuarioRegistro_CuentaPagar.Text = Cuenta_Pagar_E.Nombre_Usuario_Registro;
            txtFechaRegistro_CuentaPagar.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss tt}", Cuenta_Pagar_E.Fecha_Registro);
            txtUsuarioUltimaModificacion_CuentaPagar.Text = Cuenta_Pagar_E.Nombre_Usuario_Ultima_Modificacion;

            if (Cuenta_Pagar_E.Fecha_Ultima_Modificacion != null)
            {
                txtFechaUltimaModificacion_CuentaPagar.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss tt}", Cuenta_Pagar_E.Fecha_Ultima_Modificacion);
            }

            if (cmbMoneda_CuentaPagar.SelectedValue != "1" || cmbMoneda_CuentaPagar.SelectedValue != "0")
            {
                divValorMoneda.Visible = true;
            }
            else
            {
                divValorMoneda.Visible = false;
            }

            // Listar abonos
            ConsultarAbonos();

            // Listar archivos del ingreso
            ListarArchivos(ID_REGISTRO);

            rtsTabulador.Tabs[1].Selected = true;
            rmpTabs.SelectedIndex = 1;

            txtId_CuentaPagar.Focus();
        }

        private void LimpiarCampos()
        {
            // Cuentas por pagar
            ID_REGISTRO = "0";
            ID_REGISTRO_ABONO = "0";
            ID_REGISTRO_ARCHIVO = "0";

            EDITAR_REGISTRO = false;
            EDITAR_REGISTRO_ABONO = false;

            txtId_CuentaPagar.Text = "(Nuevo)";
            cmbDescripcion_Egreso.SelectedValue = "0";
            txtMontoTotalPagar.Text = Utilidad_N.FormatearNumero("0", 2, 2);
            cmbMoneda_CuentaPagar.SelectedValue = "0";
            txtValorMoneda_CuentaPagar.Text = Utilidad_N.FormatearNumero("0", 2, 2);
            dtpFecha_CuentaPagar.SelectedDate = DateTime.Now;
            cmbBeneficiario.SelectedValue = "0";
            txtOtroBeneficiario.Text = "";
            txtUsuarioRegistro_CuentaPagar.Text = "";
            txtFechaRegistro_CuentaPagar.Text = "";
            txtUsuarioUltimaModificacion_CuentaPagar.Text = "";
            txtFechaUltimaModificacion_CuentaPagar.Text = "";
            txtComentarioCuentaPagar.Text = "";

            if (cmbMoneda_CuentaPagar.SelectedValue == "1" || cmbMoneda_CuentaPagar.SelectedValue == "0")
            {
                divValorMoneda.Visible = false;
            }
            else
            {
                divValorMoneda.Visible = true;
            }

            txtNombreArchivoDescargar.Text = "";
            gvArchivos.DataSource = new DataTable();
            gvArchivos.DataBind();

            cmbDescripcion_Egreso.Focus();
        }

        private void Eliminar()
        {
            if (EDITAR_REGISTRO == false)
            {
                Utilidad_C.MostrarAlerta_Eliminar_Error(this, this.GetType(), "Primero seleccione un registro para poder eliminarlo");
            }
            else
            {
                bool respuesta = cuenta_pagar_N.Eliminar(ID_REGISTRO);

                if (respuesta)
                {
                    Utilidad_C.MostrarAlerta_Eliminar_Success(this, this.GetType());
                    LimpiarCampos();
                    Consultar();
                }
                else
                {
                    Utilidad_C.MostrarAlerta_Eliminar_Error_Fatal(this, this.GetType());
                }
            }
        }

        #endregion


        #region Abonos

        private void ConsultarAbonos()
        {
            DataTable dt = new DataTable();
            dt = abonoCP_N.Listar(ID_REGISTRO);
            gvAbonos.DataSource = dt;
            gvAbonos.DataBind();

            ConsultarTotalesAbonos();
        }

        private void ConsultarTotalesAbonos()
        {
            DataTable dt = new DataTable();
            dt = abonoCP_N.ObtenerTotalesPagadosRestantes(ID_REGISTRO);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMontoTotalAbonado.Text = "$" + Utilidad_N.FormatearNumero(row["Total_Pagado"].ToString(), 2, 2);
                txtMontoRestante.Text = "$" + Utilidad_N.FormatearNumero(row["Total_Restante"].ToString(), 2, 2);
            }
            else
            {
                txtMontoTotalAbonado.Text = "$" + Utilidad_N.FormatearNumero("0", 2, 2);
                txtMontoRestante.Text = "$" + Utilidad_N.FormatearNumero("0", 2, 2);
            }
        }


        private void LimpiarCamposAbonos()
        {
            // Cuentas por pagar
            ID_REGISTRO_ABONO = "0";
            EDITAR_REGISTRO_ABONO = false;

            txtIdAbono.Text = "(Nuevo)";
            dtpFechaAbono.SelectedDate = DateTime.Now;
            cmbFormaPagoAbono.SelectedValue = "0";
            txtMontoAbono.Text = Utilidad_N.FormatearNumero("0", 2, 2);
            txtComentarioAbono.Text = "";

            txtUsuarioRegistroAbono.Text = "";
            txtFechaRegistroAbono.Text = "";
            txtUsuarioUltimaModificacion_Abono.Text = "";
            txtFechaUltimaModificacion_Abono.Text = "";

            txtMontoTotalAbonado.Text = "$" + Utilidad_N.FormatearNumero("0", 2, 2);
            txtMontoRestante.Text = "$" + Utilidad_N.FormatearNumero("0", 2, 2);

            gvAbonos.DataSource = new DataTable();
            gvAbonos.DataBind();
        }

        private void VerRegistroAbono()
        {
            // Llenado de datos generales
            Abono_Cuenta_Pagar_E abono_E = new Abono_Cuenta_Pagar_E();
            abono_E = abonoCP_N.ObtenerRegistro(ID_REGISTRO_ABONO);

            txtIdAbono.Text = abono_E.Id_Abono_CP.ToString();
            txtMontoAbono.Text = Utilidad_N.FormatearNumero(abono_E.Monto_Abono.ToString(), 2, 2);
            dtpFechaAbono.SelectedDate = abono_E.Fecha_Abono;
            cmbFormaPagoAbono.SelectedValue = abono_E.Id_Forma_Pago.ToString();
            txtComentarioAbono.Text = abono_E.Comentario;

            txtUsuarioRegistroAbono.Text = abono_E.Nombre_Usuario_Registro;
            txtFechaRegistro_CuentaPagar.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss tt}", abono_E.Fecha_Registro);
            txtUsuarioUltimaModificacion_Abono.Text = abono_E.Nombre_Usuario_Ultima_Modificacion;

            if (abono_E.Fecha_Ultima_Modificacion != null)
            {
                txtFechaUltimaModificacion_Abono.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss tt}", abono_E.Fecha_Ultima_Modificacion);
            }

            txtIdAbono.Focus();
        }

        private bool ValidarCamposAbono()
        {
            bool Validacion = false;

            if (ID_REGISTRO == "0" || ID_REGISTRO == "")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe seleccionar una cuenta por pagar para registrarle abonos");
            }
            else if (dtpFechaAbono.SelectedDate.Value == null)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La fecha de abono no es válida");
            }
            else if (cmbFormaPagoAbono.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe espesificar la forma de pago del abono");
            }
            else if (!double.TryParse(txtMontoAbono.Text, out double resultado_monto))
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El monto de abono a pagar no es válido");
            }
            else
            {
                Validacion = true;
            }

            return Validacion;
        }

        private void GuardarRegistroAbono()
        {
            try
            {
                if (ValidarCamposAbono() == true)
                {
                    // Agregacion de la informacion basica del miembro
                    Abono_Cuenta_Pagar_E abono_E = new Abono_Cuenta_Pagar_E();
                    abono_E.Id_Abono_CP = int.Parse(ID_REGISTRO_ABONO);
                    abono_E.Id_Cuenta_Pagar = int.Parse(ID_REGISTRO);
                    abono_E.Monto_Abono = float.Parse(txtMontoAbono.Text);
                    abono_E.Fecha_Abono = dtpFechaAbono.SelectedDate.Value;
                    abono_E.Comentario = txtComentarioCuentaPagar.Text;
                    abono_E.Id_Forma_Pago = int.Parse(cmbFormaPagoAbono.SelectedValue);


                    abono_E.Id_Usuario_Registro = int.Parse(Utilidad_C.ObtenerUsuarioSession(this.Page));
                    abono_E.Fecha_Registro = DateTime.Now;
                    abono_E.Id_Usuario_Ultima_Modificacion = int.Parse(Utilidad_C.ObtenerUsuarioSession(this.Page));
                    abono_E.Fecha_Ultima_Modificacion = DateTime.Now;

                    if (EDITAR_REGISTRO_ABONO == true)
                    {
                        // Guardar registro existente
                        bool salida = abonoCP_N.Editar(abono_E);

                        if (salida == true)
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Success(this, this.GetType());
                            LimpiarCamposAbonos();
                            ConsultarAbonos();
                        }
                        else
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Error(this, this.GetType());
                        }
                    }
                    else
                    {
                        // Agregar registro
                        bool Id_Egreso = abonoCP_N.Agregar(abono_E);

                        if (Id_Egreso == true)
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Success(this, this.GetType());
                            LimpiarCamposAbonos();
                            ConsultarAbonos();
                        }
                        else
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Error(this, this.GetType());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                Utilidad_C.MostrarAlerta_Guardar_Error_Fatal(this, this.GetType());
            }

        }

        private void EliminarAbono()
        {
            if (EDITAR_REGISTRO_ABONO == false)
            {
                Utilidad_C.MostrarAlerta_Eliminar_Error(this, this.GetType(), "Primero seleccione un abono para poder eliminarlo");
            }
            else
            {
                bool respuesta = abonoCP_N.Eliminar(ID_REGISTRO_ABONO);

                if (respuesta)
                {
                    Utilidad_C.MostrarAlerta_Eliminar_Success(this, this.GetType());
                    LimpiarCamposAbonos();
                    ConsultarAbonos();
                }
                else
                {
                    Utilidad_C.MostrarAlerta_Eliminar_Error_Fatal(this, this.GetType());
                }
            }
        }

        #endregion


        #region Archivos
        private void ListarArchivos(string Id_Ingreso)
        {
            // Listar archivos del ingreso
            DataTable dtArchivos = new DataTable();
            Archivo_Egreso_N archivo_Ingreso_N = new Archivo_Egreso_N();
            dtArchivos = archivo_Ingreso_N.Listar(Id_Ingreso);

            gvArchivos.DataSource = dtArchivos;
            gvArchivos.DataBind();
        }

        private void ListarArchivosTemporales()
        {
            // Listar archivos temporales
            gvArchivos.DataSource = DT_DATOS_ARCHIVOS;
            gvArchivos.DataBind();
        }


        private void DescargarArchivo()
        {
            if (EDITAR_REGISTRO == true)
            {
                if (ID_REGISTRO_ARCHIVO == "" || ID_REGISTRO_ARCHIVO == "0")
                {
                    Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe seleccionar un archivo de la lista para descargar");
                }
                else
                {
                    int Id_Archivo = int.Parse(ID_REGISTRO_ARCHIVO);

                    // Llenado de datos generales
                    Archivo_Egreso_E Archivo_E = new Archivo_Egreso_E();
                    Archivo_Egreso_N Archivo_N = new Archivo_Egreso_N();
                    Archivo_E = Archivo_N.ObtenerArchivo(Id_Archivo);

                    // Simular obtener los bytes del archivo (reemplazar con tu lógica real)
                    byte[] archivoBytes = Archivo_E.Archivo;

                    // Nombre del archivo para la descarga
                    string nombreArchivo = Archivo_E.NombreArchivoCarpeta + Archivo_E.Extencion; // Puedes obtener el nombre original del archivo aquí

                    ID_REGISTRO_ARCHIVO = "0";
                    txtNombreArchivoDescargar.Text = "";

                    HttpResponse response = HttpContext.Current.Response;

                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;

                    response.AddHeader("Content-Disposition", $"attachment;filename={nombreArchivo}");
                    response.AddHeader("Content-Length", archivoBytes.Length.ToString());

                    response.OutputStream.Write(archivoBytes, 0, archivoBytes.Length);
                    response.Flush(); // Envía todo al cliente
                    response.SuppressContent = true; // Impide cualquier contenido adicional
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Finaliza la solicitud correctamente

                }
            }
        }

        private void SeleccionarArchivoDescargar(int Id_Archivo)
        {
            if (EDITAR_REGISTRO == true)
            {
                // Llenado de datos generales
                Archivo_Egreso_E Archivo_E = new Archivo_Egreso_E();
                Archivo_Egreso_N Archivo_N = new Archivo_Egreso_N();
                Archivo_E = Archivo_N.ObtenerArchivo(Id_Archivo);

                // Nombre del archivo para la descarga
                string nombreArchivo = Archivo_E.NombreArchivoCarpeta + Archivo_E.Extencion; // Puedes obtener el nombre original del archivo aquí

                txtNombreArchivoDescargar.Text = "(" + Id_Archivo.ToString() + ") " + nombreArchivo;

                ID_REGISTRO_ARCHIVO = Id_Archivo.ToString();
                txtNombreArchivoDescargar.Focus();
            }
        }

        private void SubirArchivo()
        {
            try
            {
                // Se revisa que corresponda a un nuevo registro de ingreso
                if (ID_REGISTRO.ToString() == "0" || ID_REGISTRO.ToString() == "")
                {
                    // Se verifica que el FileUpload tenga un archivo
                    if (FileUpload1.HasFile == true)
                    {
                        // Se verifica que tamaño del archivo en MB que tiene el FileUpload no supere el limite permitido
                        double TamanoArchivo = FileUpload1.PostedFile.ContentLength;
                        if (TamanoArchivo / (1024.0 * 1024.0) > 30)
                        {
                            Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo cargar el archivo", "No puede subir archivos con tamaño mayor a 30MB", "warning");
                        }
                        else
                        {
                            Archivo_Egreso_N archivo_Egreso_N = new Archivo_Egreso_N();
                            HttpPostedFile postedFile = FileUpload1.PostedFile;

                            // Se crea la estructura del objeto del archvo, se le inserta los datos del archivo del FileUpload y luego se agrega el objeto a la lista de archivos
                            int Numero_Lista = ListaArchivoE.Count;
                            Archivo_Egreso_E ArchivoTemp = new Archivo_Egreso_E();
                            ArchivoTemp = archivo_Egreso_N.EstructurarArchivo(postedFile, txtNombreArchivo.Text, txtDescripcionArchivo.Text, Numero_Lista, "0");
                            ListaArchivoE.Add(ArchivoTemp);

                            // Lista de campos para Grid de archivos:

                            // Id_Archivo
                            // NombreArchivo
                            // Descripcion
                            // NombreArchivoCarpeta
                            // Tamano
                            // Fecha_Registro

                            // Se agrega tambien el objeto al datatable de archivos temporales para presentarlos en el grid de archivos
                            DT_DATOS_ARCHIVOS.Rows.Add(ArchivoTemp.Id_Archivo, ArchivoTemp.NombreArchivo, ArchivoTemp.Descripcion, ArchivoTemp.NombreArchivoCarpeta + ArchivoTemp.Extencion, (float)Math.Round(ArchivoTemp.Tamano, 4), ArchivoTemp.Fecha_Registro.ToString("dd/MM/yyyy"));

                            txtNombreArchivo.Text = "";
                            txtDescripcionArchivo.Text = "";
                            ListarArchivosTemporales();
                        }
                    }
                    else
                    {
                        Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo cargar el archivo", "Debe cargar un archivo para continuar", "warning");
                    }
                }
                else
                {
                    if (FileUpload1.HasFile == true)
                    {
                        double TamanoArchivo = FileUpload1.PostedFile.ContentLength;
                        if (TamanoArchivo / (1024.0 * 1024.0) > 30)
                        {
                            Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo cargar el archivo", "No puede subir archivos con tamaño mayor a 30MB", "warning");
                        }
                        else
                        {
                            Archivo_Egreso_N archivo_Egreso_N = new Archivo_Egreso_N();
                            HttpPostedFile postedFile = FileUpload1.PostedFile;

                            archivo_Egreso_N.AgregarArchivo(archivo_Egreso_N.EstructurarArchivo(postedFile, txtNombreArchivo.Text, txtDescripcionArchivo.Text, 0, ID_REGISTRO), 0);

                            txtNombreArchivo.Text = "";
                            txtDescripcionArchivo.Text = "";
                            ListarArchivos(ID_REGISTRO);
                        }
                    }
                    else
                    {
                        Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "No se pudo cargar el archivo", "Debe cargar un archivo para continuar", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                string A = ex.Message;
                throw ex;
            }
        }

        private void EliminarArchivo(int Id_Archivo)
        {
            Archivo_Egreso_N archivo_N = new Archivo_Egreso_N();
            if (EDITAR_REGISTRO == false)
            {
                ListaArchivoE.RemoveAt(Id_Archivo);
                DT_DATOS_ARCHIVOS.Rows.RemoveAt(Id_Archivo);
                ListarArchivosTemporales();
                Utilidad_C.MostrarAlerta_Eliminar_Success(this, this.GetType());
            }
            else
            {
                bool respuesta = archivo_N.Eliminar(Id_Archivo);
                ListarArchivos(ID_REGISTRO);
                Utilidad_C.MostrarAlerta_Eliminar_Success(this, this.GetType());
            }

            ID_REGISTRO_ARCHIVO = "0";
            txtNombreArchivoDescargar.Text = "";
        }
        #endregion

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
                ((SiteMaster)Master).EstablecerNombrePantalla("Cuentas por pagar");
                LlenerCombos();
                LimpiarFiltros();
                LimpiarCampos();
                LimpiarCamposAbonos();

                // Preparar tabla de archivos temporales

                if (DT_DATOS_ARCHIVOS.Columns.Count == 0)
                {
                    DT_DATOS_ARCHIVOS.Columns.Add("Id_Archivo");
                    DT_DATOS_ARCHIVOS.Columns.Add("NombreArchivo");
                    DT_DATOS_ARCHIVOS.Columns.Add("Descripcion");
                    DT_DATOS_ARCHIVOS.Columns.Add("NombreArchivoCarpeta");
                    DT_DATOS_ARCHIVOS.Columns.Add("Tamano");
                    DT_DATOS_ARCHIVOS.Columns.Add("Fecha_Registro");
                }
            }


            if (ID_REGISTRO_ARCHIVO == "0" || ID_REGISTRO_ARCHIVO == "")
            {
                txtNombreArchivoDescargar.Text = "";
            }
        }

        #region Cuentas por pagar

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
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
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
            GuardarRegistroCuentasPagar();
        }

        protected void cmbMoneda_CuentaPagar_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbMoneda_CuentaPagar.SelectedValue == "1" || cmbMoneda_CuentaPagar.SelectedValue == "0")
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
            if (txtDescripcionEgresoAgregar.Text.Length > 0)
            {
                Descripcion_Egreso_E entidad = new Descripcion_Egreso_E();
                Descripcion_Egreso_N Descripcion_Egreso_N = new Descripcion_Egreso_N();
                entidad.Descripcion_Egreso = txtDescripcionEgresoAgregar.Text;
                entidad.Estado = true;
                Descripcion_Egreso_N.Agregar(entidad);
                txtDescripcionEgresoAgregar.Text = "";

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
        #endregion


        #region Abonos
        protected void btnEditarAbono_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            ID_REGISTRO_ABONO = btn.CommandArgument.ToString();
            EDITAR_REGISTRO_ABONO = true;
            VerRegistroAbono();
        }

        protected void btnAgregarAbono_Click(object sender, EventArgs e)
        {
            LimpiarCamposAbonos();
        }

        protected void btnGuardarAbono_Click(object sender, EventArgs e)
        {
            GuardarRegistroAbono();
        }

        protected void btnEliminarAbono_Click(object sender, EventArgs e)
        {
            EliminarAbono();
        }
        #endregion



        #region Archivos

        protected void btnSeleccionarArchivoDescargar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int Id_Archivo;
            Id_Archivo = System.Convert.ToInt32(btn.CommandArgument.ToString());
            SeleccionarArchivoDescargar(Id_Archivo);

        }

        protected void btnEliminarArchivo_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int Id_Archivo;
            Id_Archivo = System.Convert.ToInt32(btn.CommandArgument.ToString());
            EliminarArchivo(Id_Archivo);
        }

        protected void btnSubirArchivo_Click(object sender, EventArgs e)
        {
            SubirArchivo();
        }

        protected void btnDescargarArchivo_Click(object sender, EventArgs e)
        {
            DescargarArchivo();
        }

        #endregion


        // Estas dos funciones se utilizan para asignarle clases css de color rojo o verde a los items de la columan "Estado" en los Grid
        protected string GetStatusText(object status)
        {
            string statusText = status.ToString();
            if (statusText == "1")
            {
                return "Pagado";
            }
            else if (statusText == "2")
            {
                return "En Proceso";
            }
            else
            {
                return "Sin Abonos";
            }
        }

        protected string GetStatusColor(object status)
        {
            string statusText = status.ToString();

            if (statusText == "1")
            {
                return "status-green";
            }
            else if (statusText == "2")
            {
                return "status-yellow";
            }
            else
            {
                return "status-red";
            }
        }

        #endregion

    }
}