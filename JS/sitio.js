$(document).ready(function () {

    pathArray = location.href.split('/');
    protocol = pathArray[0];
    host = pathArray[2] + "/"+pathArray[3]+"/";
    URLactual = protocol + '//' + host;

    $(window).bind("pageshow", function () {
        var form = $('form');
        // let the browser natively reset defaults
        $("#Form_sol").trigger("reset");

    });

    $(document).on("keydown", function (e) {
        if (e.which === 8 && !$(e.target).is("input:not([readonly]):not([type=radio]):not([type=checkbox]), textarea, [contentEditable], [contentEditable=true]")) {
            e.preventDefault();
        }
    });


    $("#limpia_solicitud").click(function () {
        $("#Form_sol").trigger("reset");
    })



    jQuery.fn.preventDoubleSubmission = function () {
        $(this).on('submit', function (e) {
            var $form = $(this);

            if ($form.data('submitted') === true) {
                // Previously submitted - don't submit again
                alert('Se esta procesando la Solicitud... Favor Espere.');
                e.preventDefault();
            } else {
                // Mark it so that the next submit can be ignored
                // ADDED requirement that form be valid
                if($form.valid()) {
                    $form.data('submitted', true);
                }
            }
        });

        // Keep chainability
        return this;
    };

 
    $('#Form_sol').preventDoubleSubmission();


    function mesesgracias() {
        if (tipo_gracia == 2) {

            Sol_Fec_Vct = $("#Sol_Fec_Vct").val();
            Sol_Fec_Cse = $("#Sol_Fec_Cse").val();

            sumaFecha = function (d, fecha) {
                var Fecha = new Date();
                var sFecha = fecha || (Fecha.getFullYear() + "/" + (Fecha.getMonth() + 1) + "/" + Fecha.getDate());
                var sep = sFecha.indexOf('/') != -1 ? '/' : '-';
                var aFecha = sFecha.split(sep);
                var fecha = aFecha[0] + '/' + aFecha[1] + '/' + aFecha[2];
                fecha = new Date(fecha);
                fecha.setDate(fecha.getDate() + parseInt(d));
                var anno = fecha.getFullYear();
                var mes = fecha.getMonth() + 1;
                var dia = fecha.getDate();
                mes = (mes < 10) ? ("0" + mes) : mes;
                dia = (dia < 10) ? ("0" + dia) : dia;
                var fechaFinal = anno + sep + mes + sep + dia;
                return (fechaFinal);
            }

            Fre_Pag_Id = $("#Fre_Pag_Id").val();

            if (Fre_Pag_Id == 1) {
                frecuencia = "1";
            } else if (Fre_Pag_Id == 2) {
                frecuenica = "3";
            } else if (Fre_Pag_Id == 3) {
                frecuencia = "6";
            } else if (Fre_Pag_Id == 4) {
                frecuencia = "12";
            }

            plazoGracia = meses_gracia * 28 + frecuencia * 28;
            calculaplazo = sumaFecha(plazoGracia, Sol_Fec_Cse)

            var Fecha_Venc1 = new Date(Sol_Fec_Vct);
            var FVdd = Fecha_Venc1.getDate();
            var FVmm = Fecha_Venc1.getMonth();
            var FVyy = Fecha_Venc1.getFullYear();
            var fv = new Date(FVyy, FVmm, FVdd);

            var calculaplazo1 = new Date(calculaplazo);
            var FCdd = calculaplazo1.getDate();
            var FCmm = calculaplazo1.getMonth();
            var FCyy = calculaplazo1.getFullYear();
            var fc = new Date(FCyy, FCmm, FCdd);

            console.log("FECHA CURSE :" + Sol_Fec_Cse)
            console.log("FECHA VENCIMIENTO :" + Sol_Fec_Vct)
            console.log("PLAZO GRACIA :" + plazoGracia);
            console.log("PLAZO :" + calculaplazo);


            if (fv.getTime() <= fc.getTime()) {
                alert("ERROR FECHA PRIMER VENCIMIENTO: No puede ser inferior al Periodo de Gracia. Se deber ingresar la fecha de la primera cuota de Capital e Intereses");
                $("#Fecha_Venc").val("");
                $("#Sol_Fec_Vct").val("");
            }

        }
    }


    $("#graba_solicitud").click(function () {
        $("#Est_Sol_Id").val("1");
        $("#Etp_Sol_Id").val("1");
        mesesgracias();
    })


    $("#graba_solicitud_2").click(function () {
        $("#Est_Sol_Id").val("1");
        $("#Etp_Sol_Id").val("1");

        var fecha_curse = $("#Fecha_Curse").val();
        var dateCurse = fecha_curse.split('-');
        var newdateCurse = dateCurse[2] + '-' + dateCurse[1] + '-' + dateCurse[0].slice(-2);
        $("#Sol_Fec_Cse").val(newdateCurse);

        var fecha_Venc = $("#Fecha_Venc").val();
        var dateVenc = fecha_Venc.split('-');
        var newdateVenc = dateVenc[2] + '-' + dateVenc[1] + '-' + dateVenc[0].slice(-2);
        $("#Sol_Fec_Vct").val(newdateVenc);

        mesesgracias();
    })



    $("#envia_solicitud").click(function () {
        $("#Est_Sol_Id").val("1");
        $("#Etp_Sol_Id").val("2");

        var fecha_curse = $("#Fecha_Curse").val();
        var dateCurse = fecha_curse.split('-');
        var newdateCurse = dateCurse[2] + '-' + dateCurse[1] + '-' + dateCurse[0].slice(-2);
        $("#Sol_Fec_Cse").val(newdateCurse);

        var fecha_Venc = $("#Fecha_Venc").val();
        var dateVenc = fecha_Venc.split('-');
        var newdateVenc = dateVenc[2] + '-' + dateVenc[1] + '-' + dateVenc[0].slice(-2);
        $("#Sol_Fec_Vct").val(newdateVenc);

        mesesgracias();

    })




    $("#envia_solicitud_2").click(function () {
        $("#Est_Sol_Id").val("1");
        $("#Etp_Sol_Id").val("2");

        var fecha_curse = $("#Fecha_Curse").val();
        var dateCurse = fecha_curse.split('-');
        var newdateCurse = dateCurse[2] + '-' + dateCurse[1] + '-' + dateCurse[0].slice(-2);
        $("#Sol_Fec_Cse").val(newdateCurse);

        var fecha_Venc = $("#Fecha_Venc").val();
        var dateVenc = fecha_Venc.split('-');
        var newdateVenc = dateVenc[2] + '-' + dateVenc[1] + '-' + dateVenc[0].slice(-2);
        $("#Sol_Fec_Vct").val(newdateVenc);

        mesesgracias();

    })



    $("#actualiza_solicitud").click(function () {
        var fecha_curse = $("#Fecha_Curse").val();
        var dateCurse = fecha_curse.split('-');
        var newdateCurse = dateCurse[2] + '-' + dateCurse[1] + '-' + dateCurse[0].slice(-2);
        $("#Sol_Fec_Cse").val(newdateCurse);

        var fecha_Venc = $("#Fecha_Venc").val();
        var dateVenc = fecha_Venc.split('-');
        var newdateVenc = dateVenc[2] + '-' + dateVenc[1] + '-' + dateVenc[0].slice(-2);
        $("#Sol_Fec_Vct").val(newdateVenc);
        mesesgracias();

    })







    $(".allow_numeric").on("input", function (evt) {
        var self = $(this);
        self.val(self.val().replace(/[^0-9\.]/g, ''));
        if ((evt.which != 46 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
            evt.preventDefault();
        }
    });


    $(".telefono_numeric").on("input", function (evt) {
        var self = $(this);
        self.val(self.val().replace(/[^0-9\+]/g, ''));
        if ((evt.which != 43 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
            evt.preventDefault();
        }
    });



      
    $("#btn_trae_datos").click(function () {

        var rut_cliente = $("#Sol_Nrt_Emp").val();
        var dv = $("#Sol_Drt_Emp").val();
        if (rut_cliente != "") {
            $.ajax({
                url: URLactual + "/Solicitudes/DatosClientes?Sol_Nrt_Emp=" + rut_cliente + "&Sol_Drt_Emp=" + dv,
                type: "POST",
                dataType: "json"
            })
            .success(function (SalDatosCliente) {
                //console.log(SalDatosCliente);
                $("#Sol_Rzn_Scl").val(SalDatosCliente.cli_nom);

                if (SalDatosCliente.Tiene_Solicitud == "Si") {
                    mensaje = "USUARIO POSEE UNA SOLICITUD PENDIENTE";
                    $(".p_error").html("<div class='alert alert-danger fade in'>" + mensaje + "<a href='#' class='close' data-dismiss='alert'>&times;</a></div>");
                    $("#form_principal").css("display", "none");
                    $("#botones_envios").hide();
                } else {

       
                    $("#btn_trae_datos").attr("disabled", "disabled");
                    $("#SeleccionaPrograma").modal({ backdrop: 'static', keyboard: false });

                    $('input[type=radio][name=tipoPrograma]').change(function () {
                        $("#btn-programa").attr('disabled', false);
                    })

                    $("#btn-programa").click(function () {
                        $("#form_principal").css("display", "block");
                        $("#botones_envios").show();
                        $(".alert").hide();
                        var TipoPrograma = $("input:radio[name=tipoPrograma]:checked").val();
                        
                        if (TipoPrograma == 1) {
                            $("#PROGRAMA").val("FOGAIN");
                            $("#Catastrofe option").attr('disabled', false);
                        } else if (TipoPrograma == 2) {
                            $("#PROGRAMA").val("PROINVERSION");
                            //$("#Catastrofe option").attr('disabled', true);
                            $("#Catastrofe option").remove();
                            $("#Catastrofe").append('<option value="1"> NO AFECTADO</option>');
                        }
                    });
                };
            })
               .error(function (xrh, status) {
                   $("#form_principal").css("display", "none");
                   $(".p_error").html("");
                   mensaje = "RUT NO ENCONTRADO !!!";
                   $(".p_error").html("<div class='alert alert-danger fade in msj_error'>" + mensaje + "<a href='#' class='close' data-dismiss='alert'>&times;</a></div>");
               });
        } else {
            $("#form_principal").css("display", "none");
            $(".p_error").html("");
            mensaje = "DEBE INGRESAR UN RUT !!!";
            $(".p_error").html("<div class='alert alert-danger fade in msj_error'>" + mensaje + "<a href='#' class='close' data-dismiss='alert'>&times;</a></div>");
        }
          
    });


    $("#Catastrofe").change(function () {
        $("#Regiones").empty();
        $("#Rel_Ctf_Com_Id").empty();

        if ($("#Catastrofe").val() != "") {
            var id_catastrofe  = $("#Catastrofe").val();
            $.ajax({
                url: URLactual + "/Localidades/ListRegiones?Ctf_Cod=" + id_catastrofe + '&Ctf_Est=1',
                type: "POST",
                dataType: "json"
            })
           .success(function (SalListRegiones) {

               $("#Regiones").append($('<option selected></option>').val("").html("Seleccione Región"));
               $("#Rel_Ctf_Com_Id").append($('<option selected></option>').val("").html("Seleccione Localización"));
               
               $.each(SalListRegiones, function (Rgn_COD, option) {
                   $("#Regiones").append($('<option></option>').val(option.Rgn_Cod).html(option.Rgn_Nom));
               })
               $("#Regiones").prop("disabled", false);
           })
           .error(function (xrh, status) {
               alert(status);
               alert("Error al traer las Regiones!!!");
           });
        }
        else {
            $("#Catastrofe").empty();
        }
    });


    $("#Regiones").change(function () {
        if ($("#Regiones").val() != "") {
            var id_regiones = $("#Regiones").val();
            var id_catastrofe = $("#Catastrofe").val()

            alert(id_regiones);
            alert(id_catastrofe);


            $("#Rel_Ctf_Com_Id").empty();

            $.ajax({
                url: URLactual + "/Localidades/ListLocalizaciones?Ctf_Cod=" + id_catastrofe + "&Reg_id=" + id_regiones + '&Ctf_Est=1 ',
                type: "POST",
                dataType: "json"
            })
           .success(function (SalListComuna) {
               $("#Rel_Ctf_Com_Id").append($('<option selected></option>').val("").html("Seleccione Localización"));

               $.each(SalListComuna, function (Rel_Ctf_Com_Id, option) {
                   $("#Rel_Ctf_Com_Id").append($('<option></option>').val(option.Rel_Ctf_Com_Id).html(option.Com_Nom_02));
               })
               $("#Rel_Ctf_Com_Id").prop("disabled", false);
           })
           .error(function (xrh, status) {
               alert(status);
               alert("Error al traer las Localizaciones!!!");
           });
        }
        else {
            $("#Rel_Ctf_Com_Id").empty();
        }
    });


    $('.navbar a.dropdown-toggle').on('click', function (e) {
        var $el = $(this);
        var $parent = $(this).offsetParent(".dropdown-menu");
        $(this).parent("li").toggleClass('open');

        if (!$parent.parent().hasClass('nav')) {
            $el.next().css({ "top": $el[0].offsetTop, "left": $parent.outerWidth() - 4 });
        }

        $('.nav li.open').not($(this).parents("li")).removeClass("open");

        return false;
    });


    $('[data-submenu]').submenupicker();


});

