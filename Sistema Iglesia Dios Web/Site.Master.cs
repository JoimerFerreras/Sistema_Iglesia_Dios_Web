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

namespace Sistema_Iglesia_Dios_Web
{
    public partial class SiteMaster : MasterPage
    {

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
            while (control != null && !(control is T))
            {
                control = control.Parent;
            }
            return control as T;
        }

        private void CargarNotificaciones()
        {
            Notificacion_N notificacion_N = new Notificacion_N();
            DataTable dt = new DataTable();
            dt = notificacion_N.Listar(int.Parse(Session["ID_USUARIO_SESSION"].ToString()));

            divContenedorNotificaciones.Controls.Clear(); // Limpiar anteriores

            foreach (DataRow row in dt.Rows)
            {
                string tipo = row["Tipo_Notificacion"].ToString(); // 1=info, 2=success, 3=warning, 4=danger, 5=system
                string titulo = row["Titulo"].ToString();
                string texto = row["Texto"].ToString();
                DateTime fecha = DateTime.Parse(row["Fecha"].ToString());
                int id = Convert.ToInt32(row["Id_Notificacion"]);

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
                    CssClass = clase
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
                else if (horas >= 1 && horas <= 23)
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
                else if(dias > 365)
                {
                    TextoContenido += $@"<span class=""hora-notificacion"">hace mucho tiempo</span>";
                }
                else if (segundos < 0)
                {
                    TextoContenido += $@"<span class=""hora-notificacion"">hace 0 segundos</span>";
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

                // Agregar todo al panel
                panel.Controls.Add(iconoLiteral);
                panel.Controls.Add(contenidoLiteral);
                panel.Controls.Add(cerrarBtn);

                // Agregar al div principal
                divContenedorNotificaciones.Controls.Add(panel);
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

            CargarNotificaciones();
        }
        protected void EliminarNotificacion_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int idNoti = int.Parse(btn.CommandArgument);

            // 🧩 Lógica de eliminación en base de datos
            // NotificacionDAO.Eliminar(idNoti);

            // 🔍 Buscar el Panel que representa la notificación
            Panel notiPanel = FindParent<Panel>(btn);
            if (notiPanel != null)
            {
                notiPanel.Visible = false;  // Ocultar solo el contenido visual
            }
        }
        #endregion
    }
}