<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmResumen.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Resumen.frmResumen" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200&icon_names=trending_up" />


    <div style="padding: 20px;">
        <div class="contenedor-graficos">

            <!-- === estilos === -->
            <style>
                /* Card base */
                .kpi-card {
                    --bar-color: #2563eb; /* color por defecto (azul) */
                    --bar-w: 8px; /* ancho de la barra */
                    border: 1px solid #eef2f7;
                    border-radius: 16px;
                    background: linear-gradient(180deg,#fff 0%,#fbfcfe 100%);
                    position: relative;
                    overflow: hidden; /* recorta la barra en el radio */
                }
                    /* Barra vertical izquierda */
                    .kpi-card::before {
                        content: "";
                        position: absolute;
                        left: 0;
                        top: 0;
                        bottom: 0;
                        width: var(--bar-w);
                        background: linear-gradient(180deg, color-mix(in srgb, var(--bar-color) 92%, white 8%), var(--bar-color));
                    }
                /* Variantes de color */
                .kpi--green {
                    --bar-color: #22c55e;
                }
                /* ingresos */
                .kpi--red {
                    --bar-color: #ef4444;
                }
                /* egresos */
                .kpi--amber {
                    --bar-color: #f59e0b;
                }

                .kpi--blue {
                    --bar-color: #108df7;
                }
                

                /* Icono */
                .kpi-icon {
                    width: 52px;
                    height: 52px;
                    border-radius: 14px;
                    background: radial-gradient(120px 60px at 70% -30%, rgba(37,99,235,.12), transparent 60%), rgba(37,99,235,.08);
                    color: #2563eb;
                }

                .badge-delta {
                    background: #dcfce7;
                    color: #065f46;
                    font-weight: 700;
                }
            </style>

            <!-- === layout: cards lado a lado === -->
            <div class="container-fluid px-0">
                <div class="row g-3">
                    <!-- Ingresos del mes actual -->
                    <div class="col-12 col-md-6 col-xl-4">
                        <div class="card kpi-card kpi--green shadow-sm h-100" style="padding-left: 10px;">
                            <div class="card-body d-flex align-items-center justify-content-between">
                                <div>
                                    <div class="text-muted small fw-semibold">
                                        <label runat="server" id="lblTituloCard_TotalIngresos"></label>
                                    </div>
                                    <div class="h1 fw-bold mb-1">
                                        <asp:Label runat="server" ID="lblTotalIngresos_MesActual"></asp:Label></div>
                                </div>
                                <div class="text-end">
                                    <span class="badge rounded-pill badge-delta mb-2 label-porcentaje">
                                        <i class="bi bi-arrow-up-right me-1"></i>
                                        <label runat="server" id="lblPorcentajeTotalIngresos"></label>
                                        <%--Porcentaje de Ingresos con respecto al mes anterior--%>
                                    </span>
                                    <div class="kpi-icon d-inline-flex align-items-center justify-content-center ms-2" id="divIconTotalesIngresos" runat="server" style="background-color: #dcfce7; color: #065f46;">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Egresos del mes actual -->
                    <div class="col-12 col-md-6 col-xl-4">
                        <div class="card kpi-card kpi--red shadow-sm h-100" style="padding-left: 10px;">
                            <div class="card-body d-flex align-items-center justify-content-between">
                                <div>
                                    <div class="text-muted small fw-semibold">
                                        <label runat="server" id="lblTituloCard_TotalEgresos"></label>
                                    </div>
                                    <div class="h1 fw-bold mb-1">
                                        <asp:Label runat="server" ID="lblTotalEgresos_MesActual"></asp:Label></div>
                                </div>
                                <div class="text-end">
                                    <span class="badge rounded-pill label-porcentaje" style="background: #fee2e2; color: #991b1b; font-weight: 700;">
                                        <i class="bi bi-arrow-down-right me-1"></i>
                                        <label runat="server" id="lblPorcentajeTotalEgresos"></label>
                                        <%--Porcentaje de Egresos con respecto al mes anterior--%>
                                    </span>
                                    <div class="kpi-icon d-inline-flex align-items-center justify-content-center ms-2" style="background: rgba(239,68,68,.08); color: #ef4444;" id="divIconTotalesEgresos" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Neto del mes actual -->
                    <div class="col-12 col-md-6 col-xl-4">
                        <div class="card kpi-card kpi--amber shadow-sm h-100" style="padding-left: 10px;">
                            <div class="card-body d-flex align-items-center justify-content-between">
                                <div>
                                    <div class="text-muted small fw-semibold">
                                        <label runat="server" id="lblTituloCard_TotalNeto"></label>
                                    </div>
                                    <div class="h1 fw-bold mb-1">
                                        <asp:Label runat="server" ID="lblTotalNeto_MesActual"></asp:Label></div>
                                </div>
                                <div class="text-end">
                                    <span class="badge rounded-pill label-porcentaje" style="background: #fff7ed; color: #9a3412; font-weight: 700;">
                                        <i class="bi bi-exclamation-circle me-1"></i>
                                        <label runat="server" id="lblPorcentajeTotalNeto"></label>
                                        <%--Porcentaje Neto con respecto al mes anterior--%>
                                    </span>
                                    <div class="kpi-icon d-inline-flex align-items-center justify-content-center ms-2" style="background: rgba(245,158,11,.08); color: #f59e0b;" id="divIconTotalesNeto" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-6 col-md-6 col-xl-3">
                        <div class="card kpi-card kpi--blue shadow-sm h-100" style="padding-left: 10px;">
                            <div class="card-body d-flex align-items-center justify-content-between">
                                <div>
                                    <div class="text-muted small fw-semibold">
                                        Descripciones
                                    </div>
                                    <div class="h1 fw-bold mb-1">
                                        <asp:Label runat="server" ID="lblTotalDescripciones"></asp:Label>
                                    </div>
                                </div>
                                <div class="text-end">
                                    <div class="kpi-icon d-inline-flex align-items-center justify-content-center ms-2" style="background: rgba(12,99,228,.08); color: #0C63E4;">
                                        <i class="fa-solid fa-file-signature fa-lg"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-6 col-md-6 col-xl-3">
                        <div class="card kpi-card kpi--blue shadow-sm h-100" style="padding-left: 10px;">
                            <div class="card-body d-flex align-items-center justify-content-between">
                                <div>
                                    <div class="text-muted small fw-semibold">
                                        Misceláneos
                                    </div>
                                    <div class="h1 fw-bold mb-1">
                                        <asp:Label runat="server" ID="lblTotalMiscelaneos"></asp:Label>
                                    </div>
                                </div>
                                <div class="text-end">
                                    <div class="kpi-icon d-inline-flex align-items-center justify-content-center ms-2" style="background: rgba(12,99,228,.08); color: #0C63E4;">
                                        <i class="fa-solid fa-shuffle fa-lg"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-6 col-md-6 col-xl-3">
                        <div class="card kpi-card kpi--blue shadow-sm h-100" style="padding-left: 10px;">
                            <div class="card-body d-flex align-items-center justify-content-between">
                                <div>
                                    <div class="text-muted small fw-semibold">
                                        Formas de pago
                                    </div>
                                    <div class="h1 fw-bold mb-1">
                                        <asp:Label runat="server" ID="lblTotalFormasPago"></asp:Label>
                                    </div>
                                </div>
                                <div class="text-end">
                                    <div class="kpi-icon d-inline-flex align-items-center justify-content-center ms-2" style="background: rgba(12,99,228,.08); color: #0C63E4;">
                                        <i class="fa-solid fa-money-bill fa-lg"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-6 col-md-6 col-xl-3">
                        <div class="card kpi-card kpi--blue shadow-sm h-100" style="padding-left: 10px;">
                            <div class="card-body d-flex align-items-center justify-content-between">
                                <div>
                                    <div class="text-muted small fw-semibold">
                                        Miembros
                                    </div>
                                    <div class="h1 fw-bold mb-1">
                                        <asp:Label runat="server" ID="lblTotalMiembros"></asp:Label>
                                    </div>
                                </div>
                                <div class="text-end">
                                    <div class="kpi-icon d-inline-flex align-items-center justify-content-center ms-2" style="background: rgba(12,99,228,.08); color: #0C63E4;">
                                       
                                        <i class="fa-solid fa-church fa-lg"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="shadowed-div-body shadow-sm" style="width: 100%; margin-top: 20px; margin-right: 20px; flex-basis: 50%; white-space: nowrap; text-align: center; justify-content: center; align-items: center;">
                <div class="row" style="height: 100%;">
                    <div class="col-12 col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center; flex-direction: column; font-weight: bold; font-size: 20px;">Ingresos, Egresos y Neto</div>
                        <telerik:RadHtmlChart runat="server" ID="chtFinanzas" Width="100%" Height="420"
                            Transitions="true" Skin="Silk">
                            <PlotArea>
                                <Series>

                                    <telerik:ColumnSeries Name="Ingresos" DataFieldY="Ingresos">
                                        <Appearance>
                                            <FillStyle BackgroundColor="#16A34A" />
                                        </Appearance>
                                        <LabelsAppearance DataFormatString="RD${0:N2}" Position="OutsideEnd" />
                                        <TooltipsAppearance DataFormatString="<b>Ingresos:</b> RD${0:N2}" Color="White" />
                                    </telerik:ColumnSeries>


                                    <telerik:ColumnSeries Name="Egresos" DataFieldY="Egresos">
                                        <Appearance>
                                            <FillStyle BackgroundColor="#EF4444" />
                                        </Appearance>
                                        <LabelsAppearance DataFormatString="RD${0:N2}" Position="OutsideEnd" />
                                        <TooltipsAppearance DataFormatString="<b>Egresos:</b> RD${0:N2}" Color="White" />
                                    </telerik:ColumnSeries>


                                    <telerik:ColumnSeries Name="Neto" DataFieldY="Neto">
                                        <Appearance>
                                            <FillStyle BackgroundColor="#F59E0B" />
                                        </Appearance>
                                        <LabelsAppearance DataFormatString="RD${0:N2}" Position="OutsideEnd" />
                                        <TooltipsAppearance DataFormatString="<b>Neto:</b> RD${0:N2}" Color="Black" />
                                    </telerik:ColumnSeries>
                                </Series>

                                <XAxis DataLabelsField="MesNombre">
                                    <TitleAppearance Text="Mes" />
                                </XAxis>
                                <YAxis>
                                    <TitleAppearance Text="Monto" />
                                    <LabelsAppearance DataFormatString="{0:N0}" />
                                </YAxis>
                            </PlotArea>

                            <ChartTitle Text="Totales por mes (últimos 3 meses)" />
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











            <div class="shadowed-div-body shadow-sm d-none" style="width: 100%; margin-top: 20px; margin-right: 20px; flex-basis: 50%; white-space: nowrap; text-align: center; justify-content: center; align-items: center;">
                <div class="row" style="height: 100%;">
                    <div class="col-12 col-md-12">
                    </div>
                </div>
            </div>



            <div class="container-fluid px-0" style="margin-top: 20px;">
                <div class="row g-3">
                    <div class="col-12 col-md-12 col-xl-8">
                        <div class="card h-100"style="padding-left: 10px; border-radius: 16px; border: 0px;">
                            <div style="display: flex; justify-content: center; align-items: center; flex-direction: column; font-weight: bold; font-size: 20px; margin-top: 20px;">Cuentas por Cobrar</div>

                            <telerik:RadHtmlChart runat="server" ID="chCobrarMes" Width="100%" Height="360"
                                Transitions="true" Skin="Silk">
                                <PlotArea>
                                    <Series>
                                        <telerik:ColumnSeries Name="Cuentas por cobrar" DataFieldY="TotalCobrar">
                                            <Appearance>
                                                <FillStyle BackgroundColor="#16A34A" />
                                            </Appearance>
                                            <LabelsAppearance Position="OutsideEnd" DataFormatString="RD${0:N2}" />
                                            <TooltipsAppearance DataFormatString="<b>Cobrar:</b> RD${0:N2}" Color="White" />
                                        </telerik:ColumnSeries>
                                    </Series>

                                    <XAxis DataLabelsField="MesNombre">
                                        <TitleAppearance Text="Mes" />
                                    </XAxis>
                                    <YAxis>
                                        <TitleAppearance Text="Total" />
                                        <LabelsAppearance DataFormatString="{0:N0}" />
                                    </YAxis>
                                </PlotArea>

                                <ChartTitle Text="Totales por mes (ultimos 6 meses)" />
                                <Legend>
                                    <Appearance Position="Bottom" />
                                </Legend>
                            </telerik:RadHtmlChart>
                        </div>


                    </div>

                    <div class="col-12 col-md-12 col-xl-4">
                        <div class="card shadow-sm h-100" style="padding-left: 10px; border-radius: 16px; border: 0px;">
                            <telerik:RadHtmlChart runat="server" ID="chCxC_Antiguedad" Width="100%" Height="360"
                                Transitions="true" Skin="Silk">
                                <PlotArea>
                                    <Series>

                                        <telerik:DonutSeries Name="Antigüedad" DataFieldY="Total" NameField="Rango" ColorField="Color">
                                            <LabelsAppearance Position="OutsideEnd" DataFormatString="RD${0:N2}" />
                                            <TooltipsAppearance DataFormatString="RD${0:N2}" />
                                        </telerik:DonutSeries>
                                    </Series>
                                </PlotArea>
                                <ChartTitle Text="Antigüedad de Cuentas por Cobrar" />
                                <Legend>
                                    <Appearance Position="Right" />
                                </Legend>
                            </telerik:RadHtmlChart>
                        </div>

                    </div>
                </div>
            </div>

            <div class="container-fluid px-0" style="margin-top: 20px;">
                <div class="row g-3">
                    <div class="col-12 col-md-12 col-xl-8">
                        <div class="card h-100" style="padding-left: 10px; border-radius: 16px; border: 0px;">
                            <div style="display: flex; justify-content: center; align-items: center; flex-direction: column; font-weight: bold; font-size: 20px; margin-top: 20px;">Cuentas por Pagar</div>

                            <telerik:RadHtmlChart runat="server" ID="chPagarMes" Width="100%" Height="360"
                                Transitions="true" Skin="Silk">
                                <PlotArea>
                                    <Series>

                                        <telerik:ColumnSeries Name="Cuentas por pagar" DataFieldY="TotalPagar">
                                            <Appearance>
                                                <FillStyle BackgroundColor="#EF4444" />
                                            </Appearance>
                                            <LabelsAppearance Position="OutsideEnd" DataFormatString="RD${0:N2}" />
                                            <TooltipsAppearance DataFormatString="<b>Pagar:</b> RD${0:N2}" Color="White" />
                                        </telerik:ColumnSeries>
                                    </Series>

                                    <XAxis DataLabelsField="MesNombre">
                                        <TitleAppearance Text="Mes" />
                                    </XAxis>
                                    <YAxis>
                                        <TitleAppearance Text="Total" />
                                        <LabelsAppearance DataFormatString="{0:N0}" />
                                    </YAxis>
                                </PlotArea>

                                <ChartTitle Text="Totales por mes (Últimos 6 meses)" />
                                <Legend>
                                    <Appearance Position="Bottom" />
                                </Legend>
                            </telerik:RadHtmlChart>
                        </div>
                    </div>

                    <div class="col-12 col-md-12 col-xl-4">
                        <div class="card shadow-sm h-100" style="padding-left: 10px; border-radius: 16px; border: 0px;">
                            <telerik:RadHtmlChart runat="server" ID="chCxP_Antiguedad" Width="100%" Height="360"
                                Transitions="true" Skin="Silk">
                                <PlotArea>
                                    <Series>

                                        <telerik:DonutSeries Name="Antigüedad" DataFieldY="Total" NameField="Rango" ColorField="Color">
                                            <LabelsAppearance Position="OutsideEnd" DataFormatString="RD${0:N2}" />
                                            <TooltipsAppearance DataFormatString="RD${0:N2}" />
                                        </telerik:DonutSeries>
                                    </Series>
                                </PlotArea>
                                <ChartTitle Text="Antigüedad de Cuentas por Pagar" />
                                <Legend>
                                    <Appearance Position="Right" />
                                </Legend>
                            </telerik:RadHtmlChart>

                        </div>

                    </div>
                </div>
            </div>


            <div class="shadowed-div-body shadow-sm d-none" style="width: 100%; margin-top: 20px; margin-right: 20px; flex-basis: 50%; white-space: nowrap; text-align: center; justify-content: center; align-items: center;">
                <div class="row" style="height: 100%;">
                    <div class="col-12 col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center; flex-direction: column; font-weight: bold; font-size: 20px;">Ingresos, Egresos y Neto (últimos 3 meses)</div>

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

    <script>
        tippy('.label-porcentaje', {
            placement: 'top',
            content: 'Variación porcentual respecto al mes anterior',
            arrow: true,
        });


    </script>
</asp:Content>
