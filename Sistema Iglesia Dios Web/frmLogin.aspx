<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.frmLogin" %>

<!DOCTYPE html>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sistema Web - Iglesia de Dios La 33 | Login</title>
    <link rel="shortcut icon" href="/Recursos/Imagenes/logo_iglesia_de_dios_color_ico.ico" />

    <link rel="stylesheet" href="Recursos/CSS/login.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js" charset="utf-8"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="Recursos/CSS/login.css" />
    <link rel="stylesheet" href="Recursos/CSS/botones.css" />
    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.4.0.min.js"></script>

    <style>
       .fondo_atras {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: -1; /* Coloca el div detrás de todo */
        background-image: url('/Recursos/Imagenes/diezmo.jpg');
        background-size: cover;
        opacity: 0.9; /* Ajusta la opacidad según tus preferencias */
        filter: brightness(0.5); /* Reduce la luminosidad en un 10% */
    }

       .texto_logo {
        position: fixed;
        top: 100px;
        left: 100px;
        width: 500px;
        font-size: 60px;
        font-weight:bold;
        color: white;
    }

         .texto_logo_second {
        position: fixed;
        top: 185px;
        left: 100px;
        width: 100%;
        font-size: 30px;
        font-weight:bold;
        color: beige;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upPrincipal" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="fondo_atras"></div> <!-- Este div será el fondo fijo -->
                <div class="div-padre fondo">
                    <div class="texto_logo">Sistema Web</div>
                    <div class="texto_logo_second">Iglesia de Dios La 33</div>
                    <div class="shadowed-div-body" style="margin-top: 20px; display: flex; background-color: white;">

                        <div class="divContenedorCampos-Login">

                            <div style="position: relative; margin: 0 auto;">
                                <div class="divContenedorTitulo-Login">
                                    <a>Iglesia de Dios </a>
                                    <a style="color: #F9CB33;">&nbsp;La 33</a>
                                </div>

                                <div style="position: absolute; top: 50%; right: 50%; transform: translate(50%, 50%); margin-top: 100px; font-weight: bold; font-size: 18px;">
                                    Inicio de Sesión
                                </div>
                                <div style="position: absolute; top: 50%; right: 50%; transform: translate(50%, 50%)">

                                    <div style="margin-top: 60px;">
                                        <a style="font-size: 14px; display: none;">Usuario</a>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtUsuario" CssClass="form-control" Width="300" placeholder="Usuario" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div style="margin-top: 20px;">
                                        <a style="font-size: 14px; display: none;">Contraseña</a>
                                        <div>
                                            <div class="contenedor-btn-pass">
                                                <asp:TextBox runat="server" ID="txtPassword" MaxLength="30" Width="300" CssClass="form-control" TextMode="Password" placeholder="Contraseña" Style="padding-right: 35px;" oncopy="return false;"></asp:TextBox>
                                                <asp:LinkButton CssClass="fa-solid fa-eye btnMostrarPassword" runat="server" ID="btnMostrarPassword" OnClick="btnMostrarPassword_Click"></asp:LinkButton>
                                            </div>
                                            <div class="divRecordarPassword">
                                                <input type="checkbox" class="mycheck2" id="chkRecordarPassword" runat="server" />&nbsp;Recordar contraseña
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col align-self-auto">
                                            <asp:Button runat="server" ID="btnIniciarSesion" Text="Inicar sesión" CssClass="btn btn-primary btnIniciarSesion" OnClick="btnIniciarSesion_Click" OnClientClick="MostrarPanelCarga();" />
                                        </div>
                                    </div>
                                </div>
                                <div class="divOlvidarrPassword">
                                    <asp:LinkButton runat="server" ID="btOlvidarPassword" Text="¿Olvidaste tu contraseña?" Font-Size="10" OnClick="btOlvidarPassword_Click" OnClientClick="MostrarPanelCarga();"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <p class="etiqueta">Sistema Web - Iglesia de Dios v1.0.0.0</p>
                </div>
                <div class="panel-carga" id="divPanelCarga" style="visibility: hidden; z-index: 50000;">
                    <div class="d-flex justify-content-center">
                        <div class="spinner-border text-light" role="status">
                        </div>
                        <span class="text-light" style="text-align: center; margin-top: 5px; margin-left: 10px;">Cargando...</span>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
<script src="Recursos/Javascript/scripts_general.js"></script>
</html>
