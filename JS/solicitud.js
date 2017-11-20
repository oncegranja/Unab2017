$(document).ready(function () {

    pathArray = location.href.split('/');
    protocol = pathArray[0];
    host = pathArray[2] + "/" + pathArray[3] + "/";
    URLactual = protocol + '//' + host;


    jQuery.extend({
        getURLParam: function (strParamName) {
            var strReturn = "";
            var strHref = window.location.href;
            var bFound = false;

            var cmpstring = strParamName + "=";
            var cmplen = cmpstring.length;

            if (strHref.indexOf("?") > -1) {
                var strQueryString = strHref.substr(strHref.indexOf("?") + 1);
                var aQueryString = strQueryString.split("&");
                for (var iParam = 0; iParam < aQueryString.length; iParam++) {
                    if (aQueryString[iParam].substr(0, cmplen) == cmpstring) {
                        var aParam = aQueryString[iParam].split("=");
                        strReturn = aParam[1];
                        bFound = true;
                        break;
                    }
                }
            }
            if (bFound == false) return null;
            return strReturn;
        }
    });


    var estado_sol = $.getURLParam("estado");

    var i = 0;
    var fec_1 = "a";
    $('#ListOpeRep').DataTable({
        order: [[3,"desc"]],
        dom: 'Bfrtip',
        buttons: [
            'csv', 'print'
        ],

        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "LoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        },
        "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "ajax": {
            "url": URLactual + "Listados/ListSolicitudes?estado="+ estado_sol,
            "dataSrc": "",
            "paging": true,
            "searching": true
        },
        "columns": [
            { "data": "Tip_Ope_Pgm" },
            { "data": "Fecha_Curse" },
            { "data": "Rut_Empresa" },
            { "data": "Razon_Social" },
            { "data": "Numero_IFI" },
            { "data": "Ejecutivo" },
            { "data": "Oficina" },
            {
                "data": null,
                "mRender": function (row) {

                    var solicitud = row.Sol_Id
                    var solu = "";
                    if (estado_sol == 1) {
                        var boton_2 = '<div class="dropdown"><a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-success" data-target="#" href="#"><i class="fa fa-user fa-fw"></i> Acción <span class="caret"></span></a>'
                        boton_2 = boton_2 + '<ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">';
                        boton_2 = boton_2 + '<li><a href="../Solicitudes/Edit/' + solicitud + '"><i class="fa fa-pencil"></i> Editar </a></li>';
                        boton_2 = boton_2 + '<li><a href="../Solicitudes/Busqueda/' + solicitud + '"><i class="fa fa-bars fa-fw"></i> Detalle </a></li>';
                        boton_2 = boton_2 + '<li><a href="../Solicitudes/CambiaEstado/' + solicitud + '"><i class="fa fa-ban fa-fw"></i> Cambiar Estado </a></li>';
                        boton_2 = boton_2 + '</ul>';
                        return boton_2;
                    } else if (estado_sol == 2) {
                        var boton_2 = '<div class="dropdown"><a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-success" data-target="#" href="#"><i class="fa fa-user fa-fw"></i> Acción <span class="caret"></span></a>'
                        boton_2 = boton_2 + '<ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">';
                        boton_2 = boton_2 + '<li><a href="../Solicitudes/Busqueda/' + solicitud + '"><i class="fa fa-bars fa-fw"></i> Detalle </a></li>';
                        boton_2 = boton_2 + '</ul>';
                        return boton_2;
                            
                    }
                }
            }
        ],
        "bDestroy": true
    });


    // LISTADO POR ETAPAS

    
    var perfil = $("#perfil").val();


    var etapa = $.getURLParam("etapa");
    $('#ListOpeEtp').DataTable({
        order: [[3, "desc"]],
        dom: 'Bfrtip',
        buttons: [
            'csv', 'print'
        ],

        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "LoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        },
        "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "ajax": {
            "url": URLactual + "Listados/ListSolicitudesEtapa?etapa=" + etapa,
            "dataSrc": "",
            "paging": true,
            "searching": true
        },
        "columns": [
            { "data": "Tip_Ope_Pgm" },
            { "data": "Fecha_Curse" },
            { "data": "Rut_Empresa" },
            { "data": "Razon_Social" },
            { "data": "Numero_IFI" },
            { "data": "Ejecutivo" },
            { "data": "Oficina" },

            {
                "data": null,
                "mRender": function (row) {
                     if (etapa != 1) {
                        var N_IFI = row.Numero_IFI
                        return '<button type="button" id="myButton" data-toggle="modal" data-target="#abre_documento" class="btn btn-primary pruebabtn" data-idifi=' + N_IFI + '> Anexos </button>';
                    } else {
                        return '<button type="button" id="myButton" class="btn btn-danger"> Sin Generar </button>';
                    }
                }
                },
                {
                    "data": null,
                    "mRender": function (row) {
                        var solicitud = row.Sol_Id
                        var N_IFI = row.Numero_IFI
                        var solu = "";
                        if (perfil == "2" && etapa == 1) {
                            var boton_2 = '<div class="dropdown"><a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-success" data-target="#" href="#"><i class="fa fa-user fa-fw"></i> Acción <span class="caret"></span></a>'
                            boton_2 = boton_2 + '<ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">';
                            boton_2 = boton_2 + '<li><a href="../Solicitudes/AbreSolicitud/' + solicitud + '"><i class="fa fa-pencil"></i> Editar </a></li>';
                            boton_2 = boton_2 + '<li><a href="../Solicitudes/Busqueda/' + solicitud + '"><i class="fa fa-bars fa-fw"></i> Detalle </a></li>';
                            boton_2 = boton_2 + '<li><a href="../Solicitudes/CambiaEstado/' + solicitud + '"><i class="fa fa-ban fa-fw"></i> Cambiar Estado </a></li>';
                            boton_2 = boton_2 + '</ul>';
                            return boton_2;
                        } else {
                            if (perfil == "2" && etapa != 1 ) {
                                var boton_2 = '<div class="dropdown"><a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-success" data-target="#" href="#"><i class="fa fa-user fa-fw"></i> Acción <span class="caret"></span></a>'
                                boton_2 = boton_2 + '<ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">';
                                boton_2 = boton_2 + '<li><a href="../Solicitudes/Busqueda/' + solicitud + '"><i class="fa fa-bars fa-fw"></i> Detalle </a></li>';
                                boton_2 = boton_2 + '</ul>';
                                return boton_2;
                            } else {
                                if (etapa == 1) {
                                    var boton_2 = '<div class="dropdown"><a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-success" data-target="#" href="#"><i class="fa fa-user fa-fw"></i> Acción <span class="caret"></span></a>'
                                    boton_2 = boton_2 + '<ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">';
                                    boton_2 = boton_2 + '<li><a href="../Solicitudes/AbreSolicitud/' + solicitud + '"><i class="fa fa-pencil fa-fw"></i> Editar</a></li>';
                                    boton_2 = boton_2 + '<li><a href="../Solicitudes/Busqueda/' + solicitud + '"><i class="fa fa fa-bars fa-fw"></i> Detalle</a></li>';
                                    boton_2 = boton_2 + '<li><a href="../Solicitudes/CambiaEstado/' + solicitud + '"><i class="fa fa-ban fa-fw"></i> Cambiar Estado</a></li>';
                                    boton_2 = boton_2 + '</ul>';
                                    return boton_2;
                                } else if (etapa == 2) {
                                    var boton_2 = '<div class="dropdown"><a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-success" data-target="#" href="#"><i class="fa fa-user fa-fw"></i> Acción <span class="caret"></span></a>'
                                    boton_2 = boton_2 + '<ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">';
                                    boton_2 = boton_2 + '<li><a href="../Solicitudes/Edit/' + solicitud + '"><i class="fa fa-pencil"></i> Editar </a></li>';
                                    boton_2 = boton_2 + '<li><a href="../Solicitudes/Busqueda/' + solicitud + '"><i class="fa fa-bars fa-fw"></i> Detalle </a></li>';
                                    boton_2 = boton_2 + '<li><a href="../Solicitudes/CambiaEstado/' + solicitud + '"><i class="fa fa-ban fa-fw"></i> Cambiar Estado </a></li>';
                                    boton_2 = boton_2 + '</ul>';
                                    return boton_2;

                                } else if (etapa == 3 || etapa == 4) {
                                    var boton_2 = '<div class="dropdown"><a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-success" data-target="#" href="#"><i class="fa fa-user fa-fw"></i> Acción <span class="caret"></span></a>'
                                    boton_2 = boton_2 + '<ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">';
                                    boton_2 = boton_2 + '<li><a href="../Solicitudes/Busqueda/' + solicitud + '"><i class="fa fa-bars fa-fw"></i> Detalle </a></li>';
                                    boton_2 = boton_2 + '</ul>';
                                    return boton_2;
                                }
                            }
                        }
                    }
                }
        ],
        "bDestroy": true
    });



    $(".dropdown-menu li a").click(function () {
        $(this).parents(".dropdown").find('.btn').html($(this).text() + ' <span class="caret"></span>');
        $(this).parents(".dropdown").find('.btn').val($(this).data('value'));
    });


    $('#rutcliente_f').Rut({
        digito_verificador: '#dvrutcliente_f',
        on_error: function () { alert('Rut incorrecto'); }
    });


    $("#b_rutcliente").click(function () {
        $("#f_numifi").hide("slow");
        $("#f_fechas").hide("slow");
        $("#f_estados").hide("slow");
        $("#f_rutcliente").show("slow");
    })


    $("#b_numifi").click(function () {
        $("#f_rutcliente").hide("slow");
        $("#f_fechas").hide("slow");
        $("#f_estados").hide("slow");
        $("#f_numifi").show("slow");
    })

    $("#b_fechas").click(function () {
        $("#f_rutcliente").hide("slow");
        $("#f_numifi").hide("slow");
        $("#f_estados").hide("slow");
        $("#f_fechas").show("slow");
    })


    $("#b_estados").click(function () {
        $("#f_rutcliente").hide("slow");
        $("#f_numifi").hide("slow");
        $("#f_fechas").hide("slow");
        $("#f_estados").show("slow");
    })



    function generaListadoBusqueda(tipoConsulta) {
       
        $('#ListSolictudes').DataTable({
            order: [[3, "desc"]],
            dom: 'Bfrtip',
            buttons: [
                'csv', 'print'
            ],

            "language": {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "LoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                }
            },
            "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            "ajax": {
                url: URLactual + tipoConsulta,
                "dataSrc": "",
                "paging": true,
                "searching": true
            },
            "columns": [
                { "data": "Tip_Ope_Pgm" },
                { "data": "Numero_IFI" },
                { "data": "Fecha_Curse" },
                { "data": "Rut_Empresa" },
                { "data": "Razon_Social" },
                { "data": "Ejecutivo" },
                {
                    "data": null,
                    "mRender": function (row) {
                        var estado = $.trim(row.Estado_Solicitud)
                        if (estado == "Activa") {
                            return '<h5><span class="label label-success">ACTIVA</span></h5>';
                        } else {
                            return '<h5><span class="label label-danger">CADUCADA</span></h5';
                        }
                    }
                },

                {
                    "data": null,
                    "mRender": function (row) {
                        var estado = $.trim(row.Estapa_Solicitud)
                        return '<h5><span class="label label-danger">' + row.Estapa_Solicitud.toUpperCase() + '</span></h>';
                    }
                },

                 {
                     "data": null,
                     "mRender": function (row) {
                         var Etapa = $.trim(row.Estapa_Solicitud)
                         if (Etapa != "Simulada") {
                             var N_IFI = row.Numero_IFI
                             return '<button type="button" id="myButton" data-toggle="modal" data-target="#abre_documento" class="btn btn-primary pruebabtn" data-idifi=' + N_IFI + '> Anexos </button>';
                         } else {
                             return '<button type="button" id="myButton" class="btn btn-danger"> Sin Anexos </button>';
                         }
                     }
                 },

                {
                    "data": null,
                    "mRender": function (row) {
                        var solicitud = row.Sol_Id;
                        return '<a class="btn btn-success" href="../Solicitudes/Busqueda/' + solicitud + '"> Detalle </a>';
                    }
                }
            ],
            "bDestroy": true
        });

    }


       
    $("#f_botonrutcliente").click(function () {
        rutcliente = $("#rutcliente").val();
        var dv = $("#Sol_Drt_Emp").val();
        var rutsinpunto = $("#rutcliente_f").val();
        var rutdv = $("#dvrutcliente_f").val();
        rutsinpunto2 = rutsinpunto.replace(/[.]/g, "");
        var rut = rutsinpunto2 + "-" + rutdv;
        var rutaControlador = "/Consultas/ListSolicitudesRutCli?rut=" + rutsinpunto2;

        if (rutsinpunto != '') {

            generaListadoBusqueda(rutaControlador);
            
        } else {

            alert("ERROR : DEBE INGRESA RUT A CONSULTAR");
        }

    });


    $("#f_botonnumifi").click(function () {
        numifi = $("#f-numifi").val();
        var rutaControlador = "/Consultas/ListSolicitudesIfi?ifi=" + numifi;

        if (numifi != "") {
            generaListadoBusqueda(rutaControlador);
        } else {
            alert("ERROR : DEBE INGRESA NÚMERO IFI A CONSULTAR");
        }
    });

    
    $("#f_botonfecha").click(function () {

        fecha1 = $("#datepicker-busqueda-inicio").val();
        fecha2 = $("#datepicker-busqueda-fin").val();
        var dateInicio = fecha1.split('-');
        var newdateInicio = dateInicio[2] + '-' + dateInicio[1] + '-' + dateInicio[0].slice(-2);
        var dateFin = fecha2.split('-');
        var newdateFin = dateFin[2] + '-' + dateFin[1] + '-' + dateFin[0].slice(-2);
        var rutaControlador = "/Consultas/ListSolicitudesFecha?fecha1=" + newdateInicio + "&fecha2=" + newdateFin;

        if (fecha1 != "") {
            generaListadoBusqueda(rutaControlador);
        } else {
            alert("ERROR : DEBE INGRESAR RANGO DE FECHA A CONUSLTAR");
        }
    });


    $("#f_botonestado").click(function () {

        Estado = $("#Est_Sol_Id").val();
        var rutaControlador = "/Consultas/ListSolicitudesEstado?Estado=" + Estado;
        if (Estado != "") {
            generaListadoBusqueda(rutaControlador);
        } else {
            alert("Error 015F01 : ERROR AL TRAER INFORMACIÓN");
        }
    });


    $('#ListSolictudes').on('click', '.pruebabtn', function (e) {
        e.preventDefault();
        var idifi = ($(this).attr("data-idifi"));
        $.ajax({
            url: URLactual + "/GeneraArchivos/ver_documentos?id_ifi=" + idifi,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#vistaanexos").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });




    $('#ListOpeEtp').on('click', '.pruebabtn', function (e) {
        e.preventDefault();
        var idifi = ($(this).attr("data-idifi"));
        $.ajax({
            url: URLactual + "/GeneraArchivos/ver_documentos?id_ifi=" + idifi,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#vistaanexos").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });


    $("input.sum-mnt").change(function () {
        var suma = 0;
        var Sol_Mnt_Inv = $("input[id='Sol_Mnt_Inv']").val();
        var Sol_Mnt_Inv2 = Sol_Mnt_Inv.replace('.', '');

        suma = Number($("input[id='Sol_Mnt_Cpl_Tbj']").val());
        suma += Number($("input[id='Sol_Mnt_Inv']").val());
        suma += Number($("input[id='Sol_Mnt_Cp_Rfn']").val());
        $("#Sol_Mnt_Crd").val(suma);
       
    })


    $('#CheckCapital').change(function () {
        if ($(this).prop('checked')) {
            $(".mnt_cp_tbj_box").show();
        }
        else {
            $(".mnt_cp_tbj_box").hide();
            $("#Sol_Mnt_Cpl_Tbj").val('').change();

        }
    });


    $("#CheckInversion").change(function () {
        if ($(this).prop('checked')) {
            $(".mnt_inv_box").show();
        }else{
            $(".mnt_inv_box").hide();
            $("#Sol_Mnt_Inv").val('').change();
        }
    })


    $('#CheckRefinanciamiento').change(function () {
        if ($(this).prop('checked')) {
            $(".mnt_cp_rfn_box").show();
        } else {
            $(".mnt_cp_rfn_box").hide();
            $("#Sol_Mnt_Cp_Rfn").val('').change();
        }
    });


    $("#Comision").change(function () {
        $("#Tip_Ope_Id").empty();
        $(".capital").hide();
        $(".inversion").hide();
        $(".refinanciamiento").hide();
        $("#Sol_Mnt_Cpl_Tbj").val("0").change();
        $("#Sol_Mnt_Inv").val("0").change();
        $("#Sol_Mnt_Cp_Rfn").val("0").change();

        if ($("#Comision").val() != "") {
            var id_comision = $("#Comision").val();
            var NomPrograma = "";
            var tipoPrograma = $("#PROGRAMA").val();
            
            if (tipoPrograma == "Fogain") {
                CodPrograma = 1;
            } else {
                CodPrograma = 2;
            }

            $.ajax({
                url: URLactual + "/Proyectos/ListOperacion?TipOpeCms=" + id_comision + "&tipoPrograma=" + CodPrograma,
                type: "POST",
                dataType: "json"
            })
           .success(function (ListaOperaciones) {

               $("#Tip_Ope_Id").append($('<option selected></option>').val("").html("Seleccione Tipo Operación"));

               $.each(ListaOperaciones, function (Tip_Ope_Id, option) {
                   $("#Tip_Ope_Id").append($('<option></option>').val(option.Tip_Ope_Id).html(option.Tip_Ope_Des));
               })
               $("#Tip_Ope_Id").prop("disabled", false);
           })
           .error(function (xrh, status) {
               alert(status);
               alert("Error al traer Tipo de Operaciones!!!");
           });
        }
        else {
            $("#Tip_Ope_Id").empty();
        }
    });


    
    $("#Tip_Ope_Id").change(function () {
        var Id_TipOpeId = $("#Tip_Ope_Id").val();

        if ($("#Tip_Ope_Id").val() != "") {
            var Id_TipOpeId = $("#Tip_Ope_Id").val();

            $.ajax({
                url: URLactual + "/Proyectos/ListTipMonto?TipOpeId=" + Id_TipOpeId,
                type: "POST",
                dataType: "json"
            })
           .success(function (TipoMonto) {

               $(".capital").hide();
               $(".inversion").hide();
               $(".refinanciamiento").hide();

               if (TipoMonto.Tip_Ope_Cpl_Tbj == 1) {
                   $(".capital").show();
                   $("#Sol_Mnt_Cpl_Tbj").val("0").change();
                   $("#Sol_Mnt_Inv").val("0").change();
                   $("#Sol_Mnt_Cp_Rfn").val("0").change();
               }
               if (TipoMonto.Tip_Ope_Inv == 1) {
                   $(".inversion").show();
                   $("#Sol_Mnt_Cpl_Tbj").val("0").change();
                   $("#Sol_Mnt_Inv").val("0").change();
                   $("#Sol_Mnt_Cp_Rfn").val("0").change();
               }

               if (TipoMonto.Tip_Ope_Rfn == 1) {
                   $(".refinanciamiento").show();
                   $("#Sol_Mnt_Cpl_Tbj").val("0").change();
                   $("#Sol_Mnt_Inv").val("0").change();
                   $("#Sol_Mnt_Cp_Rfn").val("0").change();
               } else {
                   $("#Sol_Mnt_Cpl_Tbj").val('0').change();
                   $("#Sol_Mnt_Inv").val('0').change();
                   $("#Sol_Mnt_Cp_Rfn").val('0').change();
               }

           })
           .error(function (xrh, status) {
               alert(status);
               alert("Error al traer Tipo de Operaciones!!!");
           });
        }
        else {
            alert("ERROR REVISE REGLA");
        }
    });



    $("#Sol_Mnt_Inv").click(function () {
        if ($("#Sol_Mnt_Inv").val() <= 0) {
            $("#Sol_Mnt_Inv").val("");
        }
    })

    $("#Sol_Mnt_Cp_Rfn").click(function () {
        if ($("#Sol_Mnt_Cp_Rfn").val() <= 0) {
            $("#Sol_Mnt_Cp_Rfn").val("");
        }
    })

    $("#Sol_Mnt_Cpl_Tbj").click(function () {
        if ($("#Sol_Mnt_Cpl_Tbj").val() <= 0) {
            $("#Sol_Mnt_Cpl_Tbj").val("");
        }
    })
    


    ///

    $('#ListOpeDb').DataTable({
        order: [[0, "asc"]],
        "paging": false,
        "searching": false,
        "info":false,
        "ajax": {
            "url": URLactual + "/Home/Dashboard",
            "dataSrc": "",
        },
        "columns": [
            { "data": "ID" },
            { "data": "Desde" },
            { "data": "Hasta" },
            { "data": "Simulada" },
            { "data": "Solicitada" },
            { "data": "Cursada" },
            { "data": "Pendiente_Carga_en_Corfo" }
        ],
        "bDestroy": true
    });

    ///


    $("#Form_sol").change(function () {

        $("#Sol_Mnt_Cms").val("");
        $("#Sol_Por_Cbt_Dis").val("");
        $("#Sol_Mnt_Cbt_Dis").val("");
    })

    
    $("#Fecha_Venc").blur(function () {

        $("#Sol_Mnt_Cms").val("");
        $("#Sol_Por_Cbt_Dis").val("");
        $("#Sol_Mnt_Cbt_Dis").val("");

    })

    $("#Fecha_Curse").blur(function () {

        $("#Sol_Mnt_Cms").val("");
        $("#Sol_Por_Cbt_Dis").val("");
        $("#Sol_Mnt_Cbt_Dis").val("");

    })

    /* funcionalidad para seleccionar tipo de programa */




    

    /// CONTADOR CARACTERES DESCRIPCION DE PROYECTO


    init_contadorTa("des_pry", "contador", 200);

    function init_contadorTa(idtextarea, idcontador, max) {
        $("#" + idtextarea).keyup(function () {
            updateContadorTa(idtextarea, idcontador, max);
        });

        $("#" + idtextarea).change(function () {
            updateContadorTa(idtextarea, idcontador, max);
        });

    }

    function updateContadorTa(idtextarea, idcontador, max) {
        var contador = $("#" + idcontador);
        var ta = $("#" + idtextarea);
        contador.html("0/" + max);

        contador.html(ta.val().length + "/" + max);
        if (parseInt(ta.val().length) > max) {
            ta.val(ta.val().substring(0, max - 1));
            contador.html(max + "/" + max);
        }

    }

})
