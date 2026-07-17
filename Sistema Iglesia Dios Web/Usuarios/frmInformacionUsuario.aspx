<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmInformacionUsuario.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Usuarios.frmInformacionUsuario" %>

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
                            Primer nombre 
                            <asp:TextBox runat="server" ID="txtNombre1" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="2" Style="max-width: 400px"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Segundo nombre
                                    <asp:TextBox runat="server" ID="txtNombre2" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="3" Style="max-width: 400px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Primer apellido 
                            <asp:TextBox runat="server" ID="txtApellido1" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="4" Style="max-width: 400px"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Segundo apellido
                                    <asp:TextBox runat="server" ID="txtApellido2" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="5" Style="max-width: 400px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Celular
                                    <telerik:RadMaskedTextBox runat="server" ID="txtCelular" CssClass="form-control form-control" Width="100%" TabIndex="6" Mask="(###) ###-####" AutoCompleteType="Cellular" ClientIDMode="Static" Skin="Bootstrap" Font-Size="12" Style="max-width: 200px"></telerik:RadMaskedTextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Tel&eacute;fono
                                    <telerik:RadMaskedTextBox runat="server" ID="txtTelefono" CssClass="form-control form-control" Width="100%" TabIndex="7" Mask="(###) ###-####" AutoCompleteType="HomePhone" ClientIDMode="Static" Skin="Bootstrap" Font-Size="12" Style="max-width: 200px"></telerik:RadMaskedTextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Sexo
                            <asp:TextBox runat="server" ID="txtSexo" CssClass="form-control form-control" Width="100%" ReadOnly="true" TabIndex="1" Style="max-width: 150px;"></asp:TextBox>
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
                            Nombre de usuario
                                    <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control form-control" MaxLength="30" Width="100%" TabIndex="9" ReadOnly="true" Style="max-width: 400px"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Contraseña
    <br />

                            <asp:LinkButton
                                ID="btnCambiarPassword"
                                runat="server"
                                CssClass="btn btn-primary"
                                CausesValidation="false"
                                OnClick="btnCambiarPassword_Click">

        <i class="fa-solid fa-key"></i>
        Cambiar contraseña
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:Panel
                        ID="pnlCambiarPassword"
                        runat="server"
                        Visible="false">

                        <div class="linea-separador" style="margin-top: 25px;"></div>

                        <div class="row" style="margin-top: 20px;">

                            <div class="col-12 col-md-6">
                                Nueva contraseña
    <span class="LabelCampoObligatorio">*</span>

                                <i class="fa-solid fa-circle-info btnInfoControl"
                                    data-tippy-content="La contraseña debe tener al menos 8 caracteres, incluyendo una letra mayúscula, una letra minúscula, un número y un carácter especial."></i>

                                <div class="contenedor-btn-pass">
                                    <asp:TextBox
                                        ID="txtNuevaPassword"
                                        runat="server"
                                        ClientIDMode="Static"
                                        CssClass="form-control"
                                        TextMode="Password"
                                        MaxLength="30"
                                        autocomplete="new-password"
                                        Style="padding-right: 40px; max-width: 400px;"
                                        oncopy="return false;">
                                    </asp:TextBox>

                                    <button
                                        type="button"
                                        class="fa-solid fa-eye btnMostrarPassword"
                                        onclick="MostrarOcultarPassword('txtNuevaPassword', this)"
                                        aria-label="Mostrar u ocultar nueva contraseña">
                                    </button>
                                </div>
                            </div>

                            <div class="col-12 col-md-6">
                                Repetir contraseña
    <span class="LabelCampoObligatorio">*</span>

                                <div class="contenedor-btn-pass">
                                    <asp:TextBox
                                        ID="txtRepetirPassword"
                                        runat="server"
                                        ClientIDMode="Static"
                                        CssClass="form-control"
                                        TextMode="Password"
                                        MaxLength="30"
                                        autocomplete="new-password"
                                        Style="padding-right: 40px; max-width: 400px;"
                                        oncopy="return false;">
                                    </asp:TextBox>

                                    <button
                                        type="button"
                                        class="fa-solid fa-eye btnMostrarPassword"
                                        onclick="MostrarOcultarPassword('txtRepetirPassword', this)"
                                        aria-label="Mostrar u ocultar contraseña repetida">
                                    </button>
                                </div>
                            </div>

                        </div>

                        <div class="row" style="margin-top: 20px;">
                            <div class="col-12">

                                <asp:LinkButton
                                    ID="btnGuardarPassword"
                                    runat="server"
                                    CssClass="btn btn-success"
                                    OnClick="btnGuardarPassword_Click"
                                    OnClientClick="MostrarPanelCarga()">

                <i class="fa-solid fa-floppy-disk"></i>
                Guardar contraseña
                                </asp:LinkButton>

                                <asp:LinkButton
                                    ID="btnCancelarCambioPassword"
                                    runat="server"
                                    CssClass="btn btn-secondary"
                                    CausesValidation="false"
                                    OnClick="btnCancelarCambioPassword_Click"
                                    Style="margin-left: 5px;">

                <i class="fa-solid fa-xmark"></i>
                Cancelar
                                </asp:LinkButton>

                            </div>
                        </div>

                    </asp:Panel>

                    <div class="row" style="margin-top: 20px;">
                        <div class="col-12 col-md-6">
                            Correo electr&oacute;nico <i class="fa-solid fa-circle-info  btnInfoControl"
                                data-tippy-content="Un e-mail válido debe contener caracteres alfanuméricos (letras y números) y algunos caracteres especiales, 
                                    como puntos, guiones y guiones bajos. Por ejemplo: usuario123@gmail.com"></i>
                            <asp:TextBox runat="server" ID="txtCorreo" CssClass="form-control form-control" MaxLength="80" Width="100%" ReadOnly="true" TabIndex="11"></asp:TextBox>
                        </div>

                        <div class="col-12 col-md-6">
                            Rol de usuario
                            <asp:TextBox runat="server" ID="txtRolUsuario" CssClass="form-control form-control" MaxLength="80" Width="100%" ReadOnly="true" TabIndex="11"></asp:TextBox>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script src="../Recursos/Javascript/scripts_general.js"></script>

    <script type="text/javascript">
        function MostrarOcultarPassword(idCampo, boton) {
            var campo = document.getElementById(idCampo);

            if (!campo) {
                return;
            }

            if (campo.type === "password") {
                campo.type = "text";

                boton.classList.remove("fa-eye");
                boton.classList.add("fa-eye-slash");
            } else {
                campo.type = "password";

                boton.classList.remove("fa-eye-slash");
                boton.classList.add("fa-eye");
            }

            campo.focus();
        }
    </script>
</asp:Content>
