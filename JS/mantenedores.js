$(document).ready(function () {

    pathArray = location.href.split('/');
    protocol = pathArray[0];
    host = pathArray[2] + "/" + pathArray[3] + "/";
    URLactual = protocol + '//' + host;


    var table = $('#list_catastrofes').DataTable({
        initComplete: function () {
            this.api().columns().every(function () {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });
                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        },

        order: [[0, "asc"]],
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
            "url": URLactual + "Mantenedores/ListaCatastrofes",
            "dataSrc": "",
            "paging": true,
            "searching": true
        },
        "columns": [
            { "data": "Ctf_Des" },
            { "data": "Rgn_Nom" },
            { "data": "Com_Nom" },
            {
                "data": null,
                "mRender": function (row) {
                    return '<button type="button" id="" class="btn btn-warning EditCTF" data-CodCtf="' + row.Ctf_Cod + '" data-DesCtf="' + row.Ctf_Des + '" data-EstCtf="' + row.Ctf_Est + '" data-toggle="modal" data-target="#EditaCtfLoc"><i class="fa fa-pencil"></i></button> ';
                }
            }
        ],
        "bDestroy": true
    });


    $("#M_Regiones").change(function () {
        if ($("#M_Regiones").val() != "") {
            var id_regiones = $("#M_Regiones").val();

            $("#M_Comuna").empty();

            $.ajax({
                url: URLactual + "/Mantenedores/ListaComunas?&Reg_id=" + id_regiones ,
                type: "POST",
                dataType: "json"
            })
           .success(function (SalListComuna) {
               $("#M_Comuna").append($('<option selected></option>').val("").html("Seleccione Localización"));

               $.each(SalListComuna, function (Com_id, option) {
                   $("#M_Comuna").append($('<option></option>').val(option.Com_Cod).html(option.Nom));
               })
               $("#M_Comuna").prop("disabled", false);
           })
           .error(function (xrh, status) {
               alert(status);
               alert("Error al traer las Localizaciones!!!");
           });
        }
        else {
            $("#M_Comuna").empty();
        }
    });


    $("#addCatastrofe").click(function () {

        var idtitulo = "AGREGA CATASTROFE";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/GSC_CTF/create?f=1",
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });


    $("#addCatastrofeForm").click(function () {
        var idtitulo = "AGREGA CATASTROFE";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/GSC_CTF/create?f=2",
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });


     $('#lista_catastrofe').on('click', '.DeleteCTF', function (e) {

          idctf = ($(this).attr("data-idctf"));

        var idtitulo = "EDITAR CATASTROFE";
        $.ajax({
            url: URLactual + "/GSC_CTF/Delete/" + idctf,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo3").html(idtitulo);
            $("#vista3").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });



    // LISTA CATASTROFEPOR ID


      $("#M_Catastrofe").change(function () {
          var catastrofe = $("#M_Catastrofe").val();

          var lcatastrofe = $('#add_catastrofes').DataTable({
              order: [[1, "desc"]],
              dom: 'Blfrtip',
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
              "aLengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
              "ajax": {
                  url: URLactual + "/Mantenedores/ListaTipoCTFId?Id_Ctf=" + catastrofe,
                  "dataSrc": "",
                  "paging": true,
                  "searching": true
              },
              "columns": [
                  { "data": "Ctf_Des" },
                  { "data": "Rgn_Des" },
                  { "data": "Com_Nom" },
                  {
                      "data": null,
                      "mRender": function (row) {
                          var total_reg = row.Total_Reg;
                          if (total_reg == "0") {
                              return '<button type="button" id="" class="btn btn-danger EditaCTF" data-CodCtf="' + row.Rel_Ctf_Com_Id + '" data-totalctf= "' + row.Total_Reg + '" data-toggle="modal" data-target="#EditaCtfLoc"><i class="fa fa-times"></i></button> ';
                          } else {
                              return '<button type="button" id="" class="btn btn-success EditaCTF" data-CodCtf="' + row.Rel_Ctf_Com_Id + '" data-totalctf= "' + row.Total_Reg + '" data-toggle="modal" data-target="#EditaCtfLoc"><i class="fa fa-pencil"></i></button> ';
                          }
                      }
                  }
              ],
              "bDestroy": true
          });


          
      })



    var t = $("#add_catastrofes").DataTable({
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
    });





    $('#add_catastrofes').on('click', '.EditaCTF', function (e) {

        Cod_Ctf = ($(this).attr("data-codctf"));
        Total_Ctf = ($(this).attr("data-totalctf"));

        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/EditaCatastrofe?Cod_Ctf=" + Cod_Ctf + "&Cantidad_Ctf=" + Total_Ctf,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista2").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });






    $("#agrega_catastrofe").on('click', function () {
       var catastrofe =  $("#M_Catastrofe").val();
       var region =  $("#M_Regiones").val();
       var comuna = $("#M_Comuna").val();

            $.ajax({
                url: URLactual + "Mantenedores/AgregaCatastrofe_2?Rgn_Id=" + region + "&Com_Cod=" + comuna + "&Ctf_Cod=" + catastrofe,
                type: "POST",
                dataType: "json"
            })
             .success(function (SalValorMoneda) {
                 if ($("#M_Comuna").val() == "") {
                     alert("Debe seleccionar todos los campos");
                 } else {
                     $('#add_catastrofes').DataTable({
                         order: [[1, "desc"]],
                         dom: 'Blfrtip',
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
                             url: URLactual + "/Mantenedores/ListaTipoCTFId?Id_Ctf=" + catastrofe,
                             "dataSrc": "",
                             "paging": true,
                             "searching": true
                         },
                         "columns": [
                             { "data": "Ctf_Des" },
                             { "data": "Rgn_Des" },
                             { "data": "Com_Nom" },
                             {
                                 "data": null,
                                 "mRender": function (row) {
                                     var total_reg = row.Total_Reg;
                                     if (total_reg == "0") {
                                         return '<button type="button" id="" class="btn btn-danger EditaCTF" data-CodCtf="' + row.Rel_Ctf_Com_Id + '" data-totalctf= "' + row.Total_Reg + '" data-toggle="modal" data-target="#EditaCtfLoc"><i class="fa fa-times"></i></button> ';
                                     } else {
                                         return '<button type="button" id="" class="btn btn-success EditaCTF" data-CodCtf="' + row.Rel_Ctf_Com_Id + '" data-totalctf= "' + row.Total_Reg + '" data-toggle="modal" data-target="#EditaCtfLoc"><i class="fa fa-pencil"></i></button> ';
                                     }
                                 }
                             }
                         ],
                         "bDestroy": true
                     });



                 }
             })
            .error(function (xrh, status) {
                alert("Error al ingresar los datos");
            })
    })



    // BOTONES

    $('.btn-toggle').click(function () {
        $(this).find('.btn').toggleClass('active');

        if ($(this).find('.btn-primary').size() > 0) {
            $(this).find('.btn').toggleClass('btn-primary');
        }
        $(this).find('.btn').toggleClass('btn-default');
    });

    $("#ctfactivo").click(function () {
        alert("activo");
    })




    // INSERTA TIPO DE CATASTROFE



    var lcatastrofe = $('#lista_catastrofe').DataTable({
        order: [[1, "desc"]],
        dom: 'Blfrtip',
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
            url: URLactual + "/Mantenedores/ListaTipoCTF",
            "dataSrc": "",
            "paging": true,
            "searching": true
        },
        "columns": [
            { "data": "Ctf_Cod" },
            { "data": "Ctf_Des" },
            { "data": "Ctf_Rsl" },
            {
                "data": null,
                "mRender": function (row) {
                    var estado = row.Ctf_Est
                    if (estado == 1) {
                        return '<h5><span class="label label-success">ACTIVO</span></h5>';
                    } else {
                        return '<h5><span class="label label-danger">INACTIVO</span></h5';
                    }
                }
            },

            {
                "data": null,
                "mRender": function (row) {
                    return '<button type="button" id="" class="btn btn-warning EditCTF" data-CodCtf="' + row.Ctf_Cod + '" data-DesCtf="' + row.Ctf_Des + '" data-EstCtf="' + row.Ctf_Est + '" data-RslCtf="' + row.Ctf_Rsl + '" data-toggle="modal" data-target="#EditCatastrofe"><i class="fa fa-pencil"></i></button> ';
                }
            },

        ],
        "bDestroy": true
    });


    $("#AddCtfLoc").click(function () {
        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaTipoCatastrofeLoc",
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });



    $("#AddCtf").click(function () {
        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaTipoCatastrofe",
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });




    $('#lista_catastrofe').on('click', '.EditCTF', function (e) {

        Cod_Ctf = ($(this).attr("data-CodCtf"));
        Des_Ctf = ($(this).attr("data-DesCtf"));
        Est_Ctf = ($(this).attr("data-EstCtf"));
        Est_Rsl = ($(this).attr("data-RslCtf"));
        

        idctf = ($(this).attr("data-idctf"));

        var idtitulo = "EDITAR CATASTROFE";
        $.ajax({
            url: URLactual + "/Mantenedores/InsertaTipoCatastrofe?idCtf=" + Cod_Ctf + "&estadoCtf=" + Est_Ctf,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo2").html(idtitulo);
            $("#vista2").html(data);

            $(".Ctf_Cod").val(Cod_Ctf);
            $(".Ctf_Des").val(Des_Ctf);
            $(".Ctf_Est").val(Est_Ctf);
            $(".Ctf_Rsl").val(Est_Rsl);

        })
        .error(function (xrh, status) {
            alert(status);
        });
    });



    //FIN TIPO DE CATASTROFE








    // CLASIFICACION DE RIESGO


    var List_Cla_Rie = $('#List_Cla_Rie').DataTable({
        order: [[1, "desc"]],
        dom: 'Blfrtip',
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
            url: URLactual + "/Mantenedores/ListaClaRie",
            "dataSrc": "",
            "paging": true,
            "searching": true
        },
        "columns": [
            { "data": "Cla_Rie_Id" },
            { "data": "Cla_Rie_Tip" },
            { "data": "Cla_Rie_Por_Pvs" },
            {
                "data": null,
                "mRender": function (row) {
                    var estado = row.Cla_Rie_Est
                    if (estado == 1) {
                        return '<h5><span class="label label-success">ACTIVO</span></h5>';
                    } else {
                        return '<h5><span class="label label-danger">INACTIVO</span></h5';
                    }
                }
            },
            {
                "data": null,
                "mRender": function (row) {
                    return '<button type="button" id="" class="btn btn-warning EditClaRie" data-IdClaRie="' + row.Cla_Rie_Id + '"data-TipClaRie="' + row.Cla_Rie_Tip + '" data-PorClaRie="' + row.Cla_Rie_Por_Pvs + '" data-EstClaRie="' + row.Cla_Rie_Est + '" data-toggle="modal" data-target="#EditaClaRie"><i class="fa fa-pencil"></i></button> ';
                }
            },

        ],
        "bDestroy": true
    });


    


    $("#AddClaRie").click(function () {
        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaClasificacionRiesgo",
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });


    

    $('#List_Cla_Rie').on('click', '.EditClaRie', function (e) {


        Tip_ClaRie = ($(this).attr("data-TipClaRie"));
        Por_ClaRie = ($(this).attr("data-PorClaRie"));
        Id_ClaRie = ($(this).attr("data-IdClaRie"));
        Est_ClaRie = ($(this).attr("data-EstClaRie"));


        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaClasificacionRiesgo?idClaRie=" + Id_ClaRie + "&estadoClaRie=" + Est_ClaRie,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista2").html(data);
            
            $(".Cla_Rie_Id").val(Id_ClaRie);
            $(".Cla_Rie_Por_Pvs").val(Por_ClaRie);
            $(".Cla_Rie_Tip").val(Tip_ClaRie);
            $(".Cla_Rie_Est").val(Est_ClaRie);

        })
        .error(function (xrh, status) {
            alert(status);
        });
    });


    // FIN CLASIFICACION DE RIESGO



    // Lista Garantia


    var List_Gar = $('#List_Gar').DataTable({
        order: [[1, "desc"]],
        dom: 'Blfrtip',
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
            url: URLactual + "/Mantenedores/ListaGar",
            "dataSrc": "",
            "paging": true,
            "searching": true
        },
        "columns": [
            { "data": "Gar_Adc_Cod" },
            { "data": "Gar_Adc_Des" },
            {
                "data": null,
                "mRender": function (row) {
                    var estado = row.Gar_Adc_Est
                    if (estado == 1) {
                        return '<h5><span class="label label-success">ACTIVO</span></h5>';
                    } else {
                        return '<h5><span class="label label-danger">INACTIVO</span></h5';
                    }
                }
            },
            {
                "data": null,
                "mRender": function (row) {
                    return '<button type="button" id="" class="btn btn-warning EditGarantia" data-IdGarantia="' + row.Gar_Adc_Id + '"data-CodGarantia="' + row.Gar_Adc_Cod + '" data-DesGarantia="' + row.Gar_Adc_Des + '" data-EstGarantia="' + row.Gar_Adc_Est + '" data-toggle="modal" data-target="#EditaGarantia"><i class="fa fa-pencil"></i></button> ';
                }
            },

        ],
        "bDestroy": true
    });



    $("#AddGar").click(function () {
        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";

        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaGarantias",
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });



    $('#List_Gar').on('click', '.EditGarantia', function (e) {

        Garantia_Id = ($(this).attr("data-IdGarantia"));
        Garantia_Cod = ($(this).attr("data-CodGarantia"));
        Garantia_Des = ($(this).attr("data-DesGarantia"));
        Garantia_Est = ($(this).attr("data-EstGarantia"));


        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaGarantias?idGarantia=" + Garantia_Id + "&estadoGarantia=" + Garantia_Est,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista2").html(data);


            $(".Gar_Adc_Id").val(Garantia_Id);
            $(".Gar_Adc_Cod").val(Garantia_Cod);
            $(".Gar_Adc_Des").val(Garantia_Des);
            $(".Gar_Adc_Est").val(Garantia_Est);

        })
        .error(function (xrh, status) {
            alert(status);
        });
    });




    // Lista Sector Economico


    var List_Sec_Eco = $('#List_Sec_Eco').DataTable({
        order: [[1, "desc"]],
        dom: 'Blfrtip',
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
            url: URLactual + "/Mantenedores/ListaSecEco",
            "dataSrc": "",
            "paging": true,
            "searching": true
        },
        "columns": [
            { "data": "sec_eco_cod" },
            { "data": "sec_des" },
            {
                "data": null,
                "mRender": function (row) {
                    var estado = row.Sce_Est
                    if (estado == 1) {
                        return '<h5><span class="label label-success">ACTIVO</span></h5>';
                    } else {
                        return '<h5><span class="label label-danger">INACTIVO</span></h5';
                    }
                }
            },
            {
                "data": null,
                "mRender": function (row) {
                    return '<button type="button" id="EditSeg" class="btn btn-warning EditSecEco" data-IdSecEco="' + row.Sce_Id + '"data-CodSecEco="' + row.sec_eco_cod + '" data-DesSecEco="' + row.sec_des + '" data-EstSecEco="' + row.Sce_Est + '" data-toggle="modal" data-target="#EditSecEco"><i class="fa fa-pencil"></i></button> ';
                }
            },

        ],
        "bDestroy": true
    });


    $("#AddSecEco").click(function () {
        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaSectorEconomico",
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });


    $('#List_Sec_Eco').on('click', '.EditSecEco', function (e) {

        Sce_Id = ($(this).attr("data-IdSecEco"));
        Sce_Cod = ($(this).attr("data-CodSecEco"));
        Sce_Des = ($(this).attr("data-DesSecEco"));
        Sce_Est = ($(this).attr("data-EstSecEco"));


        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaSectorEconomico?idSecEco=" + Sce_Id + "&estadoSecEco=" + Sce_Est,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista2").html(data);

            $(".Sce_Id").val(Sce_Id);
            $(".Sce_Cod").val(Sce_Cod);
            $(".Sce_Des").val(Sce_Des);
            $(".Sce_Est").val(Sce_Est);

        })
        .error(function (xrh, status) {
            alert(status);
        });
    });



    // Lista Tipo Gracia


    var List_Tip_Gra = $('#List_Tip_Gra').DataTable({
        order: [[1, "desc"]],
        dom: 'Blfrtip',
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
            url: URLactual + "/Mantenedores/ListaTipGra",
            "dataSrc": "",
            "paging": true,
            "searching": true
        },
        "columns": [
            { "data": "Tip_Grc_Id" },
            { "data": "Tip_Grc_Des" },
            {
                "data": null,
                "mRender": function (row) {
                    var estado = row.Tip_Grc_Est
                    if (estado == 1) {
                        return '<h5><span class="label label-success">ACTIVO</span></h5>';
                    } else {
                        return '<h5><span class="label label-danger">INACTIVO</span></h5';
                    }
                }
            },
            {
                "data": null,
                "mRender": function (row) {
                    return '<button type="button" id="" class="btn btn-warning EditaTipGra" data-IdTipGra="' + row.Tip_Grc_Id + '"data-Tip_Grc_Id="' + row.Tip_Grc_Id + '" data-DesTipGra="' + row.Tip_Grc_Des + '" data-EstTipGra="' + row.Tip_Grc_Est + '" data-toggle="modal" data-target="#EditTipGra"><i class="fa fa-pencil"></i></button> ';
                }
            },

        ],
        "bDestroy": true
    });

    
    $("#AddTipGra").click(function () {
        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaTipoGracia",
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });




    $('#List_Tip_Gra').on('click', '.EditaTipGra', function (e) {

        Cod_TipGra = ($(this).attr("data-CodTipGra"));
        Des_TipGra = ($(this).attr("data-DesTipGra"));
        Id_TipGra = ($(this).attr("data-IdTipGra"));
        Est_TipGra = ($(this).attr("data-EstTipGra"));



        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaTipoGracia?idTipoGra=" + Id_TipGra + "&estadoTipGra=" + Est_TipGra,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista2").html(data);

            
            $(".Tip_Grc_Id").val(Id_TipGra);
            $(".Tip_Grc_Des").val(Des_TipGra);
            $(".Tip_Grc_Est").val(Est_TipGra);

        })
        .error(function (xrh, status) {
            alert(status);
        });
    });





    // Lista Seguros


    var List_Seg = $('#List_Seg').DataTable({
        order: [[1, "desc"]],
        dom: 'Blfrtip',
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
            url: URLactual + "/Mantenedores/ListaSeg",
            "dataSrc": "",
            "paging": true,
            "searching": true
        },
        "columns": [
            { "data": "Seg_Ref_Cod" },
            { "data": "Seg_Ref_Des" },
            {
                "data": null,
                "mRender": function (row) {
                    var estado = row.Seg_Ref_Est
                    if (estado == 1) {
                        return '<h5><span class="label label-success">ACTIVO</span></h5>';
                    } else {
                        return '<h5><span class="label label-danger">INACTIVO</span></h5';
                    }
                }
            },
            {
                "data": null,
                "mRender": function (row) {
                    return '<button type="button" id="EditSeg" class="btn btn-warning EditaSeg" data-Idseg="' + row.Seg_Ref_Id + '"data-CodSeg="' + row.Seg_Ref_Cod + '" data-DesSeg="' + row.Seg_Ref_Des +'" data-EstSeg="' + row.Seg_Ref_Est + '" data-toggle="modal" data-target="#EditaSeg"><i class="fa fa-pencil"></i></button> ';
                }
            },

        ],
        "bDestroy": true
    });


    $("#AddSeg").click(function () {
        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaSeguro",
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista").html(data);
        })
        .error(function (xrh, status) {
            alert(status);
        });
    });


    
    $('#List_Seg').on('click', '.EditaSeg', function (e) {

        Cod_Seg = ($(this).attr("data-CodSeg"));
        Des_Seg = ($(this).attr("data-DesSeg"));
        Id_Seg = ($(this).attr("data-IdSeg"));
        Est_Seg = ($(this).attr("data-EstSeg"));


        var idtitulo = "AGREGA CLASIFICACIÓN DE RIESGO";
        $('#agregaoperacion').modal({
            backdrop: 'static',
            keyboard: false
        })

        $.ajax({
            url: URLactual + "/Mantenedores/InsertaSeguro?idseguro=" + Id_Seg + "&estadoseguro=" + Est_Seg,
            type: "GET",
            dataType: "html"
        })
        .success(function (data) {
            $("#titulo").html(idtitulo);
            $("#vista2").html(data);

            $(".Seg_Ref_Id").val(Id_Seg);
            $(".Seg_Ref_Cod").val(Cod_Seg);
            $(".Seg_Ref_Des").val(Des_Seg);
            $(".Seg_Ref_Est").val(Est_Seg);

        })
        .error(function (xrh, status) {
            alert(status);
        });
    });


})