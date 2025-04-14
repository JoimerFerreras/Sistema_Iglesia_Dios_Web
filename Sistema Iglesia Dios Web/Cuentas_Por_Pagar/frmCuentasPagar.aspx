﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCuentasPagar.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Cuentas_Por_Pagar.frmCuentasPagar" %>

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
    </style>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upPrincipal">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGenerarPDF_Detalle" />
            <asp:PostBackTrigger ControlID="btnGenerarExcel_Detalle" />
            <asp:PostBackTrigger ControlID="btnGenerarExcel_Resumen" />
        </Triggers>
        <ContentTemplate>

            <div style="padding: 20px;">
                <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="rtsTabulador" Skin="Bootstrap" MultiPageID="rmpTabs" SelectedIndex="0" Style="margin-left: -1px;">
                    <Tabs>
                        <telerik:RadTab Text="Consulta" Font-Bold="true"></telerik:RadTab>
                        <telerik:RadTab Text="Registro" Font-Bold="true"></telerik:RadTab>
                        <telerik:RadTab Text="Archivos" Font-Bold="true"></telerik:RadTab>
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
                                <div class="col-12 col-md-6">
                                    Tipo de fecha
                                    <telerik:RadRadioButtonList runat="server" ID="rbtnTipoFecha" RepeatDirection="Horizontal" RepeatColumns="5" TabIndex="1" Direction="Horizontal" RenderMode="Lightweight" Skin="Bootstrap" AutoPostBack="false">
                                        <Items>
                                            <telerik:ButtonListItem Value="1" Text="Fecha" Selected="True"></telerik:ButtonListItem>
                                            <telerik:ButtonListItem Value="2" Text="Fecha de registro"></telerik:ButtonListItem>
                                        </Items>
                                    </telerik:RadRadioButtonList>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Fecha inicial
                                    <br>
                                    <telerik:RadDatePicker ID="dtpFechaDesdeFiltro" runat="server" Width="100%" Culture="es-DO" TabIndex="2" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                                        <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-12 col-md-6">
                                    Fecha final
                                    <br>
                                    <telerik:RadDatePicker ID="dtpFechaHastaFiltro" runat="server" Width="100%" Culture="es-DO" TabIndex="3" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                                        <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>


                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Miembro 
                                    <telerik:RadComboBox ID="cmbMiembro_Consulta" runat="server" Width="100%" ClientIDMode="Static"
                                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Todos" Value="0" Selected="true" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Miscelaneo
                                <telerik:RadComboBox ID="cmbMiscelaneo_Consulta" runat="server" Width="100%" ClientIDMode="Static"
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
                                <div class="col-12 col-md-6">
                                    Descripción 
                                    <telerik:RadComboBox ID="cmbDescripcion_Consulta" runat="server" Width="100%" ClientIDMode="Static"
                                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Todos" Value="0" Selected="true" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Tipo de documento
                                <telerik:RadComboBox ID="cmbTipoDocumento_Consulta" runat="server" Width="100%" ClientIDMode="Static"
                                    MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                                    MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                    Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Todos" Value="0" Selected="true" />
                                        <telerik:RadComboBoxItem Text="FT" Value="FT" />
                                        <telerik:RadComboBoxItem Text="NC" Value="NC" />
                                        <telerik:RadComboBoxItem Text="RI" Value="RI" />
                                        <telerik:RadComboBoxItem Text="ND" Value="ND" />
                                    </Items>
                                </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>

                        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                            <div>
                                <i class="fa-solid fa-table-list shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Detalle</span>
                                <asp:LinkButton runat="server" ID="btnGenerarPDF_Detalle" CssClass="btn btn-secondary" OnClick="btnGenerarPDF_Detalle_Click" OnClientClick="MostrarPanelCarga()"><i class="fa-solid fa-file-pdf"></i> Generar reporte PDF</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnGenerarExcel_Detalle" CssClass="btn btn-success" OnClick="btnGenerarExcel_Detalle_Click"><i class="fa-solid fa-file-excel"></i> Generar Excel</asp:LinkButton>
                            </div>
                            <div class="linea-separador" style="margin-top: 20px;"></div>

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
                                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id_Cuenta_Pagar") %>' OnClick="btnEditar_Click" CssClass="btn btn-sm btn-primary fa-solid fa-pen boton_formulario_editar" Style="height: 30px; width: 30px; padding: 7px; border-radius: 15px; margin-bottom: 3px;"></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cuenta_Pagar" HeaderText="ID" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Fecha_CP" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Miembro" HeaderText="Miembro" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Miscelaneo" HeaderText="Misceláneo" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Valor" HeaderText="Valor" DataFormatString="{0:0,0.00}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Tipo_Documento" HeaderText="Tipo de Documento" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Descripcion_Forma_Pago" HeaderText="Medio de pago" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Fecha_Registro" HeaderText="Fecha de registro" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>

                        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                            <div>
                                <i class="fa-solid fa-table-list shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Vista de libro diario</span>
                                <asp:LinkButton runat="server" ID="btnGenerarPDF_Resumen" CssClass="btn btn-secondary" OnClick="btnGenerarPDF_Resumen_Click" OnClientClick="MostrarPanelCarga()"><i class="fa-solid fa-file-pdf"></i> Generar reporte PDF</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnGenerarExcel_Resumen" CssClass="btn btn-success" OnClick="btnGenerarExcel_Resumen_Click"><i class="fa-solid fa-file-excel"></i> Generar Excel</asp:LinkButton>
                            </div>
                            <div class="linea-separador" style="margin-top: 20px;"></div>

                            <div class="col-12 div-gridview">
                                <telerik:RadGrid RenderMode="Lightweight" ID="gvResumen" runat="server" Culture="es-DO" Style="overflow-x: auto;" BorderColor="White" MasterTableView-Width="100%" Width="100%" HeaderStyle-Font-Bold="true" AlternatingItemStyle-BackColor="#F1F5FF"
                                    AllowPaging="True" AllowAutomaticUpdates="True" AllowAutomaticInserts="False" MasterTableView-PagerStyle-PageSizeLabelText="Registros" Skin="Bootstrap" HeaderStyle-BackColor="#F1F5FF" PagerStyle-AlwaysVisible="true"
                                    AllowAutomaticDeletes="True" AllowSorting="True" PagerStyle-BorderStyle="None" BorderStyle="None" FooterStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" MasterTableView-PagerStyle-NextPagesToolTip="" MasterTableView-PagerStyle-PrevPagesToolTip=""
                                    FooterStyle-ForeColor="Black" HeaderStyle-ForeColor="Black" ItemStyle-ForeColor="Black" AlternatingItemStyle-ForeColor="Black" MasterTableView-PagerStyle-PagerTextFormat="{4} <strong>{5}</strong> Registros en <strong>{1}</strong> Páginas"
                                    MasterTableView-PagerStyle-FirstPageToolTip="" MasterTableView-PagerStyle-PrevPageToolTip="" MasterTableView-PagerStyle-NextPageToolTip="" MasterTableView-PagerStyle-LastPageToolTip=""
                                    OnPageIndexChanged="gvResumen_PageIndexChanged" OnPageSizeChanged="gvResumen_PageSizeChanged" OnSortCommand="gvResumen_SortCommand">
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <MasterTableView AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Id_Cuenta_Pagar" HeaderText="ID" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="No_Documento" HeaderText="No. Documento" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Descripcion_Cuenta" HeaderText="Descripción" HeaderStyle-Width="40%" ItemStyle-Width="40%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="Débito" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("Debito").ToString() == "0" ? "" : String.Format("{0:0,0.00}", Eval("Debito")) %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                                                                        <telerik:GridTemplateColumn HeaderText="Crédito" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <%# Eval("Credito").ToString() == "0" ? "" : String.Format("{0:0,0.00}", Eval("Credito")) %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridBoundColumn DataField="Balance" HeaderText="Balance" DataFormatString="{0:0,0.00}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
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


                    <telerik:RadPageView runat="server" ID="rpvRegistro" Style="margin: 0 auto;">
                        <div class="shadowed-div-body" style="width: 100%;">
                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    ID
                               <asp:TextBox runat="server" ID="txtIdCuentaPagar" CssClass="form-control form-control" Width="100%" ReadOnly="true" TabIndex="1" Style="max-width: 150px;"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Miembro 
                                 <telerik:RadComboBox ID="cmbMiembro" runat="server" Width="100%" ClientIDMode="Static"
                                     MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                                     MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                     Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                     <Items>
                                         <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                                     </Items>
                                 </telerik:RadComboBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Misceláneo 
                                    <div class="d-flex align-items-center">
                                        <telerik:RadComboBox ID="cmbMiscelaneo" runat="server" Width="100%" ClientIDMode="Static"
                                            MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                                            MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                            Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <button type="button" class="btn btn-primary ml-2 fa-solid fa-plus fa-lg" onclick="mostrarContenido(2)"
                                            style="margin-left: 10px; height: 37px;" data-toggle="modal" data-target="#exampleModal" data-tippy-content="Cuentas por Pagar" title="" data-whatever="@mdo">
                                        </button>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Descripción <span class="LabelCampoObligatorio">*</span>
                                    <div class="d-flex align-items-center">
                                        <telerik:RadComboBox ID="cmbDescripcion" runat="server" Width="100%" ClientIDMode="Static"
                                            MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                                            MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                            Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <button type="button" class="btn btn-primary ml-2 fa-solid fa-plus fa-lg" onclick="mostrarContenido(1)"
                                            style="margin-left: 10px; height: 37px;" data-toggle="modal" data-target="#exampleModal" data-tippy-content="Agregar descripción" data-whatever="@mdo">
                                        </button>
                                    </div>
                                </div>

                                <div class="col-12 col-md-6">
                                    Forma de pago <span class="LabelCampoObligatorio">*</span><br />
                                    <telerik:RadComboBox ID="cmbFormaPago" runat="server" Width="300px" ClientIDMode="Static"
                                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Fecha <span class="LabelCampoObligatorio">*</span>
                                    <br />
                                    <telerik:RadDatePicker ID="dtpFechaCC" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                        <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                                    </telerik:RadDatePicker>
                                </div>

                                <div class="col-12 col-md-6">
                                    Valor <span class="LabelCampoObligatorio">*</span>
                                    <asp:TextBox runat="server" ID="txtValor" CssClass="form-control" Width="200px" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Tipo Documento <span class="LabelCampoObligatorio">*</span><br />
                                    <telerik:RadComboBox ID="cmbTipoDocumento" runat="server" Width="300px" ClientIDMode="Static"
                                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                                            <telerik:RadComboBoxItem Text="FT" Value="FT" />
                                            <telerik:RadComboBoxItem Text="NC" Value="NC" />
                                            <telerik:RadComboBoxItem Text="RI" Value="RI" />
                                            <telerik:RadComboBoxItem Text="ND" Value="ND" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    No. Documento
                                    <asp:TextBox runat="server" ID="txtNo_Documento" CssClass="form-control" Width="200px" MaxLength="30" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-12">
                                    Comentario
                               <asp:TextBox runat="server" ID="txtComentario" CssClass="form-control" Height="76" TextMode="MultiLine" MaxLength="500" Width="100%" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                            <div>
                                <i class="fa-solid fa-file-waveform shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Historial de registro y modificación de la cuenta por pagar</span>
                            </div>
                            <div class="linea-separador" style="margin-top: 20px;"></div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Usuario que hizo el registro
                               <asp:TextBox runat="server" ID="txtUsuarioRegistro" CssClass="form-control" Width="100%" TabIndex="2" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Fecha de registro
                                  <asp:TextBox runat="server" ID="txtFechaRegistro" CssClass="form-control" Width="100%" TabIndex="2" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Usuario de ultima modificaci&oacute;n
                                  <asp:TextBox runat="server" ID="txtUsuarioUltimaModificacion" CssClass="form-control" Width="100%" TabIndex="2" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Fecha de ultima modificaci&oacute;n
                                 <asp:TextBox runat="server" ID="txtFechaUltimaModificacion" CssClass="form-control" Width="100%" TabIndex="2" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="contenedor_botones">
                            <asp:LinkButton CssClass="fa-solid fa-plus fa-lg boton_formulario_Agregar" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click" OnClientClick="MostrarPanelCarga()"></asp:LinkButton>
                            <asp:LinkButton CssClass="fa-solid fa-floppy-disk fa-lg boton_formulario_Guardar" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" OnClientClick="MostrarPanelCarga()"></asp:LinkButton>
                            <asp:LinkButton CssClass="fa-solid fa-trash fa-lg boton_formulario_Eliminar" runat="server" ID="btnEliminar" OnClick="btnEliminar_Click" OnClientClick="return delalert(this);"></asp:LinkButton>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="rpvArchivos" Style="margin: 0 auto;">
                        <div class="shadowed-div-body" style="width: 100%;">

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-12">
                                    <asp:FileUpload ID="FileUpload1" ClientIDMode="Static" runat="server" CssClass="form-control drop-zone" Accept=".xlsx, .xls, .doc, .docx, .pdf, .jpg, .jpeg, .png, .txt" EnableViewState="true" ViewStateMode="Enabled" OnClick="btnUpload_Click" Enabled="true" Width="100%" SkinID="Bootstrap" />
                                </div>
                            </div>
                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-4">
                                    Nombre del archivo
                                  <asp:TextBox runat="server" ID="txtNombreArchivo" CssClass="form-control" MaxLength="250" Width="100%" TabIndex="2"></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-6">
                                    Descripción del archivo
                                 <asp:TextBox runat="server" ID="txtDescripcionArchivo" CssClass="form-control" MaxLength="250" Width="100%" TabIndex="2"></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-2">
                                    <br />
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubirArchivo" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnSubirArchivo" CssClass="btn btn-primary" OnClick="btnSubirArchivo_Click" Width="100%" Text="Subir archivo" />
                                            <%--<asp:Button runat="server" ID="btnCargarArchivo" Text="Cargar archivo" CssClass="btn btn-primary" OnClick="btnCargarArchivo_Click" OnClientClick="MostrarPanelCarga()" Enabled="false"></asp:Button>--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </div>
                        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-12">
                                    <telerik:RadGrid RenderMode="Lightweight" ID="gvArchivos" runat="server" Culture="es-DO" Style="overflow-x: auto;" BorderColor="White" MasterTableView-Width="100%" Width="100%" HeaderStyle-Font-Bold="true" AlternatingItemStyle-BackColor="#F1F5FF"
                                        AllowPaging="True" AllowAutomaticUpdates="True" AllowAutomaticInserts="False" MasterTableView-PagerStyle-PageSizeLabelText="Registros" Skin="Bootstrap" HeaderStyle-BackColor="#F1F5FF" PagerStyle-AlwaysVisible="true"
                                        AllowAutomaticDeletes="True" AllowSorting="True" PagerStyle-BorderStyle="None" BorderStyle="None" FooterStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" MasterTableView-PagerStyle-NextPagesToolTip="" MasterTableView-PagerStyle-PrevPagesToolTip=""
                                        FooterStyle-ForeColor="Black" HeaderStyle-ForeColor="Black" ItemStyle-ForeColor="Black" AlternatingItemStyle-ForeColor="Black" MasterTableView-PagerStyle-PagerTextFormat="{4} <strong>{5}</strong> Registros en <strong>{1}</strong> Páginas"
                                        MasterTableView-PagerStyle-FirstPageToolTip="" MasterTableView-PagerStyle-PrevPageToolTip="" MasterTableView-PagerStyle-NextPageToolTip="" MasterTableView-PagerStyle-LastPageToolTip="" OnItemDataBound="gvArchivos_ItemDataBound"
                                        OnPageIndexChanged="gvDatos_PageIndexChanged" OnPageSizeChanged="gvDatos_PageSizeChanged" OnSortCommand="gvDatos_SortCommand">
                                        <PagerStyle Mode="NextPrevAndNumeric" />
                                        <MasterTableView AutoGenerateColumns="False">
                                            <Columns>
                                                <telerik:GridTemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id_Archivo") %>' OnClick="btnSeleccionarArchivoDescargar_Click" ID="btnDescargarArchivo" ClientIDMode="Static" CssClass="btn btn-sm btn-success fa-solid fa-cloud-arrow-down boton_formulario_descargar_archivo"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id_Archivo") %>' OnClick="btnEliminarArchivo_Click" OnClientClick="return delalert(this);" CssClass="btn btn-sm btn-danger fa-solid fa-trash" Style="height: 30px; width: 30px; padding: 7px; border-radius: 15px;"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridBoundColumn DataField="Id_Archivo" HeaderText="ID" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="NombreArchivo" HeaderText="Nombre" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripcion" HeaderStyle-Width="25%" ItemStyle-Width="25%">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="NombreArchivoCarpeta" HeaderText="Archivo" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="Tamano" HeaderText="Tamaño (MB)" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="Fecha_Registro" HeaderText="Fecha de registro" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
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

    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" id="conten1" style="display: none;">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Descripción</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="col-12 col-md-12">
                        Descripción
                    <asp:TextBox runat="server" ID="txtDescripcionAgregar" CssClass="form-control" MaxLength="100" Width="100%" TabIndex="2"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button runat="server" ID="btnAgregarDescripcion" CssClass="btn btn-primary" OnClick="btnAgregarDescripcion_Click" Text="Agregar descripción" data-bs-dismiss="modal"></asp:Button>
                </div>
            </div>

            <div class="modal-content" id="conten2" style="display: none;">
                <div class="modal-header">
                    <h5 class="modal-title">Misceláneo</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="col-12 col-md-12">
                        Misceláneo
                    <asp:TextBox runat="server" ID="txtMiscelaneoAgregar" CssClass="form-control" MaxLength="100" Width="100%" TabIndex="2"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button runat="server" ID="btnAgregarMiscelaneo" CssClass="btn btn-primary" OnClick="btnAgregarMiscelaneo_Click" Text="Agregar misceláneo" data-bs-dismiss="modal"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <style>
        .drop-zone {
            border: 2px dashed #ccc;
            border-radius: 10px;
            padding: 50px;
            text-align: center !important;
            align-content: center;
            align-items: center;
            cursor: pointer;
            color: #ccc;
            transition: background-color 0.3s ease, color 0.3s ease, border-color 0.3s ease;
        }

            .drop-zone:hover {
                border-color: #000;
                background-color: #ecf9ff;
                color: #005181;
            }
    </style>

    <script>
        function mostrarContenido(numero) {
            // Ocultar ambos contenidos
            document.getElementById("conten1").style.display = "none";
            document.getElementById("conten2").style.display = "none";

            // Mostrar el contenido correspondiente
            if (numero === 1) {
                document.getElementById("conten1").style.display = "flex";
            } else if (numero === 2) {
                document.getElementById("conten2").style.display = "flex";
            }

            // Mostrar el modal
            var modal = new bootstrap.Modal(document.getElementById('exampleModal'));
            modal.show();
        }

    </script>
</asp:Content>
