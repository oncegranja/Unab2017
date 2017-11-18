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
    public class ParametrosController : Controller
    {
        private DB_DESARROLLOEntities1 db = new DB_DESARROLLOEntities1();

        // GET: Proyectos
        [HandleError()]
        public JsonResult ListOperacion(string TipOpeCms)
        {
            List<Svc_TGSC_TIP_OPE_VerOperacion_Result> ListaOperaciones = new List<Svc_TGSC_TIP_OPE_VerOperacion_Result>();
            ListaOperaciones = new ObtieneDesPyt().ListaProyecto(TipOpeCms, Constantes.numUno,"");
            return Json(ListaOperaciones, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListTipMonto(int TipOpeId)
        {
            Svc_TGSC_TIP_OPE_VerOperacion_Id_Result ObtTipMonto = new Svc_TGSC_TIP_OPE_VerOperacion_Id_Result();
            ObtTipMonto = new ObtieneDesPyt().ObtieneTipoMonto(TipOpeId);
            return Json(ObtTipMonto, JsonRequestBehavior.AllowGet);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }        
    }
}
