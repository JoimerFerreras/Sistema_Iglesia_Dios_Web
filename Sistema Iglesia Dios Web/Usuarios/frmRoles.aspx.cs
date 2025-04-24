// Autor: Joimer Ferreras

using Entidades.Usuarios;
using Negocio.Usuarios;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OleDb;
using CrystalDecisions.Shared;
using Entidades.Otros_Parametros;
using Entidades.Ingresos;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Sistema_Iglesia_Dios_Web.Usuarios
{
    [CodigoFuncionalidad("Roles")]
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

        public DataTable DT_PERMISOS
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["DT_PERMISOS"]))
                {
                    ViewState["DT_PERMISOS"] = new DataTable();
                }
                return (DataTable)ViewState["DT_PERMISOS"];
            }
            set
            {
                ViewState["DT_PERMISOS"] = value;
            }
        }

        #endregion


        #region Metodos/ Procedimientos

        private void LimpiarFiltros()
        {
            ////Primero obtenemos el día actual
            //DateTime date = DateTime.Now;

            ////Asi obtenemos el primer dia del mes actual
            //DateTime PrimerDiaMes = new DateTime(date.Year, date.Month, 1);

            ////Y de la siguiente forma obtenemos el ultimo dia del mes
            ////agregamos 1 mes al objeto anterior y restamos 1 día.
            //DateTime UltimoDiaMes = PrimerDiaMes.Date.AddMonths(1).AddDays(-1);

            //string PrimerDiaAnio = "1/1/" + DateTime.Now.Year.ToString();
            ////string UltimoDiaAnio = "31/12/" + DateTime.Now.Year.ToString();

            //dtpFechaDesdeFiltro.SelectedDate = PrimerDiaMes;
            //dtpFechaHastaFiltro.SelectedDate = DateTime.Now;

            //cmbDescripcionIngreso_Consulta.SelectedValue = "0";
            //cmbMiembro_Consulta.SelectedValue = "0";
            //cmbMiscelaneo_Consulta.SelectedValue = "0";
        }

        private void Consultar()
        {
            DT_DATOS = Rol_N.Listar();

            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private void LlenerCombos()
        {
            //DataTable dt = new DataTable();

            //// Funcionalidad
            //Miembro_N Miembro_N = new Miembro_N();
            //dt = Miembro_N.ListaCombo();
            //cmbMiembro.DataSource = dt;
            //cmbMiembro.DataValueField = "Id_Miembro";
            //cmbMiembro.DataTextField = "Nombre_Miembro";
            //cmbMiembro.DataBind();

            //cmbMiembro_Consulta.DataSource = dt;
            //cmbMiembro_Consulta.DataValueField = "Id_Miembro";
            //cmbMiembro_Consulta.DataTextField = "Nombre_Miembro";
            //cmbMiembro_Consulta.DataBind();
        }

        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }

        private bool ValidarCampos()
        {
            bool Validacion = false;

            if (txtIdRol.Text.Length == 0)
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
                            GuardarPermisos();
                        }
                        else
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Error(this, this.GetType());
                        }
                    }
                    else
                    {
                        // Agregar registro
                        int salida = Rol_N.Agregar(Rol_E);

                        if (salida > 0)
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Success(this, this.GetType());
                            LimpiarCampos();
                            Consultar();
                            GuardarPermisos();
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
            cmbEstado.SelectedValue = Rol_E.Estado.ToString();
            txtFechaRegistro.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Rol_E.Fecha_Registro);

            if (Rol_E.Fecha_Ultima_Modificacion != null)
            {
                txtFechaUltimaModificacion.Text = string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", Rol_E.Fecha_Ultima_Modificacion);
            }
            ConsultarPermisos(ID_REGISTRO);

            rtsTabulador.Tabs[1].Selected = true;
            rmpTabs.SelectedIndex = 1;

            txtNombreRol.Focus();
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
                        DT_PERMISOS.Rows.Clear();
                        ConsultarPermisos("0");
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
            cmbEstado.SelectedValue = "True";
            txtFechaRegistro.Text = "";
            txtFechaUltimaModificacion.Text = "";

            txtNombreRol.Focus();
        }

        private void GenerarReporteExcel_Detalle()
        {
            // Se establece una lista con el nombre de las columnas del grid
            List<string> NombresColumnas = new List<string>();
            for (int i = 1; i < gvDatos.MasterTableView.Columns.Count; i++)
            {
                NombresColumnas.Add(gvDatos.MasterTableView.Columns[i].HeaderText);
            }

            // Se establece el nombre del reporte
            string NombreReporte = "Reporte_Ingresos_Detalle";
            DataTable dtReporte = DT_DATOS.Copy();

            // Se crea una tabla con los parametros de los filtros
            DataTable dtParametros = new DataTable();
            dtParametros.Columns.Add("Parametro");
            dtParametros.Columns.Add("Valor");

            dtParametros.Rows.Add("Iglesia de Dios La 33 Casa de Fe", "");
            dtParametros.Rows.Add("Relación de Ingresos (Detalle)");
            dtParametros.Rows.Add("Fecha/Hora de reporte: " + string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", DateTime.Now));
            dtParametros.Rows.Add("", "");
            //dtParametros.Rows.Add("Filtros");

            //dtParametros.Rows.Add("Descripción de ingreso: ", cmbDescripcionIngreso_Consulta.Text);
            //dtParametros.Rows.Add("", "");
            dtParametros.Rows.Add("Total de registros: ", Utilidad_N.FormatearNumero(dtReporte.Rows.Count.ToString(), 0, 0));

            // Se llama al metodo de generar reporte de Utilidad_C
            Utilidad_C utilidad_C = new Utilidad_C();
            utilidad_C.GenerarReporteExcel(dtParametros, dtReporte, NombresColumnas, NombreReporte, this.Page, null);
        }

        private void ConsultarPermisos(string Id_Rol)
        {
            Permiso_N Permiso_N = new Permiso_N();
            DT_PERMISOS = Permiso_N.Listar(Id_Rol);

            gvPermisos.DataSource = DT_PERMISOS;
            gvPermisos.DataBind();
        }

        private bool GuardarPermisos()
        {
            DataTable dtOriginal = DT_PERMISOS;

            DataTable dtUpdate = dtOriginal.Clone(); // Estructura igual
            DataTable dtInsert = dtOriginal.Clone();

            foreach (DataRow row in dtOriginal.Rows)
            {
                bool existe = Convert.ToBoolean(row["RegistroExistente"]);
                if (existe)
                    dtUpdate.ImportRow(row);
                else
                    dtInsert.ImportRow(row);
            }
            bool RespuestaInsert = false;
            bool RespuestaUpdate = false;
            Permiso_N Permiso_N = new Permiso_N();  
            
            if (dtInsert.Rows.Count >0)
            {
              RespuestaInsert = Permiso_N.Agregar(dtInsert);
            }

            if (dtUpdate.Rows.Count > 0)
            {
                RespuestaUpdate = Permiso_N.Editar(dtUpdate);
            }

            if (RespuestaInsert == true && RespuestaUpdate == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Roles y permisos");
                LlenerCombos();
                LimpiarFiltros();
                LimpiarCampos();

                Consultar();
            }
        }

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
            LinkButton btn = (LinkButton)sender;
            int Id_Registro;
            Id_Registro = System.Convert.ToInt32(btn.CommandArgument.ToString());
            Eliminar(Id_Registro);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarRegistro();
        }

        protected void btnGenerarExcel_Detalle_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel_Detalle();
        }

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