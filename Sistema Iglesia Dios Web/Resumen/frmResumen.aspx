<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmResumen.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Resumen.frmResumen" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />

    <div style="padding: 20px;">
        <div class="contenedor-graficos">
             <div class="shadowed-div-body" style="width: 100%; margin-top: 20px; margin-right: 20px; flex-basis: 50%; white-space: nowrap; text-align: center; justify-content: center; align-items: center;">
                <div class="row" style="height: 100%;">
                    <div class="col-12 col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center; flex-direction: column; font-weight: bold; font-size: 20px;">Ingresos, Egresos y Neto (últimos 3 meses)</div>
                        <telerik:RadHtmlChart runat="server" ID="chtFinanzas" Width="100%" Height="420"
                            Transitions="true" Skin="Silk">
                            <PlotArea>
                                <Series>
                                    <telerik:ColumnSeries Name="Ingresos" DataFieldY="Ingresos">
                                        <LabelsAppearance DataFormatString="{0:C0}" Position="OutsideEnd" />
                                        <TooltipsAppearance DataFormatString="Ingresos: {0:C0}" />
                                    </telerik:ColumnSeries>

                                    <telerik:ColumnSeries Name="Egresos" DataFieldY="Egresos">
                                        <LabelsAppearance DataFormatString="{0:C0}" Position="OutsideEnd" />
                                        <TooltipsAppearance DataFormatString="Egresos: {0:C0}" />
                                    </telerik:ColumnSeries>

                                    <telerik:ColumnSeries Name="Neto" DataFieldY="Neto">
                                        <LabelsAppearance DataFormatString="{0:C0}" Position="OutsideEnd" />
                                        <TooltipsAppearance DataFormatString="Neto: {0:C0}" />
                                    </telerik:ColumnSeries>
                                </Series>

                                <XAxis DataLabelsField="MesNombre">
                                    <TitleAppearance Text="Mes" />
                                </XAxis>

                                <YAxis>
                                    <TitleAppearance Text="Monto"/>
                                    <LabelsAppearance DataFormatString="{0:C0}" />
                                    
                                </YAxis>
                            </PlotArea>

                            <ChartTitle Text="" />
                            <Legend>
                                <Appearance Position="Bottom" />
                            </Legend>
                        </telerik:RadHtmlChart>

                        <div runat="server" id="divMensaje_graficoIngresosMes" style="width: 100%; height: 100%; display: flex; justify-content: center; align-items: center; text-align: center; flex-direction: column;" visible="false">
                            <div style="margin-top: 20px;">
                                <i class="fa-solid fa-ban" style="color: #b3b4b5; font-size: 32px;"></i>
                                <p style="color: #b3b4b5; font-weight: 100; font-size: 16px; margin-top: 5px;">No hay datos para mostrar</p>
                            </div>
                        </div>
                    </div>
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
