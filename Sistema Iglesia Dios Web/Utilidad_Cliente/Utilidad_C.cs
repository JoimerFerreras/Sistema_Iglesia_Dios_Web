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
using SpreadsheetLight;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Table = CrystalDecisions.CrystalReports.Engine.Table;

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


        #region Generacion de reportes
        public void GenerarReporteExcel(DataTable dtParametros, DataTable dtReporte, List<string> NombresColumnas, string NombreReporte, Page Pagina, List<DataTable> TablasSecundarias)
        {
            try
            {
                if (dtReporte.Rows.Count > 0)
                {
                    // Se establece el nombre del archivo
                    string FileName = NombreReporte + "_" + string.Format("{0:ddMMyyyHHmmss}", DateTime.Now) + ".xlsx";
                    string PathExcel = Pagina.Server.MapPath(@"~/Recursos/Archivos_Temp/");

                    if (!Directory.Exists(PathExcel))
                    {
                        Directory.CreateDirectory(PathExcel);
                    }
                    PathExcel = Pagina.Server.MapPath(@"~/Recursos/Archivos_Temp/" + FileName);

                    if (NombresColumnas.Count > 0)
                    {
                        for (int i = 0; i < dtReporte.Columns.Count; i++)
                        {
                            dtReporte.Columns[i].ColumnName = NombresColumnas[i];
                        }
                    }

                    SLDocument oSLDocument = new SLDocument();


                    if (dtParametros.Rows.Count > 0 && dtParametros != null)
                    {
                        oSLDocument.ImportDataTable(1, 1, dtParametros, false);

                        // Dando formato al nombre de la institucion
                        SpreadsheetLight.SLStyle estiloParametros = new SpreadsheetLight.SLStyle();
                        estiloParametros.SetFontBold(false);
                        estiloParametros.Font.FontSize = 22;
                        estiloParametros.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.ColorTranslator.FromHtml("#EFF4FF"), System.Drawing.ColorTranslator.FromHtml("#EFF4FF"));
                        estiloParametros.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center);
                        oSLDocument.SetCellStyle(1, 1, 1, dtReporte.Columns.Count, estiloParametros);
                        oSLDocument.MergeWorksheetCells(1, 1, 1, dtReporte.Columns.Count);

                        // Dando formato al titulo del reporte
                        estiloParametros = new SLStyle();
                        estiloParametros.Font.FontSize = 20;
                        estiloParametros.SetFontBold(true);
                        estiloParametros.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.ColorTranslator.FromHtml("#EFF4FF"), System.Drawing.ColorTranslator.FromHtml("#EFF4FF"));
                        estiloParametros.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center);
                        oSLDocument.SetCellStyle(2, 1, 2, dtReporte.Columns.Count, estiloParametros);
                        oSLDocument.MergeWorksheetCells(2, 1, 2, dtReporte.Columns.Count);

                        // Dando formato a la fecha/hora del reporte
                        estiloParametros = new SLStyle();
                        estiloParametros.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right);
                        oSLDocument.SetCellStyle(3, 1, 3, dtReporte.Columns.Count, estiloParametros);
                        oSLDocument.MergeWorksheetCells(3, 1, 3, dtReporte.Columns.Count);

                        // Detectando la palabra "Filtros"
                        for (int i = 0; i < dtParametros.Rows.Count; i++)
                        {
                            if (dtParametros.Rows[i][0].ToString() == "Filtros")
                            {
                                estiloParametros = new SLStyle();
                                estiloParametros.SetFontBold(true);
                                estiloParametros.Font.FontSize = 14;
                                oSLDocument.SetRowStyle(i + 1, estiloParametros);


                                // Estableciendo formato a los filtros
                                estiloParametros = new SLStyle();
                                estiloParametros.SetFontBold(true);
                                oSLDocument.SetCellStyle(i + 2, 1, dtParametros.Rows.Count, 1, estiloParametros);
                                //oSLDocument.MergeWorksheetCells(i + 1, 1, dtParametros.Rows.Count, dtReporte.Columns.Count);
                                oSLDocument.AutoFitColumn(1);

                                break;
                            }
                        }
                    }

                    // Se importa los datos del reporte
                    oSLDocument.ImportDataTable(dtParametros.Rows.Count + 1, 1, dtReporte, true);
                    oSLDocument.RenameWorksheet(oSLDocument.GetCurrentWorksheetName(), "Reporte");

                    // Se le asigna un estilo a la pagina del reporte
                    SpreadsheetLight.SLStyle estilo = new SpreadsheetLight.SLStyle();
                    estilo.SetFontBold(true);
                    estilo.Font.FontSize = 14;
                    estilo.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.ColorTranslator.FromHtml("#EFF4FF"), System.Drawing.ColorTranslator.FromHtml("#EFF4FF"));

                    oSLDocument.SetCellStyle(dtParametros.Rows.Count + 1, 1, dtParametros.Rows.Count + 1, dtReporte.Columns.Count, estilo);
                    oSLDocument.AutoFitColumn(1, dtReporte.Columns.Count);


                    // Si existen tablas secundarias entonces se importan y se les da formato
                    if (TablasSecundarias != null && TablasSecundarias.Count > 0)
                    {
                        for (int i = 0; i < TablasSecundarias.Count; i++)
                        {
                            DataTable dtTablaSecundaria = TablasSecundarias[i];

                            oSLDocument.AddWorksheet(dtTablaSecundaria.TableName.Replace("_", " "));
                            oSLDocument.ImportDataTable(1, 1, dtTablaSecundaria, true);

                            SpreadsheetLight.SLStyle estiloTablaSecundaria = new SpreadsheetLight.SLStyle();
                            estiloTablaSecundaria.SetFontBold(true);
                            estiloTablaSecundaria.Font.FontSize = 14;
                            estiloTablaSecundaria.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.ColorTranslator.FromHtml("#EFF4FF"), System.Drawing.ColorTranslator.FromHtml("#EFF4FF"));
                            estiloTablaSecundaria.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center);
                            oSLDocument.SetCellStyle(1, 1, 1, dtTablaSecundaria.Columns.Count, estiloTablaSecundaria);
                            oSLDocument.AutoFitColumn(1, dtTablaSecundaria.Columns.Count);
                        }
                        oSLDocument.SelectWorksheet("Reporte");
                    }


                    // Se guarda el archivo en la ubicacion espesificada para guardar archivos temporales en el servidor
                    oSLDocument.SaveAs(PathExcel);

                    // Se descarga el archivo en la maquina cliente desde la ubicacion espesificada para guardar archivos temporales del servidor
                    if (File.Exists(PathExcel))
                    {
                        Pagina.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                        Pagina.Response.TransmitFile(PathExcel);
                        Pagina.Response.End();
                    }
                    else
                    {
                        MostrarAlerta_Personalizada(Pagina, Pagina.GetType(), "Error al generar el reporte", "No se pudo descargar el reporte", "error");
                    }
                }
                else
                {
                    MostrarAlerta_Personalizada(Pagina, Pagina.GetType(), "No se puede generar el reporte", "No hay datos para generar el reporte", "warning");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta_Personalizada(Pagina, Pagina.GetType(), "Error al generar el reporte", "Ocurrió un error al generar el reporte: " + ex.Message, "error");
            }
        }

        // Se realiza el login con la base de datos para los reportes en PDF de Crystal
        public static void SetLoginReport(ReportDocument report, String dbname, String dbserver, String dbuser, String userpass) {

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
        public static OleDbConnectionStringBuilder LoginReport() {
            try
            {
                var cadena = new OleDbConnectionStringBuilder();
                cadena = new OleDbConnectionStringBuilder(ConfigurationManager.ConnectionStrings["CadenaConexionSQL"].ConnectionString);

                //cadena("password") = App_Code.Criptografia.Desencriptar(ConfigurationManager.AppSettings("password"))

                return cadena;
            } catch (Exception ex) {
                throw;
            }
        }


        #endregion
    }
}