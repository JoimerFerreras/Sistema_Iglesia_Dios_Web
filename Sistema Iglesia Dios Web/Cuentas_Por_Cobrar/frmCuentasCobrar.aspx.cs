// Autor: Joimer Ferreras

using Entidades.Cuentas_Por_Cobrar;
using Negocio.Cuentas_Por_Cobrar;
using Negocio.Otros_Parametros;
using Negocio.Miembros;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OleDb;
using CrystalDecisions.Shared;
using Entidades.Otros_Parametros;

namespace Sistema_Iglesia_Dios_Web.Cuentas_Por_Cobrar
{
    public partial class frmCuentasCobrar : System.Web.UI.Page
    {
        #region Declaraciones
        Cuenta_Cobrar_N Cuenta_Cobrar_N = new Cuenta_Cobrar_N();
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

        public DataTable DT_DATOS_RESUMEN
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["DT_DATOS_RESUMEN"]))
                {
                    ViewState["DT_DATOS_RESUMEN"] = new DataTable();
                }
                return (DataTable)ViewState["DT_DATOS_RESUMEN"];
            }
            set
            {
                ViewState["DT_DATOS_RESUMEN"] = value;
            }
        }

        public List<Archivo_Cuenta_Cobrar_E> ListaArchivoE
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["ListaArchivoE"]))
                {
                    ViewState["ListaArchivoE"] = new List<Archivo_Cuenta_Cobrar_E>();
                }
                return (List<Archivo_Cuenta_Cobrar_E>)ViewState["ListaArchivoE"];
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
        #endregion


        #region Metodos/ Procedimientos

        #region Cuentas por cobrar
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

            dtpFechaDesdeFiltro.SelectedDate = PrimerDiaMes;
            dtpFechaHastaFiltro.SelectedDate = DateTime.Now;

            cmbDescripcion_Consulta.SelectedValue = "0";
            cmbMiscelaneo_Consulta.SelectedValue = "0";
            cmbMiembro_Consulta.SelectedValue = "0";
            cmbTipoDocumento_Consulta.SelectedValue = "0";
        }

        private void Consultar()
        {
            DateTime fecha;

            if (dtpFechaDesdeFiltro.SelectedDate != null && dtpFechaHastaFiltro.SelectedDate != null)
            {
                if (DateTime.TryParse(dtpFechaDesdeFiltro.SelectedDate.Value.ToString(), out fecha) == true)
                {
                    if (DateTime.TryParse(dtpFechaHastaFiltro.SelectedDate.Value.ToString(), out fecha) == true)
                    {
                        DT_DATOS = Cuenta_Cobrar_N.ListarDetalle(
                        rbtnTipoFecha.SelectedValue,
                        dtpFechaDesdeFiltro.SelectedDate.Value,
                        dtpFechaHastaFiltro.SelectedDate.Value,
                        cmbMiembro_Consulta.SelectedValue,
                        cmbMiscelaneo_Consulta.SelectedValue,
                        cmbDescripcion_Consulta.SelectedValue,
                        cmbTipoDocumento_Consulta.SelectedValue);

                        gvDatos.DataSource = DT_DATOS;
                        gvDatos.DataBind();

                        ConsultarResumen();
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

        private void ConsultarResumen()
        {
            DT_DATOS_RESUMEN = Cuenta_Cobrar_N.ListarResumen(
                                   rbtnTipoFecha.SelectedValue,
                        dtpFechaDesdeFiltro.SelectedDate.Value,
                        dtpFechaHastaFiltro.SelectedDate.Value,
                        cmbMiembro_Consulta.SelectedValue,
                        cmbMiscelaneo_Consulta.SelectedValue,
                        cmbDescripcion_Consulta.SelectedValue,
                        cmbTipoDocumento_Consulta.SelectedValue);

            gvResumen.DataSource = DT_DATOS_RESUMEN;
            gvResumen.DataBind();
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

            // Forma de pago
            Forma_Pago_N Forma_Pago_N = new Forma_Pago_N();
            dt = Forma_Pago_N.ListaCombo();
            cmbFormaPago.DataSource = dt;
            cmbFormaPago.DataValueField = "Id_Forma_Pago";
            cmbFormaPago.DataTextField = "Descripcion_Forma_Pago";
            cmbFormaPago.DataBind();

            LlenarComboDescripcion();
            LlenarComboMiscelaneo();
        }

        private void LlenarComboDescripcion()
        {
            DataTable dt = new DataTable();
            // Descripcion
            Descripciones_N Descripciones_N = new Descripciones_N();

            // Tipo de descripripcion "3" = Cuentas por cobrar
            dt = Descripciones_N.ListaCombo(3);

            // Crear un nuevo DataRow para el ítem "Seleccionar..."
            DataRow dr = dt.NewRow();
            dr["Id_Descripcion"] = 0; // Asegúrate de que este campo coincida con el nombre del campo Id_Miembro en tu DataTable
            dr["Nombre"] = "Seleccionar...";

            // Insertar el nuevo DataRow al principio del DataTable
            dt.Rows.InsertAt(dr, 0);

            cmbDescripcion.Items.Clear();
            cmbDescripcion.DataSource = dt;
            cmbDescripcion.DataValueField = "Id_Descripcion";
            cmbDescripcion.DataTextField = "Nombre";
            cmbDescripcion.DataBind();


            // Consulta
            DataTable dtConsulta = dt.Copy();

            DataRow drConsulta = dtConsulta.NewRow();
            drConsulta["Id_Descripcion"] = 0; // Asegúrate de que este campo coincida con el nombre del campo Id_Miembro en tu DataTable
            drConsulta["Nombre"] = "Todos";

            // Insertar el nuevo DataRow al principio del DataTable
            dtConsulta.Rows.RemoveAt(0);
            dtConsulta.Rows.InsertAt(drConsulta, 0);

            cmbDescripcion_Consulta.Items.Clear();
            cmbDescripcion_Consulta.DataSource = dtConsulta;
            cmbDescripcion_Consulta.DataValueField = "Id_Descripcion";
            cmbDescripcion_Consulta.DataTextField = "Nombre";
            cmbDescripcion_Consulta.DataBind();
        }

        private void LlenarComboMiscelaneo()
        {
            // Registro
            Miscelaneo_N Miscelaneo_N = new Miscelaneo_N();
            DataTable dt = new DataTable();
            dt = Miscelaneo_N.ListaCombo();

            // Crear un nuevo DataRow para el ítem "Seleccionar..."
            DataRow dr = dt.NewRow();
            dr["Id_Miscelaneo"] = 0;
            dr["Descripcion_Miscelaneo"] = "Seleccionar...";

            // Insertar el nuevo DataRow al principio del DataTable
            dt.Rows.InsertAt(dr, 0);

            cmbMiscelaneo.Items.Clear();
            cmbMiscelaneo.DataSource = dt;
            cmbMiscelaneo.DataValueField = "Id_Miscelaneo";
            cmbMiscelaneo.DataTextField = "Descripcion_Miscelaneo";
            cmbMiscelaneo.DataBind();

            // Consulta
            DataTable dtConsulta = dt.Copy();

            DataRow drConsulta = dtConsulta.NewRow();
            drConsulta["Id_Miscelaneo"] = 0;
            drConsulta["Descripcion_Miscelaneo"] = "Todos";

            // Insertar el nuevo DataRow al principio del DataTable
            dtConsulta.Rows.RemoveAt(0);
            dtConsulta.Rows.InsertAt(drConsulta, 0);

            cmbMiscelaneo_Consulta.Items.Clear();
            cmbMiscelaneo_Consulta.DataSource = dtConsulta;
            cmbMiscelaneo_Consulta.DataValueField = "Id_Miscelaneo";
            cmbMiscelaneo_Consulta.DataTextField = "Descripcion_Miscelaneo";
            cmbMiscelaneo_Consulta.DataBind();
        }

        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private void ActualizarGridResumen()
        {
            gvResumen.DataSource = DT_DATOS_RESUMEN;
            gvResumen.DataBind();
        }

        private bool ValidarCampos()
        {
            bool Validacion = false;

            if (cmbMiembro.SelectedValue == "0" && cmbMiscelaneo.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Si no va a seleccionar un miembro, entonces el campo Misceláneo no puede estar vacío");
            }
            else if (cmbDescripcion.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe especificar una descripción");
            }
            else if (dtpFechaCC.SelectedDate.Value == null)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La fecha no es válida");
            }
            else if (cmbFormaPago.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe especificar el la forma de pago");
            }
            else if (cmbTipoDocumento.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe especificar el tipo de documento");
            }
            else if (!double.TryParse(txtValor.Text, out double valor))
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El tipo de cambio no es válido");
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
                    Cuenta_Cobrar_E Cuenta_Cobrar_E = new Cuenta_Cobrar_E();
                    Cuenta_Cobrar_E.Id_Cuenta_Cobrar = int.Parse(ID_REGISTRO);
                    Cuenta_Cobrar_E.Id_Descripcion = int.Parse(cmbDescripcion.SelectedValue);
                    Cuenta_Cobrar_E.Id_Miembro = int.Parse(cmbMiembro.SelectedValue);
                    Cuenta_Cobrar_E.Id_Miscelaneo = int.Parse(cmbMiscelaneo.SelectedValue);
                    Cuenta_Cobrar_E.Fecha_CC = dtpFechaCC.SelectedDate.Value;
                    Cuenta_Cobrar_E.Valor = float.Parse(txtValor.Text);
                    Cuenta_Cobrar_E.Id_Forma_Pago = int.Parse(cmbFormaPago.SelectedValue);
                    Cuenta_Cobrar_E.Tipo_Documento = cmbTipoDocumento.SelectedValue;
                    Cuenta_Cobrar_E.No_Documento = txtNo_Documento.Text;
                    Cuenta_Cobrar_E.Comentario = txtComentario.Text;

                    Cuenta_Cobrar_E.Id_Usuario_Registro = int.Parse(Utilidad_C.ObtenerUsuarioSession(this.Page));
                    Cuenta_Cobrar_E.Fecha_Registro = DateTime.Now;
                    Cuenta_Cobrar_E.Id_Usuario_Ultima_Modificacion = int.Parse(Utilidad_C.ObtenerUsuarioSession(this.Page));
                    Cuenta_Cobrar_E.Fecha_Ultima_Modificacion = DateTime.Now;
                    Cuenta_Cobrar_E.Id_Forma_Pago = int.Parse(cmbFormaPago.SelectedValue);

                    if (EDITAR_REGISTRO == true)
                    {
                        // Guardar registro existente
                        bool salida = Cuenta_Cobrar_N.Editar(Cuenta_Cobrar_E);

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
                        int Id_Cuenta_Cobrar = Cuenta_Cobrar_N.Agregar(Cuenta_Cobrar_E);

                        if (Id_Cuenta_Cobrar > 0)
                        {
                            // Agregar los archivos que estan en la tabla temporal
                            if (ListaArchivoE.Count > 0)
                            {
                                Archivo_Cuenta_Cobrar_N archivo_N = new Archivo_Cuenta_Cobrar_N();
                                for (int i = 0; i < ListaArchivoE.Count; i++)
                                {

                                    archivo_N.AgregarArchivo(ListaArchivoE[i], Id_Cuenta_Cobrar);
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
            Cuenta_Cobrar_E Cuenta_Cobrar_E = new Cuenta_Cobrar_E();
            Cuenta_Cobrar_E = Cuenta_Cobrar_N.ObtenerRegistro(ID_REGISTRO);

            txtIdCuentaCobrar.Text = Cuenta_Cobrar_E.Id_Cuenta_Cobrar.ToString();
            cmbMiembro.SelectedValue = Cuenta_Cobrar_E.Id_Miembro.ToString();
            cmbMiscelaneo.SelectedValue = Cuenta_Cobrar_E.Id_Miscelaneo.ToString();
            cmbDescripcion.SelectedValue = Cuenta_Cobrar_E.Id_Descripcion.ToString();
            dtpFechaCC.SelectedDate = Cuenta_Cobrar_E.Fecha_CC;
            cmbFormaPago.SelectedValue = Cuenta_Cobrar_E.Id_Forma_Pago.ToString();
            cmbTipoDocumento.SelectedValue = Cuenta_Cobrar_E.Tipo_Documento.ToString();
            txtValor.Text = Cuenta_Cobrar_E.Valor.ToString();
            txtNo_Documento.Text = Cuenta_Cobrar_E.No_Documento;
            txtComentario.Text = Cuenta_Cobrar_E.Comentario;

            txtUsuarioRegistro.Text = Cuenta_Cobrar_E.Nombre_Usuario_Registro;
            txtFechaRegistro.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Cuenta_Cobrar_E.Fecha_Registro);
            txtUsuarioUltimaModificacion.Text = Cuenta_Cobrar_E.Nombre_Usuario_Ultima_Modificacion;
            cmbFormaPago.SelectedValue = Cuenta_Cobrar_E.Id_Forma_Pago.ToString();
            txtComentario.Text = Cuenta_Cobrar_E.Comentario;

            if (Cuenta_Cobrar_E.Fecha_Ultima_Modificacion != null)
            {
                txtFechaUltimaModificacion.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Cuenta_Cobrar_E.Fecha_Ultima_Modificacion);
            }

            // Listar archivos
            ListarArchivos(ID_REGISTRO);

            rtsTabulador.Tabs[1].Selected = true;
            rmpTabs.SelectedIndex = 1;

            txtIdCuentaCobrar.Focus();

            btnEliminar.Visible = true;
        }

        private void LimpiarCampos()
        {
            ID_REGISTRO = "0";
            ID_REGISTRO_ARCHIVO = "0";
            EDITAR_REGISTRO = false;

            txtIdCuentaCobrar.Text = "(Nuevo)";
            cmbMiembro.SelectedValue = "0";
            cmbMiscelaneo.SelectedValue = "0";
            cmbDescripcion.SelectedValue = "0";
            dtpFechaCC.SelectedDate = DateTime.Now;
            cmbFormaPago.SelectedValue = "0";
            cmbTipoDocumento.SelectedValue = "0";
            txtValor.Text = Utilidad_N.FormatearNumero("0", 2, 2);
            txtNo_Documento.Text = "";
            txtComentario.Text = "";

            txtUsuarioRegistro.Text = "";
            txtFechaRegistro.Text = "";
            txtUsuarioUltimaModificacion.Text = "";
            txtFechaUltimaModificacion.Text = "";
            cmbFormaPago.SelectedValue = "0";

            gvArchivos.DataSource = new DataTable();
            gvArchivos.DataBind();

            btnEliminar.Visible = false;

            cmbMiembro.Focus();
        }

        private void Eliminar()
        {
            if (EDITAR_REGISTRO == false)
            {
                Utilidad_C.MostrarAlerta_Eliminar_Error(this, this.GetType(), "Primero seleccione un registro para poder eliminarlo");
            }
            else
            {
                bool respuesta = Cuenta_Cobrar_N.Eliminar(ID_REGISTRO);

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

        private void GenerarReporteExcel_Detalle()
        {
            try
            {
                // Se establece una lista con el nombre de las columnas del grid
                List<string> NombresColumnas = new List<string>();
                for (int i = 1; i < gvDatos.MasterTableView.Columns.Count; i++)
                {
                    NombresColumnas.Add(gvDatos.MasterTableView.Columns[i].HeaderText);
                }

                // Se establece el nombre del reporte
                string NombreReporte = "Reporte_Cuentas_Cobrar_Detalle";
                DataTable dtReporte = DT_DATOS.Copy();

                // Se crea una tabla con los parametros de los filtross
                DataTable dtParametros = new DataTable();
                dtParametros.Columns.Add("Parametro");
                dtParametros.Columns.Add("Valor");

                dtParametros.Rows.Add("Iglesia de Dios La 33 Casa de Fe", "");
                dtParametros.Rows.Add("Relación de Cuentas por Cobrar (Detalle)");
                dtParametros.Rows.Add("Fecha/Hora de reporte: " + string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", DateTime.Now));
                dtParametros.Rows.Add("", "");
                dtParametros.Rows.Add("Filtros");
                dtParametros.Rows.Add("Tipo de fecha: ", rbtnTipoFecha.Items[rbtnTipoFecha.SelectedIndex].Text);

                dtParametros.Rows.Add("Fecha inicial: ", string.Format("{0:dd/MM/yyyy}", dtpFechaDesdeFiltro.SelectedDate));
                dtParametros.Rows.Add("Fecha final: ", string.Format("{0:dd/MM/yyyy}", dtpFechaHastaFiltro.SelectedDate));

                dtParametros.Rows.Add("Descripción: ", cmbDescripcion_Consulta.Text);
                dtParametros.Rows.Add("Miscelaneo: ", cmbMiscelaneo_Consulta.Text);
                dtParametros.Rows.Add("Miembro: ", cmbMiembro_Consulta.Text);
                dtParametros.Rows.Add("Tipo de documento: ", cmbTipoDocumento_Consulta.Text);
                dtParametros.Rows.Add("", "");
                dtParametros.Rows.Add("Total de registros: ", Utilidad_N.FormatearNumero(dtReporte.Rows.Count.ToString(), 0, 0));

                // Se llama al metodo de generar reporte de Utilidad_C
                Utilidad_C utilidad_C = new Utilidad_C();
                utilidad_C.GenerarReporteExcel(dtParametros, dtReporte, NombresColumnas, NombreReporte, this.Page, null);
            }
            catch (Exception ex)
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Error al generar el reporte", "Ocurrió un error al generar el reporte: " + ex.Message, "error");
            }
        }

        private void GenerarReporteExcel_Resumen()
        {
            try
            {
                // Se establece una lista con el nombre de las columnas del grid
                List<string> NombresColumnas = new List<string>();
                for (int i = 0; i < gvResumen.MasterTableView.Columns.Count; i++)
                {
                    NombresColumnas.Add(gvResumen.MasterTableView.Columns[i].HeaderText);
                }

                // Se establece el nombre del reporte
                string NombreReporte = "Reporte_Cuentas_Cobrar_Resumen";
                DataTable dtReporte = DT_DATOS_RESUMEN.Copy();
                dtReporte.Columns.RemoveAt(3); // Removiendo la columna "Tipo_Documento"

                // Se crea una tabla con los parametros de los filtross
                DataTable dtParametros = new DataTable();
                dtParametros.Columns.Add("Parametro");
                dtParametros.Columns.Add("Valor");

                dtParametros.Rows.Add("Iglesia de Dios La 33 Casa de Fe", "");
                dtParametros.Rows.Add("Relación de Cuentas por Cobrar (Resumen)");
                dtParametros.Rows.Add("Fecha/Hora de reporte: " + string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", DateTime.Now));
                dtParametros.Rows.Add("", "");
                dtParametros.Rows.Add("Filtros");
                dtParametros.Rows.Add("Tipo de fecha: ", rbtnTipoFecha.Items[rbtnTipoFecha.SelectedIndex].Text);

                dtParametros.Rows.Add("Fecha inicial: ", string.Format("{0:dd/MM/yyyy}", dtpFechaDesdeFiltro.SelectedDate));
                dtParametros.Rows.Add("Fecha final: ", string.Format("{0:dd/MM/yyyy}", dtpFechaHastaFiltro.SelectedDate));

                dtParametros.Rows.Add("Descripción: ", cmbDescripcion_Consulta.Text);
                dtParametros.Rows.Add("Miscelaneo: ", cmbMiscelaneo_Consulta.Text);
                dtParametros.Rows.Add("Miembro: ", cmbMiembro_Consulta.Text);
                dtParametros.Rows.Add("Tipo de documento: ", cmbTipoDocumento_Consulta.Text);
                dtParametros.Rows.Add("", "");
                dtParametros.Rows.Add("Total de registros: ", Utilidad_N.FormatearNumero(dtReporte.Rows.Count.ToString(), 0, 0));

                // Se llama al metodo de generar reporte de Utilidad_C
                Utilidad_C utilidad_C = new Utilidad_C();
                utilidad_C.GenerarReporteExcel(dtParametros, dtReporte, NombresColumnas, NombreReporte, this.Page, null);
            }
            catch (Exception ex)
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Error al generar el reporte", "Ocurrió un error al generar el reporte: " + ex.Message, "error");
            }
        }

        private void AgregarDescripcion()
        {
            if (txtDescripcionAgregar.Text.Length > 0)
            {
                Descripciones_E entidad = new Descripciones_E();
                Descripciones_N Descripciones_N = new Descripciones_N();
                entidad.Nombre = txtDescripcionAgregar.Text;
                entidad.Tipo_Descripcion = 3;
                entidad.Estado = true;
                Descripciones_N.Agregar(entidad);
                txtDescripcionAgregar.Text = "";

                LlenarComboDescripcion();
            }
            else
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Debe proporcionar una descripción válida");
            }
        }

        private void AgregarMiscelaneo()
        {
            if (txtMiscelaneoAgregar.Text.Length > 0)
            {
                Miscelaneo_E entidad = new Miscelaneo_E();
                Miscelaneo_N Miscelaneo_N = new Miscelaneo_N();
                entidad.Descripcion_Miscelaneo = txtMiscelaneoAgregar.Text;
                entidad.Estado = true;
                Miscelaneo_N.Agregar(entidad);
                txtMiscelaneoAgregar.Text = "";

                LlenarComboMiscelaneo();
            }
            else
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "Misceláneo no puede estar vacío");
            }
        }

        #endregion

        #region Archivos
        private void ListarArchivos(string Id_Cuenta_Cobrar)
        {
            // Listar archivos
            DataTable dtArchivos = new DataTable();
            Archivo_Cuenta_Cobrar_N Archivo_Cuenta_Cobrar_N = new Archivo_Cuenta_Cobrar_N();
            dtArchivos = Archivo_Cuenta_Cobrar_N.Listar(Id_Cuenta_Cobrar);

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
                    Archivo_Cuenta_Cobrar_E Archivo_E = new Archivo_Cuenta_Cobrar_E();
                    Archivo_Cuenta_Cobrar_N Archivo_N = new Archivo_Cuenta_Cobrar_N();
                    Archivo_E = Archivo_N.ObtenerArchivo(Id_Archivo);

                    // Simular obtener los bytes del archivo (reemplazar con tu lógica real)
                    byte[] archivoBytes = Archivo_E.Archivo;

                    // Nombre del archivo para la descarga
                    string nombreArchivo = Archivo_E.NombreArchivoCarpeta + Archivo_E.Extencion; // Puedes obtener el nombre original del archivo aquí

                    ID_REGISTRO_ARCHIVO = "0";

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
                Archivo_Cuenta_Cobrar_E Archivo_E = new Archivo_Cuenta_Cobrar_E();
                Archivo_Cuenta_Cobrar_N Archivo_N = new Archivo_Cuenta_Cobrar_N();
                Archivo_E = Archivo_N.ObtenerArchivo(Id_Archivo);
                ID_REGISTRO_ARCHIVO = Id_Archivo.ToString();

                DescargarArchivo();
            }
        }

        private void SubirArchivo()
        {
            try
            {
                // Se revisa que corresponda a un nuevo registro
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
                            Archivo_Cuenta_Cobrar_N Archivo_Cuenta_Cobrar_N = new Archivo_Cuenta_Cobrar_N();
                            HttpPostedFile postedFile = FileUpload1.PostedFile;

                            // Se crea la estructura del objeto del archvo, se le inserta los datos del archivo del FileUpload y luego se agrega el objeto a la lista de archivos
                            int Numero_Lista = ListaArchivoE.Count;
                            Archivo_Cuenta_Cobrar_E ArchivoTemp = new Archivo_Cuenta_Cobrar_E();
                            ArchivoTemp = Archivo_Cuenta_Cobrar_N.EstructurarArchivo(postedFile, txtNombreArchivo.Text, txtDescripcionArchivo.Text, Numero_Lista, "0");
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
                            Archivo_Cuenta_Cobrar_N Archivo_Cuenta_Cobrar_N = new Archivo_Cuenta_Cobrar_N();
                            HttpPostedFile postedFile = FileUpload1.PostedFile;

                            Archivo_Cuenta_Cobrar_N.AgregarArchivo(Archivo_Cuenta_Cobrar_N.EstructurarArchivo(postedFile, txtNombreArchivo.Text, txtDescripcionArchivo.Text, 0, ID_REGISTRO), 0);

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
            Archivo_Cuenta_Cobrar_N archivo_N = new Archivo_Cuenta_Cobrar_N();
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
                ((SiteMaster)Master).EstablecerNombrePantalla("Cuentas por cobrar");
                LlenerCombos();
                LimpiarFiltros();
                LimpiarCampos();

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
        }

        #region Cuentas por cobrar

        // Grid principal
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarRegistro();
        }

        protected void btnAgregarDescripcion_Click(object sender, EventArgs e)
        {
            AgregarDescripcion();
        }

        protected void btnAgregarMiscelaneo_Click(object sender, EventArgs e)
        {
            AgregarMiscelaneo();
        }

        protected void btnGenerarPDF_Detalle_Click(object sender, EventArgs e)
        {
            //GenerarReportePDF("ReporteIngresos_Detalle", "Reporte_Ingresos_Detalle");
        }

        protected void btnGenerarExcel_Detalle_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel_Detalle();
        }

        protected void gvResumen_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            ActualizarGridResumen();
        }

        protected void gvResumen_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            ActualizarGridResumen();
        }

        protected void gvResumen_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            ActualizarGridResumen();
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

        protected void gvArchivos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem) // Asegura que es una fila de datos
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                // Encuentra el botón de la fila actual
                LinkButton btnDescargar = (LinkButton)dataItem.FindControl("btnDescargarArchivo");

                if (btnDescargar != null)
                {
                    // Registrar el botón como PostBackTrigger para que funcione fuera del UpdatePanel
                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    if (scriptManager != null)
                    {
                        scriptManager.RegisterPostBackControl(btnDescargar);
                    }
                }
            }
        }
        #endregion


        #endregion
        protected void btnGenerarExcel_Resumen_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel_Resumen();
        }

        protected void btnGenerarPDF_Resumen_Click(object sender, EventArgs e)
        {

        }
    }
}