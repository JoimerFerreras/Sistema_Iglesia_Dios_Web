﻿// Autor: Joimer Ferreras

using Entidades.Usuarios;
using Entidades.Util_E;
using Negocio.Ingresos;
using Negocio.Usuarios;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Skins;

namespace Sistema_Iglesia_Dios_Web.Usuarios
{
    [CodigoFuncionalidad("Log_Usuarios_Accesos")]
    public partial class frmLogUsuariosAccesos : System.Web.UI.Page
    {
        #region Declaraciones
        Log_Usuario_Acceso_N Log_Usuario_Acceso_N = new Log_Usuario_Acceso_N();
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
            DateTime fecha;

            if (dtpFechaDesdeFiltro.SelectedDate != null && dtpFechaHastaFiltro.SelectedDate != null)
            {
                if (DateTime.TryParse(dtpFechaDesdeFiltro.SelectedDate.Value.ToString(), out fecha) == true)
                {
                    if (DateTime.TryParse(dtpFechaHastaFiltro.SelectedDate.Value.ToString(), out fecha) == true)
                    {
                        DT_DATOS = Log_Usuario_Acceso_N.Listar(
                            DateTime.Parse(dtpFechaDesdeFiltro.SelectedDate.Value.ToString("dd/MM/yyyy") + " 00:00:00"), 
                            DateTime.Parse(dtpFechaHastaFiltro.SelectedDate.Value.ToString("dd/MM/yyyy") + " 23:59:59"), 
                            cmbUsuario_Filtro.SelectedValue);

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

        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }
        private void LimpiarFiltros()
        {
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

            cmbUsuario_Filtro.SelectedValue = "0";
        }

        private void LlenerCombo()
        {
            DataTable dt = new DataTable();

            // Usuario
            Usuario_N Usuario_N = new Usuario_N();
            dt = Usuario_N.ListaCombo();
            cmbUsuario_Filtro.DataSource = dt;
            cmbUsuario_Filtro.DataValueField = "Id_Usuario";
            cmbUsuario_Filtro.DataTextField = "NombreUsuario";
            cmbUsuario_Filtro.DataBind();
        }

        private void VerRegistro()
        {
            // Llenado de datos generales
            Log_Usuario_Acceso_E Log_Usuario_Acceso_E = new Log_Usuario_Acceso_E();
            Log_Usuario_Acceso_E = Log_Usuario_Acceso_N.ObtenerRegistro(ID_REGISTRO);

            RadMap1.MarkersCollection.Clear();

            RadMap1.CenterSettings.Latitude = 18.4861;
            RadMap1.CenterSettings.Longitude = -69.9312;

            MapMarker marker = new MapMarker();

            marker.LocationSettings.Latitude = double.Parse(Log_Usuario_Acceso_E.Latitud_Coord.ToString());
            marker.LocationSettings.Longitude = double.Parse(Log_Usuario_Acceso_E.Longitud_Coord.ToString());

            string TextoMark = $@"Ubicación del acceso No.{Log_Usuario_Acceso_E.Id_Log}  |  
                                ID Usuario: {Log_Usuario_Acceso_E.Id_Usuario}  | 
                                IPv4: {Log_Usuario_Acceso_E.IPv4}  | 
                                Latitud: {Log_Usuario_Acceso_E.Latitud_Coord}, Longitud: {Log_Usuario_Acceso_E.Longitud_Coord}  |  
                                Fecha: {Log_Usuario_Acceso_E.FechaHora_Login}";

            

            marker.Title = TextoMark;

            RadMap1.MarkersCollection.Add(marker);
            RadMap1.CenterSettings.Latitude = double.Parse(Log_Usuario_Acceso_E.Latitud_Coord.ToString());
            RadMap1.CenterSettings.Longitude = double.Parse(Log_Usuario_Acceso_E.Longitud_Coord.ToString());

            rtsTabulador.Tabs[1].Selected = true;
            rmpTabs.SelectedIndex = 1;
        }

        private MapLayer ObtenerLayerMapa(string serviceListValue, string serviceListText)
        {
            string provider = serviceListValue;
            string providerName = serviceListText;

            MapLayer mapLayer = new MapLayer();

            if (provider != "Bing")
            {
                mapLayer.Type = Telerik.Web.UI.Map.LayerType.Tile;
                mapLayer.UrlTemplate = provider;

                mapLayer.Attribution = "&copy; <a href='https://www.openstreetmap.org' title='OpenStreetMap contributors' target='_blank'>OpenStreetMap contributors</a>.";

            }
            else
            {
                mapLayer.Type = Telerik.Web.UI.Map.LayerType.Bing;
                mapLayer.Key = ConfigurationManager.AppSettings["BingMapKey"].ToString(); // The key used for a local demo on your end should be provided by Microsoft as described in the description of this demo
            }

            return mapLayer;
        }

        private void CargarMapa()
        {
            string serviceListValue = "https://#= subdomain #.tile.openstreetmap.org/#= zoom #/#= x #/#= y #.png";
            string serviceListText = "OpenStreetMap";

            MapLayer mapLayer = ObtenerLayerMapa(serviceListValue, serviceListText);

            RadMap1.LayersCollection.Clear();
            RadMap1.LayersCollection.Add(mapLayer);
            RadMap1.CenterSettings.Latitude = 18.461500;
            RadMap1.CenterSettings.Longitude = -69.896500;
        }
        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            List<TooltipPersonalizado> listaToolTip = new List<TooltipPersonalizado>();

            listaToolTip.Add(new TooltipPersonalizado(".boton_formulario_editar", "Ver ubicación de sesión en mapa", "left", "true"));
            listaToolTip.Add(new TooltipPersonalizado(".boton_formulario_LimpiarFiltros", "Limpiar filtros de búsqueda", "left", "true"));
            listaToolTip.Add(new TooltipPersonalizado(".boton_formulario_Buscar", "Buscar", "left", "true"));

            Utilidad_C.RecargarTooltipPersonalizado(this, this.GetType(), listaToolTip);

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Log de accesos de usuarios");
                LlenerCombo();
                LimpiarFiltros();

                Consultar();

                CargarMapa();
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            ID_REGISTRO = btn.CommandArgument.ToString();
            
            VerRegistro();
        }
        #endregion
    }
}