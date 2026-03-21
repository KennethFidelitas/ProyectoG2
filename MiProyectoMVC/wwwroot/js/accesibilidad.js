$(document).ready(function () {

    /*  saltar al contenido principal */
    // Agrega un enlace invisible que aparece al presionar Tab
    $("body").prepend(
        '<a class="skip-link" href="#contenido-principal">Saltar al contenido</a>'
    );


    /* BARRA DE ACCESIBILIDAD= */
    // Crea la barra con los botones y la inserta antes del header
    var barraHTML =
        '<div id="barra-accesibilidad" role="toolbar" aria-label="Opciones de accesibilidad">' +
        '<span class="etiqueta">♿ Accesibilidad:</span>' +
        '<button id="btn-contraste">⬛ Alto contraste</button>' +
        '<button id="btn-fuente-mas">A+</button>' +
        '<button id="btn-fuente-normal">A</button>' +
        '<button id="btn-fuente-menos">A-</button>' +
        '</div>';

    $("header").before(barraHTML); // jQuery: inserta HTML antes del header


    /* --- Botón: Alto contraste --- */
    // Evento click
    $("#btn-contraste").on("click", function () {
        $("body").toggleClass("alto-contraste"); // agrega/quita la clase CSS

        // Cambia el texto del botón según el estado
        if ($("body").hasClass("alto-contraste")) {
            $(this).text("☀ Contraste normal");
            mostrarAlerta("Modo alto contraste activado", "exito");
        } else {
            $(this).text("⬛ Alto contraste");
            mostrarAlerta("Modo alto contraste desactivado", "exito");
        }
    });


    /* --- Botones: Tamaño de fuente --- */
    // Guardamos el tamaño actual en una variable
    var tamanoActual = 100; // 100% = tamaño normal

    // Evento click (aumentar)
    $("#btn-fuente-mas").on("click", function () {
        if (tamanoActual < 150) {          // límite máximo: 150%
            tamanoActual += 15;
            $("html").css("font-size", tamanoActual + "%"); // jQuery: cambia el CSS
            mostrarAlerta("Fuente: " + tamanoActual + "%", "exito");
        }
    });

    // Evento click (reducir)
    $("#btn-fuente-menos").on("click", function () {
        if (tamanoActual > 85) {           // límite mínimo: 85%
            tamanoActual -= 15;
            $("html").css("font-size", tamanoActual + "%");
            mostrarAlerta("Fuente: " + tamanoActual + "%", "exito");
        }
    });

    // Evento click (restablecer)
    $("#btn-fuente-normal").on("click", function () {
        tamanoActual = 100;
        $("html").css("font-size", "100%");
        mostrarAlerta("Fuente restablecida", "exito");
    });


    /* ARIA LIVE REGION */
    $("body").append(
        '<div id="lector-pantalla" aria-live="polite" aria-atomic="true" ' +
        'style="position:absolute;left:-9999px;width:1px;height:1px;overflow:hidden;"></div>'
    );

    function anunciar(mensaje) {
        $("#lector-pantalla").text(""); // limpia primero
        setTimeout(function () {
            $("#lector-pantalla").text(mensaje); // luego escribe
        }, 100);
    }


    /* NAVEGACIÓN POR TECLADO EN EL MENÚ */
    // Permite moverse con las flechas del teclado en el menú
    $("nav .nav-link").on("keydown", function (e) { // Evento keydown #2
        var $links = $("nav .nav-link"); // todos los links del menú
        var posicion = $links.index($(this)); // posición actual

        if (e.key === "ArrowRight" || e.key === "ArrowDown") {
            e.preventDefault();
            // Ir al siguiente link (vuelve al inicio si es el último)
            $links.eq((posicion + 1) % $links.length).focus();
        }

        if (e.key === "ArrowLeft" || e.key === "ArrowUp") {
            e.preventDefault();
            // Ir al anterior
            $links.eq((posicion - 1 + $links.length) % $links.length).focus();
        }
    });


    /* FORMULARIOS ACCESIBLES*/

    // Evento focus #3: resaltar el grupo del campo activo
    $(document).on("focus", "input, select, textarea", function () {
        $(this).closest(".form-group, .mb-3").css("outline", "2px solid #0056b3");
    });

    // Evento blur #4: quitar resalte al salir
    $(document).on("blur", "input, select, textarea", function () {
        $(this).closest(".form-group, .mb-3").css("outline", "none");
    });

    // Evento change #5: validar select al cambiar valor
    $(document).on("change", "select[required]", function () {
        if ($(this).val() === "") {
            $(this).attr("aria-invalid", "true");  // marca como inválido
        } else {
            $(this).attr("aria-invalid", "false"); // marca como válido
        }
    });

    // Evento input #7: contador de caracteres en campos con maxlength
    $(document).on("input", "input[maxlength], textarea[maxlength]", function () {
        var max = $(this).attr("maxlength");
        var usados = $(this).val().length;
        var campoId = $(this).attr("id");

        // Busca o crea el contador debajo del campo
        var $contador = $("#cnt-" + campoId);
        if ($contador.length === 0) {
            $contador = $('<small id="cnt-' + campoId + '" aria-live="polite"></small>');
            $(this).after($contador); // jQuery: inserta el contador después del input
        }

        $contador.text(usados + " / " + max + " caracteres");

        // Si está cerca del límite, pone el texto en rojo
        if (usados >= max * 0.9) {
            $contador.css("color", "#b00020");
        } else {
            $contador.css("color", "#595959");
        }
    });

    // Evento submit #6: validar campos obligatorios antes de enviar
    $(document).on("submit", "form", function (e) {
        var hayError = false;

        // Revisa cada campo obligatorio
        $(this).find("[required]").each(function () {
            if ($(this).val().trim() === "") {
                $(this).attr("aria-invalid", "true");
                hayError = true;
            }
        });

        if (hayError) {
            e.preventDefault(); // cancela el envío
            // Pone el foco en el primer campo con error
            $(this).find("[aria-invalid='true']").first().focus();
            anunciar("El formulario tiene errores. Revisa los campos obligatorios.");
        }
    });


    /* TOOLTIPS ACCESIBLES*/
    // Para usar: agrega data-tooltip="tu texto" a cualquier elemento HTML
    $("[data-tooltip]").each(function () {
        var $el = $(this);
        var texto = $el.data("tooltip");

        // Crea el tooltip
        var $tooltip = $('<span role="tooltip" style="' +
            'position:absolute;background:#1a1a1a;color:#fff;' +
            'padding:0.3em 0.6em;border-radius:4px;font-size:0.8rem;' +
            'display:none;z-index:9999;"></span>').text(texto);

        $("body").append($tooltip);

        // Evento mouseenter #8: mostrar tooltip
        $el.on("mouseenter focus", function () {
            var pos = $el.offset();
            $tooltip.css({ top: pos.top - 35, left: pos.left }).fadeIn(150);
        });

        // Evento mouseleave #9: ocultar tooltip
        $el.on("mouseleave blur", function () {
            $tooltip.fadeOut(150);
        });
    });


    /*  BOTÓN "VOLVER ARRIBA"*/
    // Agrega el botón al final del body
    $("body").append(
        '<button id="btn-arriba" aria-label="Volver al inicio de la página">↑</button>'
    );

    // Evento scroll: mostrar/ocultar el botón
    $(window).on("scroll", function () {
        if ($(this).scrollTop() > 300) {
            $("#btn-arriba").fadeIn(200);  // aparece al bajar
        } else {
            $("#btn-arriba").fadeOut(200); // desaparece al subir
        }
    });

    // Click en el botón: vuelve al inicio con animación
    $("#btn-arriba").on("click", function () {
        $("html, body").animate({ scrollTop: 0 }, 400);
        anunciar("Volviste al inicio de la página");
    });


    /*  ACCESIBILIDAD AUTOMÁTICA EN TABLAS */
    // Añade scope="col" a todos los th de tablas que no lo tengan
    $("table thead th").each(function () {
        if (!$(this).attr("scope")) {
            $(this).attr("scope", "col");
        }
    });


    /* mostrar alertas*/
    function mostrarAlerta(mensaje, tipo) {
        // Elimina alertas anteriores
        $(".alerta-accesible").remove();

        // Crea la alerta
        var $alerta = $(
            '<div class="alerta-accesible alerta-' + tipo + '" ' +
            'role="status" aria-live="polite" aria-atomic="true">' +
            mensaje +
            '</div>'
        );

        // La inserta al inicio del contenido principal
        $("#contenido-principal").prepend($alerta);

        // La oculta automáticamente después de 3 segundos
        setTimeout(function () {
            $alerta.slideUp(300, function () {
                $(this).remove();
            });
        }, 3000);

        anunciar(mensaje);
    }

    // Hace la función accesible desde otras vistas
    window.mostrarAlerta = mostrarAlerta;

}); // fin document.ready