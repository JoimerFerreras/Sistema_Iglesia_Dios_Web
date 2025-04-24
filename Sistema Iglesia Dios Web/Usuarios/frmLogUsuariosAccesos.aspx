<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmLogUsuariosAccesos.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Usuarios.frmLogUsuariosAccesos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />

    <style type="text/css">
        /* Estilo css del RadTabStrip para que la linea del borde no sobresalga por el color*/
        .RadTabStrip_Bootstrap .rtsLevel1 {
            border-color: transparent;
        }

        .RadMap .km-scroll-wrapper {
            border-radius: 10px;
        }

        .RadMap .k-pos-bottom {
            display: none;
        }

        .RadMap .k-zoom-control {
            background: none;
            box-shadow: none;
        }

            .RadMap .k-zoom-control .k-button {
                border-radius: 10px;
                margin-left: 3px;
            }
    </style>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upPrincipal">
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="btnGenerarExcel_Detalle" />--%>
        </Triggers>
        <ContentTemplate>

            <div style="padding: 20px;">
                <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="rtsTabulador" Skin="Bootstrap" MultiPageID="rmpTabs" SelectedIndex="0" Style="margin-left: -1px;">
                    <Tabs>
                        <telerik:RadTab Text="Consulta" Font-Bold="true"></telerik:RadTab>
                        <telerik:RadTab Text="Mapa" Font-Bold="true"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage runat="server" ID="rmpTabs" SelectedIndex="0">
                    <telerik:RadPageView runat="server" ID="rpvConsulta" Style="margin: 0 auto;">
                        <div class="shadowed-div-body" style="width: 100%; border-radius: 0px 10px 10px 10px;">
                            <div>
                                <i class="fa-solid fa-filter shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Filtros</span>
                            </div>
                            <div class="linea-separador" style="margin-top: 20px;"></div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-3">
                                    Fecha inicial
                                     <br>
                                    <telerik:RadDatePicker ID="dtpFechaDesdeFiltro" runat="server" Width="100%" Culture="es-DO" TabIndex="2" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                                        <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-12 col-md-3">
                                    Fecha final
                                      <br>
                                    <telerik:RadDatePicker ID="dtpFechaHastaFiltro" runat="server" Width="100%" Culture="es-DO" TabIndex="3" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                                        <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-12 col-md-6">
                                    Usuario
                                     <telerik:RadComboBox ID="cmbUsuario_Filtro" runat="server" Width="100%" ClientIDMode="Static"
                                         MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                                         MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                         Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                         <Items>
                                             <telerik:RadComboBoxItem Text="Todos" Value="0" Selected="true" />
                                         </Items>
                                     </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>

                        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                            <div class="col-12 div-gridview">
                                <telerik:RadGrid RenderMode="Lightweight" ID="gvDatos" runat="server" Culture="es-DO" Style="overflow-x: auto;" BorderColor="White" MasterTableView-Width="100%" Width="100%" HeaderStyle-Font-Bold="true" AlternatingItemStyle-BackColor="#F1F5FF"
                                    AllowPaging="True" AllowAutomaticUpdates="True" AllowAutomaticInserts="False" MasterTableView-PagerStyle-PageSizeLabelText="Registros" Skin="Bootstrap" HeaderStyle-BackColor="#F1F5FF" PagerStyle-AlwaysVisible="true"
                                    AllowAutomaticDeletes="True" AllowSorting="True" PagerStyle-BorderStyle="None" BorderStyle="None" FooterStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" MasterTableView-PagerStyle-NextPagesToolTip="" MasterTableView-PagerStyle-PrevPagesToolTip=""
                                    FooterStyle-ForeColor="Black" HeaderStyle-ForeColor="Black" ItemStyle-ForeColor="Black" AlternatingItemStyle-ForeColor="Black" MasterTableView-PagerStyle-PagerTextFormat="{4} <strong>{5}</strong> Registros en <strong>{1}</strong> Páginas"
                                    MasterTableView-PagerStyle-FirstPageToolTip="" MasterTableView-PagerStyle-PrevPageToolTip="" MasterTableView-PagerStyle-NextPageToolTip="" MasterTableView-PagerStyle-LastPageToolTip="" MasterTableView-PageSize="50"
                                    OnPageIndexChanged="gvDatos_PageIndexChanged" OnPageSizeChanged="gvDatos_PageSizeChanged" OnSortCommand="gvDatos_SortCommand">
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <MasterTableView AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridTemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id_Log") %>' OnClick="btnEditar_Click" CssClass="btn btn-sm btn-primary fa-solid fa-location-dot boton_formulario_editar" Style="height: 30px; width: 30px; padding: 7px; border-radius: 15px; margin-bottom: 3px;"></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id_Log" HeaderText="ID" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Usuario" HeaderText="ID Usuario" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="NombreCompleto" HeaderText="Nombre Completo" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Usuario" HeaderText="Usuario" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="IPv4" HeaderText="IPv4" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="FechaHora_Login" HeaderText="Fecha/Hora de acceso" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Latitud_Coord" HeaderText="Latitud" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Longitud_Coord" HeaderText="Longitud" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>

                        <div class="contenedor_botones">
                            <asp:LinkButton CssClass="fa-solid fa-magnifying-glass fa-lg boton_formulario_Buscar" runat="server" ID="btnBuscar" OnClientClick="MostrarPanelCarga()" OnClick="btnBuscar_Click"></asp:LinkButton>
                            <asp:LinkButton CssClass="fa-solid fa-filter-circle-xmark fa-lg boton_formulario_LimpiarFiltros" runat="server" ID="btnLimpiarFiltros" OnClick="btnLimpiarFiltros_Click"></asp:LinkButton>
                        </div>
                    </telerik:RadPageView>


                    <telerik:RadPageView runat="server" ID="rpvMapa" Style="margin: 0 auto;">
                        <div class="shadowed-div-body" style="width: 100%;">
                            <div class="col-12 div-gridview">
                                <telerik:RadMap ID="RadMap1" runat="server" Width="100%" Height="600px" RenderMode="Lightweight" Skin="Bootstrap"
                                    Zoom="8" CenterSettings-Latitude="18.461500" CenterSettings-Longitude="-69.896500">
                                    <LayersCollection>
                                        <telerik:MapLayer Type="Tile" />
                                        <telerik:MapLayer Type="Marker">
                                        </telerik:MapLayer>
                                    </LayersCollection>
                                </telerik:RadMap>
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>

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
