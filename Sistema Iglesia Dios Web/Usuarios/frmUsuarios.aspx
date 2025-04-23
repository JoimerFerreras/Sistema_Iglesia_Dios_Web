<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmUsuarios.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Usuarios.frmUsuarios" %>

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
                            <div>
                                <i class="fa-solid fa-filter shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Filtros</span>
                            </div>
                            <div class="linea-separador" style="margin-top: 20px;"></div>

                            <div class="row" style="margin-top: 20px;">

                                <div class="col-12 col-md-6">
                                    Rol de usuario
                                    <telerik:RadComboBox ID="cmbRol_Filtro" runat="server" Width="100%" ClientIDMode="Static"
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
                                    MasterTableView-PagerStyle-FirstPageToolTip="" MasterTableView-PagerStyle-PrevPageToolTip="" MasterTableView-PagerStyle-NextPageToolTip="" MasterTableView-PagerStyle-LastPageToolTip=""
                                    OnPageIndexChanged="gvDatos_PageIndexChanged" OnPageSizeChanged="gvDatos_PageSizeChanged" OnSortCommand="gvDatos_SortCommand">
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <MasterTableView AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridTemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id_Usuario") %>' OnClick="btnEditar_Click" CssClass="btn btn-sm btn-primary fa-solid fa-pen boton_formulario_editar" Style="height: 30px; width: 30px; padding: 7px; border-radius: 15px; margin-bottom: 3px;"></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id_Usuario" HeaderText="ID" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="NombreCompleto" HeaderText="Nombre Completo" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Sexo" HeaderText="Sexo" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Nombre_Rol" HeaderText="Rol" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Usuario" HeaderText="Nombre de usuario" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="Bloqueo">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <div runat="server" commandargument='<%# Eval("Bloqueo") %>' class='<%# GetStatusColor(Eval("Bloqueo")) %>'>
                                                        <asp:LinkButton runat="server" CommandArgument='<%# Eval("Bloqueo") %>' CssClass='<%# GetStatusColor(Eval("Bloqueo")) %>' Text='<%# GetStatusText(Eval("Bloqueo")) %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Verificacion en dos pasos">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <div runat="server" commandargument='<%# Eval("Verificacion_Dos_Pasos") %>' class='<%# GetStatusColor(Eval("Verificacion_Dos_Pasos")) %>'>
                                                        <asp:LinkButton runat="server" CommandArgument='<%# Eval("Verificacion_Dos_Pasos") %>' CssClass='<%# GetStatusColor(Eval("Verificacion_Dos_Pasos")) %>' Text='<%# GetStatusText(Eval("Verificacion_Dos_Pasos")) %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Restablecer contraseña">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemTemplate>
                                                    <div runat="server" commandargument='<%# Eval("RestablecerPassword") %>' class='<%# GetStatusColor(Eval("RestablecerPassword")) %>'>
                                                        <asp:LinkButton runat="server" CommandArgument='<%# Eval("RestablecerPassword") %>' CssClass='<%# GetStatusColor(Eval("RestablecerPassword")) %>' Text='<%# GetStatusText(Eval("RestablecerPassword")) %>' />
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
                            <asp:LinkButton CssClass="fa-solid fa-filter-circle-xmark fa-lg boton_formulario_LimpiarFiltros" runat="server" ID="btnLimpiarFiltros" OnClick="btnLimpiarFiltros_Click"></asp:LinkButton>
                        </div>
                    </telerik:RadPageView>


                    <telerik:RadPageView runat="server" ID="rpvRegistro" Style="margin: 0 auto;">
                        <div class="shadowed-div-body" style="width: 100%;">
                            <div>
                                <i class="fa-solid fa-user shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Datos personales</span>
                            </div>
                            <div class="linea-separador" style="margin-top: 20px;"></div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    ID
                                     <asp:TextBox runat="server" ID="txtIdUsuario" CssClass="form-control form-control" Width="100%" ReadOnly="true" TabIndex="1" Style="max-width: 150px;"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Primer nombre <span class="LabelCampoObligatorio">*</span>
                                    <asp:TextBox runat="server" ID="txtNombre1" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="2" style="max-width: 400px"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Segundo nombre
                                    <asp:TextBox runat="server" ID="txtNombre2" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="3" style="max-width: 400px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Primer apellido <span class="LabelCampoObligatorio">*</span>
                                    <asp:TextBox runat="server" ID="txtApellido1" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="4" style="max-width: 400px"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Segundo apellido
                                    <asp:TextBox runat="server" ID="txtApellido2" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="5" style="max-width: 400px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Celular
                                    <telerik:RadMaskedTextBox runat="server" ID="txtCelular" CssClass="form-control form-control" Width="100%" TabIndex="6" Mask="(###) ###-####" AutoCompleteType="Cellular" ClientIDMode="Static" Skin="Bootstrap" Font-Size="12" style="max-width: 200px"></telerik:RadMaskedTextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Tel&eacute;fono
                                    <telerik:RadMaskedTextBox runat="server" ID="txtTelefono" CssClass="form-control form-control" Width="100%" TabIndex="7" Mask="(###) ###-####" AutoCompleteType="HomePhone" ClientIDMode="Static" Skin="Bootstrap" Font-Size="12" style="max-width: 200px"></telerik:RadMaskedTextBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Sexo <span class="LabelCampoObligatorio">*</span><br />
                                    <telerik:RadComboBox ID="cmbSexo" runat="server" Width="100%" ClientIDMode="Static" 
                                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="8" style="max-width: 200px"
                                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                                            <telerik:RadComboBoxItem Text="Masculino" Value="1" />
                                            <telerik:RadComboBoxItem Text="Femenino" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>

                        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                            <div>
                                <i class="fa-solid fa-lock shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Información de usuario</span>
                            </div>
                            <div class="linea-separador" style="margin-top: 20px;"></div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Nombre de usuario <span class="LabelCampoObligatorio">*</span>
                                    <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="9" style="max-width: 400px"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Contraseña <span class="LabelCampoObligatorio" style="padding-right: 10px;">*</span><i class="fa-solid fa-circle-info  btnInfoControl"
                                        data-tippy-content="Una contraseña válida y segura debe tener al menos 8 caracteres y combinar letras (mayúsculas y minúsculas), 
                                         dígitos y al menos un carácter especial como @, $, !, %, *, #, ?. Esta combinación de elementos garantiza una mayor robustez en la seguridad de la contraseña, 
                                         reduciendo el riesgo de ser adivinada fácilmente por potenciales atacantes."></i>
                                    <div class="contenedor-btn-pass">
                                        <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control form-control" Width="100%" TextMode="Password" MaxLength="30" TabIndex="10" Style="padding-right: 35px; max-width: 400px;" oncopy="return false;"></asp:TextBox>

                                        <asp:LinkButton CssClass="fa-solid fa-eye btnMostrarPassword" runat="server" ID="btnMostrarPassword" OnClick="btnMostrarPassword_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-6">
                                    Correo electr&oacute;nico <i class="fa-solid fa-circle-info  btnInfoControl"
                                        data-tippy-content="Un e-mail válido debe contener caracteres alfanuméricos (letras y números) y algunos caracteres especiales, 
                                    como puntos, guiones y guiones bajos. Por ejemplo: usuario123@gmail.com"></i>
                                    <asp:TextBox runat="server" ID="txtCorreo" CssClass="form-control form-control" MaxLength="80" Width="100%" TabIndex="11"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Rol de usuario <span class="LabelCampoObligatorio">*</span>
                                    <telerik:RadComboBox ID="cmbRol" runat="server" Width="100%" ClientIDMode="Static"
                                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="12"
                                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-12 col-md-3">
                                    <asp:CheckBox runat="server" ID="chkBloqueo" Text="&nbsp; Bloquear usuario" TabIndex="13"/>
                                </div>

                                <div class="col-12 col-md-3">
                                    <asp:CheckBox runat="server" ID="chkVerificacionDosPasos" Text="&nbsp; Verificación en dos pasos" TabIndex="14"/>
                                </div>

                                <div class="col-12 col-md-3">
                                    <asp:CheckBox runat="server" ID="chkRestablecerPassword" Text="&nbsp; Restablecer contraseña" TabIndex="15"/>
                                </div>
                            </div>
                        </div>

                        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                            <div>
                                <i class="fa-solid fa-file-waveform shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Historial de registro y modificación del usuario</span>
                            </div>
                            <div class="linea-separador" style="margin-top: 20px;"></div>

                            <div class="row" style="margin-top: 20px;">

                                <div class="col-12 col-md-6">
                                    Fecha de registro
                                    <asp:TextBox runat="server" ID="txtFechaRegistro" CssClass="form-control" Width="100%" TabIndex="16" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-12 col-md-6">
                                    Fecha de ultima modificaci&oacute;n
                                    <asp:TextBox runat="server" ID="txtFechaUltimaModificacion" CssClass="form-control" Width="100%" TabIndex="17" ReadOnly="true"></asp:TextBox>
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
