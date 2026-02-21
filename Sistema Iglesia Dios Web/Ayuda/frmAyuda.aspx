<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAyuda.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Ayuda.frmAyuda" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />


    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upPrincipal">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDescargarManualUsuario" />
        </Triggers>
        <ContentTemplate>

            <div style="padding: 20px;">

                <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                    <div>
                        <i class="fa-solid fa-book-open shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Manual de usuario</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row">
                        <div class="col-12 col-md-3">
                            <asp:LinkButton CssClass="btn btn-primary"  runat="server" ID="btnDescargarManualUsuario" OnClick="btnDescargarManualUsuario_Click" Text="<i class='fa-solid fa-cloud-arrow-down' style='margin-right: 5px;'></i> Descargar manual de usuario"></asp:LinkButton>
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
