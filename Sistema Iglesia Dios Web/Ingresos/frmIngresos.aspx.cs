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
using System.IO;
using System.Net;
using Telerik.Web.UI;

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

        public List<Archivo_Ingreso_E> ListaArchivoE
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["ListaArchivoE"]))
                {
                    ViewState["ListaArchivoE"] = new List<Archivo_Ingreso_E>();
                }
                return (List<Archivo_Ingreso_E>)ViewState["ListaArchivoE"];
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

        #region Ingresos

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
                        cmbDescripcionIngreso_Consulta.SelectedValue,
                        cmbMoneda_Consulta.SelectedValue);

                        gvDatos.DataSource = DT_DATOS;
                        gvDatos.DataBind();

                        CalcularMontosTotalesMonedas();
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


            // Consulta
            DataTable dtConsulta = dt.Copy();

            DataRow drConsulta = dtConsulta.NewRow();
            drConsulta["Id_Descripcion_Ingreso"] = 0; // Asegúrate de que este campo coincida con el nombre del campo Id_Miembro en tu DataTable
            drConsulta["Descripcion_Ingreso"] = "Todos";

            // Insertar el nuevo DataRow al principio del DataTable
            dtConsulta.Rows.RemoveAt(0);
            dtConsulta.Rows.InsertAt(drConsulta, 0);

            cmbDescripcionIngreso_Consulta.Items.Clear();
            cmbDescripcionIngreso_Consulta.DataSource = dtConsulta;
            cmbDescripcionIngreso_Consulta.DataValueField = "Id_Descripcion_Ingreso";
            cmbDescripcionIngreso_Consulta.DataTextField = "Descripcion_Ingreso";
            cmbDescripcionIngreso_Consulta.DataBind();
        }


        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private void CalcularMontosTotalesMonedas()
        {
            if (DT_DATOS.Rows.Count > 0)
            {
                DataTable dtTotales = new DataTable();
                dtTotales.Columns.Add("Moneda");
                dtTotales.Columns.Add("Monto", typeof(float));
                for (int i = 0; i < DT_DATOS.Rows.Count; i++)
                {
                    DataRow row = DT_DATOS.Rows[i];
                    if (dtTotales.Rows.Count == 0)
                    {
                        dtTotales.Rows.Add(row["Moneda"].ToString(), 0);
                    }

                    for (int j = 0; j < dtTotales.Rows.Count; j++)
                    {
                        DataRow rowTotales = dtTotales.Rows[j];
                        
                        if (row["Moneda"].ToString() == rowTotales["Moneda"].ToString())
                        {
                            int MontoOriginal = int.Parse(rowTotales["Monto"].ToString());
                            int MontoTotal = MontoOriginal + int.Parse(row["Monto"].ToString());

                            rowTotales["Monto"] = MontoTotal;
                            break;
                        }
                        else
                        {
                            dtTotales.Rows.Add(row["Moneda"].ToString(), int.Parse(row["Monto"].ToString()));
                            break;
                        }
                    }
                }

                gvMontosTotales.DataSource = dtTotales;
                gvMontosTotales.DataBind();

            }
            else
            {
                DataTable dtTotales = new DataTable();
                dtTotales.Columns.Add("Moneda");
                dtTotales.Columns.Add("Monto", typeof(float));

                gvMontosTotales.DataSource = dtTotales;
                gvMontosTotales.DataBind();
            }

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
                        // Guardar registro existente
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
                        // Agregar registro
                        int Id_Ingreso = ingreso_N.Agregar(ingreso_E);

                        if (Id_Ingreso > 0 )
                        {
                            // Agregar los archivos que estan en la tabla temporal
                            if (ListaArchivoE.Count > 0)
                            {
                                Archivo_Ingreso_N archivo_N = new Archivo_Ingreso_N();
                                for (int i = 0; i < ListaArchivoE.Count; i++)
                                {
                                    
                                    archivo_N.AgregarArchivo(ListaArchivoE[i], Id_Ingreso);
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

            // Listar archivos del ingreso
            ListarArchivos(ID_REGISTRO);

            rtsTabulador.Tabs[1].Selected = true;
            rmpTabs.SelectedIndex = 1;

            txtId_Ingreso.Focus();
        }

        private void LimpiarCampos()
        {
            ID_REGISTRO = "0";
            ID_REGISTRO_ARCHIVO = "0";
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

            txtNombreArchivoDescargar.Text = "";
            gvArchivos.DataSource = new DataTable();
            gvArchivos.DataBind();

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
                bool respuesta = ingreso_N.Eliminar(ID_REGISTRO);

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

        #region Archivos
        private void ListarArchivos(string Id_Ingreso)
        {
            // Listar archivos del ingreso
            DataTable dtArchivos = new DataTable();
            Archivo_Ingreso_N archivo_Ingreso_N = new Archivo_Ingreso_N();
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
                    Archivo_Ingreso_E Archivo_E = new Archivo_Ingreso_E();
                    Archivo_Ingreso_N Archivo_N = new Archivo_Ingreso_N();
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
                Archivo_Ingreso_E Archivo_E = new Archivo_Ingreso_E();
                Archivo_Ingreso_N Archivo_N = new Archivo_Ingreso_N();
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
                            Archivo_Ingreso_N archivo_Ingreso_N = new Archivo_Ingreso_N();
                            HttpPostedFile postedFile = FileUpload1.PostedFile;

                            // Se crea la estructura del objeto del archvo, se le inserta los datos del archivo del FileUpload y luego se agrega el objeto a la lista de archivos
                            int Numero_Lista = ListaArchivoE.Count;
                            Archivo_Ingreso_E ArchivoTemp = new Archivo_Ingreso_E();
                            ArchivoTemp = archivo_Ingreso_N.EstructurarArchivo(postedFile, txtNombreArchivo.Text, txtDescripcionArchivo.Text, Numero_Lista, "0");
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
                            Archivo_Ingreso_N archivo_Ingreso_N = new Archivo_Ingreso_N();
                            HttpPostedFile postedFile = FileUpload1.PostedFile;

                            archivo_Ingreso_N.AgregarArchivo(archivo_Ingreso_N.EstructurarArchivo(postedFile, txtNombreArchivo.Text, txtDescripcionArchivo.Text, 0, ID_REGISTRO), 0);

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
            Archivo_Ingreso_N archivo_N = new Archivo_Ingreso_N();
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
                ((SiteMaster)Master).EstablecerNombrePantalla("Ingresos");
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


            if (ID_REGISTRO_ARCHIVO == "0" || ID_REGISTRO_ARCHIVO == "")
            {
                txtNombreArchivoDescargar.Text = "";
            }
        }

        #region Ingresos

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

        // Grid de montos totales
        protected void gvMontosTotales_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            CalcularMontosTotalesMonedas();
        }

        protected void gvMontosTotales_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            CalcularMontosTotalesMonedas();
        }

        protected void gvMontosTotales_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            CalcularMontosTotalesMonedas();
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
            GuardarRegistro();
        }

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
        #endregion

        #endregion

        protected void btnDescargarArchivo_Click(object sender, EventArgs e)
        {
            DescargarArchivo();
        }
    
    }
}