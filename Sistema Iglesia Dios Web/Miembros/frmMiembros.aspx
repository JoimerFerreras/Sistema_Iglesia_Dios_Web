<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMiembros.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Miembros.frmMiembros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />

    <div style="padding: 20px;">
        <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="rtsTabulador" Skin="Bootstrap" MultiPageID="rmpTabs" SelectedIndex="0" Style="margin-left: -1px; border-color: transparent;">
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
                            Tipo de fecha
                    <telerik:RadRadioButtonList runat="server" ID="rbtnTipoFecha" RepeatDirection="Horizontal" RepeatColumns="5" TabIndex="1" Direction="Horizontal" RenderMode="Lightweight" Skin="Bootstrap" AutoPostBack="false">
                        <Items>
                            <telerik:ButtonListItem Value="2" Text="Fecha de miembro" Selected="True"></telerik:ButtonListItem>
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
                                <DateInput ID="DateInput9" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>
                        <div class="col-12 col-md-3">
                            Fecha final
                    <br>
                            <telerik:RadDatePicker ID="dtpFechaHasta" runat="server" Width="100%" Culture="es-DO" TabIndex="3" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                                <DateInput ID="DateInput10" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
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
                            <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="100%" ClientIDMode="Static" Style="max-width: 200px;"
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
                        <i class="fa-solid fa-table-list shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Resultado</span>
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

            </telerik:RadPageView>




            <telerik:RadPageView runat="server" ID="rpvRegistro" Style="margin: 0 auto;">

                <div class="shadowed-div-body" style="width: 100%;">
                    <div>
                        <i class="fa-solid fa-filter shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Datos generales</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            ID Registro
                    <asp:TextBox runat="server" ID="txtIdMiembro" CssClass="form-control form-control" Width="100%" ReadOnly="true" TabIndex="1" Style="max-width: 150px;"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-2">
                            Número Alternativo
                    <asp:TextBox runat="server" ID="txtNumeroMiembroAlternativo" CssClass="form-control form-control" Width="100%" TabIndex="1" Style="max-width: 150px;" TextMode="Number" min="0"></asp:TextBox>

                        </div>
                    </div>


                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombres <span class="LabelCampoObligatorio" data-tippy-content="Campo obligatorio">*</span>
                            <asp:TextBox runat="server" ID="txtNombres_Miembro" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Apellidos <span class="LabelCampoObligatorio">*</span>
                            <asp:TextBox runat="server" ID="txtApellidos_Miembro" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombre de pila 
                    <asp:TextBox runat="server" ID="txtNombrePila" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Sexo
                    <telerik:RadRadioButtonList runat="server" ID="rbtnSexo" RepeatDirection="Horizontal" RepeatColumns="5" TabIndex="1" Direction="Horizontal" RenderMode="Lightweight" Skin="Bootstrap" AutoPostBack="false">
                        <Items>
                            <telerik:ButtonListItem Value="1" Text="Masculino" Selected="True"></telerik:ButtonListItem>
                            <telerik:ButtonListItem Value="2" Text="Femenino" Selected="False"></telerik:ButtonListItem>
                        </Items>
                    </telerik:RadRadioButtonList>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Fecha de nacimiento <span class="LabelCampoObligatorio">*</span>
                            <br />
                            <telerik:RadDatePicker ID="dtpFechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>

                        <div class="col-12 col-md-6">
                            Estado civil
                    <telerik:RadComboBox ID="cmbEstadoCivil" runat="server" Width="100%" ClientIDMode="Static"
                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="Soltero/a" Value="1" Selected="true" />
                            <telerik:RadComboBoxItem Text="Casado/a" Value="2" Selected="false" />
                            <telerik:RadComboBoxItem Text="Unión libre" Value="3" Selected="false" />
                            <telerik:RadComboBoxItem Text="Otro" Value="4" Selected="false" />
                        </Items>
                    </telerik:RadComboBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Email
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkTieneHijos" CssClass="form-check" Text="&nbsp;Tiene hijos" Style="padding: 0" />
                        </div>
                    </div>


                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Celular
                    <asp:TextBox runat="server" ID="txtCelular" CssClass="form-control form-control" Width="100%" MaxLength="15" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Sector
                    <asp:TextBox runat="server" ID="txtSector" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Barrio / Residencial
                    <asp:TextBox runat="server" ID="txtBarrio_Residencial" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Calle
                    <asp:TextBox runat="server" ID="txtCalle" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            No. de casa
                    <asp:TextBox runat="server" ID="txtNumeroCasa" CssClass="form-control form-control" Width="100%" MaxLength="10" TabIndex="2" Style="max-width: 150px;"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <%-- NIVEL ACADEMICO Y PROFESIONALISMO --%>
                <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                    <div>
                        <i class="fa-solid fa-user-graduate shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Nivel acad&eacute;mico y profesionalismo</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>


                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-12 bold-text">
                            NIVEL ACAD&Eacute;MICO
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkNivelPrimario" CssClass="form-check" Text="&nbsp;Primario" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkNivelSecundario" CssClass="form-check" Text="&nbsp;Secundario" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkGradoUniversitario" CssClass="form-check" Text="&nbsp;Grado universitario" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkNivePostGrado_Maestria" CssClass="form-check" Text="&nbsp;Post grado / Maestría" Style="padding: 0" />
                        </div>
                    </div>

                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row" style="margin-top: 40px;">
                        <div class="col-12 col-md-12 bold-text">
                            INFORMACI&Oacute;N LABORAL
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkEmpleadoPrivado" CssClass="form-check" Text="&nbsp;Empleado privado" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkEmpleadoPublico" CssClass="form-check" Text="&nbsp;Empleado público" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkDuenoNegocio" CssClass="form-check" Text="&nbsp;Dueño de negocio" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkIndependiente" CssClass="form-check" Text="&nbsp;Independiente" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkOtros" CssClass="form-check" Text="&nbsp;Otros" Style="padding: 0" />
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombre de la empresa / Negocio
                    <asp:TextBox runat="server" ID="txtNombreEmpresaNegocio" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                </div>


                <%-- INFORMACION FAMILIAR 1 --%>
                <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                    <div>
                        <i class="fa-solid fa-people-roof shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Información familiar I</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6 bold-text">
                            INFORMACI&Oacute;N DEL CONYUGE
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombre del conyuge
                    <asp:TextBox runat="server" ID="txtNombreConyuge" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            Fecha de nacimiento del conyuge
                    <br />
                            <telerik:RadDatePicker ID="dtpFechaNacimiento_Conyuge" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkConyugeCristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
                        </div>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6 bold-text">
                            INFORMACI&Oacute;N DE LOS HIJOS
                        </div>
                    </div>

                    <%-- HIJOS --%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombre de los hijos
                        </div>

                        <div class="col-12 col-md-3">
                            Fecha de nacimiento
                        </div>
                    </div>



                    <%--Hijo1--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            <asp:TextBox runat="server" ID="txtHijo1_Nombre" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            <telerik:RadDatePicker ID="dtpHijo1_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                <DateInput ID="DateInput4" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>

                        <div class="col-12 col-md-3">
                            <asp:CheckBox runat="server" ID="chkHijo1_Cristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
                        </div>
                    </div>


                    <%--Hijo2--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            <asp:TextBox runat="server" ID="txtHijo2_Nombre" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            <telerik:RadDatePicker ID="dtpHijo2_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>

                        <div class="col-12 col-md-3">
                            <asp:CheckBox runat="server" ID="chkHijo2_Cristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
                        </div>
                    </div>



                    <%--Hijo3--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            <asp:TextBox runat="server" ID="txtHijo3_Nombre" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            <telerik:RadDatePicker ID="dtpHijo3_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                <DateInput ID="DateInput5" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>

                        <div class="col-12 col-md-3">
                            <asp:CheckBox runat="server" ID="chkHijo3_Cristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
                        </div>
                    </div>


                    <%--Hijo4--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            <asp:TextBox runat="server" ID="txtHijo4_Nombre" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            <telerik:RadDatePicker ID="dtpHijo4_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                <DateInput ID="DateInput6" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>

                        <div class="col-12 col-md-3">
                            <asp:CheckBox runat="server" ID="chkHijo4_Cristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
                        </div>
                    </div>


                    <%--Hijo5--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            <asp:TextBox runat="server" ID="txtHijo5_Nombre" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            <telerik:RadDatePicker ID="dtpHijo5_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                <DateInput ID="DateInput7" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>

                        <div class="col-12 col-md-3">
                            <asp:CheckBox runat="server" ID="chkHijo5_Cristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
                        </div>
                    </div>


                    <%--Hijo6--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            <asp:TextBox runat="server" ID="txtHijo6_Nombre" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            <telerik:RadDatePicker ID="dtpHijo6_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                <DateInput ID="DateInput8" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>

                        <div class="col-12 col-md-3">
                            <asp:CheckBox runat="server" ID="chkHijo6_Cristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
                        </div>
                    </div>
                </div>



                <%-- INFORMACION FAMILIAR 2 --%>
                <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                    <div>
                        <i class="fa-solid fa-people-roof shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">Información familiar II</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6 bold-text">
                            DATOS DEL PADRE
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-5">
                            Nombre y apellidos completos
                    <asp:TextBox runat="server" ID="txtPadre_NombreCompleto" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            Edad
                    <asp:TextBox runat="server" ID="txtPadre_Edad" CssClass="form-control form-control" Width="100%" TabIndex="1" Style="max-width: 150px;"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkPadre_Empleado" CssClass="form-check" Text="&nbsp; Empleado" Style="padding: 0" />
                        </div>
                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkPadre_NegocioPropio" CssClass="form-check" Text="&nbsp; Negocio propio" Style="padding: 0" />
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-3">
                            # Celular
                    <asp:TextBox runat="server" ID="txtPadre_Celular" CssClass="form-control form-control" Width="100%" MaxLength="15" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkPadre_MiembroIglesia" CssClass="form-check" Text="&nbsp; ¿Es miembro de la iglesia?" Style="padding: 0" />
                        </div>
                    </div>

                    <div class="linea-separador" style="margin-top: 20px;"></div>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6 bold-text">
                            DATOS DE LA MADRE
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-5">
                            Nombre y apellidos completos
                    <asp:TextBox runat="server" ID="txtMadre_NombreCompleto" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            Edad
                    <asp:TextBox runat="server" ID="txtMadre_Edad" CssClass="form-control form-control" Width="100%" TabIndex="1" Style="max-width: 150px;"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkMadre_Empleada" CssClass="form-check" Text="&nbsp; Empleado" Style="padding: 0" />
                        </div>
                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkMadre_NegocioPropio" CssClass="form-check" Text="&nbsp; Negocio propio" Style="padding: 0" />
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-3">
                            # Celular
                    <asp:TextBox runat="server" ID="txtMadre_Celular" CssClass="form-control form-control" Width="100%" MaxLength="15" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            <br>
                            <asp:CheckBox runat="server" ID="chkMadre_MiembroIglesia" CssClass="form-check" Text="&nbsp; ¿Es miembro de la iglesia?" Style="padding: 0" />
                        </div>
                    </div>


                    <div class="linea-separador" style="margin-top: 20px;"></div>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6 bold-text">
                            DATOS DE HERMANOS (AS)
                        </div>
                    </div>


                    <%--Hermano1--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombre completo
                    <asp:TextBox runat="server" ID="txtHermano1_NombreCompleto" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Escolaridad
                     <asp:TextBox runat="server" ID="txtHermano1_Escolaridad" CssClass="form-control form-control" Width="100%" MaxLength="30" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Correo electr&oacute;nico
                    <asp:TextBox runat="server" ID="txtHermano1_CorreoElectronico" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            # Celular
                    <asp:TextBox runat="server" ID="txtHermano1_Celular" CssClass="form-control form-control" Width="100%" MaxLength="15" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>


                    <%--Hermano2--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombre completo
                    <asp:TextBox runat="server" ID="txtHermano2_NombreCompleto" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Escolaridad
                     <asp:TextBox runat="server" ID="txtHermano2_Escolaridad" CssClass="form-control form-control" Width="100%" MaxLength="30" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Correo electr&oacute;nico
                    <asp:TextBox runat="server" ID="txtHermano2_CorreoElectronico" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            # Celular
                    <asp:TextBox runat="server" ID="txtHermano2_Celular" CssClass="form-control form-control" Width="100%" MaxLength="15" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>



                    <%--Hermano3--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombre completo
                    <asp:TextBox runat="server" ID="txtHermano3_NombreCompleto" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Escolaridad
                     <asp:TextBox runat="server" ID="txtHermano3_Escolaridad" CssClass="form-control form-control" Width="100%" MaxLength="30" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Correo electr&oacute;nico
                    <asp:TextBox runat="server" ID="txtHermano3_CorreoElectronico" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            # Celular
                    <asp:TextBox runat="server" ID="txtHermano3_Celular" CssClass="form-control form-control" Width="100%" MaxLength="15" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>


                    <%--Hermano4--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombre completo
                    <asp:TextBox runat="server" ID="txtHermano4_NombreCompleto" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Escolaridad
                     <asp:TextBox runat="server" ID="txtHermano4_Escolaridad" CssClass="form-control form-control" Width="100%" MaxLength="30" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Correo electr&oacute;nico
                    <asp:TextBox runat="server" ID="txtHermano4_CorreoElectronico" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            # Celular
                    <asp:TextBox runat="server" ID="txtHermano4_Celular" CssClass="form-control form-control" Width="100%" MaxLength="15" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>


                    <%--Hermano5--%>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Nombre completo
                    <asp:TextBox runat="server" ID="txtHermano5_NombreCompleto" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Escolaridad
                     <asp:TextBox runat="server" ID="txtHermano5_Escolaridad" CssClass="form-control form-control" Width="100%" MaxLength="30" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Correo electr&oacute;nico
                    <asp:TextBox runat="server" ID="txtHermano5_CorreoElectronico" CssClass="form-control form-control" Width="100%" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-3">
                            # Celular
                    <asp:TextBox runat="server" ID="txtHermano5_Celular" CssClass="form-control form-control" Width="100%" MaxLength="15" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                </div>


                <%--INFORMACION PERSONAL--%>
                <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                    <div>
                        <i class="fa-solid fa-filter shadowed-div-body-titulo" id="informacion_personal_panel"></i><span class="shadowed-div-body-titulo">Información personal (preferencias / gustos)</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6 bold-text">
                            PASATIEMPOS FAVORITOS
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkCine" CssClass="form-check" Text="&nbsp; Cine" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkLeer" CssClass="form-check" Text="&nbsp; Leer" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkVerTV" CssClass="form-check" Text="&nbsp; Ver TV" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkSocializar" CssClass="form-check" Text="&nbsp; Socializar" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-2">
                            <br>
                            <asp:CheckBox runat="server" ID="chkViajar" CssClass="form-check" Text="&nbsp; Viajar" Style="padding: 0" />
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Otros
                    <asp:TextBox runat="server" ID="txtOtrosPasatiempos" CssClass="form-control form-control" Width="100%" MaxLength="100" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
                    <div>
                        <i class="fa-solid fa-filter shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">&nbsp;Uso interno de la iglesia</span>
                    </div>
                    <div class="linea-separador" style="margin-top: 20px;"></div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 div-gridview">
                            <telerik:RadGrid RenderMode="Lightweight" ID="gvMinisteriosMiembro" runat="server" Culture="es-DO" Style="overflow-x: auto;" BorderColor="White" MasterTableView-Width="100%" Width="100%" HeaderStyle-Font-Bold="true" AlternatingItemStyle-BackColor="#F1F5FF"
                                AllowPaging="True" AllowAutomaticUpdates="True" AllowAutomaticInserts="False" MasterTableView-PagerStyle-PageSizeLabelText="Registros" Skin="Bootstrap" HeaderStyle-BackColor="#F1F5FF" PagerStyle-AlwaysVisible="true"
                                AllowAutomaticDeletes="True" AllowSorting="True" PagerStyle-BorderStyle="None" BorderStyle="None" FooterStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" MasterTableView-PagerStyle-NextPagesToolTip="" MasterTableView-PagerStyle-PrevPagesToolTip=""
                                FooterStyle-ForeColor="Black" HeaderStyle-ForeColor="Black" ItemStyle-ForeColor="Black" AlternatingItemStyle-ForeColor="Black" MasterTableView-PagerStyle-PagerTextFormat="{4} <strong>{5}</strong> Registros en <strong>{1}</strong> Páginas"
                                MasterTableView-PagerStyle-FirstPageToolTip="" MasterTableView-PagerStyle-PrevPageToolTip="" MasterTableView-PagerStyle-NextPageToolTip="" MasterTableView-PagerStyle-LastPageToolTip=""
                                OnPageIndexChanged="gvMinisteriosMiembro_PageIndexChanged" OnPageSizeChanged="gvMinisteriosMiembro_PageSizeChanged" OnSortCommand="gvMinisteriosMiembro_SortCommand">
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <MasterTableView AutoGenerateColumns="False">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id_Columna") %>' OnClick="AgregarMinisterioMiembro_Click" CssClass="btn btn-sm btn-primary fa-solid fa-pen boton_formulario_editar" Style="height: 30px; width: 30px; padding: 7px; border-radius: 15px; margin-bottom: 3px;"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Id_Columna" HeaderText="Columna" HeaderStyle-Width="20%" ItemStyle-Width="20%" Visible="false">
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Nombre_Ministerio" HeaderText="Camp" HeaderStyle-Width="95%" ItemStyle-Width="95%">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">

                        <div class="col-12 col-md-6">
                            <br>
                            <asp:CheckBox runat="server" ID="chkEsMiembro" CssClass="form-check" Text="&nbsp;¿Es miembro?" Style="padding: 0" />
                        </div>
                        <div class="col-12 col-md-6">
                            ¿Desde cuando es miembro?
                    <br />
                            <telerik:RadDatePicker ID="dtpDesdeCuandoMiembro" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;" MinDate="01-01-1900">
                                <DateInput ID="DateInput11" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="false"></DateInput>
                            </telerik:RadDatePicker>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">

                        <div class="col-12 col-md-6">
                            <br>
                            <asp:CheckBox runat="server" ID="chkPertenece_Ministerio" CssClass="form-check" Text="&nbsp;¿Pertenece a un ministerio?" Style="padding: 0" />
                        </div>

                        <div class="col-12 col-md-6">
                            <br>
                            <asp:CheckBox runat="server" ID="chkLe_Gustaria_Pertenecer_Ministerio" CssClass="form-check" Text="&nbsp;¿Le gustaria pertenecera a un ministerio?" Style="padding: 0" />
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Rol dentro de la iglesia
                    <telerik:RadComboBox ID="cmbRol_Miembro" runat="server" Width="100%" ClientIDMode="Static"
                        MaxHeight="200px" AllowCustomText="True" Sort="Ascending" TabIndex="6"
                        MarkFirstMatch="true" OnClientKeyPressing="ChangeToUpperCase" RenderMode="Lightweight" Skin="Bootstrap"
                        Filter="Contains" DataValueField="Codigo" DataTextField="Nombre" AppendDataBoundItems="true" AutoPostBack="false">
                        <Items>
                            <telerik:RadComboBoxItem Text="Seleccionar..." Value="0" Selected="true" />
                        </Items>
                    </telerik:RadComboBox>
                        </div>
                        <div class="col-12 col-md-6">
                            Otro rol
                    <asp:TextBox runat="server" ID="txtOtroRol" CssClass="form-control form-control" Width="100%" MaxLength="100" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">

                        <div class="col-12 col-md-6">
                            Nombre del diacono
                    <asp:TextBox runat="server" ID="txtNombre_Diacono" CssClass="form-control form-control" Width="100%" MaxLength="100" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            Nombre del lider de ministerio
                    <asp:TextBox runat="server" ID="txtNombreLiderMinisterio" CssClass="form-control form-control" Width="100%" MaxLength="100" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">

                        <div class="col-12 col-md-12">
                            Comentarios del di&aacute;cono / Lider del ministerio <a style="color: gray; margin-left: 10px;">(300 caracteres m&aacute;x.)</a>
                            <asp:TextBox runat="server" ID="txtComentariosDiaconoLiderMinisterio" TextMode="MultiLine" CssClass="form-control form-control" Width="100%" MaxLength="300" Height="100" TabIndex="2"></asp:TextBox>
                        </div>

                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Revisado por
                    <asp:TextBox runat="server" ID="txtRevisadoPor" CssClass="form-control form-control" Width="100%" MaxLength="100" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            Autorizado por
                    <asp:TextBox runat="server" ID="txtAutorizadoPor" CssClass="form-control form-control" Width="100%" MaxLength="100" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                </div>


                <div class="contenedor_botones">
                    <div id="list_example">
                        <a href="#informacion_personal_panel" class="boton_formulario_Agregar"></a>
                    </div>

                    <asp:LinkButton CssClass="fa-solid fa-plus fa-lg boton_formulario_Agregar" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click" OnClientClick="MostrarPanelCarga()"></asp:LinkButton>
                    <asp:LinkButton CssClass="fa-solid fa-floppy-disk fa-lg boton_formulario_Guardar" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" OnClientClick="MostrarPanelCarga()"></asp:LinkButton>
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

        <script src="../Recursos/Javascript/scripts_general.js"></script>
    </div>
</asp:Content>
