// Autor: Joimer Ferreras

using Entidades.Miembros;
using Entidades.Otros_Parametros;
using Negocio.Miembros;
using Negocio.Otros_Parametros;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Iglesia_Dios_Web.Importacion_Masiva
{
    [CodigoFuncionalidad("Importacion_Masiva")]
    public partial class frmImportacionMasiva : System.Web.UI.Page
    {
        #region Declaraciones
        Descripciones_N Descripciones_N = new Descripciones_N();
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
        #endregion


        #region Permisos
        public bool[] PERMISOS
        {
            get
            {
                if (ViewState["PERMISOS"] == null)
                {
                    ViewState["PERMISOS"] = new bool[3];
                }
                return (bool[])ViewState["PERMISOS"];
            }
            set
            {
                ViewState["PERMISOS"] = value;
            }
        }

        private void ObtenerPermisos()
        {
            DataTable dt = Utilidad_C.ObtenerPermisos_RolFuncionalidad(Utilidad_C.ObtenerRolUsuarioSession(this), Utilidad_C.ObtenerCodigoPantalla(this));
            if (dt.Rows.Count > 0)
            {
                PERMISOS[0] = dt.Rows[0].Field<bool>("Permiso_Visualizar");
                PERMISOS[1] = dt.Rows[0].Field<bool>("Permiso_Editar");
                PERMISOS[2] = dt.Rows[0].Field<bool>("Permiso_Eliminar");
            }
            else
            {
                PERMISOS[0] = false;
                PERMISOS[1] = false;
                PERMISOS[2] = false;
            }
        }

        private bool EvaluarAccionPermiso(int Id_Accion)
        {
            bool Validacion = false;

            if (Id_Accion >= 0 && Id_Accion <= 2)
            {
                Validacion = PERMISOS[Id_Accion];
            }

            return Validacion;
        }
        #endregion


        #region Metodos/ Procedimientos
        private void Importar()
        {
            if (EvaluarAccionPermiso(1) == false)
            {
                Utilidad_C.MostrarAlerta_AccionDenegada(this, this.GetType());
                return;
            }

            if (!fuImportarMiembros.HasFile)
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Debe seleccionar un archivo Excel.", "warning");
                return;
            }

            string extension = Path.GetExtension(fuImportarMiembros.FileName).ToLowerInvariant();

            if (extension != ".xlsx")
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Solo se permiten archivos con extensión .xlsx.", "warning");
                return;
            }

            const int tamanoMaximo = 10 * 1024 * 1024;

            if (fuImportarMiembros.PostedFile.ContentLength <= 0)
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "El archivo seleccionado está vacío.", "warning");
                return;
            }

            if (fuImportarMiembros.PostedFile.ContentLength > tamanoMaximo)
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "El archivo no puede superar los 10 MB.", "warning");
                return;
            }

            try
            {
                Importador_Miembros_Excel_N lector = new Importador_Miembros_Excel_N();

                Importacion_Miembros_E importacion = lector.Leer(fuImportarMiembros.PostedFile.InputStream);

                if (importacion.Errores.Count > 0)
                {
                    string detalle = string.Join(" | ", importacion.Errores.Take(10).ToArray());

                    if (importacion.Errores.Count > 10)
                    {
                        detalle += " | Existen " + (importacion.Errores.Count - 10) + " errores adicionales.";
                    }

                    Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Archivo con errores", detalle, "warning");
                    return;
                }

                Miembro_Importacion_N importacion_N = new Miembro_Importacion_N();
                int cantidadImportada = importacion_N.Importar(importacion);

                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Importación completada", "Se importaron correctamente " + cantidadImportada + " miembros.", "success");
            }
            catch (SqlException ex)
            {
                /*
                    Los THROW del procedimiento almacenado llegarán aquí
                    conservando el mensaje definido en SQL Server.
                */
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Error de importación", ex.Message, "error");
            }
            catch (Exception ex)
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Error de importación", ex.Message, "error");
            }
        }

        private void DescargarPlantilla()
        {
            if (cmbPlantillas.SelectedValue == "0")
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Debe seleccionar una plantilla.", "warning");
                return;
            }

            string rutaRelativa;
            string nombreDescarga;

            switch (cmbPlantillas.SelectedValue)
            {
                case "1":
                    rutaRelativa = "~/Recursos/Plantillas_Importacion_Masiva/" + "Plantilla_Importacion_Miembros.xlsx";
                    nombreDescarga = "Plantilla_Importacion_Miembros.xlsx";
                    break;

                default:
                    Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "La plantilla seleccionada no está disponible.", "warning");
                    return;
            }

            string rutaFisica = Server.MapPath(rutaRelativa);

            if (!File.Exists(rutaFisica))
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Error", "No se encontró el archivo de la plantilla en el servidor.", "error");
                return;
            }

            try
            {
                byte[] archivoBytes = File.ReadAllBytes(rutaFisica);

                HttpResponse response = HttpContext.Current.Response;

                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;

                response.ContentType = "application/vnd.openxmlformats-officedocument." + "spreadsheetml.sheet";
                response.AddHeader( "Content-Disposition", "attachment; filename=\"" + nombreDescarga + "\"");
                response.AddHeader("Content-Length", archivoBytes.Length.ToString() );

                response.OutputStream.Write(archivoBytes, 0, archivoBytes.Length);
                response.Flush();

                // Evita que ASP.NET agregue el HTML de la página
                // después del contenido del Excel.
                response.SuppressContent = true;

                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(),"Error de descarga", ex.Message, "error");
            }
        }

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             * Debe ejecutarse en cada solicitud, no solamente dentro
             * de !Page.IsPostBack.
             */
            ScriptManager scriptManager = ScriptManager.GetCurrent(Page);

            if (scriptManager != null)
            {
                // La descarga necesita un postback completo.
                scriptManager.RegisterPostBackControl(btnDescargarPlantilla);

                // FileUpload también necesita un postback completo.
                scriptManager.RegisterPostBackControl(btnImportarMiembros);
            }

            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                ObtenerPermisos();

                if (EvaluarAccionPermiso(0) == false)
                {
                    ((SiteMaster)Master).IrPantallaPrincipal();
                    return;
                }

                ((SiteMaster)Master).EstablecerNombrePantalla(
                    "Importación masiva"
                );
            }
        }

        protected void btnImportarMiembros_Click( object sender, EventArgs e)
        {
            Importar();  
        }

        protected void btnDescargarPlantilla_Click(object sender, EventArgs e)
        {
            DescargarPlantilla();
        }

        #endregion
    }
}