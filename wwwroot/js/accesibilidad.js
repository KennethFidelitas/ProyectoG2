$(document).ready(function () {
    
    var barraHTML =
        '<div id="accesibilidad-menu" class="accesibilidad-menu">' +
        '<button id="menu-toggle" class="btn">Accesibilidad</button>' +
        '<div id="accesibilidad-content" class="accesibilidad-content">' +
        '<button id="btn-contraste">⬛ Alto contraste</button>' +
        '<button id="btn-fuente-mas">A+</button>' +
        '<button id="btn-fuente-normal">A</button>' +
        '<button id="btn-fuente-menos">A-</button>' +
        '</div>' +
        '</div>';

    $("body").append(barraHTML);  

  
    $("#menu-toggle").on("click", function () {
        $("#accesibilidad-menu").toggleClass('open');
    });

    
    $("#btn-contraste").on("click", function () {
        $("body").toggleClass("alto-contraste"); // agrega/quita la clase CSS
        if ($("body").hasClass("alto-contraste")) {
            $(this).text("☀ Contraste normal");
            mostrarAlerta("Modo alto contraste activado", "exito");
        } else {
            $(this).text("⬛ Alto contraste");
            mostrarAlerta("Modo alto contraste desactivado", "exito");
        }
    });

    
    var tamanoActual = 100; 

    $("#btn-fuente-mas").on("click", function () {
        if (tamanoActual < 150) {
            tamanoActual += 15;
            $("html").css("font-size", tamanoActual + "%");
            mostrarAlerta("Fuente: " + tamanoActual + "%", "exito");
        }
    });

    $("#btn-fuente-menos").on("click", function () {
        if (tamanoActual > 85) {
            tamanoActual -= 15;
            $("html").css("font-size", tamanoActual + "%");
            mostrarAlerta("Fuente: " + tamanoActual + "%", "exito");
        }
    });

    $("#btn-fuente-normal").on("click", function () {
        tamanoActual = 100;
        $("html").css("font-size", "100%");
        mostrarAlerta("Fuente restablecida", "exito");
    });

    
    function mostrarAlerta(mensaje, tipo) {
        $(".alerta-accesible").remove();
        var $alerta = $( 
            '<div class="alerta-accesible alerta-' + tipo + '" ' +
            'role="status" aria-live="polite" aria-atomic="true">' +
            mensaje +
            '</div>'
        );
        $("#contenido-principal").prepend($alerta);
        setTimeout(function () {
            $alerta.slideUp(300, function () {
                $(this).remove();
            });
        }, 3000);
    }

   
    window.mostrarAlerta = mostrarAlerta;

  
    $(document).keydown(function (e) {
        if (e.ctrlKey && e.which == 85) {  
            $('#accesibilidad-menu').toggleClass('open');
        }
    });
});