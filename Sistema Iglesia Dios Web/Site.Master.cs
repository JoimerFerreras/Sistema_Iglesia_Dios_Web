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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Literal = System.Web.UI.WebControls.Literal;

namespace Sistema_Iglesia_Dios_Web
{
    public partial class SiteMaster : MasterPage
    {
        Notificacion_N notificacion_N = new Notificacion_N();

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
                DataTable dt = new DataTable();
                dt = notificacion_N.Listar(int.Parse(Session["ID_USUARIO_SESSION"].ToString()));

                if (dt.Rows.Count > 0)
                {
                    divContenedorNotificaciones.Controls.Clear(); // Limpiar anteriores

                    foreach (DataRow row in dt.Rows)
                    {
                        string tipo = row["Tipo_Notificacion"].ToString(); // 1=info, 2=success, 3=warning, 4=danger, 5=system
                        string titulo = row["Titulo"].ToString();
                        string texto = row["Texto"].ToString();
                        DateTime fecha = DateTime.Parse(row["Fecha"].ToString());
                        int id = Convert.ToInt32(row["Id_Notificacion"]);
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
                            CssClass = clase + " position-relative"
                        };

                        // Icono
                        Literal iconoLiteral = new Literal
                        {
                            Text = $"<i class=\"fa-solid {icono} icono-notificacion\"></i>"
                        };

                        // Contenido
                        string TextoContenido = $@"<div class=""contenido-notificacion"">
                                            <strong>{titulo}:</strong> {texto}";

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
                            Text = "×",
                            CssClass = "btn-cerrar-notificacion",
                            CommandArgument = id.ToString(),
                            CausesValidation = false
                        };
                        cerrarBtn.Click += EliminarNotificacion_Click;
                        cerrarBtn.UseSubmitBehavior = false;

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
                    }
                }
                else
                {
                    divContenedorNotificaciones.InnerHtml = "<div class='div-sin-notificaciones'>No hay notificaciones</div>";
                }
                
            }
            catch (Exception ex)
            {
            }
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

            if (!Page.IsPostBack)
            {
                
            }
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
            notificacion_N.Eliminar(Id_Notificacion);

            // 🔍 Buscar el Panel que representa la notificación
            Panel notiPanel = FindParent<Panel>(btn);
            if (notiPanel != null)
            {
                notiPanel.Visible = false;  // Ocultar solo el contenido visual
            }
        }

        protected void MarcarNotificacionComoVista_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int Id_Notificacion = int.Parse(btn.CommandArgument);
            notificacion_N.MarcarComoVista(Id_Notificacion);

            // Aquí marcas como vista en base de datos
            // Ejemplo:
            // notificacion_N.MarcarComoVista(Id_Notificacion);

            // Opcional: recargar visual
            CargarNotificaciones();
        }

        protected void EliminarTodasNotificaciones_Click(object sender, EventArgs e)
        {
            notificacion_N.EliminarTodo(int.Parse(Session["ID_USUARIO_SESSION"].ToString()));
            CargarNotificaciones();
        }

        #endregion
    }
}