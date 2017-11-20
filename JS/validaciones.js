$(document).ready(function () {

     $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Anterior',
        nextText: 'Siguiente>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);



    jQuery.extend(jQuery.validator.messages, {
        required: "Este campo es obligatorio.",
        remote: "Por favor, rellena este campo.",
        email: "Por favor, escribe una dirección de correo válida",
        url: "Por favor, escribe una URL válida.",
        date: "Por favor, escribe una fecha válida.",
        dateISO: "Por favor, escribe una fecha (ISO) válida.",
        number: "Por favor, escribe un número entero válido.",
        digits: "Por favor, escribe sólo dígitos.",
        creditcard: "Por favor, escribe un número de tarjeta válido.",
        equalTo: "Por favor, escribe el mismo valor de nuevo.",
        accept: "Por favor, escribe un valor con una extensión aceptada.",
        maxlength: jQuery.validator.format("Por favor, no escribas más de {0} caracteres."),
        minlength: jQuery.validator.format("Por favor, no escribas menos de {0} caracteres."),
        rangelength: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1} caracteres."),
        range: jQuery.validator.format("Por favor, escribe un valor entre {0} y {1}."),
        max: jQuery.validator.format("Por favor, escribe un valor menor o igual a {0}."),
        min: jQuery.validator.format("Por favor, escribe un valor mayor o igual a {0}.")
    });




    $(function () {

        var Fec_Cse_Max_Dia = $("#Fec_Cse_Max_Dia").val();
        var Fec_Cse_Min_Dia = $("#Fec_Cse_Min_Dia").val();

        $("#Fecha_Curse").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: Fec_Cse_Min_Dia,
            maxDate: Fec_Cse_Max_Dia,
            onSelect: function (date) {
                var fecha_curse = $("#Fecha_Curse").val();
                var dateCurse = fecha_curse.split('-');
                var newdateCurse = dateCurse[2] + '-' + dateCurse[1] + '-' + dateCurse[0].slice(-2);
                $("#Sol_Fec_Cse").val(newdateCurse);


                var fecha_curse = $("#Fecha_Curse").val();
                $.ajax({
                    url: URLactual + "/Calculos/ObtieneValorMoneda?fecha_cse=" + fecha_curse,
                    type: "POST",
                    dataType: "json"
                })
                 .success(function (SalValorMoneda) {
                     $("#Sol_Val_Dol").val("");
                     $("#Sol_Val_Dol").val(SalValorMoneda.Valor_Dol);
                     $("#Sol_Val_UF").val(SalValorMoneda.Valor_UF);

                 })
                .error(function (xrh, status) {
                    alert("Error al recuperar valor de monedas");
                })


                if ($("#Tip_Ope_Id").val() == "18") {
                    var dateSelectCurse = $(this).datepicker('getDate'); 
                    var DateCurse = $.datepicker.formatDate('yy/mm/dd', dateSelectCurse); 
                    var tt = DateCurse;
                    var date = new Date(tt);
                    var newdate = new Date(date);
                    newdate.setDate(newdate.getDate());
                    var dd = newdate.getDate();
                    var mm = newdate.getMonth() + 1;
                    var y = newdate.getFullYear() + 1;
                    var fechaFactoring = mm + '-' + dd + '-' + y;
                    var fechaFactoringyymmdd = y + '-' + mm + '-' + dd;
                    var fechaFactoringddmmyy = dd + '-' + mm + '-' + y;
                    $("#Fecha_Venc").val(fechaFactoringddmmyy);
                    $("#Sol_Fec_Vct").val(fechaFactoringyymmdd);
                    $("#Fecha_Venc").popover({ 'trigger': 'focus', 'title': 'El tipo de Operacion Seleccionada es Factoring. Se agrega un Año a la fecha de Curse', 'placement' : 'top' });
                }
            }
        });
        
        $("#Fecha_Venc").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-mm-yy',
            minDate: '0',
            onSelect: function (date) {

                var fecha_Venc = $("#Fecha_Venc").val();
                var dateVenc = fecha_Venc.split('-');
                var newdateVenc = dateVenc[2] + '-' + dateVenc[1] + '-' + dateVenc[0].slice(-2);
                $("#Sol_Fec_Vct").val(newdateVenc);

                var dateSelect = $(this).datepicker('getDate');
		        
                var dayOfWeek = $.datepicker.formatDate('DD', dateSelect);
                $('#datepicker-day-of-week').text(dayOfWeek);

                if ((dayOfWeek == "Sábado") && ($("#Tip_Ope_Id").val() == "2" || $("#Tip_Ope_Id").val() == "4")) {
                    $("#mensajeerrorfecven").html("<font color='red'> <strong>ALERTA :</strong>  SELECCIONÓ DÍA INHÁBIL PARA UNA OPERACIÓN LEASING</font>");
                } else if (dayOfWeek == "Sábado" || dayOfWeek == "Domingo")  {
                    $("#mensajeerrorfecven").html("<font color='red'> <strong>ERROR :</strong>  SELECCIONÓ DÍA INHÁBIL</font>");
                    $("#Fecha_Venc").val("");
                    $("#Sol_Fec_Vct").val("");
                } else {
                    $("#mensajeerrorfecven").html("");
                }
            }
        });

        $("#datepicker-busqueda-inicio").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy', maxDate: '0' });
        $("#datepicker-busqueda-fin").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'dd-mm-yy', maxDate: '0' });
    })


    $('#Form_sol').validate({
        focusCleanup: true,
        rules: {
            rut: {
                required: true,
                minlength: 3
            },
            Sol_Tel: {
                required: true,
                minlength: 8,
                maxlength: 12
            },
            Sol_Num_Call: {
                required: true,
                minlength: 2,
                maxlength: 7
            },
            Sol_Mnt_Crd: {
                required: true,
                number: true,
                min: 0
            },
            rut_dv: {
                required: true
            },
            Sol_Vnt_Emp: {
                required: true,
                number: true,
                min: 0,
                max: 100000
            },
            Sol_Mnt_Cbt_Utz: {
                required: true,
                number: true
            },
            Tip_Ope_Id: {
                required: true,
                min: 0
            },
            Regiones: {
                required: true,
                min: 0
            },
            Rel_Ctf_Com_Id: {
                required: true,
                min: 0
            },
            Sol_Mes_Grc: {
                required: true,
                number: 0,
                min: 0
            },
            Sol_Num_Cuo: {
                required: true,
                number: true,
                min: 1,
                max: 240
            },
            Sol_Int_Anl: {
                required: true,
                number: true,
                min: 0.5,
                max: 36
            },
            Sol_Cro_Elt: {
                myEmail: true
            },
            Des_Proy_Cod_01: {
                required: true,
                min: 1
            },
            Des_Proy_Cod_02: {
                required: true,
                min: 1
            },
            Des_Proy_Cod_03: {
                required: true,
                min: 1
            },
        },
        messages: {
            Sol_Mnt_Crd: "MONTO CREDITO INCORRECTO",
            Des_Proy_Cod_01: "DEBE SELECCIONAR UN PROYECTO",
            Des_Proy_Cod_02: "DEBE SELECCIONAR UN PROYECTO",
            Des_Proy_Cod_03: "DEBE SELECCIONAR UN PROYECTO",
            Sol_Vnt_Emp: "CLIENTE NO CALIFICA PARA OPERAR CON GARANTÍA FOGAIN"
        }

        //,submitHandler: function (form) { // for demo
            //alert('valid form submitted'); // for demo
        //    form.submit();
            //return false; // for demo
        //}
    });


    jQuery.validator.addMethod("myEmail", function (value, element) {
        return this.optional(element) || (/^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@([a-zA-Z0-9]+(-[a-zA-Z0-9]+)*\.)+[a-zA-Z]{2,4}$/.test(value) && /^(?=.{1,64}@.{4,64}$)(?=.{6,100}$).*/.test(value));
    }, 'Por favor, escriba una dirección de correo válida.');


    ////// VALIDACION 6.2.3

    $("#Sol_Mnt_Crd").change(function () {
        tipo_moneda = $("#Tip_Mon_Id").val();
        monto_cre = $("#Sol_Mnt_Crd").val();
        monto_cre_sp = monto_cre.replace(/\./g, '');
        if (tipo_moneda == 1){
            if (monto_cre < 100000) {
                alert("ERROR : EL MONTO DEBE SER MAYOR A 100000");
                $("#Sol_Mnt_Crd").val('');
            }
        } else if (tipo_moneda == 2){
            if (monto_cre < 5 || monto_cre > 250000) {
                alert("ERROR : EL MONTO DEBE SER MAYOR A 5 UF Y MENOR A 250000");
            }
        }
    })
    
    ////// VALIDACION 6.2.8

    $("#Sol_Mes_Grc").click(function () {
        $("#mensajeerror").html("");
    })


    $("#Sol_Mes_Grc").blur(function () {
        meses_gracia = $("#Sol_Mes_Grc").val();
        tipo_gracia = $("#Tip_Grc_Id").val();
        fecha_Curse = $("#Fecha_Curse").val();
        Fecha_Venc = $("#Fecha_Venc").val();
        Sol_Fec_Vct = $("#Sol_Fec_Vct").val();
        Sol_Fec_Cse = $("#Sol_Fec_Cse").val();


        if (tipo_gracia == 0 && meses_gracia > 0) {
            $("#Sol_Mes_Grc").val('0');
            $("#mensajeerror").html("<font color='red'> <strong>ERROR :</strong>  LOS MESES DE GRACIA DEBEN SER 0 SI ELIGIÓ LA OPCIÓN SIN GRACIA</font>");
        } else if (tipo_gracia == 2 && meses_gracia <= 0) {
            $("#mensajeerror").html("<font color='red'>  <strong>ERROR :</strong> LOS MESES DE GRACIA DEBEN SER MAYOR A 0 SI ELIGIÓ LA OPCIÓN GRACIA SOLO CAPITAL</font>");
        } else if (tipo_gracia == 3 && meses_gracia <= 0) {
            $("#mensajeerror").html("<font color='red'>  <strong>ERROR :</strong> LOS MESES DE GRACIA DEBEN SER MAYOR A 0 SI ELIGIÓ LA OPCIÓN GRACIA CAPITAL E INTERÉS</font>");
        }


        
    })
    
       

    $("#Tip_Grc_Id").change(function () {

        var val_Tip_Grc_Id = $("#Tip_Grc_Id").val();

        if (val_Tip_Grc_Id == 0) {
            $("#Sol_Mes_Grc").val('0');
        } else {
            $("#Sol_Mes_Grc").val('');
        }

 
    })


    ////// VALIDACION 6.2.12

    $("#Sol_Num_Cuo").blur(function () {
        if ($("#Fre_Pag_Id").val() == 2 && $("#Sol_Num_Cuo").val() > 80) {
            $("#mensajeerrornum_cuo").html("<font color='red'> <strong>ERROR :</strong>  EL NÚMERO DE CUOTAS NO DEBE SUPERAR LAS 80 CUOTAS</font>");
            $("#Sol_Num_Cuo").val('');
        } else if ($("#Fre_Pag_Id").val() == 3 && $("#Sol_Num_Cuo").val() > 40) {
            $("#mensajeerrornum_cuo").html("<font color='red'> <strong>ERROR :</strong>  EL NÚMERO DE CUOTAS NO DEBE SUPERAR LAS 40 CUOTAS</font>");
            $("#Sol_Num_Cuo").val('');
        } else if ($("#Fre_Pag_Id").val() == 4 && $("#Sol_Num_Cuo").val() > 20) {
            $("#mensajeerrornum_cuo").html("<font color='red'> <strong>ERROR :</strong>  EL NÚMERO DE CUOTAS NO DEBE SUPERAR LAS 20 CUOTAS</font>");
            $("#Sol_Num_Cuo").val('');
        } else {
            $("#mensajeerrornum_cuo").html("");
        }
    })


    $("#Fre_Pag_Id").change(function () {
        $("#Sol_Num_Cuo").val('');
    })



    $('#rut').Rut({
        digito_verificador: '#rut_dv',
        on_error: function () {

            $("#form_principal").css("display", "none");
            $(".p_error").html("");
            mensaje = "RUT INCORRECTO";
            $(".p_error").html("<div class='alert alert-danger fade in msj_error'>" + mensaje + "<a href='#' class='close' data-dismiss='alert'>&times;</a></div>");
            $("#rut").val('');
            $("#rut_dv").val('');

        }
    });


    $("#rut_dv").blur(function () {
        var rutsinpunto = $("#rut").val();
        rutsinpunto2 = rutsinpunto.replace(/[.]/g, "");
        $("#Sol_Nrt_Emp").val(rutsinpunto2);

        var dv = $("#rut_dv").val();
        $("#Sol_Drt_Emp").val(dv);

    })

    

    $("#Fecha_Curse").change(function () {
        var fecha_curse = $("#Fecha_Curse").val();
        var dateCurse = fecha_curse.split('-');
        var newdateCurse = dateCurse[2] + '-' + dateCurse[1] + '-' + dateCurse[0].slice(-2);
        $("#Sol_Fec_Cse").val(newdateCurse);
    })

    $("#Fecha_Venc").change(function () {
        var fecha_Venc = $("#Fecha_Venc").val();
        var dateVenc = fecha_Venc.split('-');
        var newdateVenc = dateVenc[2] + '-' + dateVenc[1] + '-' + dateVenc[0].slice(-2);
        $("#Sol_Fec_Vct").val(newdateVenc);
    })
  

            $(".camponumerico").keydown(function (e) {
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                (e.keyCode == 65 && e.ctrlKey === true) || 
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                    return;
                }
 
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });
    


})

