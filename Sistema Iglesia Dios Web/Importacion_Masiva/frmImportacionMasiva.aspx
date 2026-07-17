<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmImportacionMasiva.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Importacion_Masiva.frmImportacionMasiva" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />

    <div style="padding: 20px;">

        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
            <div>
                <i class="fa-solid fa-file-pen shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Plantillas de importación</span>
            </div>
            <div class="linea-separador" style="margin-top: 20px;"></div>
            <div class="row">
                <div class="col-12 col-md-6">
                    Plantilla
                 <telerik:RadComboBox ID="cmbPlantillas" runat="server" Width="100%" ClientIDMode="Static"
                     MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                     MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                     Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                     <Items>
                         <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                         <telerik:RadComboBoxItem Text="Miembros" Value="1" />
                     </Items>
                 </telerik:RadComboBox>

                </div>

                <div class="col-12 col-md-4"
                    style="padding-top: 24px;">
                    <asp:LinkButton
    ID="btnDescargarPlantilla"
    runat="server"
    CssClass="btn btn-success"
    OnClick="btnDescargarPlantilla_Click"
    data-tippy-content="Descargar plantilla de importación">

    <i class="fa-solid fa-cloud-arrow-down"></i>
    Descargar plantilla
</asp:LinkButton>
                </div>

            </div>
        </div>



        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
            <div>
                <i class="fa-solid fa-file-pen shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Importación masiva</span>
            </div>
            <div class="linea-separador" style="margin-top: 20px;"></div>
            <div class="row">
                <div class="col-12 col-md-6">
                    Entidad a importar
                 <telerik:RadComboBox ID="cmbEntidad" runat="server" Width="100%" ClientIDMode="Static"
                     MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                     MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                     Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                     <Items>
                         <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                         <telerik:RadComboBoxItem Text="Miembros" Value="1" />
                     </Items>
                 </telerik:RadComboBox>

                </div>

            </div>


            <div class="linea-separador"
                style="margin-top: 20px;">
            </div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-8">
                    Archivo Excel (.xlsx)

            <asp:FileUpload
                ID="fuImportarMiembros"
                runat="server"
                CssClass="form-control"
                accept=".xlsx" />

                    <small class="text-muted">Para miembros utilice la plantilla oficial. Los campos
                Nombres, Apellidos y Sexo son obligatorios.
                    </small>
                </div>

                <div class="col-12 col-md-4"
                    style="padding-top: 24px;">
                    <asp:LinkButton
                        ID="btnImportarMiembros"
                        runat="server"
                        CssClass="btn btn-success"
                        OnClick="btnImportarMiembros_Click"
                        OnClientClick="MostrarPanelCarga()"
                        data-tippy-content="Importar miembros desde Excel">

                <i class="fa-solid fa-file-import"></i>
                Importar miembros
                    </asp:LinkButton>
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

        <script src="../Recursos/Javascript/scripts_general.js"></script>
    </div>
</asp:Content>
