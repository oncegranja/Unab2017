$(document).ready(function () {
    
    pathArray = location.href.split('/');
    protocol = pathArray[0];
    host = pathArray[2] + "/" + pathArray[3] + "/";
    URLactual = protocol + '//' + host;

    $("#calcular_comision").click(function () {

        $('.overlay').css("display","block");

        var Num_Sol_Ifi = Math.floor(Math.random() * 1000000);
        var Tip_Ope_Cod = $("#Tip_Ope_Id").val();
        var Tas_Anl = $("#Sol_Int_Anl").val();
        var Num_Cuo = $("#Sol_Num_Cuo").val();
        var Mnt = $("#Sol_Mnt_Crd").val();
        var Val_Dol = $("#Sol_Val_Dol").val();
        var Val_UF = $("#Sol_Val_UF").val();
        var Mon = $("#Tip_Mon_Id").val();
        var Fec_Cse = $("#Fecha_Curse").val();
        var Fec_Prm_Ven = $("#Fecha_Venc").val();
        var Frc_Pag = $("#Fre_Pag_Id").val();
        var Nvl_Vnt = $("#Sol_Vnt_Emp").val();
        var Mnt_Cbt_Ocu = $("#Sol_Mnt_Cbt_Utz").val();
        var Catastrofe = $("#Catastrofe").val();


        if (Num_Sol_Ifi == '' || Tip_Ope_Cod == '' || Tas_Anl == '' || Num_Cuo == '' || Mnt == '' || Val_Dol == '' || Val_UF == '' || Mon == '' || Fec_Cse == '' || Fec_Prm_Ven == '' || Frc_Pag == '' || Nvl_Vnt == '' || Mnt_Cbt_Ocu == '' || Catastrofe == '') {
            $('.overlay').fadeOut(2000, function () {
                $('.overlay').css("display", "none");
            });
            alert("VERIFIQUE QUE TODOS LOS CAMPOS PARA EL CÁLCULO DE COMISIÓN ESTEN COMPLETOS");
        } else {


            if (Catastrofe == "1") {
                var Por_Fjo_Cms = $("#Por_Fjo_Cms").val();
            } else {
                var Por_Fjo_Cms = $("#Por_Fjo_Cms_Ctf").val();
            }

            $.ajax({
                url: URLactual + "Calculos/Comision?Num_Sol_Ifi=" + Num_Sol_Ifi + "&Tip_Ope_Cod=" + Tip_Ope_Cod + "&Tas_Anl=" + Tas_Anl + "&Num_Cuo=" + Num_Cuo + "&Mnt=" + Mnt + "&Val_Dol=" + Val_Dol + "&Val_UF=" + Val_UF + "&Mon=" + Mon + "&Fec_Cse1=" + Fec_Cse + "&Fec_Prm_Ven1=" + Fec_Prm_Ven + "&Frc_Pag=" + Frc_Pag + "&Nvl_Vnt=" + Nvl_Vnt + "&Mnt_Cbt_Ocu=" + Mnt_Cbt_Ocu + "&Por_Fjo_Cms=" + Por_Fjo_Cms,
                type: "POST",
                dataType: "json"
            })
            .success(function (ResultadoComision) {

                setTimeout(function () {
                }, 3000);

                mnt_cms = ResultadoComision.Monto_comision_Pesos;


                $("#Sol_Mnt_Cms").val(mnt_cms);
                $("#Sol_Por_Cbt_Dis").val(ResultadoComision.Porcentaje_Cobertura_Menor_Valor_UF);
                $("#Sol_Mnt_Cbt_Dis").val(ResultadoComision.Monto_Cobertura_Menor_Valor_UF);
                $("#numero_ifi").val(Num_Sol_Ifi);

                $('.overlay').fadeOut(2000, function () {
                    $('.overlay').css("display", "none");
                });

            })
               .error(function (xrh, status) {
                   alert("Error al :  CALCULAR COMISION !!! REVISE SI ESTAN TODOS LOS CAMPOS COMPLETOS");
                   $("#Sol_Mnt_Cms").val('');
                   $("#Sol_Por_Cbt_Dis").val('');
                   $("#Sol_Mnt_Cbt_Dis").val('');

                   $('.overlay').fadeOut(2000, function () {
                       $('.overlay').css("display", "none");
                   });
               });
        }
        
    });

})