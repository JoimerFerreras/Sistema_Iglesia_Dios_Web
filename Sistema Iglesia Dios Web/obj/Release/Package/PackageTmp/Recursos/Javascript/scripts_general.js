
// Establecer ancho de RadGrid
//function setTableWidth() {
//    var Grid = document.getElementById("gvDatos");
//    var windowWidth = window.innerWidth; // Ancho de la ventana del navegador
//    Grid.style.width = "100%";
//}
//// Llamar a la funci�n cuando se carga la p�gina y cuando se cambia el tama�o de la ventana
//window.addEventListener("load", setTableWidth);
//window.addEventListener("resize", setTableWidth);


// Funcion de confirmacion de eliminacion de registro
var delalertok = false
function delalert(btn) {
    if (delalertok) {
        delalertok = false
        return true
    }

    swal({
        title: "Eliminar el registro",
        text: "\u00BFEst\u00E1 seguro que desea eliminar este registro?",
        icon: "warning",
        dangerMode: true,
        buttons: {
            cancel: {
                text: "Cancelar",
                value: null,
                visible: true,
                closeModal: true,
            },
            confirm: {
                text: "Eliminar",
                value: true,
                visible: true,
                closeModal: true,
            },
        },
    })
        .then(willDelete => {
            if (willDelete) {
                delalertok = true;
                MostrarPanelCarga();
                btn.click();
            }
        });
    return false;
}


// Aplicar animacion de opasitdad a un contenedor al cargar la aplicacion
function fadeInContainer() {
    const fadeContainer = document.getElementById('divContenedorOpasity');
    fadeContainer.classList.add('show'); /* A�adimos la clase "show" para activar la transici�n */
}

// Llamamos a la funci�n para que se active la transici�n cuando se carga la p�gina
window.addEventListener('load', fadeInContainer);





//var template = document.getElementsByClassName('item_1');
//tippy('.item_1', {
//    content: template.innerHTML,
//    placement: 'left',
//    arrow: true,
//});



// Animacion para aumentar numero
//let valueDisplays = document.querySelectorAll(".num");
//let interval = 1;

//valueDisplays.forEach((valueDisplay) => {
//    let startValue = 0;
//    let endValue = parseInt(valueDisplay.getAttribute("data-val"));
//    let duration = Math.floor(interval / endValue);
//    let counter = setInterval(function () {
//        startValue += 1;
//        valueDisplay.textContent = startValue;
//        if (startValue == endValue) {
//            clearInterval(counter);
//        }
//    }, duration);
//});




//// Se ejecuta al cambiar el tama�o
//window.addEventListener('resize', function () {
//    var elemento = document.getElementById('wrapper_1');
//    var ventanaAncho = window.innerWidth;
//    var ventanaAlto = window.innerHeight;

//    if (ventanaAncho < 1920 || ventanaAlto < 1080) {
//        elemento.classList.add('collapse_1');
//    } else {
//        elemento.classList.remove('collapse_1');
//    }
//});

//// Ejecutar el evento resize al cargar la p�gina para aplicar el estilo inicial
//window.dispatchEvent(new Event('resize'));



function MostrarPanelCarga() {
    // Obt�n una referencia al elemento div
    var divElement = document.getElementById("divPanelCarga");

    // Aplica estilos utilizando la propiedad style del elemento
    divElement.style.visibility = "visible";
}

