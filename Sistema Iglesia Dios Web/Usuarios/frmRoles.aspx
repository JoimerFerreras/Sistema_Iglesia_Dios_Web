<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRoles.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Usuarios.frmRoles" %>

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
        <ContentTemplate>

            <div style="padding: 20px;">
                <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="rtsTabulador" Skin="Bootstrap" MultiPageID="rmpTabs" SelectedIndex="0" Style="margin-left: -1px;">
                    <Tabs>
                        <telerik:RadTab Text="Consulta" Font-Bold="true"></telerik:RadTab>
                        <telerik:RadTab Text="Registro" Font-Bold="true"></telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage runat="server" ID="rmpTabs" SelectedIndex="0">
                    <telerik:RadPageView runat="server" ID="rpvConsulta" Style="margin: 0 auto;">
                        <div class="shadowed-div-body" style="width: 100%; border-radius: 0px 10px 10px 10px;">
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
                                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id_Rol") %>' OnClick="btnEditar_Click" CssClass="btn btn-sm btn-primary fa-solid fa-pen boton_formulario_editar" Style="height: 30px; width: 30px; padding: 7px; border-radius: 15px; margin-bottom: 3px;"></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id_Rol" HeaderText="ID" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Fecha_Registro" HeaderText="Fecha de registro" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nombre_Rol" HeaderText="Nombre del rol" HeaderStyle-Width="60%" ItemStyle-Width="60%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="Estado">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <div runat="server" commandargument='<%# Eval("Estado") %>' class='<%# GetStatusColor(Eval("Estado")) %>'>
                                                        <asp:LinkButton runat="server" CommandArgument='<%# Eval("Estado") %>' CssClass='<%# GetStatusColor(Eval("Estado")) %>' Text='<%# GetStatusText(Eval("Estado")) %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>

                        <div class="contenedor_botones">
                            <asp:LinkButton CssClass="fa-solid fa-magnifying-glass fa-lg boton_formulario_Buscar" runat="server" ID="btnBuscar" OnClientClick="MostrarPanelCarga()" OnClick="btnBuscar_Click"></asp:LinkButton>
                        </div>
                    </telerik:RadPageView>


                    <telerik:RadPageView runat="server" ID="rpvRegistro" Style="margin: 0 auto;">
                        <div class="shadowed-div-body" style="width: 100%;">
                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-1">
                                    ID
                                     <asp:TextBox runat="server" ID="txtIdRol" CssClass="form-control form-control" Width="100%" ReadOnly="true" TabIndex="1" Style="max-width: 150px;"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Nombre del rol <span class="LabelCampoObligatorio">*</span>
                                    <asp:TextBox runat="server" ID="txtNombreRol" CssClass="form-control form-control" MaxLength="50" Width="100%" TabIndex="2"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-3">
                                    Estado <br />
                                    <telerik:RadComboBox ID="cmbEstado" runat="server" Width="100%" ClientIDMode="Static" Style="max-width: 230px;"
                                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="3"
                                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Activo" Value="True" Selected="true" />
                                            <telerik:RadComboBoxItem Text="Inactivo" Value="False" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>

                        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                            <div class="col-12 div-gridview">
                                <asp:GridView ID="gvPermisos" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre_Funcionalidad" HeaderText="Nombre de Función" HeaderStyle-Width="20%" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="Nombre_Archivo" HeaderText="Pantalla" HeaderStyle-Width="30%" ItemStyle-Width="30%" />

                                        <asp:TemplateField HeaderText="Visualizar">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkVisualizar" runat="server" Checked='<%# Convert.ToBoolean(Eval("Permiso_Visualizar")) %>' HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEditar" runat="server" Checked='<%# Convert.ToBoolean(Eval("Permiso_Editar")) %>' HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEliminar" runat="server" Checked='<%# Convert.ToBoolean(Eval("Permiso_Eliminar")) %>' HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                            <div>
                                <i class="fa-solid fa-file-waveform shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Historial de registro y modificación del ingreso</span>
                            </div>
                            <div class="linea-separador" style="margin-top: 20px;"></div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Fecha de registro
                                  <asp:TextBox runat="server" ID="txtFechaRegistro" CssClass="form-control" Width="100%" TabIndex="2" ReadOnly="true"></asp:TextBox>
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
