var delalertok = false
function CerrarSesionAlert(btn) {
    if (delalertok) {
        delalertok = false
        return true
    }

    swal({
        title: "Cerrar sesión",
        text: "¿Está seguro que desea cerrar sesión?",
        icon: "info",
        dangerMode: false,
        buttons: {
            cancel: {
                text: "Cancelar",
                value: null,
                visible: true,
                closeModal: true,
            },
            confirm: {
                text: "Cerrar sesión",
                value: true,
                visible: true,
                closeModal: true,
            },
        },
    })
        .then(willDelete => {
            if (willDelete) {
                delalertok = true;
                // Obtencion de url del host
                let url;
                url = window.location.protocol.replace(/\:/g, '') + "://" + window.location.host + '/'
                window.location.href = url + "frmLogin.aspx";
            }
        });
    return false;
}



// Codigo para prevenir el mensaje de reenvio de formulario al recargar la pagina
if (window.history.replaceState) {
    window.history.replaceState(null, null, window.location.href);
}

// Funcion para prevenir que el usuario vaya hacia atras
function preventBack() { window.history.forward(); }
setTimeout("preventBack()", 0);
window.onunload = function () { null };

$(document).ready(function () {
    // Obtener el estado guardado en el almacenamiento local
    var estadoCSS = localStorage.getItem('estadoCSS');

    // Verificar si el estadoCSS es "activo"
    if (estadoCSS === 'activo') {
        // Aplicar la clase "collapse_1" y "fa-rotate-90" a los elementos correspondientes
        $(".wrapper_1").addClass("collapse_1");
        $(".sidebar_1-btn_1").addClass("fa-rotate-90");


    }

    // Agregar el evento click al boton
    $(".sidebar_1-btn_1").click(function () {
        $(".wrapper_1").toggleClass("collapse_1");
        $(".sidebar_1-btn_1").toggleClass("fa-rotate-90");

        // Guardar el estado en el almacenamiento local al hacer clic en el boton
        if ($(".wrapper_1").hasClass("collapse_1")) {
            localStorage.setItem('estadoCSS', 'activo');
        } else {
            localStorage.removeItem('estadoCSS');
        }
    });
});


// Funcion para que funcione correctamente los combobox de telerik
function ChangeToUpperCase(sender, args) {
    var inputElement = sender.get_inputDomElement();
    inputElement.style.textTransform = "uppercase";
}

function asignarEnlace() {

    // Obtencion de url del host
    let url;
    url = window.location.protocol.replace(/\:/g, '') + "://" + window.location.host + '/'
    // Obtener la etiqueta <a> mediante su ID

    document.getElementById('btnResumen').href = url + 'Resumen/frmResumen.aspx';
    document.getElementById('btnIngresos').href = url + 'Ingresos/frmIngresos.aspx';
    document.getElementById('btnEgresos').href = url + 'Egresos/frmEgresos.aspx';
    document.getElementById('btnCuentasCobrar').href = url + 'Cuentas_Por_Cobrar/frmCuentasCobrar.aspx';
    document.getElementById('btnCuentasPagar').href = url + 'Cuentas_Por_Pagar/frmCuentasPagar.aspx';
    document.getElementById('btnMiembros').href = url + 'Miembros/frmMiembros.aspx';
    document.getElementById('btnDescripciones').href = url + 'Otros_Parametros/frmDescripciones.aspx';
    document.getElementById('btnFormas_Pago').href = url + 'Otros_Parametros/frmFormas_Pago.aspx';
    document.getElementById('btnMiscelaneos').href = url + 'Otros_Parametros/frmMiscelaneos.aspx';
    document.getElementById('btnRoles').href = url + 'Usuarios/frmRoles.aspx';
}


// Tooltip
// With the above scripts loaded, you can call `tippy()` with a CSS
// selector and a `content` prop:
tippy('#btnCerrarSesion', {
    content: 'Cerrar sesión',
    placement: 'bottom',
    arrow: true,
});

tippy('.menu_1-btn_1', {
    placement: 'right',
    arrow: true,
});


tippy('li  div  a', {
    placement: 'right',
    arrow: true,
});

tippy('#BotonMenu', {
    content: 'Mostrar/Contraer menú de opciones',
    placement: 'bottom',
    arrow: true,
});