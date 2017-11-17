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

        public JsonResult Comision(long Num_Sol_Ifi, int Tip_Ope_Cod, float Tas_Anl, int Num_Cuo, decimal Mnt, decimal Val_Dol, decimal Val_UF, int Mon, string Fec_Cse1, string Fec_Prm_Ven1, int Frc_Pag, decimal Nvl_Vnt, decimal Mnt_Cbt_Ocu, float Por_Fjo_Cms)
        {

            Svt_Calculo_Cobertura_Result calculo_cobertura = new Svt_Calculo_Cobertura_Result();
            calculo_cobertura = new ObtieneComision().Obtiene_Cobertura(Tip_Ope_Cod, Num_Cuo, Mnt, Val_Dol, Val_UF, Mon, Fec_Cse1, Fec_Prm_Ven1, Frc_Pag, Nvl_Vnt, Mnt_Cbt_Ocu);

            decimal Por_Cob = Convert.ToDecimal(calculo_cobertura.Porcentaje_Cobertura_Menor_Valor_UF);
            string Re_Cal_Cob = calculo_cobertura.Re_Cal_Cob;
            string Re_Cal_Tip_Ope = calculo_cobertura.Re_Cal_Tip_Ope;
            var Interes = Tas_Anl;


            Sva_TGSC_CLC_REG_Calculo_Comision_Result calculo_comision = new Sva_TGSC_CLC_REG_Calculo_Comision_Result();
            calculo_comision = new ObtieneComision().Obtiene_Comision(Num_Sol_Ifi, Tas_Anl, Num_Cuo, Mnt, Val_Dol, Val_UF, Mon, Fec_Cse1, Fec_Prm_Ven1, Frc_Pag, Por_Fjo_Cms, Por_Cob, Re_Cal_Cob, Re_Cal_Tip_Ope);

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