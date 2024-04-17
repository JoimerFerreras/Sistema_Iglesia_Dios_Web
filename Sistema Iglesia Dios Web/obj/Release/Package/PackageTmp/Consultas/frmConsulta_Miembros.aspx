<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConsulta_Miembros.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Consultas.frmConsulta_Miembros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />

    <div style="padding: 20px;">
        <div class="shadowed-div-body" style="width: 100%;">
            <div>
                <i class="fa-solid fa-filter shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo"> Filtros</span>
            </div>
            <div class="linea-separador" style="margin-top: 20px;"></div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-6">
                    Tipo de fecha
                    <telerik:RadRadioButtonList runat="server" ID="rbtnTipoFecha" RepeatDirection="Horizontal" RepeatColumns="5" TabIndex="1" Direction="Horizontal" RenderMode="Lightweight" Skin="Bootstrap" AutoPostBack="false">
                        <Items>
                            <telerik:ButtonListItem Value="2" Text="Fecha de miembro" Selected = "True"></telerik:ButtonListItem>
                            <telerik:ButtonListItem Value="1" Text="Fecha de nacimiento"></telerik:ButtonListItem>
                        </Items>
                    </telerik:RadRadioButtonList>
                </div>
            </div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-3">
                    Fecha inicial
                    <br>
                    <telerik:RadDatePicker ID="dtpFechaDesde" runat="server" Width="100%" Culture="es-DO" TabIndex="2" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                    </telerik:RadDatePicker>
                </div>
                <div class="col-12 col-md-3">
                    Fecha final
                    <br>
                    <telerik:RadDatePicker ID="dtpFechaHasta" runat="server" Width="100%" Culture="es-DO" TabIndex="3" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                    </telerik:RadDatePicker>
                </div>

                <div class="col-12 col-md-6">
                    Nombre, Apellido, Nombre de pila o ID
                    <asp:TextBox runat="server" ID="txtTextoBusqueda" CssClass="form-control form-control" Width="100%" placeholder="Introducir nombre, apellido, nombre de pila o ID" MaxLength="100" TabIndex="2"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-3">
                    <div>
                        Sexo
                    </div>
                    <telerik:RadComboBox ID="cmbSexo" runat="server" Width="100%" ClientIDMode="Static" Style="max-width: 200px;"
                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="Todos" Value="0" Selected="true" />
                            <telerik:RadComboBoxItem Text="Masculino" Value="1" />
                            <telerik:RadComboBoxItem Text="Femenino" Value="2" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div class="col-12 col-md-3">
                    <div>
                        Estado civil
                    </div>
                    <telerik:RadComboBox ID="cmbEstadoCivil" runat="server" Width="100%" ClientIDMode="Static" Style="max-width: 200px;"
                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="Todos" Value="0" Selected="true" />
                            <telerik:RadComboBoxItem Text="Soltero/a" Value="1" />
                            <telerik:RadComboBoxItem Text="Casado/a" Value="2" />
                            <telerik:RadComboBoxItem Text="Unión libre" Value="3" />
                            <telerik:RadComboBoxItem Text="Otro" Value="4" />
                        </Items>
                    </telerik:RadComboBox>
                </div>

            <div class="col-12 col-md-6">
                Ministerio perteneciente
                <telerik:RadComboBox ID="cmbMinisterio" runat="server" Width="100%" ClientIDMode="Static"
                    MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                    MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                    Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                    <Items>
                        <telerik:RadComboBoxItem Text="Todos" Value="0" Selected="true" />
                    </Items>
                </telerik:RadComboBox>

            </div>
        </div>
        <div class="row" style="margin-top: 20px;">
        </div>
    </div>


    <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
        <div>
            <i class="fa-solid fa-table-list shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo"> Resultado</span>
        </div>
        <div class="linea-separador" style="margin-top: 20px;"></div>

        <div class="row" style="margin-top: 20px;">
            <div class="col-12 div-gridview">
                <telerik:RadGrid RenderMode="Lightweight" ID="gvDatos" runat="server" Culture="es-DO" Style="overflow-x: auto;" BorderColor="White" MasterTableView-Width="100%" Width="100%" HeaderStyle-Font-Bold="true" AlternatingItemStyle-BackColor="#F1F5FF"
                    AllowPaging="True" AllowAutomaticUpdates="True" AllowAutomaticInserts="False" MasterTableView-PagerStyle-PageSizeLabelText="Registros" Skin="Bootstrap" HeaderStyle-BackColor="#F1F5FF" PagerStyle-AlwaysVisible="true"
                    AllowAutomaticDeletes="True" AllowSorting="True" PagerStyle-BorderStyle="None" BorderStyle="None" FooterStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" MasterTableView-PagerStyle-NextPagesToolTip="" MasterTableView-PagerStyle-PrevPagesToolTip=""
                    FooterStyle-ForeColor="Black" HeaderStyle-ForeColor="Black" ItemStyle-ForeColor="Black" AlternatingItemStyle-ForeColor="Black" MasterTableView-PagerStyle-PagerTextFormat="{4} <strong>{5}</strong> Registros en <strong>{1}</strong> Páginas"
                    MasterTableView-PagerStyle-FirstPageToolTip="" MasterTableView-PagerStyle-PrevPageToolTip="" MasterTableView-PagerStyle-NextPageToolTip="" MasterTableView-PagerStyle-LastPageToolTip=""
                    OnPageIndexChanged="gvDatos_PageIndexChanged" OnPageSizeChanged="gvDatos_PageSizeChanged" OnSortCommand="gvDatos_SortCommand">
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <MasterTableView AutoGenerateColumns="False">
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id_Miembro") %>' OnClick="btnEditar_Click" CssClass="btn btn-sm btn-primary fa-solid fa-pen boton_formulario_editar" Style="height: 30px; width: 30px; padding: 7px; border-radius: 15px; margin-bottom: 3px;"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Id_Miembro" HeaderText="ID" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Numero_Alternativo_Miembro" HeaderText="Núm Alt." HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Miembro" HeaderText="Miembro" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Fecha_Nacimiento" HeaderText="Fecha de nacimiento" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Desde_Cuando_Miembro" HeaderText="Fecha de miembro" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Ministerios" HeaderText="Ministerios perteneciente" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>


    <div class="contenedor_botones">
        <asp:LinkButton CssClass="fa-solid fa-magnifying-glass fa-lg boton_formulario_Buscar" runat="server" ID="btnBuscar" OnClientClick="MostrarPanelCarga()" OnClick="btnBuscar_Click"></asp:LinkButton>
        <asp:LinkButton CssClass="fa-solid fa-filter-circle-xmark fa-lg boton_formulario_LimpiarFiltros" runat="server" ID="btnLimpiarFiltros" OnClick="btnLimpiarFiltros_Click"></asp:LinkButton>
        <asp:LinkButton CssClass="fa-solid fa-file-pdf fa-lg boton_formulario_Agregar" runat="server" ID="btnGenerarPDF" OnClick="btnGenerarPDF_Click" OnClientClick="MostrarPanelCarga()" data-tippy-content="Generar reporte en PDF"></asp:LinkButton>
        <asp:LinkButton CssClass="fa-solid fa-file-excel fa-lg boton_formulario_Agregar" runat="server" ID="btnGenerarExcel" OnClick="btnGenerarExcel_Click" data-tippy-content="Generar reporte en Excel"></asp:LinkButton>
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
