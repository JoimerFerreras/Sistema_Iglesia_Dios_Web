<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="frmPaginaPrincipal.aspx.cs" Inherits="Sistema_Iglesia_Dios_Web.Utilidad_Cliente.frmPaginaPrincipal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link rel="stylesheet" href="../Recursos/CSS/estilos_general.css" />
    <link rel="stylesheet" href="../Recursos/CSS/botones.css" />
    
    <telerik:RadFormDecorator runat="server" Skin="Bootstrap" RenderMode="Lightweight" />
    <style>
        .contenedor-centrado {
                display: flex;
                justify-content: center;
                align-items: center;
                height: 80vh;
                margin: 0;
            }

        .contenido {
            color: #999999;
        }

         .contenedor-opasity {
            /*opacity: 0;*/ /* Establecemos la opacidad inicial en 0 */
            /*transition: opacity 1s ease;*/ /* Definimos la transición para la propiedad "opacity" */

            animation: fadeIn 2s ease;
        }

        @keyframes fadeIn {
            0% {
                opacity: 0;
                filter: blur(20px);
            }

            20% {
                opacity: 1;
            }
        }

        .contenedor-opasity.show { /* La clase "show" activará la transición */
            opacity: 1; /* Opacidad máxima cuando se añade la clase "show" */
        }
    </style>

    <div style="padding: 20px;" class="contenedor-opasity contenedor-centrado">
        <div style="text-align: center;">

            <img src="../Recursos/Imagenes/logo_iglesia_dios.png" alt="Logo Iglesia de Dios" style="width: 118px; height: 140px;"/>
             <p class="contenido" style="font-size: 16px; font-family: Roboto, sans-serif; ">Seleccione una opci&oacute;n para comenzar</p>
        </div>
    </div>

    <script src="../Recursos/Javascript/scripts_general.js"></script>
</asp:Content>
