using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Entidades;
using Negocio.Util_N;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using Table = CrystalDecisions.CrystalReports.Engine.Table;

namespace Sistema_Iglesia_Dios_Web.Utilidad_Cliente
{
    public class Utilidad_C
    {
        #region Manejo de usuarios, roles y funcionalidades

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
        public static string ObtenerRolUsuarioSession(Page pagina)
        {
            // Se valida la sesión del usuario
            string Rol;
            if (pagina.Session["ID_ROL_SESSION"] != null && pagina.Session["ID_ROL_SESSION"].ToString() != "0" && Utilidad_N.ValidarNull(pagina.Session["ID_ROL_SESSION"].ToString()) == false)
            {
                Rol = pagina.Session["ID_ROL_SESSION"].ToString();
            }
            else
            {
                Rol = "0";
            }
            return Rol;
        }

        public static string ObtenerCodigoPantalla(Page pagina)
        {
            // Optencion del nombre de la funcionalidad asignado a la pantalla
            CodigoFuncionalidadAttribute attribute = (CodigoFuncionalidadAttribute)Attribute.GetCustomAttribute(pagina.GetType(), typeof(CodigoFuncionalidadAttribute));
            return attribute.Codigo;
        }

        public static DataTable ObtenerPermisos_RolFuncionalidad(string Id_Rol, string NombreFuncionalidad)
        {
            DataTable dtPermisos = new DataTable();

            Negocio.Usuarios.Permiso_N permisoNegocio = new Negocio.Usuarios.Permiso_N();
            dtPermisos = permisoNegocio.ObtenerPermisos_RolFuncionalidad(Id_Rol, NombreFuncionalidad);

            return dtPermisos;
        }
        #endregion



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

        // Permisos
        public const string TituloAlerta_Accion_Denegada = "Acción denegada";
        public const string TextoAlerta_Accion_Denegada = "No tiene permisos para realizar esta acción";
        public const string TituloAlerta_Acceso_Denegado = "Acceso denegado";
        public const string TextoAlerta_Acceso_Denegado = "No tiene permisos para acceder a esta funcionalidad";

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

        // Permisos
        public static void MostrarAlerta_AccionDenegada(Page pagina, Type type)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $"swal('{TituloAlerta_Accion_Denegada}', '{TextoAlerta_Accion_Denegada}', 'error');", true);
        }

        public static void MostrarAlerta_AccesoDenegado(Page pagina, Type type)
        {
            ScriptManager.RegisterStartupScript(pagina, type.GetType(), key, $"swal('{TituloAlerta_Acceso_Denegado}', '{TextoAlerta_Acceso_Denegado}', 'error');", true);
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
                content: 'Descargar archivo',
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

        public static void RecargarTooltipPersonalizado(Page pagina, Type type, List<Entidades.Util_E.TooltipPersonalizado> ParametrosTooltip)
        {
            string script = $@"<script>";
            foreach (var tooltip in ParametrosTooltip)
            {
                script += $@" 
                            tippy('{tooltip.NombreIdentificador}', {{
                                content: '{tooltip.Texto}',
                                placement: '{tooltip.Posicion}',
                                arrow: {tooltip.Arrow},
                            }});;
                        ";
            }

            script += "</script>";

            ScriptManager.RegisterStartupScript(pagina, type.GetType(), "RecargarTooltipPersonalizado", script, false);
        }
        #endregion



        #region Validaciones

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


        #region Generacion de reportes
        public void GenerarReporteExcel(DataTable dtParametros, DataTable dtReporte, List<string> NombresColumnas, string NombreReporte, Page Pagina, List<DataTable> TablasSecundarias)
        {
            try
            {
                if (Pagina == null)
                    throw new ArgumentNullException(nameof(Pagina));

                if (dtReporte == null || dtReporte.Rows.Count == 0)
                {
                    MostrarAlerta_Personalizada(
                        Pagina,
                        Pagina.GetType(),
                        "No se puede generar el reporte",
                        "No hay datos para generar el reporte",
                        "warning");
                    return;
                }

                if (dtParametros == null)
                    dtParametros = new DataTable();

                if (NombresColumnas != null && NombresColumnas.Count > 0)
                {
                    if (NombresColumnas.Count != dtReporte.Columns.Count)
                    {
                        throw new ArgumentException(
                            "La cantidad de nombres de columnas no coincide con las columnas del reporte.",
                            nameof(NombresColumnas));
                    }

                    for (int i = 0; i < dtReporte.Columns.Count; i++)
                    {
                        dtReporte.Columns[i].ColumnName = NombresColumnas[i];
                    }
                }

                string fileName = NombreReporte + "_" +
                    DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";

                string carpetaTemporal = Pagina.Server.MapPath(
                    @"~/Recursos/Archivos_Temp/");

                if (!Directory.Exists(carpetaTemporal))
                    Directory.CreateDirectory(carpetaTemporal);

                string pathExcel = Path.Combine(carpetaTemporal, fileName);
                XLColor colorEncabezado = XLColor.FromHtml("#EFF4FF");

                using (XLWorkbook libro = new XLWorkbook())
                {
                    IXLWorksheet hojaReporte = libro.Worksheets.Add("Reporte");
                    int totalColumnas = dtReporte.Columns.Count;

                    // Parámetros generales del reporte, sin encabezados.
                    if (dtParametros.Rows.Count > 0)
                    {
                        hojaReporte.Cell(1, 1).InsertData(dtParametros);

                        IXLRange nombreInstitucion = hojaReporte.Range(
                            1, 1, 1, totalColumnas);
                        nombreInstitucion.Merge();
                        nombreInstitucion.Style.Font.Bold = false;
                        nombreInstitucion.Style.Font.FontSize = 22;
                        nombreInstitucion.Style.Fill.BackgroundColor = colorEncabezado;
                        nombreInstitucion.Style.Alignment.Horizontal =
                            XLAlignmentHorizontalValues.Center;

                        if (dtParametros.Rows.Count >= 2)
                        {
                            IXLRange tituloReporte = hojaReporte.Range(
                                2, 1, 2, totalColumnas);
                            tituloReporte.Merge();
                            tituloReporte.Style.Font.Bold = true;
                            tituloReporte.Style.Font.FontSize = 20;
                            tituloReporte.Style.Fill.BackgroundColor = colorEncabezado;
                            tituloReporte.Style.Alignment.Horizontal =
                                XLAlignmentHorizontalValues.Center;
                        }

                        if (dtParametros.Rows.Count >= 3)
                        {
                            IXLRange fechaReporte = hojaReporte.Range(
                                3, 1, 3, totalColumnas);
                            fechaReporte.Merge();
                            fechaReporte.Style.Alignment.Horizontal =
                                XLAlignmentHorizontalValues.Right;
                        }

                        // Detectar y aplicar formato a la sección de filtros.
                        for (int i = 0; i < dtParametros.Rows.Count; i++)
                        {
                            if (dtParametros.Columns.Count > 0 &&
                                dtParametros.Rows[i][0].ToString() == "Filtros")
                            {
                                int filaFiltros = i + 1;

                                IXLRange encabezadoFiltros = hojaReporte.Range(
                                    filaFiltros, 1, filaFiltros, totalColumnas);
                                encabezadoFiltros.Style.Font.Bold = true;
                                encabezadoFiltros.Style.Font.FontSize = 14;

                                if (filaFiltros + 1 <= dtParametros.Rows.Count)
                                {
                                    hojaReporte.Range(
                                        filaFiltros + 1,
                                        1,
                                        dtParametros.Rows.Count,
                                        1).Style.Font.Bold = true;
                                }

                                hojaReporte.Column(1).AdjustToContents();
                                break;
                            }
                        }
                    }

                    // Encabezados del reporte principal.
                    int filaEncabezados = dtParametros.Rows.Count + 1;

                    for (int columna = 0;
                         columna < dtReporte.Columns.Count;
                         columna++)
                    {
                        hojaReporte.Cell(filaEncabezados, columna + 1).Value =
                            dtReporte.Columns[columna].ColumnName;
                    }

                    // Datos del reporte principal, sin duplicar encabezados.
                    hojaReporte.Cell(filaEncabezados + 1, 1)
                        .InsertData(dtReporte);

                    IXLRange encabezadoReporte = hojaReporte.Range(
                        filaEncabezados,
                        1,
                        filaEncabezados,
                        totalColumnas);

                    encabezadoReporte.Style.Font.Bold = true;
                    encabezadoReporte.Style.Font.FontSize = 14;
                    encabezadoReporte.Style.Fill.BackgroundColor =
                        colorEncabezado;

                    hojaReporte.Columns(1, totalColumnas).AdjustToContents();

                    // Tablas secundarias.
                    if (TablasSecundarias != null)
                    {
                        for (int i = 0; i < TablasSecundarias.Count; i++)
                        {
                            DataTable tabla = TablasSecundarias[i];

                            if (tabla == null || tabla.Columns.Count == 0)
                                continue;

                            string nombreHoja = tabla.TableName.Replace("_", " ");

                            if (string.IsNullOrWhiteSpace(nombreHoja))
                                nombreHoja = "Tabla " + (i + 1);

                            char[] caracteresInvalidos =
                            {
                                ':', '\\', '/', '?', '*', '[', ']'
                            };

                            foreach (char caracterInvalido in caracteresInvalidos)
                            {
                                nombreHoja = nombreHoja.Replace(
                                    caracterInvalido,
                                    '-');
                            }

                            if (nombreHoja.Length > 31)
                                nombreHoja = nombreHoja.Substring(0, 31);

                            string nombreBase = nombreHoja;
                            int consecutivo = 2;

                            while (libro.Worksheets.Any(
                                hoja => hoja.Name.Equals(
                                    nombreHoja,
                                    StringComparison.OrdinalIgnoreCase)))
                            {
                                string sufijo = " " + consecutivo;
                                int longitudDisponible = 31 - sufijo.Length;

                                nombreHoja =
                                    nombreBase.Substring(
                                        0,
                                        Math.Min(
                                            nombreBase.Length,
                                            longitudDisponible)) + sufijo;

                                consecutivo++;
                            }

                            IXLWorksheet hojaSecundaria =
                                libro.Worksheets.Add(nombreHoja);

                            for (int columna = 0;
                                 columna < tabla.Columns.Count;
                                 columna++)
                            {
                                hojaSecundaria.Cell(1, columna + 1).Value =
                                    tabla.Columns[columna].ColumnName;
                            }

                            if (tabla.Rows.Count > 0)
                            {
                                hojaSecundaria.Cell(2, 1).InsertData(tabla);
                            }

                            IXLRange encabezadoSecundario =
                                hojaSecundaria.Range(
                                    1,
                                    1,
                                    1,
                                    tabla.Columns.Count);

                            encabezadoSecundario.Style.Font.Bold = true;
                            encabezadoSecundario.Style.Font.FontSize = 14;
                            encabezadoSecundario.Style.Fill.BackgroundColor =
                                colorEncabezado;
                            encabezadoSecundario.Style.Alignment.Horizontal =
                                XLAlignmentHorizontalValues.Center;

                            hojaSecundaria.Columns(
                                1,
                                tabla.Columns.Count).AdjustToContents();
                        }
                    }

                    libro.SaveAs(pathExcel);
                }

                if (!File.Exists(pathExcel))
                {
                    MostrarAlerta_Personalizada(
                        Pagina,
                        Pagina.GetType(),
                        "Error al generar el reporte",
                        "No se pudo descargar el reporte",
                        "error");
                    return;
                }

                byte[] contenidoExcel = File.ReadAllBytes(pathExcel);

                Pagina.Response.Clear();
                Pagina.Response.ClearContent();
                Pagina.Response.ClearHeaders();
                Pagina.Response.BufferOutput = true;
                Pagina.Response.ContentType =
                    "application/vnd.openxmlformats-officedocument." +
                    "spreadsheetml.sheet";

                Pagina.Response.AddHeader(
                    "Content-Disposition",
                    "attachment; filename=\"" + fileName + "\"");

                Pagina.Response.AddHeader(
                    "Content-Length",
                    contenidoExcel.Length.ToString());

                Pagina.Response.BinaryWrite(contenidoExcel);
                Pagina.Response.Flush();

                // Evita que Web Forms agregue HTML al final del archivo XLSX.
                Pagina.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return;
            }
            catch
            {
                throw;
            }
        }


        // Se realiza el login con la base de datos para los reportes en PDF de Crystal
        public static void SetLoginReport(ReportDocument report, String dbname, String dbserver, String dbuser, String userpass)
        {

            var crTableLogonInfo = new TableLogOnInfo();
            var crConnectionInfo = new ConnectionInfo();

            crConnectionInfo.DatabaseName = dbname;
            crConnectionInfo.ServerName = dbserver;
            crConnectionInfo.UserID = dbuser;
            crConnectionInfo.Password = userpass;
            //crConnectionInfo.IntegratedSecurity = False
            crConnectionInfo.Type = ConnectionInfoType.SQL;

            crTableLogonInfo.ConnectionInfo = crConnectionInfo;
            crTableLogonInfo.ConnectionInfo.DatabaseName = dbname;

            foreach (Table crTable in report.Database.Tables)
            {
                crTable.ApplyLogOnInfo(crTableLogonInfo);
            }

            foreach (ReportDocument d in report.Subreports)
            {
                foreach (Table crTable in d.Database.Tables)
                {
                    crTable.ApplyLogOnInfo(crTableLogonInfo);
                }
            }
        }

        // Se obtiene la cadena de conexion para realizar el login para los reportes en PDF de Crystal
        public static OleDbConnectionStringBuilder LoginReport()
        {
            try
            {
                var cadena = new OleDbConnectionStringBuilder();
                cadena = new OleDbConnectionStringBuilder(ConfigurationManager.ConnectionStrings["CadenaConexionSQL"].ConnectionString);

                //cadena("password") = App_Code.Criptografia.Desencriptar(ConfigurationManager.AppSettings("password"))

                return cadena;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        #region Caracteristicas de la aplicacion
        // Copiar tabla a Clipboard (incluye headers o no, segun se requiera)
        public static void CopiarDataTableAlClipboard(bool includeHeaders, DataTable dt, Page page, Type type)
        {
            string tsv = DataTableToTsv(dt, includeHeaders);

            string safe = HttpUtility.JavaScriptStringEncode(tsv);

            // (Mantengo tu misma lógica que te funcionó)
            string js = $@"
                    (async function() {{
                      const text = '{safe}';
                      try {{
                        if (navigator.clipboard && window.isSecureContext) {{
                          await navigator.clipboard.writeText(text);
                        }} else {{
                          var ta = document.createElement('textarea');
                          ta.value = text;
                          ta.style.position = 'fixed';
                          ta.style.left = '-9999px';
                          document.body.appendChild(ta);
                          ta.focus();
                          ta.select();
                          document.execCommand('copy');
                          document.body.removeChild(ta);
                        }}
                      }} catch (e) {{
                        alert('No se pudo copiar al clipboard. Intenta en HTTPS o revisa permisos del navegador.');
                      }}
                    }})();";

            ScriptManager.RegisterStartupScript(page, type.GetType(),
                includeHeaders ? "copyDtHeaders" : "copyDtData",
                js, true);
        }

        private static string DataTableToTsv(DataTable dt, bool includeHeaders)
        {
            var sb = new StringBuilder();

            if (includeHeaders)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) sb.Append('\t');
                    sb.Append(dt.Columns[i].ColumnName);
                }
                sb.AppendLine();
            }

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) sb.Append('\t');
                    var val = (row[i] ?? "").ToString()
                        .Replace("\r", " ")
                        .Replace("\n", " ");
                    sb.Append(val);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }



        // Copiar datos de un RadGrid al Clipboard (incluye headers o no, segun se requiera)
        public static void CopiarRadGridAlClipboard(bool includeHeaders, RadGrid grid, Page page, Type type, bool excluirPrimeraColumnaTemplate = true)
        {
            string tsv = RadGridToTsv(grid, includeHeaders, excluirPrimeraColumnaTemplate);

            string safe = HttpUtility.JavaScriptStringEncode(tsv);

            string js = $@"
                        (async function() {{
                          const text = '{safe}';
                          try {{
                            if (navigator.clipboard && window.isSecureContext) {{
                              await navigator.clipboard.writeText(text);
                            }} else {{
                              var ta = document.createElement('textarea');
                              ta.value = text;
                              ta.style.position = 'fixed';
                              ta.style.left = '-9999px';
                              document.body.appendChild(ta);
                              ta.focus();
                              ta.select();
                              document.execCommand('copy');
                              document.body.removeChild(ta);
                            }}
                          }} catch (e) {{
                            alert('No se pudo copiar al clipboard. Intenta en HTTPS o revisa permisos del navegador.');
                          }}
                        }})();";

            ScriptManager.RegisterStartupScript(
                page,
                type, // <-- aquí va el Type, NO type.GetType()
                includeHeaders ? "copyGridHeaders" : "copyGridData",
                js,
                true
            );
        }

        private static string RadGridToTsv(RadGrid grid, bool includeHeaders, bool excluirPrimeraColumnaTemplate)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid));

            var mtv = grid.MasterTableView;
            var sb = new StringBuilder();

            int startIndex = excluirPrimeraColumnaTemplate ? 1 : 0;

            // Headers (solo columnas visibles)
            if (includeHeaders)
            {
                bool first = true;
                for (int i = startIndex; i < mtv.RenderColumns.Length; i++)
                {
                    var col = mtv.RenderColumns[i];
                    if (!col.Visible) continue;

                    if (!first) sb.Append('\t');
                    sb.Append(col.HeaderText?.Replace("\r", " ").Replace("\n", " ") ?? "");
                    first = false;
                }
                sb.AppendLine();
            }

            // Rows (lo que el grid está mostrando en esa página)
            foreach (GridDataItem item in mtv.Items)
            {
                bool first = true;

                for (int i = startIndex; i < mtv.RenderColumns.Length; i++)
                {
                    var col = mtv.RenderColumns[i];
                    if (!col.Visible) continue;

                    string value = "";

                    // 1) Si es TemplateColumn: leer controles del template
                    if (col is GridTemplateColumn)
                    {
                        value = ExtractCellTextFromControls(item.Cells[i]);
                    }
                    else
                    {
                        // 2) Bound/otras: por UniqueName si existe
                        if (!string.IsNullOrWhiteSpace(col.UniqueName))
                            value = (item[col.UniqueName] != null ? item[col.UniqueName].Text : "");
                        else
                            value = item.Cells[i].Text ?? "";
                    }

                    value = CleanForTsv(value);

                    if (!first) sb.Append('\t');
                    sb.Append(value);
                    first = false;
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }


        private static string ExtractCellTextFromControls(Control parent)
        {
            if (parent == null) return "";

            var sb = new StringBuilder();

            foreach (Control c in parent.Controls)
            {
                // Lo más común en templates: LiteralControl
                var lit = c as LiteralControl;
                if (lit != null)
                {
                    sb.Append(lit.Text);
                    continue;
                }

                // Por si tienes Label, TextBox, etc.
                var txt = c as ITextControl;
                if (txt != null)
                {
                    sb.Append(txt.Text);
                    continue;
                }

                if (c.HasControls())
                    sb.Append(ExtractCellTextFromControls(c));
            }

            return sb.ToString();
        }

        private static string CleanForTsv(string value)
        {
            value = HttpUtility.HtmlDecode(value ?? "");
            value = value.Replace("&nbsp;", "")
                         .Replace("\t", " ")
                         .Replace("\r", " ")
                         .Replace("\n", " ")
                         .Trim();
            return value;
        }
        #endregion
    }
}