using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garantia_4.Models;
using Garantia_4.Funciones;


namespace Garantia_4.Controllers
{
    public class CalculosController : Controller
    {

        [HandleError()]
         public class CalculosController : Controller
    {

        public JsonResult Comision(parametrosComision parametrosComision)
        {

            Svt_Calculo_Cobertura_Result calculo_cobertura = new Svt_Calculo_Cobertura_Result();
            calculo_cobertura = new ObtieneComision().Obtiene_Cobertura(parametrosComision.Tip_Ope_Cod, parametrosComision.Num_Cuo, parametrosComision.Mnt, parametrosComision.Val_Dol, parametrosComision.Val_UF, parametrosComision.Mon, parametrosComision.Fec_Cse1, parametrosComision.Fec_Prm_Ven1, parametrosComision.Frc_Pag, parametrosComision.Nvl_Vnt, parametrosComision.Mnt_Cbt_Ocu);

            decimal Por_Cob = Convert.ToDecimal(calculo_cobertura.Porcentaje_Cobertura_Menor_Valor_UF);
            string Re_Cal_Cob = calculo_cobertura.Re_Cal_Cob;
            string Re_Cal_Tip_Ope = calculo_cobertura.Re_Cal_Tip_Ope;
            var Interes = parametrosComision.Tas_Anl;


            Sva_TGSC_CLC_REG_Calculo_Comision_Result calculo_comision = new Sva_TGSC_CLC_REG_Calculo_Comision_Result();
            calculo_comision = new ObtieneComision().Obtiene_Comision(parametrosComision.Num_Sol_Ifi, parametrosComision.Tas_Anl, parametrosComision.Num_Cuo, parametrosComision.Mnt, parametrosComision.Val_Dol, parametrosComision.Val_UF, parametrosComision.Mon, parametrosComision.Fec_Cse1, parametrosComision.Fec_Prm_Ven1, parametrosComision.Frc_Pag, parametrosComision.Por_Fjo_Cms, Por_Cob, Re_Cal_Cob, Re_Cal_Tip_Ope);

            var Salida_Calculos = new
            {
                Monto_comision_Pesos = calculo_comision.Monto_comision_MO,
                Porcentaje_Cobertura_Menor_Valor_UF = calculo_cobertura.Porcentaje_Cobertura_Menor_Valor_UF,
                Monto_Cobertura_Menor_Valor_UF = calculo_cobertura.Monto_Cobertura_Menor_Valor_UF
            };


            return Json(Salida_Calculos, JsonRequestBehavior.AllowGet);
        }




        public JsonResult ObtieneValorMoneda(string fecha_cse)
        {
            Svc_TTAB_VAL_MON_VerValorDolarUF_v2_Result valor_moneda = new Svc_TTAB_VAL_MON_VerValorDolarUF_v2_Result();
            valor_moneda = new ObtieneValorMoneda().obtiene_valor_moneda(fecha_cse);
            return Json(valor_moneda, JsonRequestBehavior.AllowGet);
        }


    }
}
