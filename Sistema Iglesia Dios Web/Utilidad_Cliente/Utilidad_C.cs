using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Negocio.Util_N;

namespace Sistema_Iglesia_Dios_Web.Utilidad_Cliente
{
    public class Utilidad_C
    {
        #region Alertas

        // -- DECLARACIONES --
        private const string key = "MensajeAlerta"; // Key


        // Mensajes generales para las alertas
        // Guardar
        private const string TituloAlerta_Guardar_Error = "No se pudo completar el guardado del registro";
        private const string TextoAlerta_Guardar_Error = "No se pudo guardar los cambios";

        private const string TituloAlerta_Guardar_Error_Fatal = "Error al guardar el registro";
        private const string TextoAlerta_Guardar_Error_Fatal = "Ocurrió un problema al intentar guardar los cambios";

        private const string TituloAlerta_Guardar_Success = "Se han guardado los cambios correctamente";

        // Eliminar
        private const string TituloAlerta_Eliminar_Error = "No se pudo completar la eliminación del registro";
        private const string TituloAlerta_Eliminar_Error_Fatal = "Error al eliminar el registro";
        private const string TextoAlerta_Eliminar_Error_Fatal = "Ocurrió un problema al intentar eliminar el registro";
        private const string TituloAlerta_Eliminar_Success = "Se ha eliminado el registro correctamente";


        // Ejeccion de las alertas
        // -- METODOS --

        // Guardar
        public static void MostrarAlerta_Guardar_Error(Page pagina, Type type)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $@"swal('{TituloAlerta_Guardar_Error}', '{TextoAlerta_Guardar_Error}', 'warning');", true);
        }
        public static void MostrarAlerta_Guardar_Success(Page pagina, Type type)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $"swal('{TituloAlerta_Guardar_Success}', '', 'success');", true);
        }
        public static void MostrarAlerta_Guardar_Error_Personalizado(Page pagina, Type type, string TextoAlerta)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $@"swal('{TituloAlerta_Guardar_Error}', '{TextoAlerta}', 'warning');", true);
        }
        public static void MostrarAlerta_Guardar_Error_Fatal(Page pagina, Type type)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $@"swal('{TituloAlerta_Guardar_Error_Fatal}', '{TextoAlerta_Guardar_Error_Fatal}', 'error');", true);
        }


        // Eliminar
        public static void MostrarAlerta_Eliminar_Error(Page pagina, Type type, string TextoAlerta)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $"swal('{TituloAlerta_Eliminar_Error}', '{TextoAlerta}', 'warning');", true);
        }
        public static void MostrarAlerta_Eliminar_Success(Page pagina, Type type)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $"swal('{TituloAlerta_Eliminar_Success}', '', 'success');", true);
        }
        public static void MostrarAlerta_Eliminar_Error_Fatal(Page pagina, Type type)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $"swal('{TituloAlerta_Eliminar_Error_Fatal}', '{TextoAlerta_Eliminar_Error_Fatal}', 'error');", true);
        }

        // Alerta personalizada
        public static void MostrarAlerta_Personalizada(Page pagina, Type type, string TituloAlerta, string TextoAlerta, string TipoAlerta)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $"swal('{TituloAlerta}', '{TextoAlerta}', '{TipoAlerta}');", true);
        }
        #endregion


        #region Tooltip
        // Se recargan los tooltips en todos las pantallas que hangan referencia a ellos
        public static void RecargarTooltips(Page pagina, Type type)
        {
            string script = @" <script>
            // Tooltip
            // With the above scripts loaded, you can call `tippy()` with a CSS
            // selector and a `content` prop:
            tippy('.boton_formulario_Agregar', {
                content: 'Nuevo',
                placement: 'left',
                arrow: true,
            });

            tippy('.boton_formulario_Eliminar', {
                content: 'Eliminar',
                placement: 'left',
                arrow: true,
            });

            tippy('.boton_formulario_Guardar', {
                content: 'Guardar',
                placement: 'left',
                arrow: true,
            });

            tippy('.btn-danger', {
                content: 'Eliminar',
                placement: 'right',
                arrow: true,
            });

            tippy('.boton_formulario_editar', {
                content: 'Editar',
                placement: 'left',
                arrow: true,
            });

            tippy('.boton_formulario_descargar_archivo', {
                content: 'Seleccionar archivo',
                placement: 'left',
                arrow: true,
            });

            tippy('.boton_formulario_ver_info', {
                content: 'Ver información',
                placement: 'left',
                arrow: true,
            });

            tippy('.boton_formulario_Buscar', {
                content: 'Buscar',
                placement: 'left',
                arrow: true,
            });

            tippy('.btnInfoControl', {
                placement: 'top',
                arrow: true,
            });

            tippy('.boton_formulario_LimpiarFiltros', {
                content: 'Limpiar filtros de búsqueda',
                placement: 'left',
                arrow: true,
            });

            tippy('.rgPagePrev', {
                content: 'Atrás',
                placement: 'bottom',
                arrow: true,
            });

            tippy('.rgPageFirst', {
                content: 'Primera página',
                placement: 'bottom',
                arrow: true,
            });

            tippy('.rgPageNext', {
                content: 'Siguiente',
                placement: 'bottom',
                arrow: true,
            });

            tippy('.rgPageLast', {
                content: 'Ultima página',
                placement: 'bottom',
                arrow: true,
            });
        </script>";

            ScriptManager.RegisterStartupScript(pagina, type.GetType(), "RecargarTooltips", script, false);
        }

        public static void RecargarTooltips(Page pagina, Type type, string PosicionEditar, string PosicionEliminar)
        {
            string script = @" <script>
            // Tooltip
            // With the above scripts loaded, you can call `tippy()` with a CSS
            // selector and a `content` prop:
            tippy('.boton_formulario_Agregar', {
                content: 'Nuevo',
                placement: 'left',
                arrow: true,
            });

            tippy('.boton_formulario_Eliminar', {
                content: 'Eliminar',
                placement: 'left',
                arrow: true,
            });

            tippy('.boton_formulario_Guardar', {
                content: 'Guardar',
                placement: 'left',
                arrow: true,
            });

            tippy('.btn-danger', {
                content: 'Eliminar',
                placement: '" + PosicionEliminar + @"',
                arrow: true,
            });

            tippy('.boton_formulario_editar', {
                content: 'Ver detalle',
                placement: '" + PosicionEditar + @"',
                arrow: true,
            });

            tippy('.btnInfoControl', {
                placement: 'top',
                arrow: true,
            });

            tippy('.boton_formulario_Buscar', {
                content: 'Buscar',
                placement: 'left',
                arrow: true,
            });

            tippy('.boton_formulario_LimpiarFiltros', {
                content: 'Restaurar filtros de búsqueda',
                placement: 'left',
                arrow: true,
            });

            tippy('.rgPagePrev', {
                content: 'Atrás',
                placement: 'bottom',
                arrow: true,
            });

            tippy('.rgPageFirst', {
                content: 'Primera página',
                placement: 'bottom',
                arrow: true,
            });

            tippy('.rgPageNext', {
                content: 'Siguiente',
                placement: 'bottom',
                arrow: true,
            });

            tippy('.rgPageLast', {
                content: 'Ultima página',
                placement: 'bottom',
                arrow: true,
            });
        </script>";

            ScriptManager.RegisterStartupScript(pagina, type.GetType(), "RecargarTooltips", script, false);
        }
        #endregion



        #region Validaciones
        public static string ObtenerUsuarioSession(Page pagina)
        {
            // Se valida la sesión del usuario
            string Id_Usuario_Session;
            if (pagina.Session["ID_USUARIO_SESSION"] != null && pagina.Session["ID_USUARIO_SESSION"].ToString() != "0" && Utilidad_N.ValidarNull(pagina.Session["ID_USUARIO_SESSION"].ToString()) == false)
            {
                Id_Usuario_Session = pagina.Session["ID_USUARIO_SESSION"].ToString();
            }
            else
            {
                Id_Usuario_Session = "0";
            }
            return Id_Usuario_Session;
        }

        public static string ObtenerNombrePC()
        {
            return Environment.MachineName;
        }
        #endregion



        #region Ejecucion de scripts

        // Metodos para ejecutar un script de javascript
        public static void EjecutarScript(Page pagina, string script)
        {
            ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), "ejecutarScripts", "<script languaje='javascript'>" + script + "</script>", false);
        }

        public static void EjecutarScript(Page pagina, string key, string script, bool AddTagScript)
        {
            ScriptManager.RegisterStartupScript(pagina, pagina.GetType(), key, "<script languaje='javascript'>" + script + "</script>", AddTagScript);
        }
        #endregion
    }
}