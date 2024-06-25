﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmIngresos.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Ingresos.frmIngresos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />

    <div style="padding: 20px;">
        <div class="shadowed-div-body" style="width: 100%;">
            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-6">
                    ID
                    <asp:TextBox runat="server" ID="txtId_Ingreso" CssClass="form-control form-control" Width="100%" ReadOnly="true" TabIndex="1" Style="max-width: 150px;"></asp:TextBox>
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
                    Descripción
                    <div class="d-flex align-items-center">
                        <telerik:RadComboBox ID="cmbDescripcion_Ingreso" runat="server" Width="100%" ClientIDMode="Static"
                            MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                            MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                            Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                            </Items>
                        </telerik:RadComboBox>
                        <button type="button" id="btnAbrirPanelDescripcion" class="btn btn-primary ml-2 fa-solid fa-plus fa-lg" style="margin-left: 10px; height: 37px;" data-toggle="modal" data-target="#exampleModal" data-tippy-content="Agregar descripción" data-whatever="@mdo"></button>
                    </div>
                </div>
            </div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-6">
                    Fecha de Ingreso <span class="LabelCampoObligatorio">*</span>
                    <br />
                    <telerik:RadDatePicker ID="dtpFechaIngreso" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                        <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                    </telerik:RadDatePicker>
                </div>

                <div class="col-12 col-md-6">
                    Forma de pago <span class="LabelCampoObligatorio">*</span>
                    <telerik:RadComboBox ID="cmbFormaPago" runat="server" Width="100%" ClientIDMode="Static"
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

                <div class="col-12 col-md-4">
                    Moneda <span class="LabelCampoObligatorio">*</span>
                    <telerik:RadComboBox ID="cmbMoneda" runat="server" Width="100%" ClientIDMode="Static"
                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap" OnSelectedIndexChanged="cmbMoneda_SelectedIndexChanged"
                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="true">
                        <Items>
                            <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div id="divValorMoneda" runat="server" visible="false" class="col-12 col-md-4">
                    Tipo de cambio <span class="LabelCampoObligatorio">*</span>
                    <asp:TextBox runat="server" ID="txtValorMoneda" CssClass="form-control" Width="100%" MaxLength="30" TabIndex="2"></asp:TextBox>
                </div>
                <div class="col-12 col-md-4">
                    Monto <span class="LabelCampoObligatorio">*</span>
                    <asp:TextBox runat="server" ID="txtMonto" CssClass="form-control" Width="100%" MaxLength="30" TabIndex="2"></asp:TextBox>
                </div>

            </div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-12">
                    Comentario
                    <asp:TextBox runat="server" ID="txtComentario" CssClass="form-control" Height="76" TextMode="MultiLine" MaxLength="200" Width="100%" TabIndex="2"></asp:TextBox>
                </div>
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


        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
            <div>
                <i class="fa-solid fa-table-list shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Consulta</span>
            </div>
            <div class="linea-separador" style="margin-top: 20px;"></div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-6">
                    Tipo de fecha
                    <telerik:RadRadioButtonList runat="server" ID="rbtnTipoFecha" RepeatDirection="Horizontal" RepeatColumns="5" TabIndex="1" Direction="Horizontal" RenderMode="Lightweight" Skin="Bootstrap" AutoPostBack="false">
                        <Items>
                            <telerik:ButtonListItem Value="1" Text="Fecha de ingreso" Selected="True"></telerik:ButtonListItem>
                            <telerik:ButtonListItem Value="2" Text="Fecha de registro"></telerik:ButtonListItem>
                        </Items>
                    </telerik:RadRadioButtonList>
                </div>
            </div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-6">
                    Fecha inicial
                    <br>
                    <telerik:RadDatePicker ID="dtpFechaDesde" runat="server" Width="100%" Culture="es-DO" TabIndex="2" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                    </telerik:RadDatePicker>
                </div>
                <div class="col-12 col-md-6">
                    Fecha final
                    <br>
                    <telerik:RadDatePicker ID="dtpFechaHasta" runat="server" Width="100%" Culture="es-DO" TabIndex="3" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                    </telerik:RadDatePicker>
                </div>
            </div>


            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-6">
                    Descripción de ingreso 
                    <telerik:RadComboBox ID="cmbDescripcionIngreso_Consulta" runat="server" Width="100%" ClientIDMode="Static"
                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                        </Items>
                    </telerik:RadComboBox>
                </div>

                <div class="col-12 col-md-6">
                    Miembro
                    <telerik:RadComboBox ID="cmbMiembro_Consulta" runat="server" Width="100%" ClientIDMode="Static"
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
                <div class="col-12 col-md-4">
                    Moneda
                    <telerik:RadComboBox ID="cmbMoneda_Consulta" runat="server" Width="100%" ClientIDMode="Static"
                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
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
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id_Ingreso") %>' OnClick="btnEditar_Click" CssClass="btn btn-sm btn-primary fa-solid fa-pen boton_formulario_editar" Style="height: 30px; width: 30px; padding: 7px; border-radius: 15px; margin-bottom: 3px;"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Id_Ingreso" HeaderText="ID" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Descripcion_Ingreso" HeaderText="Descripción" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Miembro" HeaderText="Miembro" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Moneda" HeaderText="Moneda" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Monto" HeaderText="Monto" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Valor_Moneda" HeaderText="Tipo de cambio" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Fecha_Ingreso" HeaderText="Fecha de ingreso" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Fecha_Registro" HeaderText="Fecha de registro" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>


        <div class="contenedor_botones">
            <div class="contenedor-div-botones-segmentados" style="margin-bottom: 5px;">
                <asp:LinkButton CssClass="fa-solid fa-plus fa-lg boton_formulario_Agregar" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click" OnClientClick="MostrarPanelCarga()"></asp:LinkButton>
                <asp:LinkButton CssClass="fa-solid fa-floppy-disk fa-lg boton_formulario_Guardar" Style="margin-bottom: 0px;" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" OnClientClick="MostrarPanelCarga()"></asp:LinkButton>
            </div>

            <div class="contenedor-div-botones-segmentados" style="margin-bottom: 5px;">
                <asp:LinkButton CssClass="fa-solid fa-magnifying-glass fa-lg boton_formulario_Buscar" runat="server" ID="btnBuscar" OnClientClick="MostrarPanelCarga()" OnClick="btnBuscar_Click"></asp:LinkButton>
                <asp:LinkButton CssClass="fa-solid fa-filter-circle-xmark fa-lg boton_formulario_LimpiarFiltros" runat="server" ID="btnLimpiarFiltros" OnClick="btnLimpiarFiltros_Click"></asp:LinkButton>
                <asp:LinkButton CssClass="fa-solid fa-file-pdf fa-lg boton_formulario_Agregar" runat="server" ID="btnGenerarPDF" OnClick="btnGenerarPDF_Click" OnClientClick="MostrarPanelCarga()" data-tippy-content="Generar reporte en PDF"></asp:LinkButton>
                <asp:LinkButton CssClass="fa-solid fa-file-excel fa-lg boton_formulario_Agregar" Style="margin-bottom: 0px;" runat="server" ID="btnGenerarExcel" OnClick="btnGenerarExcel_Click" data-tippy-content="Generar reporte en Excel"></asp:LinkButton>
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

    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Descripción de ingreso</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="col-12 col-md-12">
                        Descripción
                    <asp:TextBox runat="server" ID="txtDescripcionIngresoAgregar" CssClass="form-control" MaxLength="100" Width="100%" TabIndex="2"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button runat="server" ID="btnAgregarDescripcion" CssClass="btn btn-primary" OnClick="btnAgregarDescripcion_Click" Text="Agregar descripción" data-bs-dismiss="modal"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
