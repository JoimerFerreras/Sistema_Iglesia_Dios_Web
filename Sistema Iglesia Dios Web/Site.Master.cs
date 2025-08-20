using DocumentFormat.OpenXml.Math;
using Microsoft.Ajax.Utilities;
using Negocio.Usuarios;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Literal = System.Web.UI.WebControls.Literal;

namespace Sistema_Iglesia_Dios_Web
{
    public partial class SiteMaster : MasterPage
    {
        Notificacion_N notificacion_N = new Notificacion_N();

        public DataTable DT_NOTIFICACIONES
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["DT_NOTIFICACIONES"]))
                {
                    ViewState["DT_NOTIFICACIONES"] = new DataTable();
                }
                return (DataTable)ViewState["DT_NOTIFICACIONES"];
            }
            set
            {
                ViewState["DT_NOTIFICACIONES"] = value;
            }
        }

        #region Metodos/Funciones
        private void EvaluarSesion()
        {
            if (Session["ID_USUARIO_SESSION"] != null && Session["ID_USUARIO_SESSION"].ToString() != "0" && Utilidad_N.ValidarNull(Session["ID_USUARIO_SESSION"].ToString()) == false)
            {
                lblNombreUsuario.Text = Session["USERNAME_SESSION"].ToString();
                lblRolUsuario.Text = Session["NOMBRE_ROL_SESSION"].ToString();
            }
            else
            {
                CerrarSesion();
            }
        }

        public void EstablecerNombrePantalla(string NombrePantalla)
        {
            lblNombrePantalla.Text = NombrePantalla;
        }

        private void CerrarSesion()
        {
            Response.Redirect(Utilidad_N.ObtenerRutaServer() + "frmLogin.aspx");
        }

        public void IrPantallaPrincipal()
        {
            Response.Redirect(Utilidad_N.ObtenerRutaServer() + "/Utilidad_Cliente/frmPaginaPrincipal.aspx");
        }

        // Método para encontrar el padre de un control de tipo Update panel para Notificaciones
        private T FindParent<T>(Control control) where T : Control
        {
            try
            {
                while (control != null && !(control is T))
                {
                    control = control.Parent;
                }
                return control as T;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CargarNotificaciones()
        {
            try
            {
                DT_NOTIFICACIONES = notificacion_N.Listar(int.Parse(Session["ID_USUARIO_SESSION"].ToString()));

                if (DT_NOTIFICACIONES.Rows.Count > 0)
                {
                    divContenedorNotificaciones.Controls.Clear(); // Limpiar anteriores

                    foreach (DataRow row in DT_NOTIFICACIONES.Rows)
                    {
                        string tipo = row["Tipo_Notificacion"].ToString(); // 1=info, 2=success, 3=warning, 4=danger, 5=system
                        string titulo = row["Titulo"].ToString();
                        string texto = row["Texto"].ToString();
                        DateTime fecha = DateTime.Parse(row["Fecha"].ToString());
                        int id = Convert.ToInt32(row["Id_Notificacion"]);
                        string Link = row["Link"].ToString();

                        bool Link_Destino_En_Sistema = false;
                        if (row["Link_Destino_En_Sistema"].ToString() == "True")
                        {
                            Link_Destino_En_Sistema = true;
                        }

                        bool Visto = false;
                        if (row["Visto"].ToString() == "True")
                        {
                            Visto = true;
                        }

                        // Asignar clase CSS según tipo
                        string clase = "notificacion notificacion-info"; // valor por defecto
                        switch (tipo)
                        {
                            case "1":
                                clase = "notificacion notificacion-info";
                                break;
                            case "2":
                                clase = "notificacion notificacion-success";
                                break;
                            case "3":
                                clase = "notificacion notificacion-warning";
                                break;
                            case "4":
                                clase = "notificacion notificacion-danger";
                                break;
                            case "5":
                                clase = "notificacion notificacion-system";
                                break;
                        }

                        string icono = "fa-circle-info"; // valor por defecto
                        switch (tipo)
                        {
                            case "1":
                                icono = "fa-circle-info";
                                break;
                            case "2":
                                icono = "fa-check-circle";
                                break;
                            case "3":
                                icono = "fa-triangle-exclamation";
                                break;
                            case "4":
                                icono = "fa-circle-exclamation";
                                break;
                            case "5":
                                icono = "fa-gear";
                                break;
                        }

                        Panel panel = new Panel
                        {
                            ID = "panelNotificacion_" + id, 
                            CssClass = clase + " position-relative"
                            
                        };
                        if (Link.Length > 0)
                        {
                            // Hacer que el panel navegue al hacer clic
                            string atributo = "";

                            if (Link_Destino_En_Sistema == true)
                            {
                                atributo += $"window.location.href='../{Link}'; ";
                            }
                            else
                            {
                                atributo += $"window.open('{Link}', '_blank');";
                            }
                            
                            panel.Attributes["onclick"] = atributo;
                            panel.Attributes["onclick"] += "event.stopPropagation();";
                            panel.Attributes["style"] += "cursor: pointer;";
                        }

                        // Icono
                        Literal iconoLiteral = new Literal
                        {
                            Text = $"<i class=\"fa-solid {icono} icono-notificacion\"></i>"
                        };

                        // Contenido
                        string TextoContenido = $@"<div class=""contenido-notificacion""> <strong>{titulo}:</strong> {texto}";

                        double segundos = ObtenerDiferenciaSegundos(fecha);
                        double minutos = ObtenerDiferenciaMinutos(fecha);
                        double horas = ObtenerDiferenciaHoras(fecha);
                        double dias = ObtenerDiferenciaDias(fecha);

                        if (segundos >= 0 && segundos < 60)
                        {
                            TextoContenido += $@"<span class=""hora-notificacion"">hace un momento</span>";
                        }
                        else if (minutos >= 1 && minutos < 60)
                        {
                            TextoContenido += $@"<span class=""hora-notificacion"">hace {(int)minutos} minutos</span>";
                        }
                        else if (horas >= 1 && horas <= 24)
                        {
                            TextoContenido += $@"<span class=""hora-notificacion"">hace {(int)horas} horas</span>";
                        }
                        else if (dias >= 1 && dias <= 7)
                        {
                            TextoContenido += $@"<span class=""hora-notificacion"">hace {(int)dias} días</span>";
                        }
                        else if (dias >= 8 && dias <= 30)
                        {
                            int semanas = (int)dias / 7;
                            TextoContenido += $@"<span class=""hora-notificacion"">hace {semanas} semanas</span>";
                        }
                        else if (dias > 30 && dias <= 365)
                        {
                            int meses = (int)dias / 30;
                            TextoContenido += $@"<span class=""hora-notificacion"">hace {meses} meses</span>";
                        }
                        else if (dias > 365)
                        {
                            TextoContenido += $@"<span class=""hora-notificacion"">hace mucho tiempo</span>";
                        }
                        else
                        {
                            TextoContenido += $@"<span class=""hora-notificacion"">hace mucho tiempo</span>";
                        }
                        TextoContenido += $@"</div>";

                        Literal contenidoLiteral = new Literal
                        {
                            Text = TextoContenido
                        };

                        // Botón cerrar
                        Button cerrarBtn = new Button
                        {
                            ID = "btnCerrar_" + id,
                            Text = "×",
                            CssClass = "btn-eliminar-notificacion",
                            CommandArgument = id.ToString(),
                            CausesValidation = false,
                            UseSubmitBehavior = false
                        };

                        cerrarBtn.Click += EliminarNotificacion_Click;
                        cerrarBtn.UseSubmitBehavior = false;
                        cerrarBtn.Attributes["onclick"] = "event.stopPropagation();";

                        // Botón marcar como vista
                        Button marcarBtn = new Button
                        {
                            Text = "✓", // ícono font awesome
                            CssClass = "btn-marcar-notificacion",
                            CommandArgument = id.ToString(),
                            CausesValidation = false
                        };
                        marcarBtn.Click += MarcarNotificacionComoVista_Click;
                        marcarBtn.UseSubmitBehavior = false;
                        marcarBtn.Attributes["type"] = "button"; // evita que dispare submit
                        marcarBtn.Attributes["onclick"] = "event.stopPropagation();";

                        // Marcador de nueva notificación
                        Literal burbuja = new Literal
                        {
                            Text = "<span class='badge-nueva'></span>"
                        };

                        // Agregar todo al panel
                        panel.Controls.Add(iconoLiteral);
                        panel.Controls.Add(contenidoLiteral);

                        if (Visto == false)
                        {

                            panel.Controls.Add(burbuja);
                        }
                        else
                        {
                            marcarBtn.Enabled = false;
                            marcarBtn.Text = "";
                            marcarBtn.Attributes["disabled"] = "disabled";
                            marcarBtn.Click += null;
                        }
                        panel.Controls.Add(marcarBtn);
                        panel.Controls.Add(cerrarBtn);

                        // Agregar al div principal
                        divContenedorNotificaciones.Controls.Add(panel);
                        btnEliminarTodas.Visible = true;
                        ActualizarBotonNotificaciones();
                    }
                }
                else
                {
                    LimpiarPanelNotificaciones();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ActualizarBotonNotificaciones()
        {
            // Comprueba si hay notificaciones sin leer
            bool haySinLeer = DT_NOTIFICACIONES.AsEnumerable()
                .Any(r => r["Visto"].ToString() == "False");

            // Muestra u oculta el badge con un if normal
            if (haySinLeer)
            {
                badgeNotificaciones.Style["display"] = "block";  // o "inline-block" si prefieres
            }
            else
            {
                badgeNotificaciones.Style["display"] = "none";
            }
        }

        private void LimpiarPanelNotificaciones()
        {
            divContenedorNotificaciones.InnerHtml = "<div class='div-sin-notificaciones'>No hay notificaciones</div>";
            btnEliminarTodas.Visible = false;
        }

        private double ObtenerDiferenciaSegundos(DateTime Fecha)
        {
            TimeSpan diferencia = DateTime.Now - Fecha;
            return diferencia.TotalSeconds;
        }

        private double ObtenerDiferenciaMinutos(DateTime Fecha)
        {
            TimeSpan diferencia = DateTime.Now - Fecha;
            return diferencia.TotalMinutes;
        }

        private double ObtenerDiferenciaHoras(DateTime Fecha)
        {
            TimeSpan diferencia = DateTime.Now - Fecha;
            return diferencia.TotalHours;
        }

        private double ObtenerDiferenciaDias(DateTime Fecha)
        {
            TimeSpan diferencia = DateTime.Now - Fecha;
            return diferencia.TotalDays;
        }
        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            EvaluarSesion();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // Carga los controles dinámicos en el mismo orden SIEMPRE antes de ViewState
            CargarNotificaciones();
        }

        protected void EliminarNotificacion_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int Id_Notificacion = int.Parse(btn.CommandArgument);

            // 1) Lo borras de la base
            notificacion_N.Eliminar(Id_Notificacion);

            // 2) Encuentras el panel
            Panel notiPanel = FindParent<Panel>(btn);
            if (notiPanel != null)
            {
                // en vez de .Visible = false
                divContenedorNotificaciones.Controls.Remove(notiPanel);
            }

            // 3) Fuerza la actualización del UpdatePanel
            if (divContenedorNotificaciones.Controls.Count == 0)
            {
                LimpiarPanelNotificaciones();
                ActualizarBotonNotificaciones();
            }

            upNotificaciones.Update();
        }

        protected void MarcarNotificacionComoVista_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int Id_Notificacion = int.Parse(btn.CommandArgument);
            notificacion_N.MarcarComoVista(Id_Notificacion);

            // Esto es suficiente para que se actualice el contenido
            CargarNotificaciones();
            upNotificaciones.Update();
        }

        protected void EliminarTodasNotificaciones_Click(object sender, EventArgs e)
        {
            notificacion_N.EliminarTodo(int.Parse(Session["ID_USUARIO_SESSION"].ToString()));
            LimpiarPanelNotificaciones();
            upNotificaciones.Update();
            ActualizarBotonNotificaciones();
        }

        #endregion
    }
}