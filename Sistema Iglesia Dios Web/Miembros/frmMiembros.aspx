<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMiembros.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Miembros.frmMiembros" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />

    <div style="padding: 20px;">
        <div class="shadowed-div-body" style="width: 100%;">
            <div>
                <i class="fa-solid fa-filter shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">&nbsp;Datos generales</span>
            </div>
            <div class="linea-separador" style="margin-top: 20px;"></div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-2">
                    ID Registro
                    <asp:TextBox runat="server" ID="txtIdMiembro" CssClass="form-control form-control" Width="100%" ReadOnly="true" TabIndex="1" Style="max-width: 150px;"></asp:TextBox>
                </div>
            </div>


            <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-6">
                    Nombres <span class="LabelCampoObligatorio">*</span>
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
                    Fecha de nacimiento 
                    <br />
                    <telerik:RadDatePicker ID="dtpFechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="true"></DateInput>
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
                    <asp:TextBox runat="server" ID="txtNumeroCasa" CssClass="form-control form-control" Width="100%" MaxLength="10" TabIndex="2"></asp:TextBox>
                </div>
            </div>

        </div>

        <%-- NIVEL ACADEMICO Y PROFESIONALISMO --%>
        <div class="shadowed-div-body" style="width: 100%; margin-top: 20px;">
            <div>
                <i class="fa-solid fa-filter shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">&nbsp;Nivel acad&eacute;mico y profesionalismo</span>
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
                <i class="fa-solid fa-filter shadowed-div-body-titulo"></i><span class="shadowed-div-body-titulo">&nbsp;Información Familiar 1</span>
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
                    <telerik:RadDatePicker ID="dtpFechaNacimiento_Conyuge" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="true"></DateInput>
                    </telerik:RadDatePicker>
                </div>

                <div class="col-12 col-md-3">
                    <br>
                    <asp:CheckBox runat="server" ID="txtConyugeCristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
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
                    <telerik:RadDatePicker ID="dtpHijo1_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput4" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="true"></DateInput>
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
                    <telerik:RadDatePicker ID="dtpHijo2_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="true"></DateInput>
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
                    <telerik:RadDatePicker ID="dtpHijo3_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput5" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="true"></DateInput>
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
                    <telerik:RadDatePicker ID="dtpHijo4_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput6" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="true"></DateInput>
                    </telerik:RadDatePicker>
                </div>

                <div class="col-12 col-md-3">
                    <asp:CheckBox runat="server" ID="dtpHijo4_Cristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
                </div>
            </div>


              <%--Hijo5--%>
              <div class="row" style="margin-top: 20px;">
                <div class="col-12 col-md-6">
                    <asp:TextBox runat="server" ID="txtHijo5_Nombre" CssClass="form-control form-control" Width="100%" MaxLength="80" TabIndex="2"></asp:TextBox>
                </div>

                <div class="col-12 col-md-3">
                    <telerik:RadDatePicker ID="dtpHijo5_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput7" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="true"></DateInput>
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
                    <telerik:RadDatePicker ID="dtpHijo6_FechaNacimiento" runat="server" Width="100%" Culture="es-DO" TabIndex="1" RenderMode="Lightweight" Skin="Bootstrap" Style="max-width: 200px;">
                        <DateInput ID="DateInput8" runat="server" DateFormat="dd/MM/yyyy" ReadOnly="true"></DateInput>
                    </telerik:RadDatePicker>
                </div>

                <div class="col-12 col-md-3">
                    <asp:CheckBox runat="server" ID="chkHijo6_Cristiano" CssClass="form-check" Text="&nbsp; ¿Es cristiano/a?" Style="padding: 0" />
                </div>
            </div>



        </div>
        <div class="contenedor_botones">
            <asp:LinkButton CssClass="fa-solid fa-plus fa-lg boton_formulario_Agregar" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click" OnClientClick="MostrarPanelCarga()"></asp:LinkButton>
            <asp:LinkButton CssClass="fa-solid fa-floppy-disk fa-lg boton_formulario_Guardar" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" OnClientClick="MostrarPanelCarga()"></asp:LinkButton>
        </div>

        <div class="panel-carga" id="divPanelCarga" style="visibility: hidden; z-index: 50000;">
            <div class="d-flex justify-content-center">
                <div class="spinner-border text-light" role="status">
                </div>
                <span class="text-light" style="text-align: center; margin-top: 5px; margin-left: 10px;">Cargando...</span>
            </div>
        </div>
    </div>
</asp:Content>
