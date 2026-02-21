<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAcercaDe.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Ayuda.frmAcercaDe" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />

    <script>
        async function copiarCorreo(correo, btn) {
            const icon = btn.querySelector("i");
            const text = btn.querySelector("span");

            // Guardar estado original
            const originalIconClass = icon.className;
            const originalText = text.textContent;

            try {
                if (navigator.clipboard && window.isSecureContext) {
                    await navigator.clipboard.writeText(correo);
                } else {
                    const ta = document.createElement("textarea");
                    ta.value = correo;
                    ta.style.position = "fixed";
                    ta.style.left = "-9999px";
                    document.body.appendChild(ta);
                    ta.focus();
                    ta.select();
                    document.execCommand("copy");
                    document.body.removeChild(ta);
                }

                // UI de éxito: check por 2s
                icon.className = "fa-solid fa-check";
                text.textContent = "";
                btn.disabled = true;

                setTimeout(() => {
                    icon.className = originalIconClass;
                    text.textContent = originalText;
                    btn.disabled = false;
                }, 2000);

            } catch (e) {
                // Si falla, deja el botón igual y muestra error simple
                alert("No se pudo copiar. Revisa permisos del navegador o usa HTTPS.");
            }
        }
</script>

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upPrincipal">
        <Triggers>
        </Triggers>
        <ContentTemplate>

            <div style="padding: 20px;">

                <div class="shadowed-div-body" style="width: 100%;">
                    <div>
                        <i class="fa-solid fa-circle-info shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Acerca del sistema</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row">
                        <div class="col-12 col-md-4">
                            <b>Nombre oficial del sistema</b><br />
                            Sistema Web Iglesia de Dios Casa de Fe La 33
                        </div>

                        <div class="col-12 col-md-4">
                            <b>Cliente</b><br />
                            Iglesia de Dios Casa de Fe La 33
                        </div>
                    </div>

                </div>

                <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                    <div>
                        <i class="fa-solid fa-gear shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Información técnica</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row">
                        <div class="col-12 col-md-4">
                            <b>Versión oficial</b><br />
                            v1.0.0
                        </div>

                        <div class="col-12 col-md-4">
                            <b>Ultima actualización</b><br />
                            Febrero 2026
                        </div>

                        <div class="col-12 col-md-4">
                            <b>Fecha de lanzamiento</b><br />
                            Febrero 2026
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-4">
                            <b>Plataforma</b><br />
                            .Net Framework 4.7.2
                        </div>
                    </div>

                </div>

                <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                    <div>
                        <i class="fa-solid fa-code shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Créditos</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row">
                        <div class="col-12 col-md-4">
                            <b>Desarrollado por</b><br />
                            Joimer Emanuel Ferreras Cuevas
                        </div>

                        <div class="col-12 col-md-4">
                            <b>Contacto del desarrollador</b><br />
                            <span id="correoDev">ferrerascuevasjoimer@gmail.com</span>

                            <button type="button"
                                class="btn btn-dark btn_copiar_correo"
                                onclick="copiarCorreo('ferrerascuevasjoimer@gmail.com', this)">
                                <i class="fa-solid fa-copy"></i><span></span>
                            </button>
                        </div>
                    </div>


                    <div class="row" style="margin-top: 20px;">

                        <div class="col-12 col-md-4">
                            <b>Librerías utilizadas</b><br />

                            <ul class="list-unstyled mb-0">
                                <li>• Font Awesome</li>
                                <li>• Tippy.js</li>
                                <li>• Bootstrap</li>
                            </ul>
                        </div>
                    </div>

                </div>




                <div class="panel-carga" id="divPanelCarga" style="visibility: hidden; z-index: 50000;">
                    <div class="d-flex justify-content-center">
                        <div class="spinner-border text-light" role="status">
                        </div>
                        <span class="text-light" style="text-align: center; margin-top: 5px; margin-left: 10px;">Cargando...</span>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="../Recursos/Javascript/scripts_general.js"></script>

</asp:Content>
